using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using vesa.core.DTOs.Onboarding;
using vesa.core.Models;
using vesa.core.Models.Onboarding;
using vesa.core.Services;
using vesa.core.UnitOfWorks;
using vesa.repository;
using vesa.core.EnumExtensions;

namespace vesa.service.Services
{
	public class OnboardingService : IOnboardingService
	{
		private readonly AppDbContext _db;
		private readonly IUnitOfWork _uow;
		private readonly UserManager<UserApp> _userManager;
		private readonly IMailService _mail;
        private readonly RoleManager<IdentityRole> _roleManager;

		public OnboardingService(AppDbContext db, IUnitOfWork uow, UserManager<UserApp> userManager, IMailService mail, RoleManager<IdentityRole> roleManager)
		{
			_db = db; _uow = uow; _userManager = userManager; _mail = mail; _roleManager = roleManager;
		}

		public async Task<OnboardRegisterResponse> RegisterAsync(OnboardRegisterRequest req, string baseUrl)
		{
			// benzersiz e-posta kontrolü
			var exists = await _userManager.Users.AnyAsync(u => u.Email == req.Admin.Email);
			if (exists) throw new InvalidOperationException("Bu e-posta ile kullanıcı zaten mevcut.");

			// aktivasyon kaydı oluştur
			var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(48));
			var hash = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(token)));
			var activation = new OnboardingActivation
			{
				Id = Guid.NewGuid(),
				Token = hash,
				Plan = req.Plan,
				AgreedToTerms = req.AgreedToTerms,
				CompanyName = req.Company.CompanyName,
				CompanyEmail = req.Company.CompanyEmail,
				CompanyPhone = req.Company.CompanyPhone,
				CompanyAddress = req.Company.CompanyAddress,
				TaxNumber = req.Company.TaxNumber,
				Sector = req.Company.Sector,
				EmployeeCount = req.Company.EmployeeCount,
				AdminFirstName = req.Admin.FirstName,
				AdminLastName = req.Admin.LastName,
				AdminEmail = req.Admin.Email,
				AdminPhone = req.Admin.Phone,
				ExpiresAt = DateTime.UtcNow.AddHours(24),
				CreatedBy = "system",
				UpdatedBy = "",
			};
			await _db.OnboardingActivations.AddAsync(activation);
			await _uow.CommitAsync();

			// aktivasyon maili
			var link = $"{baseUrl}/api/onboarding/activate?token={Uri.EscapeDataString(token)}";
			await _mail.SendEmailAsync(req.Admin.Email, "Formneo Hesap Aktivasyonu", $"Hesabınızı aktifleştirmek için tıklayın: <a href='{link}'>Aktivasyon</a>");

			return new OnboardRegisterResponse { Message = "Aktivasyon e-postası gönderildi." };
		}

		public async Task<OnboardActivateResponse> ActivateAsync(string token)
		{
			var hash = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(token)));
			var act = await _db.OnboardingActivations.FirstOrDefaultAsync(x => x.Token == hash && !x.IsDelete);
			if (act == null || act.IsUsed || act.ExpiresAt < DateTime.UtcNow) throw new InvalidOperationException("Token geçersiz veya süresi dolmuş.");

			// Tenant oluştur
			var tenant = new MainClient
			{
				Id = Guid.NewGuid(),
				Name = act.CompanyName,
				Email = act.CompanyEmail,
				PhoneNumber = act.CompanyPhone,
				Status = MainClientStatus.Active,
				Plan = Enum.TryParse<MainClientPlan>(act.Plan, true, out var plan) ? plan : MainClientPlan.Free,
				CreatedDate = DateTime.UtcNow,
				UpdatedDate = DateTime.UtcNow,
				IsActive = true,
				FeatureFlags = "{}",
				Quotas = "{}"
			};
			_db.Set<MainClient>().Add(tenant);

			// Admin user oluştur
			var user = new UserApp
			{
				UserName = act.AdminEmail,
				Email = act.AdminEmail,
				FirstName = act.AdminFirstName,
				LastName = act.AdminLastName,
				EmailConfirmed = true,
				isSystemAdmin = false,
				isBlocked = false
			};
			var tempPassword = "Temp#" + Guid.NewGuid().ToString("N").Substring(0, 10);
			var res = await _userManager.CreateAsync(user, tempPassword);
			if (!res.Succeeded) throw new InvalidOperationException("Kullanıcı oluşturulamadı.");

			// Tenant owner olarak ilişkilendirme (OwnerUserId)
			tenant.OwnerUserId = user.Id;
			_db.Update(tenant);

			// UserTenant üyeliği oluştur
			var existingLink = await _db.Set<UserTenant>().IgnoreQueryFilters().FirstOrDefaultAsync(x => x.UserId == user.Id && x.TenantId == tenant.Id);
			if (existingLink == null)
			{
				await _db.Set<UserTenant>().AddAsync(new UserTenant
				{
					Id = Guid.NewGuid(),
					UserId = user.Id,
					TenantId = tenant.Id,
					IsActive = true,
					HasTicketPermission = true,
					HasDepartmentPermission = false,
					HasOtherCompanyPermission = false,
					HasOtherDeptCalendarPerm = false,
					canEditTicket = false,
					DontApplyDefaultFilters = false,
					CreatedBy = "system",
					UpdatedBy = string.Empty
				});
			}

			// TenantAdmin rolünü kullanıcıya ata (RoleTenant + UserTenantRole)
			const string tenantAdminRoleId = "7f3d3baf-2f5c-4f6c-9d1e-6b6d3b25a001";
			var tenantAdminRole = await _roleManager.FindByIdAsync(tenantAdminRoleId) ?? await _roleManager.FindByNameAsync("TenantAdmin");
			if (tenantAdminRole != null)
			{
				var roleTenant = await _db.Set<RoleTenant>().FirstOrDefaultAsync(rt => rt.TenantId == tenant.Id && rt.RoleId == tenantAdminRole.Id);
				if (roleTenant == null)
				{
					roleTenant = new RoleTenant
					{
						Id = Guid.NewGuid(),
						RoleId = tenantAdminRole.Id,
						TenantId = tenant.Id,
						IsActive = true,
						IsLocked = false,
						CreatedBy = "system",
						UpdatedBy = string.Empty
					};
					await _db.Set<RoleTenant>().AddAsync(roleTenant);
				}

				var hasRole = await _db.Set<UserTenantRole>().AnyAsync(utr => utr.UserId == user.Id && utr.RoleTenantId == roleTenant.Id);
				if (!hasRole)
				{
					await _db.Set<UserTenantRole>().AddAsync(new UserTenantRole
					{
						Id = Guid.NewGuid(),
						UserId = user.Id,
						RoleTenantId = roleTenant.Id,
						IsActive = true,
						CreatedBy = "system",
						UpdatedBy = string.Empty
					});
				}

				// TenantAdmin için global menü yetkilerini (AspNetRolesMenu) tenant özel tablosuna (AspNetRolesTenantMenu) kopyala
				var globalMenusForRole = await _db.Set<AspNetRolesMenu>()
					.AsNoTracking()
					.Where(x => x.RoleId == tenantAdminRole.Id)
					.ToListAsync();
				if (globalMenusForRole.Count > 0)
				{
					foreach (var gm in globalMenusForRole)
					{
						bool existsTenantMenu = await _db.Set<AspNetRolesTenantMenu>()
							.AnyAsync(t => t.TenantId == tenant.Id && t.RoleId == tenantAdminRole.Id && t.MenuId == gm.MenuId);
						if (!existsTenantMenu)
						{
							await _db.Set<AspNetRolesTenantMenu>().AddAsync(new AspNetRolesTenantMenu
							{
								Id = Guid.NewGuid(),
								TenantId = tenant.Id,
								RoleId = tenantAdminRole.Id,
								MenuId = gm.MenuId,
								CanView = gm.CanView,
								CanAdd = gm.CanAdd,
								CanEdit = gm.CanEdit,
								CanDelete = gm.CanDelete,
								Description = string.IsNullOrWhiteSpace(gm.Description) ? "TenantAdmin default access" : gm.Description,
								CreatedBy = "system",
								UpdatedBy = string.Empty
							});
						}
					}
				}
			}

			act.IsUsed = true;
			act.ActivatedAt = DateTime.UtcNow;
			_db.Update(act);
			await _uow.CommitAsync();

			// Geçici şifreyi admin'e gönder
			await _mail.SendEmailAsync(act.AdminEmail, "Formneo Hesap Bilgileri", $"Merhaba {act.AdminFirstName},<br/>Hesabınız oluşturuldu.<br/>Geçici şifreniz: <b>{tempPassword}</b><br/>Giriş yaptıktan sonra lütfen şifrenizi değiştirin.");

			return new OnboardActivateResponse { Message = "Hesap aktifleştirildi." };
		}
	}
}
