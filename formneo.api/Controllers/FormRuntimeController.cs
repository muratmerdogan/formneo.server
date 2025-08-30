using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json.Linq;
using NLayer.Core.Services;
using System.Dynamic;
using System.Text;
using vesa.api.Filters;
using vesa.api.Helper;
using vesa.core.DTOs;
using vesa.core.DTOs.FormAssign;
using vesa.core.DTOs.FormDto;
using vesa.core.Models;
using vesa.core.Models.FormEnums;
using vesa.core.Services;
using vesa.core.UnitOfWorks;
using vesa.repository.UnitOfWorks;
using vesa.service.Services;

namespace vesa.api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FormRuntimeController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IServiceWithDto<FormRuntime, FormRuntimeDto> _service;

        //private readonly IServiceWithDto<Form, FormDataListDto> _formService;
        private readonly IFormService _formservice;
        private readonly IServiceWithDto<FormAssign, FormAssignDto> _formAssignService;
        private readonly DbNameHelper _dbNameHelper;

        private readonly UserManager<UserApp> _userManager;

        IUnitOfWork _unitOfWork;

        Dictionary<string, string> keyLabelMap = new();
        public FormRuntimeController(DbNameHelper dbNameHelper,UserManager<UserApp> userManager, IMapper mapper, IServiceWithDto<FormRuntime, FormRuntimeDto> service, IServiceWithDto<Form, FormDataListDto> formService, IFormService formservice,
            IServiceWithDto<FormAssign, FormAssignDto> formAssignService, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _service = service;
            _formservice = formservice;
            _formAssignService = formAssignService;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _dbNameHelper = dbNameHelper;
        }
        /// GET api/products
        [HttpGet]
        public async Task<List<FormRuntimeDto>> All()
        {
            var forms = await _service.GetAllAsync();

            return forms.Data.ToList();
        }

        [HttpGet("{id}")]
        public async Task<FormRuntimeDto> GetById(string id)
        {

            var form = await _service.GetByIdGuidAsync(new Guid(id));
            var formDto = _mapper.Map<FormRuntimeDto>(form);
            return formDto;
        }
        // GET /api/products/5

        //de


        [HttpPost]
        public async Task<IActionResult> Save(FormRuntimeDto dto, string? formAssignId = null)
        {
            _unitOfWork.BeginTransaction();

            var result = await _service.AddAsync(dto);

            if (formAssignId != null)
            {
                var formAssigns = await _formAssignService.Include();
                formAssigns = formAssigns.Where(e => e.Id == new Guid(formAssignId));

                var selectedAssign = _mapper.Map<FormAssignDto>(formAssigns.FirstOrDefault());

                selectedAssign.Status = FormStatus.completed;
                selectedAssign.FormRunTimeId = result.Data.Id;
                await _formAssignService.UpdateAsync(selectedAssign);

                var user = await _userManager.FindByIdAsync(selectedAssign.UserAppId);
                string userName = $"{user.FirstName} {user.LastName}";
                var form = await _formservice.GetByIdStringGuidAsync(dto.FormId);
                SendMailFormUpdate(userName, form.FormName, dto.ValuesJson, form.FormDesign);
            }

            _unitOfWork.Commit();



            return CreateActionResult(CustomResponseDto<FormRuntimeDto>.Success(204));

        }
        [HttpPut]
        public async Task<IActionResult> Update(FormRuntimeDto formDto)
        {
            await _service.UpdateAsync(formDto);

            return CreateActionResult(CustomResponseDto<FormRuntimeDto>.Success(204));
        }

        // DELETE api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {

            await _service.RemoveAsyncByGuid(id);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }


        [HttpGet("[action]/{formId}")]
        public async Task<List<FormRuntimeDto>> getFormDataById(Guid formId)
        {
            //var forms = await _service.Find(e => e.FormId == formId);
            var result = await _service.Include();
            var forms = result.Where(e => e.FormId == formId).ToList();

            var dto = _mapper.Map<List<FormRuntimeDto>>(forms);

            return dto;
        }


        [HttpGet("[action]/{id}")]
        public async Task<FormRuntimeDto> getDetail(Guid id)
        {
            var forms = await _service.Find(e => e.Id == id);
            var dto = _mapper.Map<FormRuntimeDto>(forms.Data);
            return dto;
        }

        [HttpGet("[action]/{formId}")]
        public async Task<List<FormColumnDto>> GetColumnList(Guid formId)
        {




            //foreach (var item in forms.Data)
            //{

            //    var jObject = JObject.Parse(item.ValuesJson);
            //    var jComponents = jObject["data"] as JArray;

            //    if (jComponents != null)
            //    {
            //        foreach (var jComponent in jComponents)
            //        {
            //            var customBooleanProp = jComponent["customBooleanProp"]?.Value<bool>();
            //            if (customBooleanProp == true)
            //            {

            //            }
            //        }
            //    }
            //}

            var list = await GetColumn(formId);
            return list;

        }

        private static List<JToken> FindComponentsWithProp(JToken root, string propName, bool propValue)
        {
            var results = new List<JToken>();
            TraverseAndCollect(root, propName, propValue, results);
            return results;
        }

        private static void TraverseAndCollect(JToken current, string propName, bool propValue, List<JToken> results)
        {
            //if (current is JObject obj)
            //{
            //    if (obj[propName] != null && obj[propName].Type == JTokenType.Boolean && (bool)obj[propName] == propValue)
            //    {
            //        results.Add(obj);
            //    }
            //    // Check if there's a 'components' field and if it is an array
            //    if (obj["components"] is JArray subComponents)
            //    {
            //        foreach (var subComponent in subComponents)
            //        {
            //            TraverseAndCollect(subComponent, propName, propValue, results);
            //        }
            //    }
            //}
            if (current is JObject obj)
            {
                if (obj[propName] != null &&
                    obj[propName].Type == JTokenType.Boolean &&
                    (bool)obj[propName] == propValue)
                {
                    results.Add(obj);
                }

                // Tüm property'leri gez
                foreach (var property in obj.Properties())
                {
                    TraverseAndCollect(property.Value, propName, propValue, results);
                }
            }
            else if (current is JArray array)
            {
                foreach (var item in array)
                {
                    TraverseAndCollect(item, propName, propValue, results);
                }
            }
        }

        private async Task<List<FormColumnDto>> GetColumn(Guid formId)
        {

            var forms = await _service.Where(e => e.FormId == formId);

            var form = await _formservice.GetByIdStringGuidAsync(formId);




            var jObject = JObject.Parse(form.FormDesign);
            //var componentsWithCustomBooleanProp = jObject["components"]
            //    .Where(c => c["customBooleanProp"] != null && (bool)c["customBooleanProp"])
            //    .ToList();

            var result = FindComponentsWithProp(jObject, "customBooleanProp", true);


            List<FormColumnDto> list = new List<FormColumnDto>();
            foreach (var component in result)
            {
                Console.WriteLine($"ID: {component["id"]}");

                list.Add(new FormColumnDto { ColumnName = component["key"].ToString(), ColumnLabel = component["label"].ToString(), Key = component["key"].ToString() });
            }

            return list;

        }

        [HttpGet("[action]/{formId}")]
        public async Task<List<dynamic>> GetList(Guid formId)

        {

            var columnList = await GetColumn(formId);

            var forms = await _service.Where(e => e.FormId == formId);

            List<dynamic> list = new List<dynamic>();
            foreach (var item in forms.Data.OrderByDescending(e => e.CreatedDate))
            {

                var jObject = JObject.Parse(item.ValuesJson);
                var jComponents = jObject["data"];

                if (jComponents != null)
                {
                    dynamic expando = new ExpandoObject();
                    var expandoDict = (IDictionary<string, object>)expando; // ExpandoObject'i sözlük olarak kullan

                    foreach (var jComponent in jComponents)
                    {


                        string deger = jComponent.ToString();

                        string cleanedKey = deger.Split(":")[0].Replace("\"", "");

                        expandoDict["id"] = item.Id;

                        // JSON objesine dönüştür
                        JObject jsonObject = JObject.Parse(item.ValuesJson);


                        if (jComponent.Type == JTokenType.Property)
                        {
                            // 'dsInput1' değerini al


                            var isExist = columnList.Where(e => e.Key == cleanedKey).FirstOrDefault();

                            if (isExist != null)

                            {
                                object dsInput1Value = (string)jsonObject["data"][cleanedKey];

                                object dsInput1Obj = jsonObject["data"][cleanedKey];

                                expandoDict[cleanedKey] = dsInput1Value;

                            }
                        }
                    }
                    list.Add(expandoDict);
                }
            }
            return list;

        }


        private async Task SendMailFormUpdate(string userName, string formName, string valuesJson, string formDesign)
        {
            List<string> tolist = new List<string>();
            tolist.Add("busra.aydemir@vesacons.com");
            tolist.Add("murat.merdogan@vesacons.com");

            JObject dataObj = JObject.Parse(valuesJson);
            JObject formData = (JObject)dataObj["data"];
            JObject designObj = JObject.Parse(formDesign);
            JArray components = (JArray)designObj["components"];

            ExtractLabels(components);
            string dbName = _dbNameHelper.GetDatabaseName();
            string htmlTableRows = BuildHtmlTableRows(formData, keyLabelMap);
            string htmlDataContent = $@"
        <table width=""100%"" cellspacing=""0"" cellpadding=""10"" border=""0"" style=""border-collapse: collapse;"">
            {htmlTableRows}
        </table>";



            string emailBody = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <title>Form Bilgilendirme</title>
</head>
<body style=""margin:0; padding:0; font-family: Arial, sans-serif; background-color:#f4f7fc;"">
     <table role=""presentation"" width=""100%"" cellspacing=""0"" cellpadding=""0"" border=""0"" style=""background-color:#f4f7fc; padding:20px;"">
        <tr>
            <td align=""center"">
                <table role=""presentation"" width=""800"" cellspacing=""0"" cellpadding=""0"" border=""0"" style=""background-color:#ffffff; box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.1);"">
                    <!-- HEADER -->
                    <td style=""background-color:white;"">
                        <table style=""width: 100%; table-layout: fixed; display: inline-table;"">
                            <tr>
                                <!-- Logo ve Başlık birlikte yer alacak -->
                                <td style=""background-color: white; padding:12px; width: auto;"">
                                    <img src=""{VesaLogo.Logo}"" alt=""Logo"" width=""100"" height=""60"" style=""display: block; width: 100%; height: auto;"">
                                </td>
                                <td style=""background-color: white; padding:12px; width: auto;"">
                                    <img src=""{VesaLogo.ColorImg}"" alt=""Logo"" width=""650"" height=""20"" style=""display: block; width: 100%; height: auto;"">
                                </td>
                            </tr>
                        </table>
                    </td>

                    <!-- CONTENT -->
                    <tr>
                        <td style=""padding:20px;"">
                            <p style=""font-size:14px; margin-bottom:10px;""><strong>{userName}</strong> kullanıcısına ait <strong>{formName}</strong> isimli formda güncelleme gerçekleştirilmiştir.</p>
                            <br>
                            <p style='font-size:13px;'><strong>Form Detayları:</strong></p>
                            {htmlDataContent}
                            <br>
                            <p style=""color:#0073e6;""><strong>Formlara ulaşmak için: https://support.vesa-tech.com/userFormList</strong></p>
                        </td>
                    </tr>
                    <!-- FOOTER -->
                    <tr>
                        <td style=""background-color:#f4f7fc; padding:15px; text-align:center; font-size:12px; color:#555;"">
                            Bu e-posta otomatik olarak oluşturulmuştur, lütfen yanıtlamayınız. {dbName}
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>";


            utils.Utils.SendMail("Form Bilgilendirme", emailBody, tolist);
        }


        private void ExtractLabels(JArray components)
        {
            foreach (var component in components)
            {
                var type = component["type"]?.ToString();
                var action = component["action"]?.ToString();

                // Eğer bu bir "submit" butonuysa, atla
                if (type == "button" && action == "submit")
                    continue;

                // Eğer key ve label varsa, ekle
                var key = component["key"]?.ToString();
                var label = component["label"]?.ToString();
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(label))
                {
                    if (!keyLabelMap.ContainsKey(key))
                        keyLabelMap[key] = label;
                }

                // Eğer nested components varsa, onları da gez
                if (component["components"] is JArray nested)
                {
                    ExtractLabels(nested);
                }

                // Eğer bu bir table yapısıysa ve "rows" içeriyorsa
                if (component["rows"] is JArray rows)
                {
                    foreach (var row in rows)
                    {
                        foreach (var cell in row)
                        {
                            if (cell["components"] is JArray cellComponents)
                            {
                                ExtractLabels(cellComponents);
                            }
                        }
                    }
                }

                // Eğer columns varsa (örneğin "columns" component)
                if (component["columns"] is JArray columns)
                {
                    foreach (var col in columns)
                    {
                        if (col["components"] is JArray colComponents)
                        {
                            ExtractLabels(colComponents);
                        }
                    }
                }
            }
        }

        private string BuildHtmlTableRows(JObject formData, Dictionary<string, string> keyLabelMap)
        {
            try
            {
                var sb = new StringBuilder();

                foreach (var item in formData)
                {
                    string key = item.Key;

                    // "submit" veya keyLabelMap içinde olmayan key'ler atlanır
                    if (key.ToLower() == "submit" || !keyLabelMap.TryGetValue(key, out string label))
                        continue;

                    string value = item.Value.Type == JTokenType.Boolean
                        ? ((bool)item.Value ? "Evet" : "Hayır")
                        : item.Value?.ToString() ?? "";

                    sb.Append($@"
<tr>
    <th style=""border:1px solid #ddd; background-color:#f0f4f8; text-align:left; padding:12px; font-size:14px;"">{label}</th>
    <td style=""border:1px solid #ddd; padding:12px; font-size:14px;"">{value}</td>
</tr>");
                }

                return sb.ToString();
            }
            catch (Exception e)
            {
                return null;
            }

        }



    }
}
