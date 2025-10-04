using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NLayer.Core.Services;
using formneo.core.DTOs;
using formneo.core.Models;
using formneo.core.Services;

namespace formneo.api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FormController : Controller
    {



        private readonly IMapper _mapper;
        private readonly IFormService _service;
        private readonly IServiceWithDto<FormRuleEngine, FormRuleEngineDto> _formRuleEngineService;
        private readonly IServiceWithDto<FormRuntime, FormRuntimeDto> _runtimeService;
        public FormController(IMapper mapper, IFormService formService, IServiceWithDto<FormRuleEngine, FormRuleEngineDto> formRuleEngineService, IServiceWithDto<FormRuntime, FormRuntimeDto> runtimeService)
        {
            _mapper = mapper;
            _service = formService;
            _formRuleEngineService = formRuleEngineService;
            _runtimeService = runtimeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormsAsync(string type, int limit = 100, int skip = 0)
        {
            var data = await _service.GetAllAsync();

            List<FormResource> formList = new List<FormResource>();

            foreach (var item in data)
            {

                formList.Add(new FormResource { _id = item.Id.ToString(), title = item.FormName, count = 100 });

            }
            // Veritabanından form verilerini çekme ve filtreleme işlemleri
            // Örnek bir liste döndürülecek
      

            return Ok(formList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFormAsync(Guid id, [FromQuery] int limit = 10, [FromQuery] int skip = 0)
        {
            // Burada veritabanınızdan form bilgilerini çekmek için işlemler yapılacak.
            //// Örneğin:
            //var form = GetFormFromDatabase(id, limit, skip);
            //if (form == null)
            //{
            //    return NotFound();
            //}

            var result = await _service.GetByIdStringGuidAsync(id);
                return Ok(result.FormDesign);
        }

        // GET api/form/{formId}/submission
        [HttpGet("{formId}/submission")]
        public async Task<IActionResult> GetSubmissionAsync(Guid formId, [FromQuery] int limit = 100, [FromQuery] int skip = 0)
        {
            try
            {
                // Burada formId kullanılarak veritabanından veri çekme işlemini simüle ediyorum.
                // Gerçek bir uygulamada burada veritabanı sorgusu veya iş mantığı kodu olacaktır.



                //var forms = await _runtimeService.Where(e => e.FormId == formId);




                //var dto = _mapper.Map<List<FormRuntimeSubmission>>(forms.Data);


                //foreach (var item in dto)
                //{
                //    item._id = formId.ToString();
                //    item.ValuesJsonData = item.ValuesJsonData.ToString();
                //}

                //// Veriler başarıyla alındıysa, HTTP 200 (OK) ile verileri dön
                //return Ok(dto);

                var sampleData = new[]
                {
                new
                {
                _id = "57d0a2fd76f943a4007e1529",
                modified = "2016-09-07T23:30:05.758Z",
                data = new
                {
                    firstName = "Joe",
                    lastName = "Smith",
                    email = "joe@example.com",
                    phoneNumber = "123123123"
                },
                form = "57d0a2f876f943a4007e1527",
                created = "2016-09-07T23:30:05.757Z",
                externalIds = new string[] { },
                access = new string[] { },
                roles = new string[] { },
                owner = "57d010fe76f943a4007e11da"
                },
                new
                {
                _id = "57d0a2fd76f943a4007e1529",
                modified = "2016-09-07T23:30:05.758Z",
                data = new
                {
                    firstName = "Joe",
                    lastName = "Smith",
                    email = "joe@example.com",
                    phoneNumber = "123123123"
                },
                form = "57d0a2f876f943a4007e1527",
                created = "2016-09-07T23:30:05.757Z",
                externalIds = new string[] { },
                access = new string[] { },
                roles = new string[] { },
                owner = "57d010fe76f943a4007e11da"
                }
                };

                return Ok(sampleData);
            }
            catch (Exception ex)
            {
                // Hata oluşursa, HTTP 500 (Internal Server Error) dön
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }

    public class RootObject
    {
        public Dictionary<string, object> Data { get; set; }
    }
    public class FormResource
    {
        public string _id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int count { get; set; }
        public int __v { get; set; }


    }

}
