using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using NLayer.Core.Services;
using System.Dynamic;
using vesa.core.DTOs;
using vesa.core.DTOs.Budget.JobCodeRequest;
using vesa.core.DTOs.Budget.NormCodeRequest;
using vesa.core.DTOs.Clients;
using vesa.core.Models;
using vesa.core.Models.BudgetManagement;
using WorkflowCore.Services;

namespace vesa.api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    //[ApiController]
    //[Authorize(AuthenticationSchemes = "BasicAuthentication")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BudgetPromotionRequestController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IServiceWithDto<BudgetPromotionRequest, BudgetPromotionRequestListDto> _service;

        public BudgetPromotionRequestController(IMapper mapper, IServiceWithDto<BudgetPromotionRequest, BudgetPromotionRequestListDto> service)
        {
            _mapper = mapper;

            _service = service;
        }
        /// GET api/products
        [HttpGet]
        public async Task<List<BudgetPromotionRequestListDto>> All()
        {
            var forms = await _service.Include();
            var data = forms.Include(e => e.WorkflowHead).ToList();


            return _mapper.Map<List<BudgetPromotionRequestListDto>>(data);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BudgetPromotionRequestListDto>> GetById(string id)
        {
            var forms = await _service.Include();

            var data = forms.Include(e => e.WorkflowHead).Where(e => e.Id == new Guid(id)).FirstOrDefault();

            return _mapper.Map<BudgetPromotionRequestListDto>(data);
        }
        // GET /api/products/5

        //de

        [HttpPost]
        public async Task<ActionResult<BudgetPromotionRequestListDto>> Save(BudgetPromotionRequestInsertDto dto)
        {

            

            var result = await _service.AddAsync(_mapper.Map<BudgetPromotionRequestListDto>(dto));
            return result.Data;
        }
        [HttpPut]
        public async Task<ActionResult<BudgetPromotionRequestUpdateDto>> Update(BudgetPromotionRequestUpdateDto dto)
        {

            await _service.UpdateAsync(_mapper.Map<BudgetPromotionRequestListDto>(dto));
            return dto;
        }
        // DELETE api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(string id)
        {
            await _service.RemoveAsyncByGuid(new Guid(id));
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
        //[HttpGet("[action]/{formId}")]
        //public async Task<FormRuntimeDto> getFormDataById(Guid formId)
        //{
        //    var forms = await _service.Find(e => e.FormId == formId);

        //    var dto = _mapper.Map<FormRuntimeDto>(forms.Data);

        //    return dto;
        //}

    }
}
