using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLayer.Core.Services;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Xml.Linq;
using vesa.core.DTOs.Budget.JobCodeRequest;
using vesa.core.DTOs.Budget.NormCodeRequest;
using vesa.core.DTOs.Budget.SF;
using vesa.core.DTOs.Budget.UpsertDto;
using vesa.core.DTOs.Clients;
using vesa.core.Models;
using vesa.core.Models.BudgetManagement;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace vesa.api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SFFOJobCodeController
    {

        private readonly IMapper _mapper;
        private readonly IServiceWithDto<BudgetJobCodeRequest, BudgetJobCodeRequestListDto> _service;



        public SFFOJobCodeController(IMapper mapper, IServiceWithDto<BudgetJobCodeRequest, BudgetJobCodeRequestListDto> service)
        {
            _mapper = mapper;

            _service = service;


        }
        [HttpPost]
        public async Task<ActionResult<string>> Save(Guid? workFlowId)
        {

            try
            {

                var result = await _service.GetAllAsync();

                var data = result.Data.Where(e => e.WorkflowHeadId == workFlowId).FirstOrDefault();



                if (result.Data == null)
                    return "bulunamadı";

                string externalCode = data.Id.ToString();
                var jobCode = new JobCode
                {
                    Metadata = new Metadata { Uri = "FOJobCode" },
                    ExternalCode = externalCode,
                    StartDate = utils.Utils.ConvertToSapFormattedDate(data.StartDate),
                    Name = data.Name,
                    NameRuRu = data.Name_Ru_RU,
                    NameEnDebug = data.Name_En_Debug,
                    NameTrTr = data.Name_Tr_TR,
                    NameEnUs = data.Name_En_US,
                    DescriptionDefaultValue = data.Description_Tr_TR,
                    DescriptionEnDebug = data.Description_En_US,
                    DescriptionEnUs = data.Description_En_US,
                    DescriptionRuRu = data.Description_Ru_RU,
                    DescriptionTrTr = data.Description_Tr_TR,
                    Status = "A",
                    RegularTemporary = data.RegularTemporary,
                    DefaultEmployeeClass = data.DefaultEmployeeClass,
                    IsFulltimeEmployee = true,
                    Grade = data.Grade,
                    JobFunction = data.JobFunction,
                    CustPositionLevel = data.PositionLevel,
                    CustJobLevelGroup = data.Cust_Joblevelgroup,
                    CustMetin = data.Cust_Metin,
                    CustJobCode = data.Cust_Jobcode,
                    CustAdinesStatus = data.Cust_AdinesStatus,
                    CustEmploymentType = data.Cust_EmploymentType,
                    CustGorevBirimTipi = data.Cust_GorevBirimTipi,
                    CustIsManager = data.Cust_IsManager,
                    CustBolum = data.Cust_Bolum,
                    CustRonesansKademe = data.Cust_Ronesanskademe,
                    CustHayKademe = data.Cust_Haykademe
                };
                string json = JsonConvert.SerializeObject(jobCode);

                using (var client = new HttpClient())

                {

                    client.BaseAddress = new Uri($"{Config.Config.SfAddress}/");

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Basic Authentication
                    var authToken = Convert.ToBase64String(Encoding.ASCII.GetBytes(
                $"{Config.Config.UserName}:{Config.Config.Password}"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);


                    var content = new StringContent(json, Encoding.UTF8, "application/json");


                    List<string> tolist = new List<string>();
                    tolist = utils.Utils.GetMails();

                    HttpResponseMessage response = await client.PostAsync("upsert", content);

                    if (response.IsSuccessStatusCode)
                    {
                        string res = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Data posted successfully.");

                        string htmlBody = $@"
                    <!DOCTYPE html>
                    <html>
                    <head>
                    <meta charset=""UTF-8"">
                    <title>Yeni İş Kodu Oluşturuldu</title>
                    <style>
                    body {{ font-family: Arial, sans-serif; }}
                    .container {{ width: 80%; margin: auto; padding: 20px; }}
                    .header {{ background-color: #f4f4f4; padding: 10px; border-bottom: 2px solid #ddd; }}
                    .content {{ margin: 20px 0; }}
                    .footer {{ margin-top: 20px; font-size: 0.9em; color: #555; }}
                    pre {{ background-color: #f4f4f4; padding: 10px; border: 1px solid #ddd; border-radius: 4px; }}
                    </style>
                    </head>
                    <body>
                    <div class=""container"">
                    <div class=""header"">
                    <h1>Yeni İş Kodu Oluşturuldu</h1>
                    </div>
                    <div class=""content"">
                    <h2>JSON Verisi:</h2>
                    <pre>{json}</pre>
                    </div>
                    <div class=""footer"">
                    <p>{res}</p>
                    </div>
                    </div>
                    </body>
                    </html>";

                        utils.Utils.SendMail("Yeni İş Kodu Oluşturuldu Bilgilendirme", htmlBody, tolist);
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode}");
                        string responseContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(responseContent);
                    }
                }

                return externalCode.ToString();
            }
            catch (Exception ex)
            {
                var tolist = utils.Utils.GetMails();
                utils.Utils.SendMail("Yeni posizyon ismi oluşturma methodunda hata", ex.Message, tolist);

                return "";
            }
        }

        [HttpGet]
        public async Task<FOJobCodeDto> All(int skip = 0, int top = 50, string name = "")
        {


            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(
                System.Text.ASCIIEncoding.ASCII.GetBytes(
                $"{Config.Config.UserName}:{Config.Config.Password}")));


                string filter = "";
                if (!string.IsNullOrEmpty(name))
                {
                    filter = $"&$filter=substringof('{name.ToLowerInvariant()}',tolower(name))";
                }


                using (var response = await httpClient.GetAsync($"{Config.Config.SfAddress}/FOJobCode?$select=name,externalCode&$format=json&$inlinecount=allpages&$skip={skip}&$top={top}" + filter))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var responseBody = await response.Content.ReadAsStringAsync();

                    // JSON verisini JObject'e dönüştür
                    var json = JObject.Parse(responseBody);

                    // "d" kısmını al ve sonuçları liste olarak çıkar
                    var results = json["d"]["results"].ToObject<List<FOJobCodeList>>();

                    FOJobCodeDto dto = new FOJobCodeDto();
                    dto.FOJobCodeList = results;
                    dto.Count = (int)json["d"]["__count"]; ;
                    return dto;

                }
            }

        }


        [HttpGet("{code}")]
        public async Task<FOJobCodeDto> GetJobCodeByCode(string code)
        {


            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(
                System.Text.ASCIIEncoding.ASCII.GetBytes(
                $"{Config.Config.UserName}:{Config.Config.Password}")));




                using (var response = await httpClient.GetAsync($"{Config.Config.SfAddress}/FOJobCode?$select=name,cust_joblevelgroup,grade,cust_ronesanskademe,externalCode&$format=json&$filter=externalCode eq '" + code + "'"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var responseBody = await response.Content.ReadAsStringAsync();

                    // JSON verisini JObject'e dönüştür
                    var json = JObject.Parse(responseBody);

                    // "d" kısmını al ve sonuçları liste olarak çıkar
                    var results = json["d"]["results"].ToObject<List<FOJobCodeList>>();

                    FOJobCodeDto dto = new FOJobCodeDto();
                    dto.FOJobCodeList = results;

                    return dto;

                }
            }

        }


        [HttpGet("checkRecruitment/{code}")]
        public async Task<bool> checkRecruitment()
        {
            bool iseAlimVarmi = false ;
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(
                System.Text.ASCIIEncoding.ASCII.GetBytes(
                $"{Config.Config.UserName}:{Config.Config.Password}")));

                //bütün verileri cek
                using (var response = await httpClient.GetAsync($"{Config.Config.SfAddress}/JobReqGOPosition?$format=json"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var responseBody = await response.Content.ReadAsStringAsync();

                    // JSON verisini JObject'e dönüştür
                    var json = JObject.Parse(responseBody);
                    var results = json["d"]["results"].ToObject<List<FOJobReqGOPositionList>>();

                    //her bir jobreq aktifliğini kontrol et
                    foreach (var jobReq in results)
                    {
                        using (var responseAktif = await httpClient.GetAsync($"{Config.Config.SfAddress}/JobRequisition?$format=json&$filter=jobReqId  eq '" + jobReq.jobReqId + "'&$select=jobReqId,status"))
                        {
                            string apiResponseAktif = await responseAktif.Content.ReadAsStringAsync();

                            var responseBodyAktif = await responseAktif.Content.ReadAsStringAsync();

                            // JSON verisini JObject'e dönüştür
                            var jsonAktif = JObject.Parse(responseBodyAktif);
                            var resultsAktif = json["d"]["results"].ToObject<List<FOJobRequisitionList>>();

                            foreach(var data in resultsAktif)
                            {
                                if(data.status != null) { 
                                using (var res = await httpClient.GetAsync(data.status.__deferred.uri + "?$format=json&$select=id"))
                                {
                                    string apiRes = await res.Content.ReadAsStringAsync();

                                    var resBody= await res.Content.ReadAsStringAsync();

                                    // JSON verisini JObject'e dönüştür
                                    var jsonRes = JObject.Parse(resBody);
                                    var resultRes = jsonRes["d"]["results"].ToObject<List<FOJobRequisition>>();

                                    foreach(var reqId in resultRes)
                                    {
                                        if (reqId.id == "3552" || reqId.id == "3550" || reqId.id == "3554" || reqId.id == "3549")
                                        {
                                            //aktif bir personel talep formu var
                                            using (var resEnd = await httpClient.GetAsync(jobReq.value.__deferred.uri + "?$format=json"))
                                            {
                                                string apiResEnd = await res.Content.ReadAsStringAsync();

                                                var reEndsBody = await res.Content.ReadAsStringAsync();

                                                // JSON verisini JObject'e dönüştür
                                                var jsonResEnd = JObject.Parse(resBody);
                                                var jobReqCode = jsonRes["d"]?["code"]!.ToString();
                                                if (jobReqCode == jobReq.jobReqId)
                                                {
                                                    //işe alım ilanı var demek
                                                    iseAlimVarmi =  true;
                                                }
                                                else
                                                    iseAlimVarmi = false;

                                            }
                                        }
                                    }

                                }
                                }
                            }

                        }
                    }

                }

            }
            return iseAlimVarmi;
        }
    }
}
