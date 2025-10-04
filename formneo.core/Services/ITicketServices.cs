using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.DTOs;
using formneo.core.DTOs.Company;
using formneo.core.DTOs.DashboardDto;
using formneo.core.DTOs.Ticket.Tickets;
using formneo.core.Models.TaskManagement;
using formneo.core.Models.Ticket;

namespace formneo.core.Services
{
    public interface ITicketServices : IService<Tickets>
    {

        Task<TicketDtoResult> GetAllTicketsWithEnumDescriptionsAsync(string createUser, int skip = 0, int top = 50, List<int>? statues = null, TicketFilters? filters = null);
        Task<CustomResponseDto<UserAppDto>> CreateTicketAsync(TicketInsertDto createUserDto);
        Task<TicketDtoResult> GetAllAssignTicketsWithEnumDescriptionsAsync(string createUser, int skip = 0, int top = 50, List<int>? statues = null, TicketFilters? filters = null);
        Task UpdateTicket(Tickets ticket);
        Task<GetSumTicketDto> CountAsync(string userId, DateTime? startDate = null, DateTime? endDate = null);
        Task<List<GetCompanyTicketDto>> GetUserTicketAsync(string userId, DateTime? startDate = null, DateTime? endDate = null);
        Task<List<GetCompanyTicketDto>> GetUserOpenTicketAsync(string userId, DateTime? startDate = null, DateTime? endDate = null);
        Task<List<GetCompanyTicketInfoDto>> GetUserTicketInfoAsync(string userId, DateTime? startDate = null, DateTime? endDate = null);
        Task<List<GetTicketSubjectInfoDto>> GetCustomerTicketSubjectList(string userId, DateTime? startDate = null, DateTime? endDate = null);
        Task<GetTicketStatusDto> GetCustomerTicketStatus(string userId, DateTime? startDate = null, DateTime? endDate = null);
        Task<GetSumTicketDto> CustomerCountAsync(string userId, DateTime? startDate = null, DateTime? endDate = null);

        Task<List<GetTicketCustomerOpenCloseDto>> CustomerOpenClose(string userId, DateTime? startDate = null, DateTime? endDate = null);

        Task<List<GetTicketCustomerAssignGroupGroup>> CustomerAssignTeamInfo(string userId, DateTime? startDate = null, DateTime? endDate = null);

    }
}
