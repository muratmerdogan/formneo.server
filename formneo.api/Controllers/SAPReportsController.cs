using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using vesa.core.DTOs.SapDtos;
using vesa.core.DTOs.SAPReportsFormDatas;
using vesa.core.Models;

namespace vesa.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SAPReportsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private string _endPoint; 
        private readonly IHttpClientFactory _httpClientFactory;
        //private string customer = _endPoint + "sap/bc/zhrvs_get_cust?sap-client=100";
        private string customer;
        private string department;
        private string project;
        private string last12MonthInvoiceCustomer;
        private string last12MonthInvoiceProject;
        private string PositionNumber;
        private string teamActivity;
        private string teamLastSixInvoice;
        private string topManagementActivityReport;
        private string employeeList;
        private string _userName;
        private string passWord;


        public SAPReportsController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;


            _endPoint = _configuration["SapConnectionInfo:apiendpoint"];
            passWord = _configuration["SapConnectionInfo:PassWord"];
            _userName = _configuration["SapConnectionInfo:userName"];


            customer = $"{_endPoint}zhrvs_get_cust?sap-client=100";
            department = $"{_endPoint}zhrvs_get_dep?sap-client=100";
            project = $"{_endPoint}zhrvs_get_prj?sap-client=100";
            last12MonthInvoiceCustomer = $"{_endPoint}zhrvs_linechart?sap-client=100";
            last12MonthInvoiceProject = $"{_endPoint}zhrvs_line_prj?sap-client=100";
            PositionNumber = $"{_endPoint}zhrvs_pers_poz?sap-client=100";
            teamActivity = $"{_endPoint}zhrvs_team_act?sap-client=100";
            teamLastSixInvoice = $"{_endPoint}zhrvs_team_lc?sap-client=100";
            topManagementActivityReport = $"{_endPoint}zhrvs_get_act?sap-client=100";
            employeeList = $"{_endPoint}zrest_core?sap-client=100";

        }
        private HttpClient CreateHttpClient(string userName,string password)
        {
            var client = _httpClientFactory.CreateClient();
            var basicAuth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{userName}:{password}"));
            client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", basicAuth);
            return client;
        }

        [HttpGet("GetCustomerList")]
        public async Task<List<CustomerListDto>> GetCustomerList()
        {
            var client = _httpClientFactory.CreateClient();


            var byteArray = Encoding.ASCII.GetBytes("");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));


            var responseMessage = await client.GetAsync(customer);
            var jsonData=await responseMessage.Content.ReadAsStringAsync();
            var customerList = JsonConvert.DeserializeObject<List<CustomerListDto>>(jsonData);
            return customerList;
        }


        [HttpGet("GetEmployeeList")]
        public async Task<List<EmployeeDto>> GetEmployeeList()
        {
            var client = _httpClientFactory.CreateClient();


            var byteArray = Encoding.ASCII.GetBytes("");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));


            var responseMessage = await client.GetAsync(employeeList);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var employeList = JsonConvert.DeserializeObject<List<EmployeeDto>>(jsonData);
            return employeList;
        }
        [HttpGet("GetDepartmentList")]
        public async Task<IActionResult> GetDepartmentList()
        {
            var client = _httpClientFactory.CreateClient();
            var byteArray = Encoding.ASCII.GetBytes("");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var responseMessage = await client.GetAsync(department);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var departmentList = JsonConvert.DeserializeObject<List<DepartmentListDto>>(jsonData);
            return Ok(departmentList);
        }
        [HttpGet("GetProjectList")]
        public async Task<List<ProjectListDto>> GetProjectList(int custId)
        {
            var client = _httpClientFactory.CreateClient();

            var byteArray = Encoding.ASCII.GetBytes("sifre");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));


            var responseMessage = await client.GetAsync(project);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var projectList = JsonConvert.DeserializeObject<List<ProjectListDto>>(jsonData);

            if (custId != 0)
                projectList = projectList.Where(e => e.CUSID == custId).ToList();

            return projectList;
        }

        [HttpPost("CustomerLast12MonthInvoiceList")]
        public async Task<List<ProjectLastYearInvoiceList>> CustomerLast12MonthInvoiceList(string cusId)
        {
            var client = _httpClientFactory.CreateClient();



            var byteArray = Encoding.ASCII.GetBytes("");

            if (cusId == "-99")
                cusId = "";

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var requestData = new { pernr = "", cusid = cusId };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync(last12MonthInvoiceCustomer, jsonContent);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var teamActivityList = JsonConvert.DeserializeObject<IEnumerable<ProjectLastYearInvoiceList>>(jsonData);
            return teamActivityList.ToList();
        }

        [HttpPost("EmoloyeeLast12MonthInvoiceList")]
        public async Task<List<ProjectLastYearInvoiceList>> EmoloyeeLast12MonthInvoiceList(string? cusId, string employeId)
        {
            var client = _httpClientFactory.CreateClient();

            if (cusId == null)
                cusId = "";

            if (cusId == "-99")
                cusId = "";


            var byteArray = Encoding.ASCII.GetBytes("");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var requestData = new { pernr = employeId, cusid = cusId };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync(last12MonthInvoiceCustomer, jsonContent);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var teamActivityList = JsonConvert.DeserializeObject<IEnumerable<ProjectLastYearInvoiceList>>(jsonData);
            return teamActivityList.ToList();
        }

        [HttpPost("ProjectLast12MonthInvoice")]
        public async Task<List<ProjectLastYearInvoiceList>> ProjectLast12MonthInvoiceList(string? projectId,string? custId)
        {
            var client = _httpClientFactory.CreateClient();
            var byteArray = Encoding.ASCII.GetBytes("");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var requestData = new { vprid = projectId, cusid = custId };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync(last12MonthInvoiceProject, jsonContent);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var teamActivityList = JsonConvert.DeserializeObject<IEnumerable<ProjectLastYearInvoiceList>>(jsonData);
            return teamActivityList!.ToList();
        }

        [HttpPost("TeamActivity/{orgeh}")]
        public async Task<IActionResult> TeamActivityList(string orgeh)
        {

            var client = _httpClientFactory.CreateClient();
            var byteArray = Encoding.ASCII.GetBytes("");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var requestData = new { orgeh = orgeh };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync(teamActivity, jsonContent);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var teamActivityList = JsonConvert.DeserializeObject<IEnumerable<IEnumerable<TeamActivityListDto>>>(jsonData);
            return Ok(teamActivityList);
        }
        [HttpPost("TeamLastSixInvoice/{orgeh}")]
        public async Task<IActionResult> TeamLastSixInvoiceList(string orgeh)
        {
            var client = _httpClientFactory.CreateClient();
            var byteArray = Encoding.ASCII.GetBytes("");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var requestData = new { orgeh = orgeh };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync(teamLastSixInvoice, jsonContent);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var teamLastSixtInvoiceList = JsonConvert.DeserializeObject<IEnumerable<TeamLastSixtMonthInvoiceListDto>>(jsonData);
            return Ok(teamLastSixtInvoiceList);
        }
        [HttpPost("TopManagementActivityReport")]
        public async Task<IActionResult> TopManagementActivityReport([FromForm] TopManagementActivityFormData data)
        {

            var client = _httpClientFactory.CreateClient();
            var byteArray = Encoding.ASCII.GetBytes("");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var requestData = new { pernr = data.Pernr, begda= data.Begda, endda= data.Endda, cusid= data.Cusid };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync(topManagementActivityReport, jsonContent);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var topManagementActivityReportList = JsonConvert.DeserializeObject<IEnumerable<TopManagementActivityRaportListDto>>(jsonData);
            return Ok(topManagementActivityReportList);
        }

        [HttpGet("GetEmployeePictureAsBase64")]
        public async Task<string> GetEmployeePictureAsBase64(string email)

        {

            var emp = await GetEmployeeList();
            var pers = emp.Where(e => e.EMAIL == email.ToUpper()).FirstOrDefault();
            string baseUrl = "https://fiori.vesa-tech.com//sap/opu/odata/sap/HCMFAB_COMMON_SRV/EmployeePictureSet";
            string applicationId = "REST_SERVICE";
            string url = $"{baseUrl}(ApplicationId='{applicationId}',EmployeeId='{pers.PERNR}')/$value";

            using (var client = new HttpClient())
            {
                try
                {

                    string username = "";
                    string password = "";
                    // Basic Auth ayarları
                    var authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

                    // API çağrısı yap
                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    // Görüntü verisini indir
                    byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();

                    // Base64'e çevir
                    return Convert.ToBase64String(imageBytes);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hata: {ex.Message}");
                    return null;
                }
            }
        }

        [HttpGet("GetEmployeePictureAsBase64ByUser")]

        public async Task<string> GetEmployeePictureAsBase64ByUser(string userId)

        {

     

            string baseUrl = "https://fiori.vesa-tech.com//sap/opu/odata/sap/HCMFAB_COMMON_SRV/EmployeePictureSet";
            string applicationId = "REST_SERVICE";
            string url = $"{baseUrl}(ApplicationId='{applicationId}',EmployeeId='"+ userId + "')/$value";

            using (var client = new HttpClient())
            {
                try
                {

                    string username = "";
                    string password = "";
                    // Basic Auth ayarları
                    var authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

                    // API çağrısı yap
                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    // Görüntü verisini indir
                    byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();

                    // Base64'e çevir
                    return Convert.ToBase64String(imageBytes);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hata: {ex.Message}");
                    return null;
                }
            }
        }
        [HttpGet("getSapInfo")]
        public async Task<EmployeeDto> getSapInfo(string email)
        {


            var value = _configuration["apiendpoint"];
            var PassWord = _configuration["PassWord"];
            var userName = _configuration["userName"];


            var emp = await GetEmployeeList();
            var pers = emp.Where(e => e.EMAIL == email.ToUpper()).FirstOrDefault();



            if (pers != null)
            {
                var photo = await GetEmployeePictureAsBase64ByUser(pers.PERNR.ToString());
                pers.Photo = photo;
            }

            return pers;

        }

    }
}
