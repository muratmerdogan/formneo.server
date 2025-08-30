using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLayer.Core.Services;
using vesa.core.DTOs;
using vesa.core.DTOs.PositionsDtos;
using vesa.core.DTOs.TaskManagement;
using vesa.core.DTOs.Ticket.TicketTeams;
using vesa.core.Models;
using vesa.core.Models.Ticket;
using vesa.core.Services;

namespace vesa.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WorkCompanyTicketMatrisController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IServiceWithDto<WorkCompanyTicketMatris, WorkCompanyTicketMatrisListDto> _workCompanyMatrisService;
        private readonly IServiceWithDto<WorkCompany, WorkCompanyDto> _workCompanyService;

        public WorkCompanyTicketMatrisController(IServiceWithDto<WorkCompanyTicketMatris, WorkCompanyTicketMatrisListDto> workCompanyMatrisService, IMapper mapper, IServiceWithDto<WorkCompany, WorkCompanyDto> workCompanyService)
        {
            _workCompanyMatrisService = workCompanyMatrisService;
            _mapper = mapper;
            _workCompanyService = workCompanyService;
        }

        [HttpGet]
        public async Task<List<WorkCompanyTicketMatrisListDto>> GetAll()
        {
            var result = await _workCompanyMatrisService.Include();
            var data = result.Include(e => e.FromCompany).Include(e => e.ToCompanies).ToList();
           
            var companies = await _workCompanyService.GetAllAsync();
            var companiess = companies.Data;

            var dtoList = data.Select(item => new WorkCompanyTicketMatrisListDto
            {
                Id = item.Id,
                FromCompany = _mapper.Map<WorkCompanyDto>(item.FromCompany),
                FromCompanyId = item.FromCompany.Id,
                ToCompaniesIds = item.ToCompaniesIds,
                ToCompanies = companiess != null && item.ToCompaniesIds != null
                            ? companiess.Where(c => item.ToCompaniesIds.Contains(c.Id))
                                        .ToList()
                            : null
            }).ToList();

            return dtoList;
        }

        [HttpGet("id")]
        public async Task<WorkCompanyTicketMatrisListDto> GetById(Guid id)
        {
            var result = await _workCompanyMatrisService.Include();

            var data = result.Where(e => e.Id == id).Include(e => e.FromCompany).Include(e => e.ToCompanies).FirstOrDefault();
            
            var companies = await _workCompanyService.GetAllAsync();
            var companiess = companies.Data;

            var dto = new WorkCompanyTicketMatrisListDto
            {
                Id = data.Id,
                FromCompany = _mapper.Map<WorkCompanyDto>(data.FromCompany),
                FromCompanyId = data.FromCompany.Id,
                ToCompaniesIds = data.ToCompaniesIds,
                ToCompanies = companiess != null && data.ToCompaniesIds != null
                            ? companiess.Where(c => data.ToCompaniesIds.Contains(c.Id))
                                        .ToList()
                            : null
            };
            return dto;
        }

        [HttpPost]
        public async Task<IActionResult> Save(WorkCompanyTicketMatrisInsertDto dto)
        {
            try
            {
                await _workCompanyMatrisService.AddAsync(_mapper.Map<WorkCompanyTicketMatrisListDto>(dto));

                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));

            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, ex.Message));
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(WorkCompanyTicketMatrisUpdateDto dto)
        {
            try
            {
                var service = await _workCompanyMatrisService.Include();
                var existing = service.Where(e => e.FromCompanyId == dto.FromCompanyId).FirstOrDefault();

                if (existing == null)
                {
                    return NotFound("Work company matris not found.");
                }

                existing.ToCompaniesIds = dto.ToCompaniesIds;
              
                await _workCompanyMatrisService.UpdateAsync(_mapper.Map<WorkCompanyTicketMatrisListDto>(existing));

                return Ok("Work company matris updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the work company: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var ticketMatris = await _workCompanyMatrisService.GetByIdGuidAsync(id);

                if (ticketMatris == null)
                {
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "ticketMatris bulunamadı"));
                }

                await _workCompanyMatrisService.RemoveAsyncByGuid(id);

                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, ex.Message));
            }
        }

    }
}
