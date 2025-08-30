using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.DTOs;
using vesa.core.DTOs.Company;
using vesa.core.DTOs.DashboardDto;
using vesa.core.DTOs.Ticket.Tickets;
using vesa.core.Models.TaskManagement;
using vesa.core.Models.Ticket;

namespace vesa.core.Services
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
