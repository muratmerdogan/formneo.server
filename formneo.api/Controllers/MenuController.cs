using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using NLayer.Core.Services;
using vesa.core.DTOs;
using vesa.core.DTOs.FormAuth;
using vesa.core.DTOs.Menu;
using vesa.core.Models;
using vesa.core.Repositories;
using vesa.core.Services;
using vesa.core.Options;

namespace vesa.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MenuController : CustomBaseController
    {
        private readonly IGlobalServiceWithDto<AspNetRolesMenu, RoleMenuListDto> _roleMenuService;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<UserApp> _userManager;
        private readonly IGlobalServiceWithDto<Menu, MenuListDto> _menuService;
        private readonly IServiceWithDto<FormRuleEngine, FormRuleEngineDto> _formRuleEngineService;
        private readonly IFormService _formservice;
        private readonly IMemoryCache _memoryCache;

        private readonly IFormRepository _formRepository;
        private readonly IServiceWithDto<FormAuth, FormAuthDto> _formAuthService;
        private readonly IUserService _userService;
        private readonly ITenantContext _tenantContext;
        private readonly IRoleTenantMenuService _roleTenantMenuService;
        private readonly IUserTenantRoleService _userTenantRoleService;
        private readonly IUserTenantService _userTenantService;
        private readonly IOptions<RoleScopeOptions> _roleScopeOptions;

        public MenuController(IMapper mapper, IGlobalServiceWithDto<Menu, MenuListDto> Service, IServiceWithDto<FormRuleEngine, FormRuleEngineDto> formRuleEngineService,
            IFormService formService, IGlobalServiceWithDto<AspNetRolesMenu, RoleMenuListDto> roleMenuService, IMemoryCache memoryCache, RoleManager<IdentityRole> roleManager, UserManager<UserApp> userManager, IFormRepository formRepository,
            IServiceWithDto<FormAuth, FormAuthDto> formAuthService, IUserService userService, ITenantContext tenantContext, IRoleTenantMenuService roleTenantMenuService, IUserTenantRoleService userTenantRoleService, IUserTenantService userTenantService, IOptions<RoleScopeOptions> roleScopeOptions)
        {

            _menuService = Service;
            _mapper = mapper;
            _formRuleEngineService = formRuleEngineService;
            _formservice = formService;
            _roleManager = roleManager;
            _roleMenuService = roleMenuService;
            _memoryCache = memoryCache;
            _userManager = userManager;

            _formRepository = formRepository;
            _formAuthService = formAuthService;
            _userService = userService;
            _tenantContext = tenantContext;
            _roleTenantMenuService = roleTenantMenuService;
            _userTenantRoleService = userTenantRoleService;
            _userTenantService = userTenantService;
            _roleScopeOptions = roleScopeOptions;
        }
        private async Task<bool> IsCurrentUserGlobalAdminAsync()
        {
            try
            {
                var userName = User?.Identity?.Name;
                if (string.IsNullOrWhiteSpace(userName)) return false;

                // Cache by user id if possible
                UserApp user = await _userManager.FindByNameAsync(userName);
                if (user == null)
                {
                    user = await _userManager.FindByEmailAsync(userName);
                }
                if (user == null) return false;

                var cacheKey = $"global-admin:{user.Id}";
                if (_memoryCache.TryGetValue(cacheKey, out bool cachedIsGlobal))
                {
                    return cachedIsGlobal;
                }

                var roleNames = await _userManager.GetRolesAsync(user);
                if (roleNames == null || roleNames.Count == 0) return false;

                var roleIds = _roleManager.Roles.Where(r => roleNames.Contains(r.Name)).Select(r => r.Id).ToList();
                var globalOnlyRoleIds = _roleScopeOptions?.Value?.GlobalOnlyRoleIds ?? new List<string>();
                bool isGlobalAdmin = roleIds.Any(id => globalOnlyRoleIds.Contains(id));

                _memoryCache.Set(cacheKey, isGlobalAdmin, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));
                return isGlobalAdmin;
            }
            catch
            {
                return false;
            }
        }

        private void ClearAllMenuCaches()
        {
            try
            {
                // Tüm kullanıcıların menü cache'lerini temizle
                // Bu işlem biraz maliyetli olabilir ama menü değişiklikleri nadiren olur
                var allUsers = _userManager.Users.ToList();
                foreach (var user in allUsers)
                {
                    // Global admin cache'ini temizle
                    _memoryCache.Remove($"global-admin:{user.Id}");
                    
                    // Eski format menü cache'ini temizle (RoleMenuController'dan)
                    _memoryCache.Remove($"{user.UserName}menus");
                    
                    // Kullanıcının tüm tenant'ları için menü cache'ini temizle
                    var userTenants = _userTenantService.GetByUserAsync(user.Id).Result;
                    foreach (var userTenant in userTenants)
                    {
                        var cacheKey = $"{user.UserName}:{userTenant.TenantId}:menus";
                        _memoryCache.Remove(cacheKey);
                    }
                }
            }
            catch (Exception ex)
            {
                // Cache temizleme hatası log'lanabilir ama ana işlemi etkilememeli
                // Log hatası burada eklenebilir
            }
        }
        [HttpGet]
        public async Task<List<Menu>> All()
        {
            var isGlobalAdmin = await IsCurrentUserGlobalAdminAsync();

            var menus = await _menuService.Include();

            var menuList = menus.ToList();


            var rootMenus = menuList.Where(m => m.ParentMenuId == null && m.IsDelete == false).Select(m => new Menu
            {
                Id = m.Id,
                Name = m.Name,
                ParentMenuId = m.ParentMenuId,
                Order = m.Order,
                Href = m.Href,
                Icon = m.Icon,
                IsDelete = m.IsDelete,
                MenuCode = m.MenuCode,
                IsTenantOnly = m.IsTenantOnly
            }).ToList().OrderBy(e => e.Order);

            // Alt menüleri ekleyelim
            foreach (var menu in rootMenus)
            {
                //AddSubMenus(menu, menus.ToList().OrderBy(e => e.Order).ToList());
                AddSubMenus(menu, menuList.OrderBy(e => e.Order).ToList());
            }

            var rootMenusList = rootMenus.ToList();


            // Global mod (tenant header yok/boş) ise ve kullanıcı global admin ise tüm menüyü döndür
            if (isGlobalAdmin && (_tenantContext?.CurrentTenantId == null || _tenantContext.CurrentTenantId == Guid.Empty))
            {
                // Tenant-only işaretli kök dalları gizle
                var filtered = rootMenusList.Where(r => r.IsTenantOnly == false).ToList();
                return filtered;
            }
       
            var data = await GetAuthByUser();
            var authorizedMenuIds = new HashSet<Guid>((data ?? new List<Menu>()).Select(d => d.Id));

            var filteredMenus = rootMenusList
               .Select(menu =>
               {
                   // Yetkili olan submenu'leri filtreliyoruz (Menu ID bazlı)
                   var subs = menu.SubMenus ?? new List<Menu>();
                   menu.SubMenus = subs
                       .Where(sub => sub != null && authorizedMenuIds.Contains(sub.Id))
                       .ToList();

                   return menu;
               })
               .Where(menu => menu.SubMenus != null && menu.SubMenus.Any())
               .ToList();

            return filteredMenus;

        }


        [HttpGet("AllListData")]
        public async Task<List<MenuListDto>> AllListData()
        {
            var isGlobalAdmin = await IsCurrentUserGlobalAdminAsync();
            var menus = _menuService.Where(e => e.IsDelete == false).Result.Data.ToList();

            // Global moddaysa ve kullanıcı global admin ise tüm listeyi döndür
            if (isGlobalAdmin && (_tenantContext?.CurrentTenantId == null || _tenantContext.CurrentTenantId == Guid.Empty))
            {

                var filtered = menus.Where(r => r.IsTenantOnly == false).ToList();
                return filtered;

            }

            var data = await GetAuthByUser();
            var authorizedMenuIds = new HashSet<Guid>((data ?? new List<Menu>()).Select(d => d.Id));
            
            // Yetkili menüleri al (Menu ID bazlı)
            var authorizedMenus = menus.Where(m => authorizedMenuIds.Contains(m.Id)).ToList();
            
            // Parent menü kontrolü: Eğer bir alt menüye yetki varsa, parent menüsünü de ekle
            var parentMenuIds = new HashSet<Guid>();
            foreach (var menu in authorizedMenus)
            {
                if (menu.ParentMenuId.HasValue)
                {
                    parentMenuIds.Add(menu.ParentMenuId.Value);
                }
            }
            
            // Parent menüleri de listeye ekle (eğer zaten listede yoksa)
            var parentMenus = menus.Where(m => parentMenuIds.Contains(m.Id) && !authorizedMenus.Any(am => am.Id == m.Id)).ToList();
            authorizedMenus.AddRange(parentMenus);
            
            return authorizedMenus;
        }

        // GET: api/Menu/all-without-auth
        [HttpGet("all-without-auth")]
        public async Task<List<Menu>> GetAllMenusWithoutAuth()
        {
            var menus = await _menuService.Include();
            var menuList = menus.ToList();

            var rootMenus = menuList.Where(m => m.ParentMenuId == null && m.IsDelete == false).Select(m => new Menu
            {
                Id = m.Id,
                Name = m.Name,
                ParentMenuId = m.ParentMenuId,
                Order = m.Order,
                Href = m.Href,
                Icon = m.Icon,
                IsDelete = m.IsDelete,
                MenuCode = m.MenuCode,
                IsTenantOnly = m.IsTenantOnly
            }).ToList().OrderBy(e => e.Order);

            // Alt menüleri ekleyelim
            foreach (var menu in rootMenus)
            {
                AddSubMenus(menu, menuList.OrderBy(e => e.Order).ToList());
            }

            return rootMenus.ToList();
        }

        // Global admin önizleme: Belirli tenant ve kullanıcı için efektif menüyü döndürür
        [HttpGet("effective-preview")]
        public async Task<ActionResult<List<Menu>>> EffectivePreview([FromQuery] Guid tenantId, [FromQuery] string userId)
        {
            var isGlobalAdmin = await IsCurrentUserGlobalAdminAsync();
            if (!isGlobalAdmin)
            {
                return Forbid();
            }
            if (tenantId == Guid.Empty || string.IsNullOrWhiteSpace(userId))
            {
                return BadRequest("tenantId ve userId zorunludur.");
            }

            // Tüm menü ağacını hazırla
            var menusQuery = await _menuService.Include();
            var allMenus = menusQuery.ToList();
            var rootMenus = allMenus.Where(m => m.ParentMenuId == null && m.IsDelete == false)
                .Select(m => new Menu
                {
                    Id = m.Id,
                    Name = m.Name,
                    ParentMenuId = m.ParentMenuId,
                    Order = m.Order,
                    Href = m.Href,
                    Icon = m.Icon,
                    IsDelete = m.IsDelete
                })
                .ToList()
                .OrderBy(e => e.Order)
                .ToList();

            foreach (var menu in rootMenus)
            {
                AddSubMenus(menu, allMenus.OrderBy(e => e.Order).ToList());
            }

            // Kullanıcının bu tenant’taki rol kimlikleri
            var userTenantRoles = await _userTenantRoleService.GetByUserAndTenantAsync(userId, tenantId);
            var roleIds = userTenantRoles
                .Where(x => x.IsActive && x.RoleTenant != null && x.RoleTenant.IsActive)
                .Select(x => x.RoleTenant.RoleId)
                .Distinct()
                .ToList();

            if (roleIds.Count == 0)
            {
                return new List<Menu>();
            }

            // Tenant + rol bazlı menü izinlerinden görüntülenebilir olanlar
            var query = await _roleTenantMenuService.Include();
            var allowedMenus = await query
                .Where(x => x.TenantId == tenantId && roleIds.Contains(x.RoleId) && x.CanView)
                .Include(x => x.Menu)
                .Select(x => x.Menu)
                .ToListAsync();

            var authorizedHrefs = new HashSet<string>(
                (allowedMenus ?? new List<Menu>())
                    .Where(m => m != null && !string.IsNullOrEmpty(m.Href))
                    .Select(m => m.Href)
            );

            // Yetkili alt menülerle filtrelenmiş kökler
            var filteredMenus = rootMenus
                .Select(menu =>
                {
                    var subs = menu.SubMenus ?? new List<Menu>();
                    menu.SubMenus = subs
                        .Where(sub => sub != null && !string.IsNullOrEmpty(sub.Href) && authorizedHrefs.Contains(sub.Href))
                        .ToList();
                    return menu;
                })
                .Where(menu => menu.SubMenus != null && menu.SubMenus.Any())
                .ToList();

            return filteredMenus;
        }

        // Yetki kontrolü yapmadan, silinmemiş tüm menü ağacını döner.
        // Rol atama başlangıcında kullanılabilir.
        [HttpGet("all-plain")]
        public async Task<List<MenuListDto>> AllPlain([FromQuery] bool tenantOnly = false)
        {
            // Tüm menüler (IsDelete == false), opsiyonel tenantOnly filtresi
            var menus = _menuService.Where(e => e.IsDelete == false && (!tenantOnly || e.IsTenantOnly)).Result.Data.ToList();
            return menus;
        }

		[HttpGet("tenant-only")]
		public async Task<List<MenuListDto>> TenantOnly()
		{
			var response = await _menuService.Where(e => e.IsDelete == false && e.IsTenantOnly);
			return response.Data.OrderBy(m => m.Order).ToList();
		}

        public static void AddSubMenus(Menu menu, List<Menu> allMenus)
        {
            var subMenus = allMenus.Where(m => m.ParentMenuId == menu.Id && m.IsDelete == false).Select(m => new Menu
            {
                Id = m.Id,
                Name = m.Name,
                ParentMenuId = m.ParentMenuId,
                Order = m.Order,
                Href = m.Href,
                Icon = m.Icon,
                IsDelete = m.IsDelete
            }).ToList();
            foreach (var subMenu in subMenus)
            {

                if (menu.SubMenus == null)
                    menu.SubMenus = new List<Menu>();

                menu.SubMenus.Add(subMenu);
                // Eğer alt menülerin alt menüleri varsa, onları da ekleyelim
                AddSubMenus(subMenu, allMenus);
            }
        }


        [HttpPost]
        public async Task<ActionResult<MenuListDto>> Save(MenuInsertDto dto)
        {
            var result = await _menuService.AddAsync(_mapper.Map<MenuListDto>(dto));

            // Menü cache'lerini temizle - yeni menü eklendiğinde tüm kullanıcıların menü cache'i geçersiz olur
            ClearAllMenuCaches();

            return result.Data;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var menu = await _menuService.GetByIdGuidAsync(id);

                if (menu == null)
                {

                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "Menü bulunamadı"));
                }

                // Alt menüleri olan bir menüyü silmeye çalışıyorsa engelleyelim
                var hasChildren = await _menuService.AnyAsync(x => x.ParentMenuId == id);
                if (hasChildren.Data == true)
                {

                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "Alt menüleri olan bir menüyü silemezsiniz"));
                }

                await _menuService.SoftDeleteAsync(id);

                // Menü cache'lerini temizle - menü silindiğinde tüm kullanıcıların menü cache'i geçersiz olur
                ClearAllMenuCaches();

                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, ex.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<MenuListDto> GetById(Guid id)
        {
            try
            {
                var menu = await _menuService.GetByIdGuidAsync(id);


                var menuDto = _mapper.Map<MenuListDto>(menu.Data);
                return menuDto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(MenuUpdateDto dto)
        {
            try
            {

                var result = await _menuService.UpdateAsync(_mapper.Map<MenuListDto>(dto));

                // Menü cache'lerini temizle - menü güncellendiğinde tüm kullanıcıların menü cache'i geçersiz olur
                ClearAllMenuCaches();

                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, ex.Message));
            }
        }


        //public List<MenuListDto> BuildMenuHierarchy(List<Menu> allMenus, Guid? parentMenuId)
        //{
        //    var menuItems = allMenus
        //        .Where(m => m.ParentMenuId == parentMenuId)
        //        .OrderBy(m => m.Order)
        //        .Select(m => new MenuListDto({ Name = m.Name,Route= m.Route, Icon=m.Icon })
        //        {
        //            SubMenu = BuildMenuHierarchy(allMenus, m.Id) // Alt menüleri al
        //        })
        //        .ToList();

        //    return menuItems;
        //}
        [HttpGet("GetAuthByUser")]
        public async Task<List<Menu>> GetAuthByUser()
        {
            var tenantId = _tenantContext.CurrentTenantId;
            if (tenantId == null || tenantId == Guid.Empty)
            {
                // Global mod: sadece global admin'e tam erişim ver
                var isGlobalAdmin = await IsCurrentUserGlobalAdminAsync();
                if (!isGlobalAdmin)
                {
                    return new List<Menu>();
                }
                
                
                
                // User.Identity.Name null ise cache kullanma
                var currentUserName = User.Identity?.Name;
                if (!string.IsNullOrEmpty(currentUserName))
                {
                    var globalCacheKey = $"{currentUserName}:global:menus";
                    if (_memoryCache.TryGetValue(globalCacheKey, out var cachedGlobal))
                    {
                        return cachedGlobal as List<Menu> ?? new List<Menu>();
                    }
                }
                var menusQueryGlobal = await _menuService.Include();
                var allMenusGlobal = menusQueryGlobal
                    .Where(m => m.IsDelete == false && m.IsTenantOnly==false)
                    .ToList();
                
                // Cache'e kaydet (sadece currentUserName varsa)
                if (!string.IsNullOrEmpty(currentUserName))
                {
                    var globalCacheKey = $"{currentUserName}:global:menus";
                    _memoryCache.Set(globalCacheKey, allMenusGlobal, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(1)));
                }
                return allMenusGlobal;
            }
            var cacheKey = $"{User.Identity.Name}:{tenantId}:menus";
            if (_memoryCache.TryGetValue(cacheKey, out var cachedValue))
            {
                return cachedValue as List<Menu> ?? new List<Menu>();
            }
            string userName = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;
            if (string.IsNullOrWhiteSpace(userName))
            {
                return new List<Menu>();
            }

            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return new List<Menu>();
            }

            // Kullanıcının aktif tenant'taki rol kimliklerini al
            var userTenantRoles = await _userTenantRoleService.GetByUserAndTenantAsync(user.Id, tenantId.Value);
            var roleIds = userTenantRoles
                .Where(x => x.IsActive && x.RoleTenant != null && x.RoleTenant.IsActive)
                .Select(x => x.RoleTenant.RoleId)
                .Distinct()
                .ToList();

            if (roleIds.Count == 0)
            {
                return new List<Menu>();
            }

            // Tenant + rol bazlı menü izinlerinden görüntüleyebilecekleri çek
            var query = await _roleTenantMenuService.Include();
            var allowedMenus = await query
                .Where(x => x.TenantId == tenantId.Value && roleIds.Contains(x.RoleId) && x.CanView)
                .Include(x => x.Menu)
                .Select(x => x.Menu)
                .ToListAsync();

            // Tekilleştir
            var menus = allowedMenus
                .Where(m => m != null && m.IsDelete == false)
                .GroupBy(m => m.Id)
                .Select(g => g.First())
                .ToList();

            _memoryCache.Set(cacheKey, menus, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(1)));
            return menus;
        }
    }
}
