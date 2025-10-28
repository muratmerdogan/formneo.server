using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using formneo.core.DTOs.RoleForm;
using formneo.core.Services;

namespace formneo.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RoleTenantFormController : CustomBaseController
    {
        private readonly IRoleTenantFormService _service;
        private readonly IFormTenantRoleService _formRoleService;

        public RoleTenantFormController(IRoleTenantFormService service, IFormTenantRoleService formRoleService)
        {
            _service = service;
            _formRoleService = formRoleService;
        }

        // Ana grid: tüm rol-form kayıtlarını döner (aktif tenant filtresiyle)
        [HttpGet]
        public async Task<IEnumerable<FormTenantRoleListDto>> GetAll()
        {
            var roles = await _formRoleService.GetAllAsync();
            return roles.ToList();
        }

        [HttpGet("{formTenantRoleId}")]
        public async Task<RoleTenantFormDetailDto> Get(Guid formTenantRoleId)
        {
            return await _service.GetDetailByFormTenantRoleAsync(formTenantRoleId);
        }

        // Rol listesi (tenant filtreli)
        [HttpGet("roles")]
        public async Task<IEnumerable<formneo.core.DTOs.RoleForm.FormTenantRoleListDto>> GetRoles()
        {
            return await _formRoleService.GetAllAsync();
        }

        // Insert: rol adı + formlar (role id yok)
        [HttpPost("insert")]
        public async Task<ActionResult<Guid>> Insert(RoleTenantFormInsertDto dto)
        {
            var id = await _service.InsertAsync(dto);
            return Ok(id);
        }

        // Update: role id + formlar
        [HttpPost("update")]
        public async Task<ActionResult<Guid>> Update(RoleTenantFormUpdateDto dto)
        {
            var id = await _service.UpdateAsync(dto);
            return Ok(id);
        }
    }
}


