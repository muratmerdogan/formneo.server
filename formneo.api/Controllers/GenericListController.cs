using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json.Linq;
using NLayer.Core.Services;
using System.ComponentModel;
using System.Globalization;
using System.Net.Http.Headers;
using formneo.core.DTOs.Budget.PeriodUserDto;
using formneo.core.DTOs.Budget.SF;
using formneo.core.DTOs.Clients;
using formneo.core.DTOs.GeneralFolder;
using formneo.core.GenericListDto;
using formneo.core.Models.BudgetManagement;
using formneo.core.Services;

namespace vesa.api.Controllers
{

    public enum GenericList
    {

        [Description("cust_locationGroup")]
        cust_locationGroup = 1,

        [Description("location")]
        location = 2,

        [Description("cust_jobfunction")]
        cust_jobfunction = 3,

        [Description("cust_sub_division")]
        cust_sub_division = 4,

        [Description("department")]
        department = 5,

        [Description("businessUnit")]
        businessUnit = 8,

        [Description("cust_companyGroup")]
        cust_companyGroup = 9,

        [Description("cust_customlegalEntity")]
        cust_customlegalEntity = 10,

        [Description("division")]
        division = 11,

        [Description("cust_phisicalLocation")]
        cust_phisicalLocation = 12,

        [Description("cust_GeoZone")]
        cust_GeoZone = 13,

        [Description("company")]
        company = 14,

        [Description("parentPosition")]
        parentPosition = 15,

        [Description("payGrade")]
        payGrade = 16,

        [Description("costCenter")]
        costCenter = 17,

        [Description("cust_payGroup")]
        cust_payGroup = 18,

        [Description("jobCode")]
        jobCode = 19,

        [Description("ticket_departments")]
        ticket_departments = 100,

        [Description("ticket_users")]
        ticket_users = 101

    }
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GenericListController : ControllerBase
    {

        private readonly IMapper _mapper;

        private readonly IUserService _userService;


        public GenericListController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;

        }

        [HttpGet]
        public async Task<IGenericListDto> All(GenericList pc, int skip = 0, int top = 50, string name = "", string parentId = "")
        {
          
            return null;


        }
        [HttpGet("GetFilteredDataAsync")]
        public async Task<string> GetFilteredDataAsync(GenericList pc, string value)
        {
            return "";
            
            return "";
        }
        [HttpGet("GetHardwareList")]
        public async Task<List<HardwareList>> GetHardwareList()
        {
            return null;
        }

        [HttpGet("GetLicenceList")]
        public async Task<List<LicenceList>> GetLicenceList()
        {

            return null;
        }




        [HttpGet("bygrupBaskanlik/GetCompanyByGrupBaskanlik")]
        public async Task<IGenericListDto> GetCompanyByGrupBaskanlik(int skip = 0, int top = 50, string name = "", string grupBaskanlik = "")
        {
   
            return null;

        }


        [HttpGet("byCompany/GetBolgeFonksiyonByCompany")]
        public async Task<IGenericListDto> GetBolgeFonksiyonByCompany(int skip = 0, int top = 50, string name = "", string company = "")
        {
            return null;

        }


        [HttpGet("byBolgeFonksiyon/GetBolumProjeByBolgeFonksiyon")]
        public async Task<IGenericListDto> GetBolumProjeByBolgeFonksiyon(int skip = 0, int top = 50, string name = "", string company = "")
        {


            return null;

        }

    }
}
