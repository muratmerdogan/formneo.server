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
using formneo.core.Models;
using formneo.service.Services;
using formneo.api.Helper;
using formneo.core.Models.Security;

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
        private readonly IService<ProjectRelation> _projectRelationService;
        private readonly IProjectTeamMemberService _projectTeamMemberService;

        public ProjectsController(IProjectService projectService, IMapper mapper, IUserService userService, IService<ProjectRelation> projectRelationService, IProjectTeamMemberService projectTeamMemberService)
        {
            _projectService = projectService;
            _mapper = mapper;
            _userService = userService;
            _projectRelationService = projectRelationService;
            _projectTeamMemberService = projectTeamMemberService;
        }
        [HttpGet]
        [RequirePermission("Projects", Actions.View)]
        public async Task<IActionResult> GetAllProjectList()
        {
            var values = await _projectService.GetAllProductListAsync();
            return Ok(values);
        }
        [HttpPost]
        [RequirePermission("Projects", Actions.Create)]
        public async Task<IActionResult> CreateProject(CreateProjectDto dto)
        {
            var project= _mapper.Map<Project>(dto);
            string name = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;
            var user = await _userService.GetUserByNameAsync(name);
            project.UserId = user.Data.Id;
            await _projectService.AddAsync(project);

            // Parents (many-to-many via ProjectRelation)
            if (dto.ParentProjectIds != null && dto.ParentProjectIds.Count > 0)
            {
                var relations = dto.ParentProjectIds
                    .Where(pid => pid != Guid.Empty && pid != project.Id)
                    .Distinct()
                    .Select(pid => new ProjectRelation { ParentProjectId = pid, ChildProjectId = project.Id })
                    .ToList();
                if (relations.Count > 0)
                    await _projectRelationService.AddRangeAsync(relations);
            }

            // Managers (multiple) via ProjectTeamMember
            if (dto.ManagerIds != null && dto.ManagerIds.Count > 0)
            {
                var managers = dto.ManagerIds
                    .Where(uid => !string.IsNullOrWhiteSpace(uid))
                    .Distinct()
                    .Select(uid => new ProjectTeamMember { ProjectId = project.Id, UserId = uid, Role = "Manager", IsActive = true })
                    .ToList();
                if (managers.Count > 0)
                    await _projectTeamMemberService.AddRangeAsync(managers);
            }
            return Ok(dto);
        }
        [HttpPut]
        [RequirePermission("Projects", Actions.Update)]
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

            // Sync parents
            var currentRelations = await _projectRelationService.Where(r => r.ChildProjectId == existingProject.Id).ToListAsync();
            await _projectRelationService.RemoveRangeAsync(currentRelations);
            if (dto.ParentProjectIds != null && dto.ParentProjectIds.Count > 0)
            {
                var newRelations = dto.ParentProjectIds
                    .Where(pid => pid != Guid.Empty && pid != existingProject.Id)
                    .Distinct()
                    .Select(pid => new ProjectRelation { ParentProjectId = pid, ChildProjectId = existingProject.Id })
                    .ToList();
                if (newRelations.Count > 0)
                    await _projectRelationService.AddRangeAsync(newRelations);
            }

            // Sync managers
            var currentManagers = await _projectTeamMemberService.Where(m => m.ProjectId == existingProject.Id && m.Role == "Manager").ToListAsync();
            await _projectTeamMemberService.RemoveRangeAsync(currentManagers);
            if (dto.ManagerIds != null && dto.ManagerIds.Count > 0)
            {
                var newManagers = dto.ManagerIds
                    .Where(uid => !string.IsNullOrWhiteSpace(uid))
                    .Distinct()
                    .Select(uid => new ProjectTeamMember { ProjectId = existingProject.Id, UserId = uid, Role = "Manager", IsActive = true })
                    .ToList();
                if (newManagers.Count > 0)
                    await _projectTeamMemberService.AddRangeAsync(newManagers);
            }

            return Ok(existingProject);
        }
        [HttpDelete]
        [RequirePermission("Projects", Actions.Delete)]
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
