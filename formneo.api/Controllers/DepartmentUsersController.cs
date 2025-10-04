using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NLayer.Core.Services;
using formneo.core.DTOs;
using formneo.core.DTOs.DepartmentUserDto;
using formneo.core.DTOs.Ticket;
using formneo.core.DTOs.Ticket.TicketDepartments;
using formneo.core.Models.Ticket;
using formneo.core.Services;

namespace formneo.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DepartmentUsersController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IMemoryCache _memoryCache;
        private readonly IServiceWithDto<DepartmentUser, DepartmentUserListDto> _departmentUserService;

        public DepartmentUsersController(IMapper mapper, IUserService userService, IMemoryCache memoryCache, IServiceWithDto<DepartmentUser, DepartmentUserListDto> departmentUserService)
        {
            _mapper = mapper;
            _userService = userService;
            _memoryCache = memoryCache;
            _departmentUserService = departmentUserService;
        }
        [HttpGet]
        public async Task<DepartmentUserListDto> All(Guid ticketDepartmentId)
        {

            return null;
            //var myUser = new List<MyUser>();

            //var departmentUserQuery = await _departmentUserService.Include();

            //var usersList = departmentUserQuery
            //    .Include(e => e.User)
            //    .Where(x => x.TicketDepartmentId == ticketDepartmentId)
            //    .Select(y => new UserAppDto
            //    {
            //        Id = y.User.Id.ToString(),
            //      FirstName = y.User.FirstName + " " + y.User.LastName
            //    }).ToList();

            //// `myUser` içine kullanıcıları ekliyoruz
            //myUser.AddRange(usersList);

            //// Departman bilgisiyle birlikte dönüyoruz
            //var departmentUserDto = new DepartmentUserListDto
            //{
            //    Id = ticketDepartmentId, // Varsa ilgili departman ID'sini kullanabilirsin
            //    TicketDepartmentId = ticketDepartmentId,
            //    Users = myUser
            //};

            //return departmentUserDto;

        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartmentUser(DepartmentUserInsertDto dto)
        {
            try
            {
                
                var result = await _departmentUserService.AddAsync(_mapper.Map<DepartmentUserListDto>(dto));

                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, ex.Message));
            }
        }
        [HttpDelete("{ticketDepartmentId}")]
        public async Task<IActionResult> Delete(Guid ticketDepartmentId)
        {
            try
            {
                var departmentUserQuery = await _departmentUserService.Include();

                var departmentUserIdList = departmentUserQuery
                    .Include(e => e.User)
                    .Where(x => x.TicketDepartmentId == ticketDepartmentId)
                    .Select(y => y.Id).ToList();
                if (departmentUserIdList != null && departmentUserIdList.Any())
                {
                    foreach (var id in departmentUserIdList)
                    {
                        await _departmentUserService.RemoveAsyncByGuid(id);
                    }
                }


                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, ex.Message));
            }
        }


    }
}
