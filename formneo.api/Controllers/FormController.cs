using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NLayer.Core.Services;
using formneo.core.DTOs;
using formneo.core.Models;
using formneo.core.Services;
using System.Collections.Generic;
using System.Linq;

namespace formneo.api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FormController : Controller
    {



        private readonly IMapper _mapper;
        private readonly IFormService _service;
        private readonly IServiceWithDto<FormRuleEngine, FormRuleEngineDto> _formRuleEngineService;
        private readonly IServiceWithDto<FormRuntime, FormRuntimeDto> _runtimeService;
        public FormController(IMapper mapper, IFormService formService, IServiceWithDto<FormRuleEngine, FormRuleEngineDto> formRuleEngineService, IServiceWithDto<FormRuntime, FormRuntimeDto> runtimeService)
        {
            _mapper = mapper;
            _service = formService;
            _formRuleEngineService = formRuleEngineService;
            _runtimeService = runtimeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormsAsync(string type, int limit = 100, int skip = 0)
        {
            var data = await _service.GetAllAsync();

            List<FormResource> formList = new List<FormResource>();

            foreach (var item in data)
            {

                formList.Add(new FormResource { _id = item.Id.ToString(), title = item.FormName, count = 100 });

            }
            // Veritabanından form verilerini çekme ve filtreleme işlemleri
            // Örnek bir liste döndürülecek
      

            return Ok(formList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFormAsync(Guid id, [FromQuery] int limit = 10, [FromQuery] int skip = 0)
        {
            // Burada veritabanınızdan form bilgilerini çekmek için işlemler yapılacak.
            //// Örneğin:
            //var form = GetFormFromDatabase(id, limit, skip);
            //if (form == null)
            //{
            //    return NotFound();
            //}

            var result = await _service.GetByIdStringGuidAsync(id);
                return Ok(result.FormDesign);
        }

        /// <summary>
        /// Form'un tüm elemanlarını getirir (recursive - iç içe yapıları da içerir)
        /// </summary>
        /// <param name="id">Form ID</param>
        /// <returns>Tüm form elemanlarının listesi</returns>
        [HttpGet("{id}/elements")]
        public async Task<IActionResult> GetFormElementsAsync(Guid id)
        {
            try
            {
                var elements = await GetAllFormElements(id);
                return Ok(elements);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Form'un tüm input elemanlarını getirir (Input, Password, DatePicker, Select vb.)
        /// </summary>
        /// <param name="id">Form ID</param>
        /// <returns>Tüm input elemanlarının listesi (FormInputElementDto)</returns>
        [HttpGet("{id}/inputs")]
        public async Task<IActionResult> GetFormInputElementsAsync(Guid id)
        {
            try
            {
                var inputElements = await GetAllInputElements(id);
                var dtoList = inputElements.Select(element => MapToFormInputElementDto(element)).ToList();
                return Ok(dtoList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Form'un belirli bir component tipindeki elemanlarını getirir
        /// </summary>
        /// <param name="id">Form ID</param>
        /// <param name="componentType">Component tipi (örn: "Input", "DatePicker.RangePicker", "FormTab")</param>
        /// <returns>Belirtilen component tipindeki elemanların listesi</returns>
        [HttpGet("{id}/elements/{componentType}")]
        public async Task<IActionResult> GetFormElementsByTypeAsync(Guid id, string componentType)
        {
            try
            {
                var elements = await GetFormElementsByComponentType(id, componentType);
                return Ok(elements);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Form'un belirli bir designable-id'ye sahip elemanını getirir
        /// </summary>
        /// <param name="id">Form ID</param>
        /// <param name="designableId">Designable ID</param>
        /// <returns>Bulunan eleman veya NotFound</returns>
        [HttpGet("{id}/element/{designableId}")]
        public async Task<IActionResult> GetFormElementByDesignableIdAsync(Guid id, string designableId)
        {
            try
            {
                var element = await GetFormElementByDesignableId(id, designableId);
                if (element == null)
                {
                    return NotFound($"Element with designable-id '{designableId}' not found in form '{id}'");
                }
                return Ok(element);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET api/form/{formId}/submission
        [HttpGet("{formId}/submission")]
        public async Task<IActionResult> GetSubmissionAsync(Guid formId, [FromQuery] int limit = 100, [FromQuery] int skip = 0)
        {
            try
            {
                // Burada formId kullanılarak veritabanından veri çekme işlemini simüle ediyorum.
                // Gerçek bir uygulamada burada veritabanı sorgusu veya iş mantığı kodu olacaktır.



                //var forms = await _runtimeService.Where(e => e.FormId == formId);




                //var dto = _mapper.Map<List<FormRuntimeSubmission>>(forms.Data);


                //foreach (var item in dto)
                //{
                //    item._id = formId.ToString();
                //    item.ValuesJsonData = item.ValuesJsonData.ToString();
                //}

                //// Veriler başarıyla alındıysa, HTTP 200 (OK) ile verileri dön
                //return Ok(dto);

                var sampleData = new[]
                {
                new
                {
                _id = "57d0a2fd76f943a4007e1529",
                modified = "2016-09-07T23:30:05.758Z",
                data = new
                {
                    firstName = "Joe",
                    lastName = "Smith",
                    email = "joe@example.com",
                    phoneNumber = "123123123"
                },
                form = "57d0a2f876f943a4007e1527",
                created = "2016-09-07T23:30:05.757Z",
                externalIds = new string[] { },
                access = new string[] { },
                roles = new string[] { },
                owner = "57d010fe76f943a4007e11da"
                },
                new
                {
                _id = "57d0a2fd76f943a4007e1529",
                modified = "2016-09-07T23:30:05.758Z",
                data = new
                {
                    firstName = "Joe",
                    lastName = "Smith",
                    email = "joe@example.com",
                    phoneNumber = "123123123"
                },
                form = "57d0a2f876f943a4007e1527",
                created = "2016-09-07T23:30:05.757Z",
                externalIds = new string[] { },
                access = new string[] { },
                roles = new string[] { },
                owner = "57d010fe76f943a4007e11da"
                }
                };

                return Ok(sampleData);
            }
            catch (Exception ex)
            {
                // Hata oluşursa, HTTP 500 (Internal Server Error) dön
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Form ID'ye göre form schema'sındaki tüm elemanları recursive olarak gezer ve bulur
        /// İç içe geçmiş yapıları (FormTab, TabPane, Card vb.) da destekler
        /// </summary>
        /// <param name="formId">Form ID (Guid)</param>
        /// <returns>Tüm form elemanlarının listesi (her eleman bir JObject)</returns>
        [NonAction]
        public async Task<List<JObject>> GetAllFormElements(Guid formId)
        {
            var form = await _service.GetByIdStringGuidAsync(formId);
            if (form == null || string.IsNullOrEmpty(form.FormDesign))
            {
                return new List<JObject>();
            }

            return GetAllFormElements(form.FormDesign);
        }

        /// <summary>
        /// Form schema'sındaki tüm elemanları recursive olarak gezer ve bulur
        /// İç içe geçmiş yapıları (FormTab, TabPane, Card vb.) da destekler
        /// </summary>
        /// <param name="formSchemaJson">Form schema JSON string'i</param>
        /// <returns>Tüm form elemanlarının listesi (her eleman bir JObject)</returns>
        [NonAction]
        public List<JObject> GetAllFormElements(string formSchemaJson)
        {
            var elements = new List<JObject>();
            
            if (string.IsNullOrEmpty(formSchemaJson))
                return elements;

            try
            {
                var formObject = JObject.Parse(formSchemaJson);
                var schema = formObject["schema"];
                
                if (schema != null && schema["properties"] != null)
                {
                    var properties = schema["properties"] as JObject;
                    if (properties != null)
                    {
                        // Root seviyedeki tüm properties'leri recursive olarak gez
                        foreach (var property in properties.Properties())
                        {
                            var element = property.Value as JObject;
                            if (element != null)
                            {
                                TraverseFormElement(element, elements);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Parse hatası durumunda boş liste döndür
                // Loglama yapılabilir
            }

            return elements;
        }

        /// <summary>
        /// Tek bir form elemanını recursive olarak gezer
        /// </summary>
        /// <param name="element">Gezen form elemanı (JObject)</param>
        /// <param name="elements">Bulunan tüm elemanların listesi</param>
        [NonAction]
        private void TraverseFormElement(JObject element, List<JObject> elements)
        {
            if (element == null)
                return;

            // Elemanı yeni bir JObject olarak oluştur (tüm property'leri dahil)
            var elementCopy = new JObject();
            
            // Tüm property'leri kopyala (properties hariç)
            foreach (var prop in element.Properties())
            {
                if (prop.Name != "properties")
                {
                    // Property değerini direkt kopyala (DeepClone yerine direkt atama)
                    // Bu şekilde property'ler doğru şekilde kopyalanır
                    elementCopy[prop.Name] = prop.Value;
                }
            }

            // Kopyalanmış elemanı ekle
            elements.Add(elementCopy);

            // Eğer bu elemanın içinde "properties" varsa, iç içe elemanlar var demektir
            var nestedProperties = element["properties"] as JObject;
            if (nestedProperties != null)
            {
                // İç içe elemanları recursive olarak gez
                foreach (var nestedProperty in nestedProperties.Properties())
                {
                    var nestedElement = nestedProperty.Value as JObject;
                    if (nestedElement != null)
                    {
                        TraverseFormElement(nestedElement, elements);
                    }
                }
            }
        }

        /// <summary>
        /// Form ID'ye göre belirli bir component tipindeki tüm elemanları bulur
        /// </summary>
        /// <param name="formId">Form ID (Guid)</param>
        /// <param name="componentType">Aranacak component tipi (örn: "Input", "DatePicker.RangePicker", "FormTab")</param>
        /// <returns>Belirtilen component tipindeki tüm elemanların listesi</returns>
        [NonAction]
        public async Task<List<JObject>> GetFormElementsByComponentType(Guid formId, string componentType)
        {
            var form = await _service.GetByIdStringGuidAsync(formId);
            if (form == null || string.IsNullOrEmpty(form.FormDesign))
            {
                return new List<JObject>();
            }

            return GetFormElementsByComponentType(form.FormDesign, componentType);
        }

        /// <summary>
        /// Form schema'sındaki belirli bir component tipindeki tüm elemanları bulur
        /// </summary>
        /// <param name="formSchemaJson">Form schema JSON string'i</param>
        /// <param name="componentType">Aranacak component tipi (örn: "Input", "DatePicker.RangePicker", "FormTab")</param>
        /// <returns>Belirtilen component tipindeki tüm elemanların listesi</returns>
        [NonAction]
        public List<JObject> GetFormElementsByComponentType(string formSchemaJson, string componentType)
        {
            var allElements = GetAllFormElements(formSchemaJson);
            var filteredElements = new List<JObject>();

            foreach (var element in allElements)
            {
                var xComponent = element["x-component"]?.ToString();
                if (!string.IsNullOrEmpty(xComponent) && xComponent.Equals(componentType, StringComparison.OrdinalIgnoreCase))
                {
                    filteredElements.Add(element);
                }
            }

            return filteredElements;
        }

        /// <summary>
        /// Form ID'ye göre belirli bir designable-id'ye sahip elemanı bulur
        /// </summary>
        /// <param name="formId">Form ID (Guid)</param>
        /// <param name="designableId">Aranacak designable-id</param>
        /// <returns>Bulunan eleman (JObject) veya null</returns>
        [NonAction]
        public async Task<JObject?> GetFormElementByDesignableId(Guid formId, string designableId)
        {
            var form = await _service.GetByIdStringGuidAsync(formId);
            if (form == null || string.IsNullOrEmpty(form.FormDesign))
            {
                return null;
            }

            return GetFormElementByDesignableId(form.FormDesign, designableId);
        }

        /// <summary>
        /// Form schema'sındaki belirli bir designable-id'ye sahip elemanı bulur
        /// </summary>
        /// <param name="formSchemaJson">Form schema JSON string'i</param>
        /// <param name="designableId">Aranacak designable-id</param>
        /// <returns>Bulunan eleman (JObject) veya null</returns>
        [NonAction]
        public JObject? GetFormElementByDesignableId(string formSchemaJson, string designableId)
        {
            var allElements = GetAllFormElements(formSchemaJson);

            foreach (var element in allElements)
            {
                var xDesignableId = element["x-designable-id"]?.ToString();
                if (!string.IsNullOrEmpty(xDesignableId) && xDesignableId.Equals(designableId, StringComparison.OrdinalIgnoreCase))
                {
                    return element;
                }
            }

            return null;
        }

        /// <summary>
        /// Form ID'ye göre tüm input elemanlarını bulur (Input, Password, DatePicker vb.)
        /// </summary>
        /// <param name="formId">Form ID (Guid)</param>
        /// <returns>Tüm input elemanlarının listesi</returns>
        [NonAction]
        public async Task<List<JObject>> GetAllInputElements(Guid formId)
        {
            var form = await _service.GetByIdStringGuidAsync(formId);
            if (form == null || string.IsNullOrEmpty(form.FormDesign))
            {
                return new List<JObject>();
            }

            return GetAllInputElements(form.FormDesign);
        }

        /// <summary>
        /// Form schema'sındaki tüm input elemanlarını bulur (Input, Password, DatePicker vb.)
        /// Tüm property'leri (nested properties dahil) dahil eder
        /// </summary>
        /// <param name="formSchemaJson">Form schema JSON string'i</param>
        /// <returns>Tüm input elemanlarının listesi (tüm property'leri dahil)</returns>
        [NonAction]
        public List<JObject> GetAllInputElements(string formSchemaJson)
        {
            var allElements = GetAllFormElements(formSchemaJson);
            var inputElements = new List<JObject>();

            // Input component'leri
            var inputComponentTypes = new[] { "Input", "Password", "TextArea", "NumberPicker", "DatePicker", "DatePicker.RangePicker", "Select", "Checkbox", "Radio", "Switch" };

            foreach (var element in allElements)
            {
                var xComponent = element["x-component"]?.ToString();
                if (!string.IsNullOrEmpty(xComponent))
                {
                    // Tam eşleşme veya başlangıç kontrolü (örn: "DatePicker.RangePicker" için "DatePicker" kontrolü)
                    foreach (var inputType in inputComponentTypes)
                    {
                        if (xComponent.Equals(inputType, StringComparison.OrdinalIgnoreCase) ||
                            xComponent.StartsWith(inputType + ".", StringComparison.OrdinalIgnoreCase))
                        {
                            // Eleman zaten GetAllFormElements içinde clone edildi, direkt ekle
                            inputElements.Add(element);
                            break;
                        }
                    }
                }
            }

            return inputElements;
        }

        /// <summary>
        /// JObject'ten FormInputElementDto'ya mapping yapar
        /// </summary>
        /// <param name="element">Form element JObject</param>
        /// <returns>FormInputElementDto</returns>
        [NonAction]
        private FormInputElementDto MapToFormInputElementDto(JObject element)
        {
            var dto = new FormInputElementDto();

            // Id (x-designable-id)
            dto.Id = element["x-designable-id"]?.ToString() ?? string.Empty;

            // Title
            dto.Title = element["title"]?.ToString();

            // Name (name veya title - önce name, yoksa title)
            dto.Name = element["name"]?.ToString() ?? element["name"]?.ToString();

            // Type
            dto.Type = element["type"]?.ToString();

            // ComponentType (x-component)
            dto.ComponentType = element["x-component"]?.ToString() ?? string.Empty;

            // Decorator (x-decorator)
            dto.Decorator = element["x-decorator"]?.ToString();

            // Index (x-index)
            if (element["x-index"] != null)
            {
                if (int.TryParse(element["x-index"]?.ToString(), out int index))
                {
                    dto.Index = index;
                }
            }

            // Validators (x-validator)
            var validators = element["x-validator"];
            if (validators != null)
            {
                if (validators is JArray validatorArray)
                {
                    foreach (var validator in validatorArray)
                    {
                        var validatorStr = validator?.ToString();
                        if (!string.IsNullOrEmpty(validatorStr))
                        {
                            dto.Validators.Add(validatorStr);
                        }
                    }
                }
                else if (validators.Type == JTokenType.String)
                {
                    dto.Validators.Add(validators.ToString());
                }
            }

            // ComponentProps (x-component-props)
            var componentProps = element["x-component-props"];
            if (componentProps != null && componentProps is JObject componentPropsObj)
            {
                foreach (var prop in componentPropsObj.Properties())
                {
                    dto.ComponentProps[prop.Name] = ConvertJTokenToObject(prop.Value);
                }
            }

            // DecoratorProps (x-decorator-props)
            var decoratorProps = element["x-decorator-props"];
            if (decoratorProps != null && decoratorProps is JObject decoratorPropsObj)
            {
                foreach (var prop in decoratorPropsObj.Properties())
                {
                    dto.DecoratorProps[prop.Name] = ConvertJTokenToObject(prop.Value);
                }
            }

            return dto;
        }

        /// <summary>
        /// JToken'ı object'e çevirir
        /// </summary>
        /// <param name="token">JToken</param>
        /// <returns>object</returns>
        [NonAction]
        private object ConvertJTokenToObject(JToken token)
        {
            if (token == null)
                return null;

            switch (token.Type)
            {
                case JTokenType.String:
                    return token.ToString();
                case JTokenType.Integer:
                    return token.ToObject<int>();
                case JTokenType.Float:
                    return token.ToObject<double>();
                case JTokenType.Boolean:
                    return token.ToObject<bool>();
                case JTokenType.Null:
                    return null;
                case JTokenType.Array:
                    var array = token as JArray;
                    return array?.Select(ConvertJTokenToObject).ToList();
                case JTokenType.Object:
                    var obj = token as JObject;
                    var dict = new Dictionary<string, object>();
                    if (obj != null)
                    {
                        foreach (var prop in obj.Properties())
                        {
                            dict[prop.Name] = ConvertJTokenToObject(prop.Value);
                        }
                    }
                    return dict;
                default:
                    return token.ToString();
            }
        }

    }

    public class RootObject
    {
        public Dictionary<string, object> Data { get; set; }
    }
    public class FormResource
    {
        public string _id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int count { get; set; }
        public int __v { get; set; }


    }

}
