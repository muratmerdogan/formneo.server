using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.Services;
using System.Security.Claims;
using formneo.core.DTOs.DashboardDto;
using formneo.core.DTOs.FormAssign;
using formneo.core.Models;
using formneo.core.Models.FormEnums;
using formneo.core.Services;

namespace formneo.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DashboardsController : CustomBaseController
    {
        private readonly ITicketServices _ticketServices;
        private readonly UserManager<UserApp> _userManager;
        private readonly IServiceWithDto<FormAssign, FormAssignDto> _formAssignService;

        public DashboardsController(ITicketServices ticketServices, UserManager<UserApp> userManager, IServiceWithDto<FormAssign, FormAssignDto> formAssignService)
        {
            _ticketServices = ticketServices;
            _userManager = userManager;
            _formAssignService = formAssignService;
        }

        [HttpGet("GetUserCompanyTicketCount")]
        public async Task<GetSumTicketDto> GetUserCompanyTicketCount(string userId, DateTime? startDate = null, DateTime? endDate = null)
        {
            var count = await _ticketServices.CountAsync(userId, startDate, endDate);
            return count;
        }
        [HttpGet("GetUserCompanyAllTicketCount")]
        public async Task<IEnumerable<GetCompanyTicketDto>> GetUserCompanyAllTicketCount(string userId, DateTime? startDate = null, DateTime? endDate = null)
        {
            var tickets = await _ticketServices.GetUserTicketAsync(userId, startDate, endDate);
            return tickets;
        }
        [HttpGet("GetUserCompanyOpenTicketCount")]
        public async Task<IEnumerable<GetCompanyTicketDto>> GetUserCompanyOpenTicketCount(string userId, DateTime? startDate = null, DateTime? endDate = null)
        {
            var tickets = await _ticketServices.GetUserOpenTicketAsync(userId, startDate, endDate);
            return tickets;
        }
        [HttpGet("GetUserCompanyTicketInfoCount")]
        public async Task<IEnumerable<GetCompanyTicketInfoDto>> GetUserCompanyTicketInfoCount(string userId, DateTime? startDate = null, DateTime? endDate = null)
        {
            var tickets = await _ticketServices.GetUserTicketInfoAsync(userId, startDate, endDate);
            return tickets;
        }
        [HttpGet("GetTicketSubjectCountAsync")]
        public async Task<IEnumerable<GetTicketSubjectInfoDto>> GetTicketSubjectCountAsync(string userId, DateTime? startDate = null, DateTime? endDate = null)
        {
            var tickets = await _ticketServices.GetCustomerTicketSubjectList(userId, startDate, endDate);
            return tickets;
        }

        [HttpGet("GetTicketStatusAsync")]
        public async Task<GetTicketStatusDto> GetTicketStatusAsync(string userId, DateTime? startDate = null, DateTime? endDate = null)
        {
            var tickets = await _ticketServices.GetCustomerTicketStatus(userId, startDate, endDate);
            return tickets;

        }

        [HttpGet("GetTicketAsync")]
        public async Task<GetSumTicketDto> GetTicketAsync(string userId, DateTime? startDate = null, DateTime? endDate = null)
        {
            var tickets = await _ticketServices.CustomerCountAsync(userId, startDate, endDate);
            return tickets;
        }


        [HttpGet("GetCustomerOpenClosee")]
        public async Task<List<GetTicketCustomerOpenCloseDto>> GetCustomerOpenClose(string userId, DateTime? startDate = null, DateTime? endDate = null)
        {
            var tickets = await _ticketServices.CustomerOpenClose(userId, startDate, endDate);
            return tickets.ToList();
        }


        [HttpGet("CustomerAssignTeamInfo")]
        public async Task<List<GetTicketCustomerAssignGroupGroup>> CustomerAssignTeamInfo(string userId, DateTime? startDate = null, DateTime? endDate = null)
        {
            var tickets = await _ticketServices.CustomerAssignTeamInfo(userId, startDate, endDate);
            return tickets;
        }

        [HttpGet("UserOpenFormCount")]
        public async Task<int> UserOpenFormCount(string userId, DateTime? startDate = null, DateTime? endDate = null)
        {
            var forms = await _formAssignService.Include();
            var count = forms.Where(e =>
                        e.UserAppId == userId &&
                        e.Status == FormStatus.waiting &&
                        (!startDate.HasValue || e.CreatedDate >= startDate.Value) &&
                        (!endDate.HasValue || e.CreatedDate <= endDate.Value)
            ).Count();

            return count;
        }



    }
}
