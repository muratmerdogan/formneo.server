using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using NLayer.Core.Services;
using System.Dynamic;
using System.Net.Http;
using System.Net.Http.Headers;
using vesa.core.DTOs;
using vesa.core.DTOs.Budget.PeriodUserDto;
using vesa.core.DTOs.Clients;
using vesa.core.EnumExtensions;
using vesa.core.Models;
using vesa.core.Models.BudgetManagement;

namespace vesa.api.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "BasicAuthentication")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BudgetPeriodUserController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IServiceWithDto<BudgetPeriodUser, BudgetPeriodUserListDto> _service;

        public BudgetPeriodUserController(IMapper mapper, IServiceWithDto<BudgetPeriodUser, BudgetPeriodUserListDto> service)
        {
            _mapper = mapper;

            _service = service;


        }
        /// GET api/products
        [HttpGet]
        public async Task<List<BudgetPeriodUserListDto>> All()
        {
            var forms = await _service.Include();

            var data = forms.Include(e => e.BudgetPeriod).ToList();
            var processedData = data
                    .Select(d => new BudgetPeriodUserListDto
                    {
                        Id = d.Id,
                        permission = d.permission,
                        nameSurname= d.nameSurname,
                        requestType = d.requestType,
                        requestTypeText = d.requestType.GetDescription(),
                        UserName = d.UserName,
                        BudgetPeriodCode = d.BudgetPeriodCode,
                        permissiontypeText = d.permission.GetDescription(),
                        BudgetPeriod = _mapper.Map<BudgetPeriodListDto>(d.BudgetPeriod),
                        processtypeText = d.processType.GetDescription(),
                    })
                    .ToList().OrderBy(e => e.CreatedDate);


            return _mapper.Map<List<BudgetPeriodUserListDto>>(processedData);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BudgetPeriodUserListDto>> GetById(string id)
        {
            var data = await _service.GetByIdGuidAsync(new Guid(id));
            data.Data.permissiontypeText = data.Data.permission.GetDescription();
            data.Data.requestTypeText = data.Data.requestType.GetDescription();
            data.Data.processtypeText = data.Data.processType.GetDescription();
            return data.Data;
        }
        // GET /api/products/5

        //de

        [HttpPost]
        public async Task<ActionResult<BudgetPeriodUserListDto>> Save(BudgetPeriodUserInsertDto dto)
        {


            var forms = await _service.Include();
            var data = forms.Include(e => e.BudgetPeriod).ToList();
            var isExists = data.Any(e => e.BudgetPeriodCode == dto.BudgetPeriodCode && e.UserName == dto.UserName &&
                                    e.requestType == dto.requestType && e.processType == dto.processType);

            if (isExists)
            {
                return BadRequest($"{dto.UserName} için zaten bir bütçe dönemi var.");
            }



            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(
                System.Text.ASCIIEncoding.ASCII.GetBytes(
               $"{Config.Config.UserName}:{Config.Config.Password}")));


                using (var response = await httpClient.GetAsync($"{Config.Config.SfAddress}/User?$format=json&$filter=userId eq '" + dto.UserName + "'"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var responseBody = await response.Content.ReadAsStringAsync();

                    // JSON verisini JObject'e dönüştür
                    var json = JObject.Parse(responseBody);

                    // "d" kısmını al ve sonuçları liste olarak çıkar
                    string nameSurname = json["d"]["results"][0]["displayName"].ToString();

                    //dto.Count = (int)json["d"]["__count"]; ;
                    dto.nameSurname = nameSurname;

                }
            }

            var result = await _service.AddAsync(_mapper.Map<BudgetPeriodUserListDto>(dto));

            return result.Data;

        }
        [HttpPut]
        public async Task<ActionResult<BudgetPeriodUserUpdateDto>> Update(BudgetPeriodUserUpdateDto dto)
        {



            await _service.UpdateAsync(_mapper.Map<BudgetPeriodUserListDto>(dto));

            return dto;
        }

        // DELETE api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(string id)
        {

            await _service.RemoveAsyncByGuid(new Guid(id));

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }


        [HttpGet("GetActivePeriod/{userid}")]
        public async Task<ActionResult<BudgetPeriodUserListDto>> GetActivePeriod(string userid, RequestType ScreenType)
        {
            var data = await _service.Include();
            var list = data.Include(e => e.BudgetPeriod).Where(e => e.UserName == userid && (e.requestType == ScreenType || e.requestType == RequestType.Hepsi) && e.BudgetPeriod.StartDate <= DateTime.Now && e.BudgetPeriod.EnDate >= DateTime.Now).ToList().FirstOrDefault();

            if (list == null)
            {
                return NotFound("Aktif Dönem Yok");
            }


            var result = _mapper.Map<BudgetPeriodUserListDto>(list);

            result.processtypeText = result.processType.GetDescription();
            result.requestTypeText = result.requestType.GetDescription();
            result.permissiontypeText = result.permission.GetDescription();
            return result;
        }



    }

    //[HttpGet("[action]/{formId}")]
    //public async Task<FormRuntimeDto> getFormDataById(Guid formId)
    //{
    //    var forms = await _service.Find(e => e.FormId == formId);

    //    var dto = _mapper.Map<FormRuntimeDto>(forms.Data);

    //    return dto;
    //}



}



