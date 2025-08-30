using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using vesa.core.DTOs.Budget.SF;

namespace vesa.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SFcust_sub_divisionController : Controller
    {
        [HttpGet]
        public async Task<cust_sub_divisionDto> All(int skip = 0, int top = 50, string name = "")
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
                    filter = $"&$filter=substringof('{name}',tolower(externalName_defaultValue))";
                }


                 filter += $"&$filter=not startswith(externalName_defaultValue, '0')";

                using (var response = await httpClient.GetAsync($"{Config.Config.SfAddress}/cust_sub_division?$select=externalName_defaultValue,externalCode&$format=json&$inlinecount=allpages&$skip={skip}&$top={top}" + filter))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var responseBody = await response.Content.ReadAsStringAsync();

                    // JSON verisini JObject'e dönüştür
                    var json = JObject.Parse(responseBody);

                    // "d" kısmını al ve sonuçları liste olarak çıkar
                    var results = json["d"]["results"].ToObject<List<cust_sub_divisioList>>();

                    cust_sub_divisionDto dto = new cust_sub_divisionDto();
                    dto.cust_sub_divisioList = results;
                    dto.Count = (int)json["d"]["__count"]; ;
                    return dto;

                }
            }

        }



        [HttpGet("AllByBusinessUnit")]
        public async Task<cust_sub_divisionDto> AllByBolgeFonksiyon(int skip = 0, int top = 50, string name = "", string division = "")
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
                    filter = $"&$filter=substringof('{name}',tolower(externalName_defaultValue))&cust_division eq '" + division + "'";
                }
                else
                {
                    filter = $"&$filter=cust_division  eq '" + division + "'";
                }


                filter += $"&$filter=not startswith(externalName_defaultValue, '0')";

                using (var response = await httpClient.GetAsync($"{Config.Config.SfAddress}/cust_sub_division?$select=externalName_defaultValue,externalCode&$format=json&$inlinecount=allpages&$skip={skip}&$top={top}" + filter))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var responseBody = await response.Content.ReadAsStringAsync();

                    // JSON verisini JObject'e dönüştür
                    var json = JObject.Parse(responseBody);

                    // "d" kısmını al ve sonuçları liste olarak çıkar
                    var results = json["d"]["results"].ToObject<List<cust_sub_divisioList>>();

                    cust_sub_divisionDto dto = new cust_sub_divisionDto();
                    dto.cust_sub_divisioList = results;
                    dto.Count = (int)json["d"]["__count"]; ;
                    return dto;

                }
            }

        }
    }
}
