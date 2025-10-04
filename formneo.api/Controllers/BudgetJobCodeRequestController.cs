using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using NLayer.Core.Services;
using System.Dynamic;
using formneo.core.DTOs;
using formneo.core.DTOs.Budget.JobCodeRequest;
using formneo.core.DTOs.Budget.NormCodeRequest;
using formneo.core.DTOs.Clients;
using formneo.core.Models;
using formneo.core.Models.BudgetManagement;
using WorkflowCore.Services;

namespace formneo.api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    //[ApiController]
    //[Authorize(AuthenticationSchemes = "BasicAuthentication")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BudgetJobCodeRequestController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IServiceWithDto<BudgetJobCodeRequest, BudgetJobCodeRequestListDto> _service;

        public BudgetJobCodeRequestController(IMapper mapper, IServiceWithDto<BudgetJobCodeRequest, BudgetJobCodeRequestListDto> service)
        {
            _mapper = mapper;

            _service = service;
        }
        /// GET api/products
        [HttpGet]
        public async Task<BudgetJobCodeRequestListDtoResult> All(int skip = 0, int top = 50)
        {
            var forms = await _service.Include();

            var count = forms.Count();

            BudgetJobCodeRequestListDtoResult result = new BudgetJobCodeRequestListDtoResult();

            var data = forms.Include(e => e.WorkflowHead).ToList().OrderByDescending(e => e.CreatedDate).Skip(skip).Take(top);

            result.BudgetJobCodeRequestListDtoList = _mapper.Map<List<BudgetJobCodeRequestListDto>>(data);
            result.Count = count;

            return result;        
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BudgetJobCodeRequestListDto>> GetById(string id)
        {

            var forms = await _service.Include();

            var data = forms.Include(e=>e.WorkflowHead).Where(e => e.Id == new Guid(id)).FirstOrDefault();

            return _mapper.Map<BudgetJobCodeRequestListDto>(data);
        }
        // GET /api/products/5

        //de

        [HttpPost]
        public async Task<ActionResult<BudgetJobCodeRequestListDto>> Save(BudgetJobCodeRequestInsertDto dto)
        {

            var data = await _service.Where(e=>e.JobCode.Trim()==dto.JobCode.Trim());

            if (data.Data.Count() != 0)
            {
                return NotFound("Daha önce bu job code tanımlanmış");
            }

            if (string.IsNullOrEmpty(dto.JobCode.Trim()))
            {
                return NotFound("İş Kodu Boş Bırakılamaz");
            }
            if (string.IsNullOrEmpty(dto.Name.Trim()))
            {
                return NotFound("Adı Boş Bırakılamaz");
            }
            if (string.IsNullOrEmpty(dto.Name_En_US.Trim()))
            {
                return NotFound("İngilizce Adı Boş Bırakılamaz");
            }

            if (string.IsNullOrEmpty(dto.Name_Ru_RU.Trim()))
            {
                return NotFound("Rusça Adı Boş Bırakılamaz");
            }

            if (string.IsNullOrEmpty(dto.RequestReason.Trim()))
            {
                return NotFound("Talep Nedeni Boş Bırakılamaz");
            }
            if (string.IsNullOrEmpty(dto.Cust_Joblevelgroup.Trim()))
            {
                return NotFound("Cust_Joblevelgroup Boş Bırakılamaz");
            }
            if (string.IsNullOrEmpty(dto.Cust_Joblevelgroup.Trim()))
            {
                return NotFound("Cust_Joblevelgroup Boş Bırakılamaz");
            }
            if (string.IsNullOrEmpty(dto.JobFunction.Trim()))
            {
                return NotFound("İş İşlevi Boş Bırakılamaz");
            }
            if (string.IsNullOrEmpty(dto.JobFunction.Trim()))
            {
                return NotFound("İş İşlevi Boş Bırakılamaz");
            }

            var result = await _service.AddAsync(_mapper.Map<BudgetJobCodeRequestListDto>(dto));
            return result.Data;
        }
        [HttpPut]
        public async Task<ActionResult<BudgetJobCodeRequestUpdateDto>> Update(BudgetJobCodeRequestUpdateDto dto)
        {

            if (string.IsNullOrEmpty(dto.JobCode.Trim()))
            {
                return NotFound("İş Kodu Boş Bırakılamaz");
            }
            if (string.IsNullOrEmpty(dto.Name.Trim()))
            {
                return NotFound("Adı Boş Bırakılamaz");
            }

            if (string.IsNullOrEmpty(dto.Name_En_US.Trim()))
            {
                return NotFound("İngilizce Adı Boş Bırakılamaz");
            }

            if (string.IsNullOrEmpty(dto.RequestReason.Trim()))
            {
                return NotFound("Talep Nedeni Boş Bırakılamaz");
            }


            await _service.UpdateAsync(_mapper.Map<BudgetJobCodeRequestListDto>(dto));
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
