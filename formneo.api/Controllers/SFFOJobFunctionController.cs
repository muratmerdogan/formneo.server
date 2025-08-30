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
    public class SFFOJobFunctionController
    {
        [HttpGet]
        public async Task<FOJobFunctionSFDto> All(int skip = 0, int top = 50, string name = "")
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
                    filter = $"&$filter=substringof('{name}',tolower(name))";
                }


                using (var response = await httpClient.GetAsync($"{Config.Config.SfAddress}/FOJobFunction?$select=name,externalCode&$format=json&$inlinecount=allpages&$skip={skip}&$top={top}" + filter))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var responseBody = await response.Content.ReadAsStringAsync();

                    // JSON verisini JObject'e dönüştür
                    var json = JObject.Parse(responseBody);

                    // "d" kısmını al ve sonuçları liste olarak çıkar
                    var results = json["d"]["results"].ToObject<List<FOJobFunctionList>>();

                    FOJobFunctionSFDto dto = new FOJobFunctionSFDto();
                    dto.FOJobFunctionList = results;
                    dto.Count = (int)json["d"]["__count"]; ;
                    return dto;

                }
            }

        }
    }
}
