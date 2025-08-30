using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NLayer.Core.Services;
using System.Dynamic;
using vesa.core.DTOs;
using vesa.core.DTOs.Clients;
using vesa.core.Models;

namespace vesa.api.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BudgetPeriodController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IServiceWithDto<BudgetPeriod, BudgetPeriodListDto> _service;

        public BudgetPeriodController(IMapper mapper, IServiceWithDto<BudgetPeriod, BudgetPeriodListDto> service)
        {
            _mapper = mapper;

            _service = service;


        }
        /// GET api/products
        [HttpGet]
        public async Task<List<BudgetPeriodListDto>> All()
        {
            var forms = await _service.GetAllAsync();

            return forms.Data.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BudgetPeriodListDto>> GetById(string id)
        {
            var data = await _service.GetByIdAsync(id);
            return data.Data;
        }
        // GET /api/products/5

        //de
        [HttpPost]
        public async Task<ActionResult<BudgetPeriodListDto>> Save(BudgetPeriodInsertDto dto)
        {

            if (dto.PeriodCode.Trim() == "")
            {
                return NotFound("Periyod Kodu Boş Bırakılamaz");
            }
            if (dto.Name.Trim() == "")
            {
                return NotFound("Periyod Adı Boş Bırakılamaz");
            }

            if (dto.StartDate > dto.EnDate)
            {
                return NotFound("Başlangıç Tarihi bitiş tarihinden büyük olamaz");
            }

            if (dto.StartDate == dto.EnDate)
            {
                return NotFound("Başlangıç Tarihi ve bitiş bitiş tarihi aynı olamaz");
            }

            var data = await _service.GetByIdAsync(dto.PeriodCode.Trim());

            if (data.Data != null)
            {
                return NotFound("Daha önce bu periyod kod tanımlanmış");
            }


            var result = await _service.AddAsync(_mapper.Map<BudgetPeriodListDto>(dto));

            return result.Data;

        }
        [HttpPut]
        public async Task<ActionResult<BudgetPeriodUpdateDto>> Update(BudgetPeriodUpdateDto dto)
        {

            if (dto.PeriodCode.Trim() == "")
            {
                return NotFound("Periyod Kodu Boş Bırakılamaz");
            }
            if (dto.Name.Trim() == "")
            {
                return NotFound("Periyod Adı Boş Bırakılamaz");
            }

            if (dto.StartDate > dto.EnDate)
            {
                return NotFound("Başlangıç Tarihi bitiş tarihinden büyük olamaz");
            }

            if (dto.StartDate == dto.EnDate)
            {
                return NotFound("Başlangıç Tarihi ve bitiş bitiş tarihi aynı olamaz");
            }

           
            await _service.UpdateAsync(_mapper.Map<BudgetPeriodListDto>(dto));

            return dto;
        }

        // DELETE api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(string id)
        {

            await _service.RemoveAsync(id);

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
