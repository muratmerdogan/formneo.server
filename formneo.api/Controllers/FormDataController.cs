using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json.Linq;
using NLayer.Core.Services;
using System.ComponentModel;
using System.Reflection;
using vesa.api.Filters;
using vesa.core.DTOs;
using vesa.core.DTOs.FormAuth;
using vesa.core.DTOs.FormDatas;
using vesa.core.EnumExtensions;
using vesa.core.Models;
using vesa.core.Models.FormEnums;
using vesa.core.Repositories;
using vesa.core.Services;

namespace vesa.api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FormDataController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IFormService _service;
        private readonly IServiceWithDto<FormRuleEngine, FormRuleEngineDto> _formRuleEngineService;
        private readonly IFormRepository _formRepository;


        public FormDataController(IMapper mapper, IFormService formService, IServiceWithDto<FormRuleEngine, FormRuleEngineDto> formRuleEngineService, IFormRepository formRepository)
        {
            _mapper = mapper;
            _service = formService;
            _formRuleEngineService = formRuleEngineService;
            _formRepository = formRepository;
        }
        /// GET api/products
        [HttpGet]
        public async Task<ActionResult<List<FormDataListDto>>> All()
        {
            var query = _formRepository.GetAll().Include(x => x.WorkFlowDefination).OrderByDescending(e => e.CreatedDate);
            var dto = await query.Select(x => new FormDataListDto
            {
                CreatedDate = x.CreatedDate,
                FormCategory = x.FormCategory,
                FormName = x.FormName,
                FormCategoryText = x.FormCategory.GetDescription(),
                FormDescription = x.FormDescription,
                FormPriority = x.FormPriority,
                FormType = x.FormType,
                FormDesign = x.FormDesign,
                FormPriorityText = x.FormPriority.GetDescription(),
                FormTypeText = x.FormType.GetDescription(),
                Id = x.Id,
                IsActive = x.IsActive,
                JavaScriptCode = x.JavaScriptCode,
                Revision = x.Revision,
                WorkFlowDefinationId = x.WorkFlowDefinationId,
                WorkFlowName= x.WorkFlowDefination != null ? x.WorkFlowDefination.WorkflowName : null,
                ParentFormId=x.ParentFormId,
                CanEdit=x.CanEdit,
                ShowInMenu=x.ShowInMenu,
            }).ToListAsync();
            return dto;
        }
   
        [HttpGet("{id}")]
        public async Task<ActionResult<FormDataListDto>> GetById(string id)
        {

            var query = _formRepository.Where(x=>x.Id==new Guid(id)).Include(x => x.WorkFlowDefination);
            var dto = await query.Select(x => new FormDataListDto
            {
                CreatedDate = x.CreatedDate,
                
                FormCategory = x.FormCategory,
                FormName = x.FormName,
                FormCategoryText = x.FormCategory.GetDescription(),
                FormDescription = x.FormDescription,
                FormPriority = x.FormPriority,
                FormType = x.FormType,
                FormDesign = x.FormDesign,
                FormPriorityText = x.FormPriority.GetDescription(),
                FormTypeText = x.FormType.GetDescription(),
                Id = x.Id,
                IsActive = x.IsActive,
                JavaScriptCode = x.JavaScriptCode,
                Revision = x.Revision,
                WorkFlowDefinationId = x.WorkFlowDefinationId,
                WorkFlowName = x.WorkFlowDefination != null ? x.WorkFlowDefination.WorkflowName : null,
                ParentFormId=x.ParentFormId,
                CanEdit = x.CanEdit,
                ShowInMenu = x.ShowInMenu,
            }).FirstAsync();

            return dto;
        }

        // GET /api/products/5

        //de
        [HttpPost]
        public async Task<IActionResult> Save(FormDataInsertDto formDto)
        {
            var result = await _service.AddAsync(_mapper.Map<Form>(formDto));
            if (result.ParentFormId == null) {
                result.ParentFormId = result.Id;
                await _service.UpdateAsync(result);
            }
            

            return CreateActionResult(CustomResponseDto<FormDataInsertDto>.Success(204));

        }
        [HttpPut]
        public async Task<IActionResult> Update(FormDataUpdateDto formDto)
        {
            //var form = await _formRepository.GetByIdStringGuidAsync(formDto.Id);
            var form = _mapper.Map<Form>(formDto);
            //form.CanEdit = false;
            //form.ShowInMenu = false;
            await _service.UpdateAsync(form);

            return CreateActionResult(CustomResponseDto<FormDataUpdateDto>.Success(204));
        }

        // DELETE api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var form = await _service.GetByIdStringGuidAsync(id);


            await _service.RemoveAsync(form);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpGet("[action]/{guid}")]
        public async Task<ActionResult<List<FieldInfoDto>>> GetFieldByForm(Guid guid)
        {
            

            var form = await _service.GetByIdStringGuidAsync(guid);

            JToken token = JObject.Parse(form.FormDesign);


            List<FieldInfoDto> fieldInfos = new List<FieldInfoDto>();

            // JSON verisini JToken nesnesine dönüştürelim


            // "components" dizisinin her bir öğesini işleyelim
            foreach (JToken component in token["components"])
            {
                // Özelliklere erişim örnekleri
                string label = component["label"]?.ToString();
                string type = component["type"]?.ToString();
                string id = component["id"]?.ToString();

                fieldInfos.Add(new FieldInfoDto
                {
                    Id = id,
                    Label = label,
                    Type = type
                });
            }

            return fieldInfos;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddFormRuleEngine(FormRuleEngineDto dto)
        {
            var result =  await _formRuleEngineService.AddAsync(_mapper.Map<FormRuleEngineDto>(dto));
            return CreateActionResult(CustomResponseDto<FormRuleEngineDto>.Success(204));
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateFormRuleEngine(FormRuleEngineDto dto)
        {
            var result = await _formRuleEngineService.UpdateAsync(_mapper.Map<FormRuleEngineDto>(dto));
            return CreateActionResult(CustomResponseDto<FormRuleEngineDto>.Success(204));
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<FormDataListDto>>> GetFormListByMenu()
        {
            var forms = await _service.GetAllAsync();
            var dto = _mapper.Map<List<FormDataListDto>>(forms.Where(e => e.IsActive == 1));

            return dto;
        }
        [HttpGet("GetFormPrioritiesEnum")]
        public IActionResult GetFormPrioritiesEnum()
        {
            var values = Enum.GetValues(typeof(FormPriority))
                             .Cast<FormPriority>()
                             .Select(e => new
                             {
                                 Id = (int)e,
                                 Name = e.ToString(),
                                 Description = GetEnumDescription(e)
                             });

            return Ok(values);
        }
        [HttpGet("GetFormCategoriesEnum")]
        public IActionResult GetFormCategoriesEnum()
        {
            var values = Enum.GetValues(typeof(FormCategory))
                             .Cast<FormCategory>()
                             .Select(e => new
                             {
                                 Id = (int)e,
                                 Name = e.ToString(),
                                 Description = GetEnumDescription(e)
                             });

            return Ok(values);
        }
        [HttpGet("GetFormTypesEnum")]
        public IActionResult GetFormTypesEnum()
        {
            var values = Enum.GetValues(typeof(FormType))
                             .Cast<FormType>()
                             .Select(e => new
                             {
                                 Id = (int)e,
                                 Name = e.ToString(),
                                 Description = GetEnumDescription(e)
                             });

            return Ok(values);
        }
        private string GetEnumDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = field?.GetCustomAttribute<DescriptionAttribute>();

            return attribute?.Description ?? value.ToString();
        }


    }
}
