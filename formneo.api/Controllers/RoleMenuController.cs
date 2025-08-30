using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLayer.Core.Services;
using vesa.core.DTOs;
using vesa.core.Models;
using vesa.core.UnitOfWorks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Caching.Memory;

namespace vesa.api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RoleMenuController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly UserManager<UserApp> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMemoryCache _memoryCache;
        private readonly IGlobalServiceWithDto<AspNetRolesMenu, RoleMenuListDto> _roleMenuService;
        IUnitOfWork _unitOfWork;

        public RoleMenuController(
            IMapper mapper,
            RoleManager<IdentityRole> roleManager,
            IGlobalServiceWithDto<AspNetRolesMenu, RoleMenuListDto> roleMenuService, IUnitOfWork unitOfWork, UserManager<UserApp> userManager,
             IMemoryCache memoryCache)
        {
            _mapper = mapper;
            _roleManager = roleManager;
            _roleMenuService = roleMenuService;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<List<RoleMenuListDto>> All()
        {
            var roleMenus = await _roleMenuService.Include();
            var roleMenuList = _mapper.Map<List<RoleMenuListDto>>(roleMenus);
            return roleMenuList.ToList();
        }

        [HttpGet("AllOnlyHead")]
        public async Task<List<IdentityRole>> AllOnlyHead()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            //var roleList = roles.Select(r => new
            //{
            //    r.Id,
            //    r.Name,
            //    r.NormalizedName,
            //    r.ConcurrencyStamp
            //});


            return roles.ToList();
        }

        [HttpPost]
        public async Task<IActionResult> Save(RoleMenuInsertDto dto)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                // Önce rolü oluştur
                var role = new IdentityRole
                {
                    Name = dto.RoleName,
                    NormalizedName = dto.RoleName.ToUpper()
                };

                var roleResult = await _roleManager.CreateAsync(role);

                if (!roleResult.Succeeded)
                {

                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(400, "Rol oluşturulurken hata oluştu"));
                }

                // Sonra menü yetkilerini ekle
                foreach (var menuPermission in dto.MenuPermissions)
                {
                    var roleMenu = new AspNetRolesMenu
                    {
                        RoleId = role.Id,
                        MenuId = menuPermission.MenuId,
                        CanView = menuPermission.CanView,
                        CanAdd = menuPermission.CanAdd,
                        CanEdit = menuPermission.CanEdit,
                        CanDelete = menuPermission.CanDelete,
                        Description = dto.Description
                    };

                    var menuResult = await _roleMenuService.AddAsync(_mapper.Map<RoleMenuListDto>(roleMenu));

                    if (menuResult.StatusCode != 201)  // Eğer ekleme başarısız olursa
                    {
                        return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(400, "Menü yetkileri eklenirken hata oluştu"));
                    }
                }

                // Tüm işlemler başarılı olduğunda commit yap
                await _unitOfWork.CommitAsync();
                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
            }
            catch (Exception ex)
            {

                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, ex.Message));
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(RoleMenuUpdateDto dto)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                // Önce rolü kontrol et ve güncelle
                var role = await _roleManager.FindByIdAsync(dto.RoleId);
                if (role == null)
                {
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "Rol bulunamadı"));
                }

                role.Name = dto.RoleName;
                role.NormalizedName = dto.RoleName.ToUpper();
                var updateRoleResult = await _roleManager.UpdateAsync(role);

                if (!updateRoleResult.Succeeded)
                {

                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(400, "Rol güncellenirken hata oluştu"));
                }

                // Mevcut yetkileri sil
                var existingPermissions = await _roleMenuService.Where(x => x.RoleId == dto.RoleId);
                if (existingPermissions.Data != null)
                {
                    foreach (var permission in existingPermissions.Data)
                    {
                        var deleteResult = await _roleMenuService.RemoveAsyncByGuid(permission.Id);
                        if (deleteResult.StatusCode != 204)
                        {

                            return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(400, "Mevcut yetkiler silinirken hata oluştu"));
                        }
                    }
                }

                // Yeni yetkileri ekle
                foreach (var menuPermission in dto.MenuPermissions)
                {
                    var roleMenu = new AspNetRolesMenu
                    {
                        RoleId = role.Id,
                        MenuId = menuPermission.MenuId,
                        CanView = menuPermission.CanView,
                        CanAdd = menuPermission.CanAdd,
                        CanEdit = menuPermission.CanEdit,
                        CanDelete = menuPermission.CanDelete,
                        Description = dto.Description
                    };

                    var addResult = await _roleMenuService.AddAsync(_mapper.Map<RoleMenuListDto>(roleMenu));
                    if (addResult.StatusCode != 201)
                    {
                        return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(400, "Yeni yetkiler eklenirken hata oluştu"));
                    }
                }

                // Tüm işlemler başarılı olduğunda commit yap
                await _unitOfWork.CommitAsync();
                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
            }
            catch (Exception ex)
            {

                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, ex.Message));
            }
        }

        [HttpDelete("{roleId}")]
        public async Task<IActionResult> Delete(string roleId)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                // Önce rolü sil
                var role = await _roleManager.FindByIdAsync(roleId);
                if (role == null)
                {
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "Rol bulunamadı"));
                }


                // Sonra menü yetkilerini sil
                var permissions = await _roleMenuService.Where(x => x.RoleId == roleId);
                foreach (var permission in permissions.Data)
                {
                    await _roleMenuService.RemoveAsyncByGuid(permission.Id);
                }

                await _roleManager.DeleteAsync(role);


                _unitOfWork.Commit();
                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, ex.Message));
            }
        }

        [HttpGet("GetById/{roleId}")]
        public async Task<RoleMenuResuResultDto> GetById(string roleId)
        {
            try
            {
                // Rolü Id ile bul. Bulunamazsa isimle dene.
                var role = await _roleManager.FindByIdAsync(roleId);
                if (role == null)
                {
                    role = await _roleManager.FindByNameAsync(roleId);
                }
                if (role == null)
                {
                    return new RoleMenuResuResultDto
                    {
                        RoleId = roleId,
                        RoleName = null,
                        Description = null,
                        MenuPermissions = new List<MenuPermissionDto>()
                    };
                }

                // Rolün menü yetkilerini getir
                var permissionsResponse = await _roleMenuService.Where(x => x.RoleId == role.Id);
                var permissions = permissionsResponse.Data ?? new List<RoleMenuListDto>();

                // Güvenli description alma (boş koleksiyonda null kalır)
                var description = permissions.FirstOrDefault()?.Description;

                // Sonucu hazırla
                var result = new RoleMenuResuResultDto
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    Description = description,
                    MenuPermissions = permissions
                        .Select(p => new MenuPermissionDto
                        {
                            MenuId = p.MenuId,
                            CanView = p.CanView,
                            CanAdd = p.CanAdd,
                            CanEdit = p.CanEdit,
                            CanDelete = p.CanDelete
                        })
                        .ToList()
                };

                return result;
            }
            catch
            {
                // Hata durumunda boş obje döndürerek UI'ın kırılmasını engelle
                return new RoleMenuResuResultDto
                {
                    RoleId = roleId,
                    RoleName = null,
                    Description = null,
                    MenuPermissions = new List<MenuPermissionDto>()
                };
            }
        }

        [HttpGet("GetAuthByUser")]
        public async Task<List<Menu>> GetAuthByUser()
        {



            if (_memoryCache.TryGetValue(User.Identity.Name + "menus", out var cachedValue))
            {
                // Değer zaten önbellekte var, cachedValue'yu kullanabilirsiniz
                return cachedValue as List<Menu>;
            }

            List<Menu> menus = new List<Menu>();
            //return menus;
            string userName = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;

            if (userName == null)
            {
                return null;
            }
            var user = await _userManager.FindByNameAsync(userName);

            // Kullanıcının rollerini al
            var roles = await _userManager.GetRolesAsync(user);


            var alldata = await _roleMenuService.Include();
            var menusWithData = alldata.Include(e => e.Menu).ToList();

            foreach (var item in roles)
            {


                var role = await _roleManager.FindByNameAsync(item);

                var result = menusWithData.Where(e => e.RoleId == role.Id).ToList();


                foreach (var it in result)
                {
                    menus.Add(it.Menu);
                }

            }

            _memoryCache.Set(User.Identity.Name + "menus", menus,
                 new MemoryCacheEntryOptions()
                 .SetAbsoluteExpiration(TimeSpan.FromDays(1)));

            return menus;
        }

        [HttpGet("GetAuthByUserWithHref")]
        public async Task<bool> GetAuthByUserWithHref(string href)
        {
            List<Menu> menus = new List<Menu>();
            string userName = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;



            if (userName == null)
            {
                return false;
            }
            var user = await _userManager.FindByNameAsync(userName);

            // Kullanıcının rollerini al
            var roles = await _userManager.GetRolesAsync(user);


            var alldata = await _roleMenuService.Include();
            var menusWithData = alldata.Include(e => e.Menu).ToList();

            foreach (var item in roles)
            {
                var role = await _roleManager.FindByNameAsync(item);

                var result = menusWithData.Where(e => e.RoleId == role.Id).FirstOrDefault();

                if (result.Menu.Href == href)
                {
                    return true;

                }

            }

            return false;
        }
    }
}