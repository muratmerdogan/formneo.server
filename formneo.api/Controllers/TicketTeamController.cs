using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.Services;
using vesa.core.DTOs.Ticket;
using vesa.core.DTOs;
using vesa.core.DTOs.Ticket.TicketDepartments;
using vesa.core.DTOs.Ticket.TicketTeams;
using vesa.core.Models.Ticket;
using vesa.core.Services;
using vesa.service.Services;
using vesa.core.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace vesa.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TicketTeamController : CustomBaseController
    {
        private readonly ITicketServices _ticketService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IServiceWithDto<TicketTeam, TicketTeamListDto> _ticketTeamService;
        private readonly IServiceWithDto<TicketTeamUserApp, TicketTeamUserAppListDto> _ticketTeamUserAppService;
        IUnitOfWork _unitOfWork;

        public TicketTeamController(IUserService userService, IMapper mapper, ITicketServices ticketServices, IUnitOfWork unitOfWork, IServiceWithDto<TicketTeam, TicketTeamListDto> ticketTeamService, IServiceWithDto<TicketTeamUserApp, TicketTeamUserAppListDto> ticketTeamUserAppService)
        {
            _ticketService = ticketServices;
            _ticketTeamService = ticketTeamService;
            _ticketTeamUserAppService = ticketTeamUserAppService;
            _mapper = mapper;
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("all")]
        public async Task<List<TicketTeamListDto>> GetAllTickets()
        {

            //ticket team departman zorunlu değil
            var result = await _ticketTeamService.Include();

            result = result.Include(e => e.Department).Include(e => e.Manager).Include(e => e.WorkCompany).Include(e=>e.TeamList).ThenInclude(e => e.UserApp); ;

            var data = result.ToList().Where(e => e.IsDelete == false);
            //var values = await _ticketTeamService.Where(e => e.IsDelete == false);
            return _mapper.Map<List<TicketTeamListDto>>(data);
        }

        [HttpGet("without-team")]
        public async Task<List<TicketTeamListDto>> GetWithoutTeam()
        {

            //ticket team departman zorunlu değil
            var result = await _ticketTeamService.Include();

            result = result.Include(e => e.Department).Include(e => e.Manager).Include(e => e.WorkCompany).Where(e => e.IsDelete == false);







            //var values = await _ticketTeamService.Where(e => e.IsDelete == false);
            return _mapper.Map<List<TicketTeamListDto>>(result.ToList());
        }
        [HttpPost]
        public async Task<IActionResult> Save(TicketTeamInsertDto dto)
        {
            try
            {
                // Aynı departman kodundan var mı kontrol
                _unitOfWork.BeginTransaction();
                List<TicketTeamUserAppInsertDto> userapplist = new List<TicketTeamUserAppInsertDto>();
                foreach (var item in dto.TeamList)
                {
                    TicketTeamUserAppInsertDto x = new TicketTeamUserAppInsertDto();
                    x.UserAppId = item.UserAppId;

                    //await _ticketTeamUserAppService.AddAsync(_mapper.Map<TicketTeamUserAppListDto>(x));


                    userapplist.Add(x);
                }
                dto.TeamList = userapplist;

                var listdto = _mapper.Map<TicketTeamListDto>(dto);
                var result = await _ticketTeamService.AddAsync(listdto);

                _unitOfWork.Commit();
                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, ex.Message));
            }
        }


        [HttpGet("{id}")]
        public async Task<TicketTeamListDto> GetById(Guid id)
        {
            try
            {
                var query = await _ticketTeamService.Include();
                query = query.Include(e => e.Department).Include(e => e.Manager).Include(e => e.WorkCompany).Include(x => x.TeamList).ThenInclude(e => e.UserApp);

                var result = await query.Where(e => e.Id == id && e.IsDelete == false).FirstOrDefaultAsync();

                return _mapper.Map<TicketTeamListDto>(result);
            }
            catch (Exception ex)
            {
                return null;

            }
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {

                await _ticketTeamService.SoftDeleteAsync(id);

                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, ex.Message));
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTicketTeamAsync(TicketTeamUpdateDto updateDto)
        {
            try
            {


                var ticketTeamService = await _ticketTeamService.Include();
                // Mevcut TicketTeam'i ve ilişkili verileri yükle
                var existingTicketTeam = await ticketTeamService.Include(e => e.TeamList)
                                                         .Where(e => e.Id == new Guid(updateDto.Id))
                                                         .FirstOrDefaultAsync();

                if (existingTicketTeam == null)
                {
                    return NotFound("Ticket team not found.");
                }

                // TicketTeam özelliklerini güncelle
                existingTicketTeam.Name = updateDto.Name;
                existingTicketTeam.DepartmentId = updateDto.DepartmentId;
                existingTicketTeam.WorkCompanyId = updateDto.WorkCompanyId;
                existingTicketTeam.ManagerId = updateDto.ManagerId;
                // Diğer özellikleri güncelle

                // Mevcut TeamList'i güncelle

                foreach (var teamMemberId in existingTicketTeam.TeamList)
                {
                    await _ticketTeamUserAppService.RemoveAsyncByGuid(teamMemberId.Id);
                }

                existingTicketTeam.TeamList.Clear();

                foreach (var teamMemberId in updateDto.TeamList)
                {
                
                    existingTicketTeam.TeamList.Add(new TicketTeamUserApp
                    {
                        UserAppId = teamMemberId.UserAppId.ToString(),
                    }); ;
                }

                // Değişiklikleri kaydet
                await _ticketTeamService.UpdateAsync(_mapper.Map<TicketTeamListDto>(existingTicketTeam));
                await _unitOfWork.CommitAsync();

                return Ok("Ticket team updated successfully.");
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                return StatusCode(500, $"An error occurred while updating the ticket team: {ex.Message}");
            }
        }



        //public class TicketSourceListDto
        //{
        //    public int Id { get; set; }
        //    public string Name { get; set; }

        //}
    }

}
