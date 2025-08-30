using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using vesa.core.DTOs.Budget.SF;
using vesa.core.DTOs.Clients;
using vesa.core.Models;

namespace vesa.api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SFUserController : CustomBaseController
    {
        [HttpGet]
        public async Task<UserSFListDto> All(int skip = 0, int top = 50, string firstName = "")
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
                    filter = $"&$filter=substringof('{firstName.ToLower()}',tolower(displayName))";
                }


                using (var response = await httpClient.GetAsync($"{Config.Config.SfAddress}/User?$select=userId,department,firstName,username,defaultFullName,location,lastName,email,custom07&$format=json&$inlinecount=allpages&$skip={skip}&$top={top}" + filter))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var responseBody = await response.Content.ReadAsStringAsync();

                    // JSON verisini JObject'e dönüştür
                    var json = JObject.Parse(responseBody);

                    // "d" kısmını al ve sonuçları liste olarak çıkar
                    var results = json["d"]["results"].ToObject<List<SFUSerList>>();

                    UserSFListDto dto = new UserSFListDto();
                    dto.SFUSerList = results;
                    dto.Count = (int)json["d"]["__count"]; ;
                    return dto;

                }
            }

        }

        [HttpGet("user/{userid}")]
        public async Task<UserSFListDto> GetByIdEmpJob(string userid)
        {

            UserSFListDto result = new UserSFListDto();


            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(
                System.Text.ASCIIEncoding.ASCII.GetBytes(
                $"{Config.Config.UserName}:{Config.Config.Password}")));


                using (var response = await httpClient.GetAsync($"{Config.Config.SfAddress}/User?$filter=userId eq '" + userid + "'&$select=userId,department,firstName,username,defaultFullName,location,lastName,email,custom07&$format=json"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var responseBody = await response.Content.ReadAsStringAsync();

                    // JSON verisini JObject'e dönüştür
                    var json = JObject.Parse(responseBody);


                    // "d" kısmını al ve sonuçları liste olarak çıkar
                    var results = json["d"]["results"].ToObject<List<SFUSerList>>();
                    UserSFListDto dto = new UserSFListDto();
                    dto.SFUSerList = results;
                    dto.Count = 0; ;

                    return dto;

                }
            }


            return result;

        }
    }
}
