using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json.Linq;
using NLayer.Core.Services;
using System.Diagnostics.Contracts;
using System.Dynamic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using vesa.core.DTOs;
using vesa.core.DTOs.Budget.JobCodeRequest;
using vesa.core.DTOs.Budget.NormCodeRequest;
using vesa.core.DTOs.Budget.SF;
using vesa.core.DTOs.Clients;
using vesa.core.EnumExtensions;
using vesa.core.Models;
using vesa.core.Models.BudgetManagement;
using vesa.workflow;
using WorkflowCore.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace vesa.api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    //[ApiController]
    //[Authorize(AuthenticationSchemes = "BasicAuthentication")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BudgetNormCodeRequestController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IServiceWithDto<BudgetNormCodeRequest, BudgetNormCodeRequestListDto> _service;

        public BudgetNormCodeRequestController(IMapper mapper, IServiceWithDto<BudgetNormCodeRequest, BudgetNormCodeRequestListDto> service)
        {
            _mapper = mapper;

            _service = service;
        }
        /// GET api/products
        [HttpGet]
        public async Task<BudgetNormCodeRequestListDtoResult> All(int skip = 0, int top = 50, string createdBy = "test")
        {
            BudgetNormCodeRequestListDtoResult result = new BudgetNormCodeRequestListDtoResult();


            var forms = await _service.Include();


            // Toplam kayıt sayısını almak için Count sorgusu
            var count = forms.Include(e => e.WorkflowHead).Where(e => e.isDeleted == false).Count();
            var data = forms.Include(e => e.WorkflowHead).Where(e => e.isDeleted == false ).ToList().OrderByDescending(e => e.CreatedDate).Skip(skip).Take(top);
            var list = _mapper.Map<List<BudgetNormCodeRequestListDto>>(data).OrderByDescending(e => e.CreatedDate).ToList();


            foreach (var item in list)
            {
                try
                {
                    item.ProcessTypeText = item.ProcessType.GetDescription();
                    item.InternalEmploymentTypeText = item.InternalEmploymentType.GetDescription();
                }
                catch
                {

                    item.ProcessTypeText = "";
                }
            }


            result.Count = count;
            result.BudgetNormCodeRequestListDtoList = list;
            return result;
        }



        [HttpGet("all/")]
        public async Task<BudgetNormCodeRequestListDtoOnlyCodeResult> AllData()
        {
            BudgetNormCodeRequestListDtoOnlyCodeResult result = new BudgetNormCodeRequestListDtoOnlyCodeResult();


            var forms = await _service.Include();




            var data = forms.Include(e => e.WorkflowHead).Where(e => e.isDeleted == false).ToList().OrderByDescending(e => e.CreatedDate);

            var list = _mapper.Map<List<BudgetNormCodeRequestListOnlyCodeDto>>(data).ToList();


            var resulData = list.Where(e => e.WorkflowHead.workFlowStatus == WorkflowStatus.InProgress);



            result.Count = 0;
            result.BudgetNormCodeRequestListDtoList = list;
            return result;
        }

        [HttpGet("allWaitingApproveCount/")]
        public async Task<int> AllWaitingApproveCount()
        {
            BudgetNormCodeRequestListDtoOnlyCodeResult result = new BudgetNormCodeRequestListDtoOnlyCodeResult();


            var forms = await _service.Include();
            var data = forms.Include(e => e.WorkflowHead).ToList().OrderByDescending(e => e.CreatedDate);

            var list = _mapper.Map<List<BudgetNormCodeRequestListOnlyCodeDto>>(data).ToList();


            var listCount = list.Where(e => e.WorkflowHead?.workFlowStatus == WorkflowStatus.InProgress).Count();

            return listCount;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<BudgetNormCodeRequestListDto>> GetById(string id)
        {

            var forms = await _service.Include();

            var data = forms.Include(e => e.WorkflowHead).Where(e => e.Id == new Guid(id) && e.isDeleted == false).FirstOrDefault();

            var item = _mapper.Map<BudgetNormCodeRequestListDto>(data);

            try
            {
                item.ProcessTypeText = item.ProcessType.GetDescription();
                item.InternalEmploymentTypeText = item.InternalEmploymentType.GetDescription();
            }
            catch
            {

            }

            return item;
        }


        [HttpGet("byCode/{code}")]
        public async Task<ActionResult<BudgetNormCodeRequestListDto>> GetByCode(string code)
        {

            var forms = await _service.Include();

            var data = forms.Include(e => e.WorkflowHead).Where(e => e.code == code && e.isDeleted == false).FirstOrDefault();

            var item = _mapper.Map<BudgetNormCodeRequestListDto>(data);

            try
            {
                item.ProcessTypeText = item.ProcessType.GetDescription();
                item.InternalEmploymentTypeText = item.InternalEmploymentType.GetDescription();
            }
            catch
            {

            }

            return item;
        }



        // GET /api/products/5

        //de

        [HttpPost]
        public async Task<ActionResult<BudgetNormCodeRequestListDto>> Save(BudgetNormCodeRequestInsertDto dto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (string.IsNullOrEmpty(dto.targetFTE))
            //{
            //    return NotFound("Tam Zamanlı Boş Bırakılamaz");
            //}


            dto.isDeleted = false;
            var result = await _service.AddAsync(_mapper.Map<BudgetNormCodeRequestListDto>(dto));
            return result.Data;
        }
        [HttpPut]
        public async Task<ActionResult<BudgetNormCodeRequestUpdateDto>> Update(BudgetNormCodeRequestUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _service.UpdateAsync(_mapper.Map<BudgetNormCodeRequestListDto>(dto));
            return dto;
        }
        // DELETE api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(string id)
        {
            var forms = await _service.Include();
            var data = forms.Include(e => e.WorkflowHead).Where(e => e.Id == new Guid(id) && e.isDeleted == false).FirstOrDefault();
            var item = _mapper.Map<BudgetNormCodeRequestListDto>(data);
            if (item != null)
            {
                item.isDeleted = true;
                await _service.UpdateAsync(item);
            }
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
            //var value = await GetByCode(id);
            //if (value != null)
            //{

            //}

            //await _service.RemoveAsyncByGuid(new Guid(id));
            //return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
        //[HttpGet("[action]/{formId}")]
        //public async Task<FormRuntimeDto> getFormDataById(Guid formId)
        //{
        //    var forms = await _service.Find(e => e.FormId == formId);

        //    var dto = _mapper.Map<FormRuntimeDto>(forms.Data);

        //    return dto;
        //}




        [HttpGet("CheckPositionHasJobPost")]
        public async Task<string> CheckPositionHasJobPost(string code)
        {

            string url = $"{Config.Config.SfAddress}/JobReqGOPosition?$expand=value&$format=json&$filter=value/code eq  '" + code + "'&$select=value/code,jobReqId";


            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(
                System.Text.ASCIIEncoding.ASCII.GetBytes(
               $"{Config.Config.UserName}:{Config.Config.Password}")));

                using (var response = await httpClient.GetAsync(url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var responseBody = await response.Content.ReadAsStringAsync();

                    Root rootObject = JsonSerializer.Deserialize<Root>(responseBody);

                    // `results` dizisinin boş olup olmadığını kontrol et
                    if (rootObject.d != null && rootObject.d.results != null && rootObject.d.results.Count > 0)
                    {
                        //return true;
                        return rootObject.d.results[0].jobReqId;
                    }
                    else
                    {
                        //return false;
                        return "";
                    }

                }
            }

        }




        //[HttpGet("byCode/{code}")]
        //public async Task<ActionResult<BudgetNormCodeRequestListDto>> GetDetailByWorkFlowId(string workFlowId)
        //{

        //    var forms = await _service.Include();

        //    var data = forms.Include(e => e.WorkflowHead).Where(e => e.WorkflowHeadId == new Guid(workFlowId) && e.isDeleted == false).FirstOrDefault();

        //    var item = _mapper.Map<BudgetNormCodeRequestListDto>(data);

        //    try
        //    {
        //        item.ProcessTypeText = item.ProcessType.GetDescription();
        //        item.InternalEmploymentTypeText = item.InternalEmploymentType.GetDescription();
        //    }
        //    catch
        //    {

        //    }

        //    return item;
        //}

    }
    public class Root
    {
        public D d { get; set; }
    }

    public class D
    {
        public List<Result> results { get; set; }
    }

    public class Result
    {
        // Result nesnesindeki olası alanlar burada yer alır.
        public string jobReqId { get; set; }
    }
}
