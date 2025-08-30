using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLayer.Core.Services;
using System.ComponentModel;
using System.Reflection;
using System.Security.Claims;
using vesa.core.DTOs;
using vesa.core.DTOs.FormAssign;
using vesa.core.DTOs.PositionsDtos;
using vesa.core.DTOs.Ticket.TicketTeams;
using vesa.core.Models;
using vesa.core.Models.Ticket;
using vesa.core.Services;
using vesa.core.UnitOfWorks;

namespace vesa.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WorkCompanyController : CustomBaseController
    {
        private readonly IPositionService _positionService;
        private readonly IMapper _mapper;
        private readonly IServiceWithDto<WorkCompany, WorkCompanyDto> _workCompanyService;
        private readonly UserManager<UserApp> _userManager;
        private readonly IServiceWithDto<core.Models.WorkCompanyTicketMatris, WorkCompanyTicketMatrisListDto> _workCompanyMatrisService;

        public WorkCompanyController(IServiceWithDto<WorkCompany, WorkCompanyDto> workCompanyService, IMapper mapper, UserManager<UserApp> userManager, IServiceWithDto<core.Models.WorkCompanyTicketMatris, WorkCompanyTicketMatrisListDto> workCompanyMatrisService)
        {
            _workCompanyService = workCompanyService;
            _mapper = mapper;
            _userManager = userManager;
            _workCompanyMatrisService = workCompanyMatrisService;
        }

        // Yalnızca aktif şirketler
        [HttpGet]
        public async Task<ActionResult<List<WorkCompanyDto>>> GetPositionAllList()
        {
            try
            {

                var service = await _workCompanyService.Include();
                var values = service.Include(e => e.WorkFlowDefination).OrderBy(e => e.Name).Where(e => e.IsActive != false);
                return _mapper.Map<List<WorkCompanyDto>>(values.ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        // Tüm şirketler
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<WorkCompanyDto>>> GetAll()
        {
            try
            {

                var service = await _workCompanyService.Include();
                var values = service.Include(e => e.WorkFlowDefination).OrderBy(e => e.Name);
                return _mapper.Map<List<WorkCompanyDto>>(values.ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpGet("GetAssingList")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<List<WorkCompanyDto>> GetAllOnlyName()
        {
            var userId = User.FindFirstValue(ClaimTypes.Name);
            var user = await _userManager.FindByNameAsync(userId);
            var serviceResult = await _workCompanyService.GetAllAsync();
            var adminWorkCompanyId = new Guid("2e5c2ba5-3eb8-414d-8bc7-08dd44716854");

            var activeCompanies = serviceResult.Data.Where(x => x.IsActive != false).ToList();

            if (user != null)
            {
                var workCompanyMatris = await _workCompanyMatrisService.Include();
                var data = workCompanyMatris.Where(e => e.FromCompanyId == user.WorkCompanyId).FirstOrDefault();
                if (data != null)
                {
                    var companiesIds = data.ToCompaniesIds;
                    if (companiesIds.Count > 0)
                    {
                        companiesIds.Add((Guid)user.WorkCompanyId);
                        return activeCompanies.Where(x => companiesIds.Contains(x.Id)).OrderBy(e => e.Name).ToList();
                    }
                }

                return user.WorkCompanyId == adminWorkCompanyId
                ? activeCompanies.OrderBy(e => e.Name).ToList()
                : activeCompanies.Where(x => x.Id == user.WorkCompanyId).OrderBy(e => e.Name).ToList();
            }
            else
            {
                return new List<WorkCompanyDto>();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Save(WorkCompanyInsertDto dto)
        {
            try
            {
                await _workCompanyService.AddAsync(_mapper.Map<WorkCompanyDto>(dto));

                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));

            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var workCompany = await _workCompanyService.GetByIdGuidAsync(id);

                if (workCompany == null)
                {
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "Şirket bilgisi bulunamadı"));
                }

                await _workCompanyService.RemoveAsyncByGuid(id);

                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, ex.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<WorkCompanyDto> GetById(Guid id)
        {
            try
            {
                var query = await _workCompanyService.Include();
                query = query.Include(e => e.UserApp).Include(e => e.WorkFlowDefination);

                var result = await query.Where(e => e.Id == id).FirstOrDefaultAsync();

                return _mapper.Map<WorkCompanyDto>(result);
            }
            catch (Exception ex)
            {
                return null;

            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(WorkCompanyUpdateDto updateDto)
        {
            try
            {


                var workCompanyService = await _workCompanyService.Include();
                var existingWorkCompany = await workCompanyService.Where(e => e.Id == new Guid(updateDto.Id)).FirstOrDefaultAsync();

                if (existingWorkCompany == null)
                {
                    return NotFound("Work company not found.");
                }

                existingWorkCompany.Name = updateDto.Name;
                existingWorkCompany.UserAppId = updateDto.UserAppId;
                existingWorkCompany.ApproveWorkDesign = updateDto.ApproveWorkDesign;
                //existingWorkCompany.WorkFlowDefinationId = new Guid(updateDto.WorkFlowDefinationId);
                existingWorkCompany.WorkFlowDefinationId = string.IsNullOrWhiteSpace(updateDto.WorkFlowDefinationId) ? null : new Guid(updateDto.WorkFlowDefinationId);
                existingWorkCompany.IsActive = updateDto.IsActive;


                // Değişiklikleri kaydet
                await _workCompanyService.UpdateAsync(_mapper.Map<WorkCompanyDto>(existingWorkCompany));

                return Ok("Work company updated successfully.");
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                return StatusCode(500, $"An error occurred while updating the work company: {ex.Message}");
            }
        }

        [HttpGet("GetApproveWorkDesign")]
        public IActionResult GetApproveWorkDesign()
        {
            var values = Enum.GetValues(typeof(ApproveWorkDesign))
                             .Cast<ApproveWorkDesign>()
                             .Select(e => new
                             {
                                 Id = (int)e,
                                 Name = e.ToString(),
                                 Description = GetEnumDescription(e)
                             });

            return Ok(values);
        }


        //enum desc alanini almak icin
        private string GetEnumDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = field?.GetCustomAttribute<DescriptionAttribute>();

            return attribute?.Description ?? value.ToString();
        }
    }
}
