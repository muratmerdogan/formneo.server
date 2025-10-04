using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using formneo.core.DTOs;
using formneo.core.DTOs.ProjectDtos;
using formneo.core.EnumExtensions;
using formneo.core.Models;
using formneo.core.Services;
using formneo.service.Services;

namespace formneo.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProjectsController : CustomBaseController
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public ProjectsController(IProjectService projectService, IMapper mapper, IUserService userService)
        {
            _projectService = projectService;
            _mapper = mapper;
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProjectList()
        {
            var values = await _projectService.GetAllProductListAsync();
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProject(CreateProjectDto dto)
        {
            var project= _mapper.Map<Project>(dto);
            string name = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;
            var user = await _userService.GetUserByNameAsync(name);
            project.UserId = user.Data.Id;
            await _projectService.AddAsync(project);
            return Ok(dto);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProject(UpdateProjectDto dto)
        {
            string name = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;
            var user = await _userService.GetUserByNameAsync(name);
            var existingProject = await _projectService.GetByIdStringGuidAsync(dto.Id);
            if (existingProject == null)
            {
                return NotFound("Project not found.");
            }

            existingProject.UserId = user.Data.Id;
            existingProject.Name = dto.Name;
            existingProject.Description = dto.Description;
            existingProject.CategoryId = dto.CategoryId;
            existingProject.Photo = dto.Photo;
            existingProject.ProjectLearn= dto.ProjectLearn;
            existingProject.ProjectGain= dto.ProjectGain;
            existingProject.ProjectTags = dto.ProjectTags;
            existingProject.StartDate = dto.StartDate;
            existingProject.EndDate = dto.EndDate;

            

            await _projectService.UpdateAsync(existingProject);

            return Ok(existingProject);
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveProject(Guid id)
        {
            var value = await _projectService.GetByIdStringGuidAsync(id);
            await _projectService.RemoveAsync(value);
            return Ok("proje silindi");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdProject(Guid id)
        {
            var value = await _projectService.GetByIdStringGuidAsync(id);
            return Ok(value);
        }

        [HttpGet("GetByUserIdProjectList")]
        public async Task<IActionResult> GetByUserIdProjectList(string userId)
        {
            var value = await _projectService.GetByUserProductListAsync(userId);
            return Ok(value);
        }

        [HttpGet("GetByProjectIdProjectList")]
        public async Task<ActionResult<Project>> GetByProjectIdProjectList(string id)
        {
            var value = await _projectService.GetByIdStringGuidAsync(new Guid(id));
            return Ok(value);
        }


        [HttpGet("GetUserProject")]
        public async Task<IActionResult> GetUserProjectList()
        {
            string name = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;
            var user=await _userService.GetUserByNameAsync(name);
            var values = await _projectService.GetByUserProductListAsync(user.Data.Id);
            return Ok(values);
        }
        [HttpGet("GetCategory")]
        [AllowAnonymous]
        public IActionResult GetCategories()
        {
            var categories = Enum.GetValues(typeof(Category))
            .Cast<Category>()
            .Select(c => new
            {
                Name = c.ToString(),
                Description = c.GetDescription()
            })
            .ToList();

            return Ok(categories);
        }
    }
}
