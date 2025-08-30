using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.Services;
using vesa.core.DTOs;
using vesa.core.DTOs.Ticket.TicketRuleEngine;
using vesa.core.DTOs.Ticket.TicketTeams;
using vesa.core.Models.Ticket;
using vesa.core.Services;
using vesa.core.UnitOfWorks;

namespace vesa.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TicketRuleEngineController : CustomBaseController
    {
        private readonly ITicketServices _ticketService;
        private readonly IMapper _mapper;
        private readonly IServiceWithDto<TicketRuleEngine, TicketRuleEngineListDto> _ticketRuleEngineService;
        IUnitOfWork _unitOfWork;

        public TicketRuleEngineController(IUserService userService, IMapper mapper, ITicketServices ticketServices, IUnitOfWork unitOfWork, IServiceWithDto<TicketRuleEngine, TicketRuleEngineListDto> ticketRuleEngineService)
        {
            _ticketService = ticketServices;
            _ticketRuleEngineService = ticketRuleEngineService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("all")]
        public async Task<List<TicketRuleEngineListDto>> GetAllTickets()
        {

            //ticket team departman zorunlu değil
            var result = await _ticketRuleEngineService.GetAllAsync();


            //var values = await _ticketTeamService.Where(e => e.IsDelete == false);
            return result.Data.ToList();
        }

        [HttpPost]
        public async Task<IActionResult> Save(TicketRuleEngineInsertDto dto)
        {
            try
            {
                await _ticketRuleEngineService.AddAsync(_mapper.Map<TicketRuleEngineListDto>(dto));

          
                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, ex.Message));
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateRuleEngineAsync(TicketRuleEngineUpdateDto updateDto)
        {
            try
            {

                await _ticketRuleEngineService.UpdateAsync(_mapper.Map<TicketRuleEngineListDto>(updateDto));
                return Ok("TicketRuleEngine updated successfully.");
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                return StatusCode(500, $"An error occurred while updating the ticket team: {ex.Message}");
            }
        }


        [HttpGet("{id}")]
        public async Task<TicketRuleEngineListDto> GetById(Guid id)
        {
            try
            {

                var result = await _ticketRuleEngineService.GetByIdGuidAsync(id);

                return _mapper.Map<TicketRuleEngineListDto>(result.Data);
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
                await _ticketRuleEngineService.RemoveAsyncByGuid(id);

                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, ex.Message));
            }
        }

    }
}
