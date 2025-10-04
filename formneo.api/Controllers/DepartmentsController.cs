using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using formneo.core.DTOs;
using formneo.core.Models;
using formneo.core.Services;

namespace formneo.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentsService;
        private readonly IMapper _mapper;

        public DepartmentsController(IDepartmentService departmentsService, IMapper mapper)
        {
            _departmentsService = departmentsService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartmentsList()
        {
            var values = await _departmentsService.GetDepermantListAsync();
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateDepartments(DepartmentsInsertDto dto)
        {
            var entity=_mapper.Map<Departments>(dto);
            await _departmentsService.AddAsync(entity);
            return Ok(entity);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateDepartments(DepartmentsUpdateDto dto)
        {
            var entity = await _departmentsService.GetByIdStringGuidAsync(dto.Id);
            entity.Code = dto.Code;
            entity.DepartmentText = dto.DepartmentText;
            await _departmentsService.UpdateAsync(entity);
            return Ok(entity);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdDepartments(Guid id)
        {
            var value = await _departmentsService.GetByIdStringGuidAsync(id);
            return Ok(value);
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveDepartments(Guid id)
        {
            var value = await _departmentsService.GetByIdStringGuidAsync(id);
            await _departmentsService.RemoveAsync(value);
            return Ok(value);
        }

    }
}
