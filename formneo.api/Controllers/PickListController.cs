using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using NLayer.Core.Services;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Runtime.Caching;
using vesa.api.Controllers.MyApplication.Services;
using vesa.core.DTOs.Budget.PeriodUserDto;
using vesa.core.DTOs.Budget.SF;
using vesa.core.DTOs.Clients;
using vesa.core.EnumExtensions;
using vesa.core.Models.BudgetManagement;

namespace vesa.api.Controllers
{


    public enum PickList
    {
        [Description("Regular Temporary")]
        RegularTemporary = 1,

        [Description("Employee Class")]
        EmployeeClass = 2,

        [Description("Gorev Seviyesi")]
        GorevSeviyesi = 3,

        [Description("Adines Status")]
        AdinesStatus = 4,

        [Description("Employment Type 1")]
        EmploymentType1 = 5,

        [Description("Type Of Division")]
        TypeOfDivision = 6,

        [Description("Ronesans Kademesi")]
        RonesansKademesi = 7,

        [Description("EC Hay Degree")]
        EcHayDegree = 8,

        [Description("Unknown PickList Name")]
        Unknown = 9,

        [Description("cust_empSubGroupNav")]
        employeetype1 = 10,

        [Description("Çalışan Tipi")]
        EmpGroup = 11,

        [Description("ec_PosTicket")]
        ec_PosTicket = 12,

        [Description("EC_calisma_yeri_turu")]
        EC_calisma_yeri_turu = 13,

        [Description("IsAlani")]
        IsAlani = 14,

