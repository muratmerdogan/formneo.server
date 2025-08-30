using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json.Linq;
using NLayer.Core.Services;
using vesa.api.Filters;
using vesa.core.DTOs;
using vesa.core.Models;
using vesa.core.Services;
using static NpgsqlTypes.NpgsqlTsQuery;

namespace vesa.api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FormRuleEngineController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IServiceWithDto<FormRuleEngine, FormRuleEngineDto> _service;

        public FormRuleEngineController(IMapper mapper,IServiceWithDto<FormRuleEngine, FormRuleEngineDto> formRuleEngineService)
        {
            _mapper = mapper;
            _service = formRuleEngineService;
        }
        /// GET api/products
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var forms = await _service.GetAllAsync();
            return CreateActionResult(forms);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            
            var form = await _service.GetByIdGuidAsync(new Guid(id));
            return CreateActionResult(form);
        }

        // GET /api/products/5

        //de


        [HttpPost]
        public async Task<IActionResult> Save(FormRuleEngineDto formDto)
        {
            var result = await _service.AddAsync(formDto);

            return CreateActionResult(CustomResponseDto<FormRuleEngineDto>.Success(204));

        }
        [HttpPut]
        public async Task<IActionResult> Update(FormRuleEngineDto formDto)
        {
            await _service.UpdateAsync(formDto);

            return CreateActionResult(CustomResponseDto<FormRuleEngineDto>.Success(204));
        }

        // DELETE api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            

            await _service.RemoveAsyncByGuid(id);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }


        [HttpGet("[action]/{nodeId}")]
        public async Task<IActionResult> GetRuleByNodeId(Guid nodeId)
        {
            var form = await _service.Find(e => e.NodeId == nodeId);



       
            return CreateActionResult(form);
        }



        //[HttpGet("[action]/{guid}")]


        //[HttpPost("[action]")]
        //public async Task<IActionResult> AddFormRuleEngine(FormRuleEngineDto dto)
        //{

        //    var result =  await _formRuleEngineService.AddAsync(_mapper.Map<FormRuleEngineDto>(dto));
        //    return CreateActionResult(CustomResponseDto<FormRuleEngineDto>.Success(204));
        //}

        //[HttpPut("[action]")]
        //public async Task<IActionResult> UpdateFormRuleEngine(FormRuleEngineDto dto)
        //{

        //    var result = await _formRuleEngineService.UpdateAsync(_mapper.Map<FormRuleEngineDto>(dto));
        //    return CreateActionResult(CustomResponseDto<FormRuleEngineDto>.Success(204));
        //}


    }
}
