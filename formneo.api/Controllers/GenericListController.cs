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
using vesa.core.DTOs.Budget.PeriodUserDto;
using vesa.core.DTOs.Budget.SF;
using vesa.core.DTOs.Clients;
using vesa.core.DTOs.GeneralFolder;
using vesa.core.GenericListDto;
using vesa.core.Models.BudgetManagement;
using vesa.core.Services;

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
    public class GenericListController : Controller
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
          
            if (pc == GenericList.department)
            {
                SFFODepartmentController s = new SFFODepartmentController();
                var list = await s.All(skip, top, name);
                list.list = list.FODepartmentList.Cast<object>().ToList();
                return list;
            }
            if (pc == GenericList.businessUnit)
            {
                SFFOBusinessUnitController s = new SFFOBusinessUnitController();
                var list = await s.All(skip, top, name);
                list.list = list.FOBusinessUnitList.Cast<object>().ToList();
                return list;
            }
            if (pc == GenericList.cust_companyGroup)
            {
                SFCust_companyGroupController s = new SFCust_companyGroupController();
                var list = await s.All(skip, top, name);
                list.list = list.cust_companyGroupList.Cast<object>().ToList();
                return list;
            }
            if (pc == GenericList.cust_customlegalEntity)
            {
                SFCust_legalEntityController s = new SFCust_legalEntityController();
                var list = await s.All(skip, top, name);
                list.list = list.cust_legalEntityList.Cast<object>().ToList();
                return list;
            }
            if (pc == GenericList.division)
            {

                SFFODivisionController s = new SFFODivisionController();
                var list = await s.All(skip, top, name);
                list.list = list.FODivisionList.Cast<object>().ToList();
                return list;
            }
            if (pc == GenericList.cust_phisicalLocation)
            {
                SFFOLocationController s = new SFFOLocationController();
                var list = await s.All(skip, top, name, parentId);
                list.list = list.FOLocationList.Cast<object>().ToList();

                return list;
            }

            if (pc == GenericList.cust_GeoZone)
            {
                SFFOGeozoneController s = new SFFOGeozoneController();
                var list = await s.All(skip, top, name);
                list.list = list.FOGeozoneDtoList.Cast<object>().ToList();
                return list;
            }

            if (pc == GenericList.company)
            {
                SFFOCompanyController s = new SFFOCompanyController();
                var list = await s.All(skip, top, name);
                list.list = list.FOCompanyDtoList.Cast<object>().ToList();
                return list;
            }
            if (pc == GenericList.parentPosition)
            {
                SFPositionsController s = new SFPositionsController(null, null);
                var list = await s.All(skip, top, name);
                list.list = list.SFPositionList.Cast<object>().ToList();
                return list;
            }
            if (pc == GenericList.payGrade)
            {
                SFFOPayGradeController s = new SFFOPayGradeController();
                var list = await s.All(skip, top, name);
                list.list = list.FOPayGradeList.Cast<object>().ToList();
                return list;
            }
            if (pc == GenericList.costCenter)
            {
                SFFOCostCenterController s = new SFFOCostCenterController();
                var list = await s.All(skip, top, name);
                list.list = list.SFFOCostCenterList.Cast<object>().ToList();
                return list;
            }
            if (pc == GenericList.cust_payGroup)
            {
                SFFOFOPayGroupController s = new SFFOFOPayGroupController();
                var list = await s.All(skip, top, name);
                list.list = list.FOPayGroupSFList.Cast<object>().ToList();
                return list;
            }
            if (pc == GenericList.jobCode)
            {
                SFFOJobCodeController s = new SFFOJobCodeController(null, null);
                var list = await s.All(skip, top, name);
                list.list = list.FOJobCodeList.Cast<object>().ToList();
                return list;
            }

            if (pc == GenericList.cust_sub_division)
            {
                SFcust_sub_divisionController s = new SFcust_sub_divisionController();
                var list = await s.All(skip, top, name);
                list.list = list.cust_sub_divisioList.Cast<object>().ToList();
                return list;
            }


            if (pc == GenericList.ticket_departments)
            {
                SFcust_sub_divisionController s = new SFcust_sub_divisionController();
                var list = await s.All(skip, top, name);
                list.list = list.cust_sub_divisioList.Cast<object>().ToList();
                return list;
            }

            if (pc == GenericList.ticket_users)
            {

                var list = await _userService.GetAllUsersAsync();
                var supportUsers = _mapper.Map<List<support_UsersGenericList>>(list.Data);


                var returnList = new support_UsersGenericSelectDto();

                returnList.Count = supportUsers.Count;
                returnList.list = supportUsers.Cast<object>().ToList();

                return returnList;


            }

            return null;


        }
        [HttpGet("GetFilteredDataAsync")]
        public async Task<string> GetFilteredDataAsync(GenericList pc, string value)
        {
            return "";
            using (
                var httpClient = new HttpClient())
            {


                string entity = "", filterField = "", selectField = "";


                if (pc == GenericList.cust_locationGroup)
                {
                    entity = "FOLocationGroup";
                    selectField = "name";
                    filterField = "externalCode";
                }
                if (pc == GenericList.location)
                {
                    entity = "FOLocation";
                    selectField = "name";
                    filterField = "externalCode";

                }
                if (pc == GenericList.cust_jobfunction)
                {
                    entity = "FOJobFunction";
                    selectField = "name";
                    filterField = "externalCode";
                }
                if (pc == GenericList.cust_sub_division)
                {
                    entity = "cust_sub_division";
                    selectField = "externalName_defaultValue";
                    filterField = "externalCode";
                }
                if (pc == GenericList.department)
                {
                    entity = "FODepartment";
                    selectField = "name";
                    filterField = "externalCode";
                }
                if (pc == GenericList.department)
                {
                    entity = "FODepartment";
                    selectField = "name";
                    filterField = "externalCode";
                }
                if (pc == GenericList.department)
                {
                    entity = "FODepartment";
                    selectField = "name";
                    filterField = "externalCode";
                }
                if (pc == GenericList.businessUnit)
                {
                    entity = "FOBusinessUnit";
                    selectField = "name";
                    filterField = "externalCode";
                }
                if (pc == GenericList.cust_companyGroup)
                {
                     entity = "cust_companyGroup";
                    selectField = "externalName_defaultValue";
                    filterField = "externalCode";
                }
                if (pc == GenericList.cust_customlegalEntity)
                {
        
                    entity = "cust_legalEntity";
                    selectField = "externalName_defaultValue";
                    filterField = "externalCode";
                }
                if (pc == GenericList.division)
                {
                    entity = "FODivision";
                    selectField = "name_tr_TR";
                    filterField = "externalCode";
                }

                if (pc == GenericList.cust_phisicalLocation)
                {
                    entity = "FOLocation";
                    selectField = "name";
                    filterField = "externalCode";
                }

                if (pc == GenericList.cust_GeoZone)
                {
                    entity = "FOGeozone";
                    selectField = "name";
                    filterField = "externalCode";

                }

                if (pc == GenericList.company)
                {
                    entity = "FOCompany";
                    selectField = "name";
                    filterField = "externalCode";
                }
                if (pc == GenericList.parentPosition)
                {
                    entity = "Position";
                    selectField = "externalName_defaultValue";
                    filterField = "code";
                }
                if (pc == GenericList.payGrade)
                {
                    entity = "FOPayGrade";
                    selectField = "name";
                    filterField = "externalCode";
                }
                if (pc == GenericList.costCenter)
                {
                    entity = "FOCostCenter";
                    selectField = "name";
                    filterField = "externalCode";
                }
                if (pc == GenericList.cust_payGroup)
                {
                    entity = "FOPayGroup";
                    selectField = "name";
                    filterField = "externalCode";
                }

                if (pc == GenericList.jobCode)
                {
                    entity = "FOJobCode";
                    selectField = "name";
                    filterField = "externalCode";

                }

                  httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(
                System.Text.ASCIIEncoding.ASCII.GetBytes(
                $"{Config.Config.UserName}:{Config.Config.Password}")));


                // API adresini oluşturuyoruz
                string apiUrl = $"{Config.Config.SfAddress}/{entity}?" +
                                $"$select={selectField}&$format=json&" +
                                $"$filter={filterField} eq '{value}'";

                using (var response = await httpClient.GetAsync(apiUrl))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        // Başarılı bir şekilde cevap alındığında, içerik JSON olarak dönecek
                        var content = await response.Content.ReadAsStringAsync();

                        var json = JObject.Parse(content);
                        var nameValues = json["d"]["results"]
                        .Select(result => result[selectField].ToString())
                        .ToList();

                        return nameValues[0];
                    }
                    else
                    {
                        // Hata mesajı döndürebiliriz
                        return $"Error: {response.StatusCode}";
                    }
                }
                return "";
            }

        }




        [HttpGet("GetHardwareList")]
        public async Task<List<HardwareList>> GetHardwareList()
        {

            List<HardwareList> list = new List<HardwareList>();

            list.Add(new HardwareList { Name = "Hardware 1", Value = "1" });
            list.Add(new HardwareList { Name = "Hardware 2", Value = "2" });
            list.Add(new HardwareList { Name = "Hardware 3", Value = "3" });
            list.Add(new HardwareList { Name = "Hardware 4", Value = "4" });

            return list;
        }

        [HttpGet("GetLicenceList")]
        public async Task<List<LicenceList>> GetLicenceList()
        {

            List<LicenceList> list = new List<LicenceList>();

            list.Add(new LicenceList{ Name = "Licence 1", Value = "1" });
            list.Add(new LicenceList{ Name = "Licence 2", Value = "2" });
            list.Add(new LicenceList{ Name = "Licence 3", Value = "3" });
            list.Add(new LicenceList{ Name = "Licence 4", Value = "4" });

            return list;
        }




        [HttpGet("bygrupBaskanlik/GetCompanyByGrupBaskanlik")]
        public async Task<IGenericListDto> GetCompanyByGrupBaskanlik(int skip = 0, int top = 50, string name = "", string grupBaskanlik = "")
        {

            SFFOBusinessUnitController s = new SFFOBusinessUnitController();
            var list = await s.AllByGrupBaskanlik(skip, top, name, grupBaskanlik);
            list.list = list.FOBusinessUnitList.Cast<object>().ToList();
            return list;

        }


        [HttpGet("byCompany/GetBolgeFonksiyonByCompany")]
        public async Task<IGenericListDto> GetBolgeFonksiyonByCompany(int skip = 0, int top = 50, string name = "", string company = "")
        {

            SFFODivisionController s = new SFFODivisionController();
            var list = await s.AllByBusinessUnit(skip, top, name, company);
            list.list = list.FODivisionList.Cast<object>().ToList();
            return list;

        }


        [HttpGet("byBolgeFonksiyon/GetBolumProjeByBolgeFonksiyon")]
        public async Task<IGenericListDto> GetBolumProjeByBolgeFonksiyon(int skip = 0, int top = 50, string name = "", string company = "")
        {

            SFcust_sub_divisionController s = new SFcust_sub_divisionController();
            var list = await s.AllByBolgeFonksiyon(skip, top, name, company);
            list.list = list.cust_sub_divisioList.Cast<object>().ToList();
            return list;

        }

    }
}
