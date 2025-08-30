using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLayer.Core.Services;
using vesa.core.DTOs;
using vesa.core.DTOs.PositionsDtos;
using vesa.core.DTOs.Ticket.TicketTeams;
using vesa.core.Models;
using vesa.core.Models.Ticket;
using vesa.core.Services;

namespace vesa.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WorkCompanySystemInfoController : CustomBaseController
    {
        private readonly IPositionService _positionService;
        private readonly IMapper _mapper;
        private readonly IServiceWithDto<WorkCompanySystemInfo, WorkCompanySystemInfoListDto> _workCompanySystemInfoService;

        public WorkCompanySystemInfoController(IServiceWithDto<WorkCompanySystemInfo, WorkCompanySystemInfoListDto> workCompanySystemInfoService, IMapper mapper)
        {
            _workCompanySystemInfoService = workCompanySystemInfoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<WorkCompanySystemInfoListDto>> GetAllWorkCompanySystemList()
        {
            var result = await _workCompanySystemInfoService.Include();

            result = result.Include(e => e.WorkCompany);
            var data = result.ToList();

            return _mapper.Map<List<WorkCompanySystemInfoListDto>>(data);

            //var values = await _workCompanySystemInfoService.GetAllAsync();
            //return values.Data.ToList();
        }

        [HttpPost]
        public async Task<IActionResult> Save(WorkCompanySystemInfoInsertDto dto)
        {
            try
            {
                if (dto.Name == null || dto.Name == "")
                {
                    return StatusCode(500, "Company name field cannot be left blank..!");
                }

                await _workCompanySystemInfoService.AddAsync(_mapper.Map<WorkCompanySystemInfoListDto>(dto));

                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));

            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, ex.Message));
            }
        }

        [HttpGet("by-company-id/{id}")]
        public async Task<List<WorkCompanySystemInfoListDto>> GetById(Guid id)
        {
            try
            {
                var query = await _workCompanySystemInfoService.Include();
                query = query.Include(x => x.WorkCompany);

                var result = await query.Where(e => e.WorkCompanyId == id).ToListAsync();

                return _mapper.Map<List<WorkCompanySystemInfoListDto>>(result);
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
                var systemInfo = await _workCompanySystemInfoService.GetByIdGuidAsync(id);

                if (systemInfo == null)
                {
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "Sistem bilgisi bulunamadı"));
                }

                await _workCompanySystemInfoService.RemoveAsyncByGuid(id);

                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, ex.Message));
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateAsync(WorkCompanySystemInfoUpdateDto updateDto)
        {
            try
            {


                var companySysService = await _workCompanySystemInfoService.Include();

                var existingCompanySys = await companySysService.Where(e => e.Id == new Guid(updateDto.Id)).FirstOrDefaultAsync();

                if (existingCompanySys == null)
                {
                    return NotFound("Work company system info not found.");
                }

                if(updateDto.Name==null || updateDto.Name == "")
                {
                    return StatusCode(500, "Company name field cannot be left blank..!");
                }

                existingCompanySys.WorkCompanyId = updateDto.WorkCompanyId;
                existingCompanySys.Name = updateDto.Name;


                // Değişiklikleri kaydet
                await _workCompanySystemInfoService.UpdateAsync(_mapper.Map<WorkCompanySystemInfoListDto>(existingCompanySys));

                return Ok("Work company system info updated successfully.");
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                return StatusCode(500, $"An error occurred while updating the work company system info: {ex.Message}");
            }
        }


        [HttpGet("by-system-id/{id}")]
        public async Task<WorkCompanySystemInfoListDto> GetSystemById(Guid id)
        {
            try
            {
                var query = await _workCompanySystemInfoService.Include();
                query = query.Include(x => x.WorkCompany);

                var result = await query.Where(e => e.Id == id).FirstOrDefaultAsync();

                return _mapper.Map<WorkCompanySystemInfoListDto>(result);
            }
            catch (Exception ex)
            {
                return null;

            }
        }
    }
}
