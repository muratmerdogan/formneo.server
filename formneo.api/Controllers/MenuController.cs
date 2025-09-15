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
        private readonly IOptions<RoleScopeOptions> _roleScopeOptions;

        public MenuController(IMapper mapper, IGlobalServiceWithDto<Menu, MenuListDto> Service, IServiceWithDto<FormRuleEngine, FormRuleEngineDto> formRuleEngineService,
            IFormService formService, IGlobalServiceWithDto<AspNetRolesMenu, RoleMenuListDto> roleMenuService, IMemoryCache memoryCache, RoleManager<IdentityRole> roleManager, UserManager<UserApp> userManager, IFormRepository formRepository,
            IServiceWithDto<FormAuth, FormAuthDto> formAuthService, IUserService userService, ITenantContext tenantContext, IRoleTenantMenuService roleTenantMenuService, IUserTenantRoleService userTenantRoleService, IOptions<RoleScopeOptions> roleScopeOptions)
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
        [HttpGet]
        public async Task<List<Menu>> All()
        {
            var isGlobalAdmin = await IsCurrentUserGlobalAdminAsync();

            var menus = await _menuService.Include();

            var menuList = menus.ToList();
            List<Menu> extFormMenus = [];
            if (menuList != null)
            {
                //var forms = _formRepository.GetAll().Include(x => x.WorkFlowDefination).Where(e => e.IsDelete == false && e.IsActive == 1);

                var loginUser = await _userService.GetUserByEmailAsync(User.Identity.Name);
                var loginUserId = loginUser.Data.Id;
                var result = await _formAuthService.Include();
                var forms = await result.Include(e => e.Form).ToListAsync();
                forms = forms.Where(e => e.Form.ShowInMenu == true && e.UserIds.Contains(Guid.Parse(loginUserId))).ToList();

                foreach (var form in forms)
                {
                    var newForm = new Menu
                    {
                        Href = $"/formList/{form.FormId}",
                        Description = form.Form.FormDescription,
                        Name = form.Form.FormName,
                        Order = 6,
                        IsActive = form.Form.IsActive == 1,
                        IsDelete = form.Form.IsDelete,
                        ParentMenuId = new Guid("3db50541-830a-44c1-beb6-12905f615abe")
                    };
                    menuList.Add(newForm);
                    extFormMenus.Add(newForm);
                }
                //var eduForm = new Menu
                //{
                //    Href = "/userFormList/8f6276ba-0e59-4c53-a59a-161c41f77214",
                //    Description = "8f6276ba-0e59-4c53-a59a-161c41f77214",
                //    Name = "Eğitimin Davranışa Dönüşümü",
                //    Order = 6,
                //    IsActive = true,
                //    IsDelete = false,
                //    ParentMenuId = new Guid("84D1D30A-D99C-4B2E-AACB-0AED9EDBB4B9")
                //};

                //menuList.Add(eduForm);
            }


            //var rootMenus = menus.ToList().Where(m => m.ParentMenuId == null && m.IsDelete == false).Select(m => new Menu
            var rootMenus = menuList.Where(m => m.ParentMenuId == null && m.IsDelete == false).Select(m => new Menu
            {
                Id = m.Id,
                Name = m.Name,
                ParentMenuId = m.ParentMenuId,
                Order = m.Order,
                Href = m.Href,
                Icon = m.Icon,
                IsDelete = m.IsDelete
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
                return rootMenusList;
            }
            //List<Menu> parametersMenu = new List<Menu>();
            //var forms = await _formservice.GetAllAsync();
            //foreach (var item in forms)
            //{
            //    parametersMenu.Add(new Menu { Name = item.FormName, Href = "/paramEdit/" + item.Id });

            //}

            // Yeni menü öğesini ekl
            //rootMenusList.Add(new Menu { Name = "Parametreler", SubMenus = parametersMenu, Icon = "hammer", ParentMenu = null, Href = "", Order = 99999 });

            //return rootMenusList.ToList().OrderBy(e => e.Order).ToList();

            // data içindeki href değerlerini alıyoruz
            var data = await GetAuthByUser();
            var authorizedHrefs = new HashSet<string>((data ?? new List<Menu>()).Select(d => d.Href));

            //authorizedHrefs.Add("/userFormList/8f6276ba-0e59-4c53-a59a-161c41f77214");
            foreach (var form in extFormMenus)
            {
                authorizedHrefs.Add(form.Href);
            }

            // Yetkili olan menüleri filtreliyoruz



            var filteredMenus = rootMenusList
               .Select(menu =>
               {
                   // Yetkili olan submenu'leri filtreliyoruz (null güvenli)
                   var subs = menu.SubMenus ?? new List<Menu>();
                   menu.SubMenus = subs
                       .Where(sub => sub != null && sub.Href != null && authorizedHrefs.Contains(sub.Href))
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
                return menus;
            }

            var data = await GetAuthByUser();
            var authorizedHrefs = new HashSet<string>((data ?? new List<Menu>()).Select(d => d.Href));
            menus = menus.Where(m => !string.IsNullOrEmpty(m.Href) && authorizedHrefs.Contains(m.Href)).ToList();
            return menus;
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
        public async Task<List<MenuListDto>> AllPlain()
        {
            var menusQuery = await _menuService.Include();
            var allMenus = menusQuery.ToList();

            // Tüm menüleri (IsDelete == false) ile filtrele
            var menus = _menuService.Where(e => e.IsDelete == false).Result.Data.ToList();

            return menus;
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

                var globalCacheKey = $"{User.Identity.Name}:global:menus";
                if (_memoryCache.TryGetValue(globalCacheKey, out var cachedGlobal))
                {
                    return cachedGlobal as List<Menu> ?? new List<Menu>();
                }

                var menusQueryGlobal = await _menuService.Include();
                var allMenusGlobal = menusQueryGlobal
                    .Where(m => m.IsDelete == false)
                    .ToList();

                _memoryCache.Set(globalCacheKey, allMenusGlobal, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(1)));
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
