using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NLayer.Core.Services;
using vesa.core.DTOs.DepartmentUserDto;
using vesa.core.DTOs.Ticket.TicketDepartments;
using vesa.core.DTOs.Ticket;
using vesa.core.DTOs;
using vesa.core.Models.Ticket;
using vesa.core.Services;
using vesa.core.Models.TaskManagement;

namespace vesa.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController : CustomBaseController
    {
        private readonly IMemoryCache _memoryCache;

        public CacheController(IMapper mapper, IServiceWithDto<TicketDepartment, TicketDepartmensListDto> Service, IUserService userService, IMemoryCache memoryCache, IServiceWithDto<DepartmentUser, DepartmentUserListDto> departmentUserService)
        {

            _memoryCache = memoryCache;

        }


        [HttpGet("DeleteAllCache")]
        public async Task DeleteAllCache(string userName = "")
        {
            if (_memoryCache.TryGetValue("AllOnlyName", out var cachedValue))
            {
                _memoryCache.Remove("AllOnlyName");
            }
            if (_memoryCache.TryGetValue(userName + "menus", out var user))
            {
                _memoryCache.Remove(userName + "menus");
            }
        }
    }
}