        [Description("ChangeReason")]
        ChangeReason = 15,
        
    }
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PickListController : Controller
    {

        private readonly IMapper _mapper;

        const ServiceCollection _serviceProvider= null;

        private readonly IMemoryCache _memoryCache;
        public string cacheKey = "picklist";

        public PickListController(IMapper mapper, IMemoryCache memoryCache)
        {
            _mapper = mapper;


            _memoryCache = memoryCache;


        }

        [HttpGet]
        public async Task<List<PickListDto>> All(PickList pc)
        {


            if (_memoryCache.TryGetValue(pc.GetDescription(), out var cachedValue))
            {
                // Değer zaten önbellekte var, cachedValue'yu kullanabilirsiniz
                return cachedValue as List<PickListDto>;
            }

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(
                System.Text.ASCIIEncoding.ASCII.GetBytes(
                $"{Config.Config.UserName}:{Config.Config.Password}")));

          
                string url = "";
                if (pc == PickList.RegularTemporary)
                {
                    url = $"{Config.Config.SfAddress}/PickListValueV2?$format=json&$filter=PickListV2_id eq 'RegularTemporary'&$select=externalCode,label_localized";
                }
                else if (pc == PickList.EmployeeClass)
                {
                    url = $"{Config.Config.SfAddress}/PickListValueV2?$format=json&$filter=PickListV2_id eq 'EmployeeClass' and externalCode ne '4'&$select=externalCode,label_localized";

                }
                else if (pc == PickList.GorevSeviyesi)
                {
                    url = $"{Config.Config.SfAddress}/PickListValueV2?$format=json&$filter=PickListV2_id eq 'GorevSeviyesi'&$select=externalCode,label_localized";

                }
                else if (pc == PickList.AdinesStatus)
                {
                    url = $"{Config.Config.SfAddress}/PickListValueV2?$format=json&$filter=PickListV2_id eq 'AdinesStatus'&$select=externalCode,label_localized";

                }
                else if (pc == PickList.EmploymentType1)
                {
                    url = $"{Config.Config.SfAddress}/PickListValueV2?$format=json&$filter=PickListV2_id eq 'employmenttype1' and status eq 'A'&$select=externalCode,label_localized";

                }
                else if (pc == PickList.TypeOfDivision)
                {
                    url = $"{Config.Config.SfAddress}/PickListValueV2?$format=json&$filter=PickListV2_id eq 'TypeOfDivision'&$select=externalCode,label_localized";
                }
                else if (pc == PickList.RonesansKademesi)
                {
                    url = $"{Config.Config.SfAddress}/PickListValueV2?$format=json&$filter=PickListV2_id eq 'ronesanskademesi'&$select=externalCode,label_localized";

                }
                else if (pc == PickList.EcHayDegree)
                {
                    url = $"{Config.Config.SfAddress}/PickListValueV2?$format=json&$filter=PickListV2_id eq 'ec_haydegree'&$select=externalCode,label_localized";
                }
                else if (pc == PickList.employeetype1)
                {
                    
                    url = $"{Config.Config.SfAddress}/PickListValueV2?$format=json&$filter=PickListV2_id eq 'employeetype1'";
                }
                else if (pc == PickList.EmpGroup)
                {
                    url = $"{Config.Config.SfAddress}/PickListValueV2?$format=json&$filter=PickListV2_id eq 'EmpGroup'";
                }
                else if (pc == PickList.ec_PosTicket)
                {
                    url = $"{Config.Config.SfAddress}/PickListValueV2?$format=json&$filter=PickListV2_id eq 'ec_PosTicket'";
                }

                else if (pc == PickList.IsAlani)
                {
                    url = $"{Config.Config.SfAddress}/PickListValueV2?$format=json&$filter=PickListV2_id eq 'IsAlani'";
                }
                else if (pc == PickList.EC_calisma_yeri_turu)
                {
                    url = $"{Config.Config.SfAddress}/PickListValueV2?$format=json&$filter=PickListV2_id eq 'EC_calisma_yeri_turu'";
                }
                else if (pc == PickList.ChangeReason)
                {
                    url = $"{Config.Config.SfAddress}/PickListValueV2?$format=json&$filter=PickListV2_id eq 'ChangeReason'";
                }



                //  https://api12preview.sapsf.eu/odata/v2/PickListValueV2?$format=json&$filter=PickListV2_id eq 'employeetype1'

                using (var response = await httpClient.GetAsync(url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var responseBody = await response.Content.ReadAsStringAsync();

                    // JSON verisini JObject'e dönüştür
                    var json = JObject.Parse(responseBody);

                    // "d" kısmını al ve sonuçları liste olarak çıkar
                    var dto = json["d"]["results"].ToObject<List<PickListDTO>>();


                    var results = _mapper.Map<List<PickListDto>>(dto);

                    if (pc == PickList.GorevSeviyesi)
                    {
                        results = results.Where(r => r.name.All(c => !char.IsLetter(c) || char.IsUpper(c))).ToList();
                    }

                    if (pc == PickList.employeetype1)
                    {

                        var filteredDto = dto.Where(x => !x.label_localized.StartsWith("RUS", StringComparison.OrdinalIgnoreCase)).ToList().DistinctBy(x => x.label_localized);  // Name alanına göre distinct yapıyoruz
                        results = _mapper.Map<List<PickListDto>>(filteredDto).OrderBy(e => e.name).ToList();
                    }
                    if(pc == PickList.EC_calisma_yeri_turu)
                    {
                        var dtos = json["d"]["results"].ToObject<List<PickListDTO>>();
                        results = _mapper.Map<List<PickListDto>>(dtos);
                    }
                    if (pc == PickList.EmpGroup)
                    {

                        var filteredDistinctDto = dto
                        .Where(x => !x.label_localized.StartsWith("rus", StringComparison.OrdinalIgnoreCase) &&
                        x.label_localized.IndexOf("stajyer", StringComparison.OrdinalIgnoreCase) < 0 &&
                        x.label_localized.IndexOf("hayalet", StringComparison.OrdinalIgnoreCase) < 0)
                        .DistinctBy(x => x.label_localized)
                        .ToList();

                        results = _mapper.Map<List<PickListDto>>(filteredDistinctDto);
                    }






                    var value = results.OrderBy(e => e.name).DistinctBy(x => x.name).ToList();




                    //if (firstCache.TryGetValue(pc.GetDescription(), out var cachedValue))
                    //{
                    //    // Değer zaten önbellekte var, cachedValue kullanabilirsiniz
                    //    Console.WriteLine("Önbellekte değer bulundu.");
                    //}


                    try
                    {


                        _memoryCache.Set(pc.GetDescription(), value,
                            new MemoryCacheEntryOptions()
                            .SetAbsoluteExpiration(TimeSpan.FromDays(1)));


                        return value;
                    }
                    catch (Exception ex)
                    {

                    }


                    return value;
                }
            }

        }
    }


}
