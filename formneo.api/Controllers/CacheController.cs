using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NLayer.Core.Services;
using formneo.core.DTOs.DepartmentUserDto;
using formneo.core.DTOs.Ticket.TicketDepartments;
using formneo.core.DTOs.Ticket;
using formneo.core.DTOs;
using formneo.core.Models.Ticket;
using formneo.core.Services;
using formneo.core.Models.TaskManagement;

namespace formneo.api.Controllers
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
