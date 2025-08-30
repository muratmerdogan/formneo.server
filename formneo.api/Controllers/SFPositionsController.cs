using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLayer.Core.Services;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using vesa.api.Controllers.utils;
using vesa.core.DTOs.Budget.JobCodeRequest;
using vesa.core.DTOs.Budget.NormCodeRequest;
using vesa.core.DTOs.Budget.SF;
using vesa.core.DTOs.Budget.UpsertDto;
using vesa.core.DTOs.Clients;
using vesa.core.Models;
using vesa.core.Models.BudgetManagement;

namespace vesa.api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SFPositionsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IServiceWithDto<BudgetNormCodeRequest, BudgetNormCodeRequestListDto> _service;


        //public SFPositionsController()
        //{


        //}

        public SFPositionsController(IMapper mapper, IServiceWithDto<BudgetNormCodeRequest, BudgetNormCodeRequestListDto> service)
        {
            _mapper = mapper;

            _service = service;

        }

        [HttpGet]
        public async Task<PositionSFDto> All(int skip = 0, int top = 50, string firstName = "")
        {

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(
                System.Text.ASCIIEncoding.ASCII.GetBytes(
                $"{Config.Config.UserName}:{Config.Config.Password}")));


                string filter = "";
                if (!string.IsNullOrEmpty(firstName))
                {
                    filter = $"&$filter=substringof('{firstName}',tolower(externalName_defaultValue))";
                }


                using (var response = await httpClient.GetAsync($"{Config.Config.SfAddress}/Position?$select=code,externalName_defaultValue,positionTitle&$format=json&$inlinecount=allpages&$skip={skip}&$top={top}" + filter))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var responseBody = await response.Content.ReadAsStringAsync();

                    // JSON verisini JObject'e dönüştür
                    var json = JObject.Parse(responseBody);

                    // "d" kısmını al ve sonuçları liste olarak çıkar
                    var results = json["d"]["results"].ToObject<List<SFPositionList>>();

                    PositionSFDto dto = new PositionSFDto();
                    dto.SFPositionList = results.OrderBy(e => e.name).ToList();
                    dto.Count = (int)json["d"]["__count"]; ;
                    return dto;

                }
            }

        }
        [HttpPost]
        public async Task<bool> Save(Guid? workFlowId)
        {

            try
            {
                var result = await _service.GetAllAsync();

                var data = result.Data.Where(e => e.WorkflowHeadId == workFlowId.ToString()).FirstOrDefault();


                var parentPosition = await GetByIdPositon(data.parentPosition);


                string format = "MM/dd/yyyy HH:mm:ss";
                string input = parentPosition[0].effectiveStartDate;
                DateTime dateTime = DateTime.ParseExact(input, format, CultureInfo.InvariantCulture);
                string output = dateTime.ToString("yyyy-MM-ddTHH:mm:ss");
                string parentPositionEffectiveDate = dateTime.ToString("yyyy-MM-ddTHH:mm:ss");



                var position = new
                {
                    __metadata = new { uri = "Position" },
                    code = "55555555",
                    effectiveStartDate = Utils.ConvertToSapFormattedDate(data.effectiveStartDate),
                    cust_IseBaslamaTarihi = Utils.ConvertToSapFormattedDate(data.cust_IseBaslamaTarihi),
                    cust_PlanlananIseGiris = Utils.ConvertToSapFormattedDate(data.cust_PlanlananIseGiris),
                    cust_plannedEndDate = Utils.ConvertToSapFormattedDate(data.cust_plannedEndDate),
                    cust_actualhiredate = Utils.ConvertToSapFormattedDate(data.cust_actualhiredate),
                    effectiveStatus = "A",
                    vacant = data.vacant,
                    changeReason = data.changeReason,
                    cust_GeoZone = data.cust_GeoZone,
                    company = data.cust_company,
                    externalName_tr_TR = data.externalName_tr_TR,
                    externalName_defaultValue = data.externalName_defaultValue,
                    externalName_ru_RU = data.externalName_ru_RU,
                    externalName_en_US = data.externalName_en_US,
                    externalName_en_DEBUG = data.externalName_en_DEBUG,
                    multipleIncumbentsAllowed = data.multipleIncumbentsAllowed,
                    targetFTE = data.targetFTE,
                    standardHours = data.standardHours,
                    jobCode = data.jobCode,
                    cust_jobfunction = data.cust_jobfunction,
                    cust_ronesansjoblevel = data.cust_ronesansjoblevel,
                    cust_ronesansKademe = data.cust_ronesansKademe,
                    payGrade = data.payGrade,
                    jobTitle = data.jobTitle,
                    employeeClass = data.employeeClass,
                    cust_empSubGroup = data.cust_empSubGroup,
                    cust_EmpGroup = data.cust_EmpGroup,
                    cust_companyGroup = data.cust_companyGroup,
                    cust_customlegalEntity = data.cust_customlegalEntity,
                    businessUnit = data.businessUnit,
                    division = data.division,
                    cust_sub_division = data.cust_sub_division,
                    department = data.department,
                    cust_parentDepartment2 = data.cust_parentDepartment2,
                    cust_parentDepartment = data.cust_parentDepartment,
                    costCenter = data.costCenter,
                    cust_locationGroup = data.cust_locationGroup,
                    location = data.location,
                    cust_calismaYeriTuru = data.cust_calismaYeriTuru,
                    comment = data.comment,
                    cust_payGroup = data.cust_payGroup,
                    cust_isAlani = data.cust_isAlani,
                    cust_phisicalLocation = data.cust_phisicalLocation,
                    cust_ticket = data.cust_ticket,
                    cust_HayKademe = data.cust_HayKademe,
                    cust_ChiefPosition = false,
                    parentPosition = new { __metadata = new { type = "SFOData.Position", uri = "Position(code='" + data.parentPosition + "',effectiveStartDate=datetime'" + parentPositionEffectiveDate + "')" } }
                };


                string json = JsonConvert.SerializeObject(position);

                using (var client = new HttpClient())

                {

                    client.BaseAddress = new Uri($"{Config.Config.SfAddress}/");

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Basic Authentication
                    var authToken = Convert.ToBase64String(Encoding.ASCII.GetBytes(
                $"{Config.Config.UserName}:{Config.Config.Password}"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);


                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync("upsert", content);
                    string res = await response.Content.ReadAsStringAsync();
                    List<string> tolist = new List<string>();
                    tolist = utils.Utils.GetMails();
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
                        {

                 
                            Console.WriteLine("Data posted successfully.");
                            tolist = utils.Utils.GetMails();
                            string htmlBody = $@"
                    <!DOCTYPE html>
                    <html>
                    <head>
                    <meta charset=""UTF-8"">
                    <title>Yeni  Norm Kadro  Oluşturuldu</title>
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
                    <h1>Yeni Norm Kodro Oluşturuldu</h1>
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



                            utils.Utils.SendMail("Yeni Norm Kadro Oluşturuldu Bilgilendirme", htmlBody, tolist);

                            Console.WriteLine("Data posted successfully.");
                            return true;

                        }
                        else
                        {
                            Console.WriteLine($"Error: {response.StatusCode}");
                            string responseContent = await response.Content.ReadAsStringAsync();
                            utils.Utils.SendMail("Norm kadro oluşturma methodunda hata", res, tolist);
                            return false;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode}");
                        string responseContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(responseContent);
                        return false;
                    }
                }

            }
            catch (Exception ex)
            {
                var tolist = utils.Utils.GetMails();
                utils.Utils.SendMail("Norm kadro oluşturma methodunda hata", ex.Message, tolist);
                return false;

            }

        }


        [HttpGet("{code}")]
        public async Task<List<SFPositionAllPropertyDto>> GetByIdPositon(string code)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(
                System.Text.ASCIIEncoding.ASCII.GetBytes(
                $"{Config.Config.UserName}:{Config.Config.Password}")));


                using (var response = await httpClient.GetAsync($"{Config.Config.SfAddress}/Position?$filter=code eq '" + code + "'&$select=code,effectiveStartDate&$format=json"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var responseBody = await response.Content.ReadAsStringAsync();

                    // JSON verisini JObject'e dönüştür
                    var json = JObject.Parse(responseBody);


                    // "d" kısmını al ve sonuçları liste olarak çıkar
                    var results = json["d"]["results"].ToObject<List<SFPositionAllPropertyDto>>();

                    return results;

                }
            }
            return new List<SFPositionAllPropertyDto>();
        }


        [HttpGet("emp/{code}")]
        public async Task<List<SFEmpJobDto>> GetByIdEmpJob(string code)
        {


            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(
                System.Text.ASCIIEncoding.ASCII.GetBytes(
                $"{Config.Config.UserName}:{Config.Config.Password}")));


                using (var response = await httpClient.GetAsync($"{Config.Config.SfAddress}/Position?$filter=position eq '" + code + "'&$select=userId&$format=json"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var responseBody = await response.Content.ReadAsStringAsync();

                    // JSON verisini JObject'e dönüştür
                    var json = JObject.Parse(responseBody);


                    // "d" kısmını al ve sonuçları liste olarak çıkar
                    var results = json["d"]["results"].ToObject<List<SFEmpJobDto>>();

                    return results;

                }
            }
            return new List<SFEmpJobDto>();
        }

        [HttpGet("byUserId/{UserId}")]
        public async Task<PositionSFDto> GetPositionListByUserId(string UserId)
        {
            //todo
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(
                System.Text.ASCIIEncoding.ASCII.GetBytes(
                $"{Config.Config.UserName}:{Config.Config.Password}")));


                var positionCode = await getPositionCodeByUserId(UserId);
                using (var response = await httpClient.GetAsync($"{Config.Config.SfAddress}/Position?$format=json&$filter=effectiveStatus eq 'A' and parentPosition/code eq '" + positionCode + "'&$select=code,externalName_defaultValue,vacant,cust_plannedEndDate,cust_IseBaslamaTarihi,cust_customlegalEntity,businessUnit,cust_sub_division,department,"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var responseBody = await response.Content.ReadAsStringAsync();

                    // JSON verisini JObject'e dönüştür
                    var json = JObject.Parse(responseBody);

                    // "d" kısmını al ve sonuçları liste olarak çıkar
                    var results = json["d"]["results"].ToObject<List<SFPositionList>>();

                    PositionSFDto dto = new PositionSFDto();
                    dto.SFPositionList = results.OrderBy(e => e.name).ToList();
                    foreach (var pos in dto.SFPositionList)
                    {
                        (string userName, string userId) = await (findPersonAsync(pos.externalCode));
                        pos.userName = userName;
                        pos.userId = userId;
                    }

                    //dto.Count = (int)json["d"]["__count"]; ;
                    return dto;

                }

            }

        }

        [HttpPost("byUserIdAll/{UserId}")]
        public async Task<PositionSFDto> GetPositionListByUserIdAll(string UserId, [FromBody] PositionSFDto? param = null)
        {
            //todo
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(
                System.Text.ASCIIEncoding.ASCII.GetBytes(
                $"{Config.Config.UserName}:{Config.Config.Password}")));


                var positionCode = await getPositionCodeByUserId(UserId);
                using (var response = await httpClient.GetAsync($"{Config.Config.SfAddress}/Position?$format=json&$filter=effectiveStatus eq 'A' and parentPosition/code eq '" + positionCode + "'&$select=code,externalName_defaultValue,vacant"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var responseBody = await response.Content.ReadAsStringAsync();

                    // JSON verisini JObject'e dönüştür
                    var json = JObject.Parse(responseBody);

                    // "d" kısmını al ve sonuçları liste olarak çıkar
                    var results = json["d"]["results"].ToObject<List<SFPositionList>>();


                    if (param == null)
                        param = new PositionSFDto();

                    param.SFPositionList.AddRange(results.OrderBy(e => e.name).ToList());
                    foreach (var pos in (results.OrderBy(e => e.name).ToList()))
                    {
                        var dto = await (findPersonAsyncReturnDto(pos.externalCode));
                        pos.userName = dto.UserName;
                        pos.userId = dto.UserId;
                        GetPositionListByUserIdAll(dto.UserId, param);
                    }

                    //dto.Count = (int)json["d"]["__count"]; ;
                    return param;

                }

            }

        }
        private async Task<(string userId, string username)> findPersonAsync(string positionCode)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Basic", Convert.ToBase64String(
            System.Text.ASCIIEncoding.ASCII.GetBytes(
            $"{Config.Config.UserName}:{Config.Config.Password}")));

                using (var response = await httpClient.GetAsync($"{Config.Config.SfAddress}/EmpJob?$format=json&$filter=position eq '" + positionCode + "'&$select=userId"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var responseBody = await response.Content.ReadAsStringAsync();

                    // JSON verisini JObject'e dönüştür
                    var json = JObject.Parse(responseBody);

                    // "d" kısmını al ve sonuçları liste olarak çıkar
                    var results = json["d"]["results"].ToObject<List<SFEmpJob>>();
                    if (results != null && results.Count > 0)
                    {
                        string userId = results[0].userId;

                        using (var responseUser = await httpClient.GetAsync($"{Config.Config.SfAddress}/User?$format=json&$filter=userId eq '" + userId + "'&$select=userId,displayName"))
                        {
                            var responseBodyUser = await responseUser.Content.ReadAsStringAsync();

                            // JSON verisini JObject'e dönüştür
                            var jsonUser = JObject.Parse(responseBodyUser);

                            // "d" kısmını al ve sonuçları liste olarak çıkar
                            var resultsUser = jsonUser["d"]["results"].ToObject<List<SFUsers>>(); ;
                            if (resultsUser != null && resultsUser.Count > 0)
                            {
                                string username = (string)resultsUser[0].displayName;
                                return (userId, username);
                            }
                            else
                            {
                                return ("", "");
                            }

                        }
                    }
                    else
                    {
                        return ("", "");
                    }

                }
            }

            return ("", "");
        }

        private async Task<ResultUser> findPersonAsyncReturnDto(string positionCode)
        {

            ResultUser result = new ResultUser();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Basic", Convert.ToBase64String(
            System.Text.ASCIIEncoding.ASCII.GetBytes(
            $"{Config.Config.UserName}:{Config.Config.Password}")));

                using (var response = await httpClient.GetAsync($"{Config.Config.SfAddress}/EmpJob?$format=json&$filter=position eq '" + positionCode + "'&$select=userId"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var responseBody = await response.Content.ReadAsStringAsync();

                    // JSON verisini JObject'e dönüştür
                    var json = JObject.Parse(responseBody);

                    // "d" kısmını al ve sonuçları liste olarak çıkar
                    var results = json["d"]["results"].ToObject<List<SFEmpJob>>();
                    if (results != null && results.Count > 0)
                    {
                        string userId = results[0].userId;

                        using (var responseUser = await httpClient.GetAsync($"{Config.Config.SfAddress}/User?$format=json&$filter=userId eq '" + userId + "'&$select=userId,displayName"))
                        {
                            var responseBodyUser = await responseUser.Content.ReadAsStringAsync();

                            // JSON verisini JObject'e dönüştür
                            var jsonUser = JObject.Parse(responseBodyUser);

                            // "d" kısmını al ve sonuçları liste olarak çıkar
                            var resultsUser = jsonUser["d"]["results"].ToObject<List<SFUsers>>(); ;
                            if (resultsUser != null && resultsUser.Count > 0)
                            {
                                result.UserId = (string)resultsUser[0].userId;
                                result.UserName = (string)resultsUser[0].displayName;
                                return result;
                            }
                            else
                            {
                                return result;
                            }

                        }
                    }
                    else
                    {
                        return result;
                    }

                }
            }

            return result;
        }


        [HttpGet("byPositionCode/{UserId}")]
        public async Task<string> getPositionCodeByUserId(string UserId)
        {


            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(
                System.Text.ASCIIEncoding.ASCII.GetBytes(
                $"{Config.Config.UserName}:{Config.Config.Password}")));



                using (var response = await httpClient.GetAsync($"{Config.Config.SfAddress}/EmpJob?$format=json&$filter=userId eq '" + UserId + "'&$select=position"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var responseBody = await response.Content.ReadAsStringAsync();

                    // JSON verisini JObject'e dönüştür
                    var json = JObject.Parse(responseBody);

                    // "d" kısmını al ve sonuçları liste olarak çıkar


                    string position = json["d"]["results"][0]["position"].ToString();

                    //dto.Count = (int)json["d"]["__count"]; ;
                    return position;

                }
            }

            return "";

        }

        [HttpGet("detail/{code}")]
        public async Task<List<SFPositionDto>> getDetailPosition(string code)
        {


            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(
                System.Text.ASCIIEncoding.ASCII.GetBytes($"{Config.Config.UserName}:{Config.Config.Password}")));

                string parentCode = "";
                string parentName = "";
                using (var response = await httpClient.GetAsync($"{Config.Config.SfAddress}/Position?$filter=code eq '" + code + "'&$format=json"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var responseBody = await response.Content.ReadAsStringAsync();

                    // JSON verisini JObject'e dönüştür
                    var json = JObject.Parse(responseBody);



                    string deferredUri = json["d"]["results"][0]["parentPosition"]["__deferred"]["uri"].ToString();
                    using (var responseParent = await httpClient.GetAsync(deferredUri + "?$format=json"))
                    {
                        string res = await responseParent.Content.ReadAsStringAsync();
                        var respBody = await responseParent.Content.ReadAsStringAsync();
                        var jsonParent = JObject.Parse(respBody);
                        parentCode = jsonParent["d"]["code"].ToString();
                        parentName = jsonParent["d"]["externalName_defaultValue"].ToString();
                    }



                    // "d" kısmını al ve sonuçları liste olarak çıkar
                    var results = json["d"]["results"].ToObject<List<SFPositionDto>>();
                    results[0].parentPositionValue = parentCode;
                    results[0].parentPositionTxt = parentName;
                    return results;

                }
            }
            return new List<SFPositionDto>();
        }

        [HttpGet("detailByPositionCode/{code}")]
        public async Task<List<FOJobCodeList>> getDetailPositionByPositionCode(string code)
        {


            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(
                System.Text.ASCIIEncoding.ASCII.GetBytes($"{Config.Config.UserName}:{Config.Config.Password}")));

                using (var response = await httpClient.GetAsync($"{Config.Config.SfAddress}/FOJobCode?$filter=externalCode eq '" + code + "'&$format=json"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var responseBody = await response.Content.ReadAsStringAsync();

                    // JSON verisini JObject'e dönüştür
                    var json = JObject.Parse(responseBody);

                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    };

                    var result = JsonConvert.DeserializeObject<List<FOJobCodeList>>(json["d"]["results"].ToString(), settings);
                    return result;

                }
            }
            return new List<FOJobCodeList>();
        }


        [HttpGet("detailByUserId/{userId}")]
        public async Task<List<SFPositionDto>> getDetailPositionByUserId(string userId)
        {


            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(
                System.Text.ASCIIEncoding.ASCII.GetBytes(
                $"{Config.Config.UserName}:{Config.Config.Password}")));

                var positionCode = await getPositionCodeByUserId(userId);

                using (var response = await httpClient.GetAsync($"{Config.Config.SfAddress}/Position?$filter=code eq '" + positionCode + "'&$format=json"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var responseBody = await response.Content.ReadAsStringAsync();

                    // JSON verisini JObject'e dönüştür
                    var json = JObject.Parse(responseBody);

                    string parentCode = "";
                    string parentName = "";
                    string deferredUri = json["d"]["results"][0]["parentPosition"]["__deferred"]["uri"].ToString();
                    using (var responseParent = await httpClient.GetAsync(deferredUri + "?$format=json"))
                    {
                        string res = await responseParent.Content.ReadAsStringAsync();
                        var respBody = await responseParent.Content.ReadAsStringAsync();
                        var jsonParent = JObject.Parse(respBody);
                        parentCode = jsonParent["d"]["code"].ToString();
                        parentName = jsonParent["d"]["externalName_defaultValue"].ToString();
                    }

                    var result = new List<SFPositionDto>();
                    try
                    {
                        var settings = new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore
                        };

                        result = JsonConvert.DeserializeObject<List<SFPositionDto>>(json["d"]["results"].ToString(), settings);
                        result[0].parentPositionValue = parentCode;
                        result[0].parentPositionTxt = parentName;

                    }
                    catch (Exception ex)
                    {

                        return result;
                    }
                    return result;

                }
            }
            return new List<SFPositionDto>();
        }

        [HttpGet("getMngByCodeFromEmpJob/{positioncode}")]
        public async Task<string> getMngByCodeFromEmpJob(string positioncode)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(
                System.Text.ASCIIEncoding.ASCII.GetBytes(
                $"{Config.Config.UserName}:{Config.Config.Password}")));

                using (var response = await httpClient.GetAsync($"{Config.Config.SfAddress}/EmpJob?$filter=position eq '" + positioncode + "'&$format=json"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var responseBody = await response.Content.ReadAsStringAsync();

                    // JSON verisini JObject'e dönüştür
                    var json = JObject.Parse(responseBody);

                    if (json != null)
                    {
                        var results = json["d"]["results"].ToObject<List<SFEmpJob>>();
                        return results[0].managerId ?? "";
                    }
                    else
                    {
                        return "";
                    }


                }
            }
        }

        [HttpGet("getMngFromEmpJob/{userId}")]
        public async Task<string> getMngFromEmpJob(string userId)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(
                System.Text.ASCIIEncoding.ASCII.GetBytes(
                $"{Config.Config.UserName}:{Config.Config.Password}")));

                using (var response = await httpClient.GetAsync($"{Config.Config.SfAddress}/EmpJob?$filter=userId eq '" + userId + "'&$format=json"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var responseBody = await response.Content.ReadAsStringAsync();

                    // JSON verisini JObject'e dönüştür
                    var json = JObject.Parse(responseBody);

                    if (json != null)
                    {
                        var results = json["d"]["results"].ToObject<List<SFEmpJob>>();
                        return results[0].managerId ?? "";
                    }
                    else
                    {
                        return "";
                    }


                }
            }
        }



        [HttpGet("getMngDisplayNameFromEmpUser/{userId}")]
        public async Task<string> getMngDisplayNameFromEmpUser(string userId)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(
                System.Text.ASCIIEncoding.ASCII.GetBytes(
                $"{Config.Config.UserName}:{Config.Config.Password}")));

                using (var response = await httpClient.GetAsync($"{Config.Config.SfAddress}/User?$format=json&$filter=userId eq '" + userId + "'&$select=displayName"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var responseBody = await response.Content.ReadAsStringAsync();

                    // JSON verisini JObject'e dönüştür
                    var json = JObject.Parse(responseBody);

                    if (json != null)
                    {
                        var results = json["d"]["results"].ToObject<List<SFEmpJob>>();
                        return results[0].displayName ?? "";
                    }
                    else
                    {
                        return "";
                    }


                }
            }
        }



    }

    public class ResultUser
    {

        public string UserId { get; set; }

        public string UserName { get; set; }

    }
}
