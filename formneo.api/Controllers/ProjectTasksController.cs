using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vesa.core.DTOs;
using vesa.core.DTOs.Budget.SF;
using vesa.core.DTOs.ProjectTasks;
using vesa.core.DTOs.TaskManagement;
using vesa.core.DTOs.TicketProjects;
using vesa.core.Models;
using vesa.core.Services;
using vesa.service.Services;

namespace vesa.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProjectTasksController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IProjectTasksService _projectTasksService;
        private readonly ITicketProjectsService _ticketProjectsService;
        private readonly UserManager<UserApp> _userManager;
        private readonly IUserService _userService;
        private readonly ITenantContext _tenantContext;
        private readonly IUserTenantService _userTenantService;

        public ProjectTasksController(IUserService userService,UserManager<UserApp> userManager, ITicketProjectsService ticketProjectsService, IProjectTasksService projectTasksService, IMapper mapper, ITicketServices ticketService, ITenantContext tenantContext, IUserTenantService userTenantService)
        {
            _ticketProjectsService = ticketProjectsService;
            _mapper = mapper;
            _projectTasksService = projectTasksService;
            _userManager = userManager;
            _userService = userService;
            _tenantContext = tenantContext;
            _userTenantService = userTenantService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProjectTasksListDto>>> GetAll([FromQuery] string projectId)
        {
            try
            {

                var data = await _projectTasksService.Where(e => e.ProjectId == Guid.Parse(projectId)).ToListAsync();

                // Tüm userId'leri toplayıp tek sorguda user'ları getireceğiz
                var allUserIds = data
                    .Where(d => !string.IsNullOrWhiteSpace(d.UserIds))
                    .SelectMany(d => d.UserIds.Split(';', StringSplitOptions.RemoveEmptyEntries))
                    .Select(id => id.Trim())
                    .Distinct()
                    .ToList();

                var usersDict = await _userManager.Users
                    .Where(u => allUserIds.Contains(u.Id))
                    .Select(u => new UserAppDtoOnlyNameId
                    {
                        Id = u.Id,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        UserName = u.UserName
                    })
                    .ToDictionaryAsync(u => u.Id);

                var result = data.OrderBy(d => d.CreatedDate).Select(d => new ProjectTasksListDto
                {
                    Id = d.Id,
                    taskId = d.taskId,
                    Name = d.Name,
                    StartDate = d.StartDate,
                    ProjectId = d.ProjectId,
                    ParentId = d.ParentId,
                    Duration = d.Duration,
                    Progress = d.Progress,
                    Predecessor = d.Predecessor,
                    Milestone = d.Milestone,
                    Notes = d.Notes,
                    IsManual = d.IsManual,
                    Users = string.IsNullOrWhiteSpace(d.UserIds)
                ? new List<UserAppDtoOnlyNameId>()
                : d.UserIds
                    .Split(';', StringSplitOptions.RemoveEmptyEntries)
                    .Select(uid => uid.Trim())
                    .Where(uid => usersDict.ContainsKey(uid))
                    .Select(uid => usersDict[uid])
                    .ToList()
                }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectTasksListDto>> GetById(Guid id)
        {
            try
            {
                var data = await _projectTasksService.Where(e => e.Id == id).FirstOrDefaultAsync();

                if (data == null)
                {
                    return NotFound("Project task not found.");
                }

                List<UserAppDtoOnlyNameId> users = new List<UserAppDtoOnlyNameId>();
                if (!string.IsNullOrWhiteSpace(data.UserIds))
                {
                    List<string> userIds = data.UserIds.Split(';', StringSplitOptions.RemoveEmptyEntries).Select(u => u.Trim()).ToList();
                    users = await _userManager.Users.Where(e => userIds.Contains(e.Id)).Select(e => new UserAppDtoOnlyNameId
                    {
                        Id = e.Id,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        UserName = e.UserName
                    }).ToListAsync();
                }
                var result = new ProjectTasksListDto
                {
                    Id = data.Id,
                    taskId = data.taskId,
                    Name = data.Name,
                    StartDate = data.StartDate,
                    ProjectId = data.ProjectId,
                    ParentId = data.ParentId,
                    Duration = data.Duration,
                    Progress = data.Progress,
                    Predecessor = data.Predecessor,
                    Milestone = data.Milestone,
                    Notes = data.Notes,
                    IsManual = data.IsManual,
                    Users = users,
                };

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        // Task eklerken taskın bağlı olduğu projenin kullanıcıları arasından seçim yapılmalı
        [HttpGet("GetProjectUsers")]
        public async Task<ActionResult<List<UserAppDtoOnlyNameId>>> GetProjectUsers([FromQuery] string projectId)
        {
            try
            {
                var data = await _ticketProjectsService.Where(e => e.Id == Guid.Parse(projectId)).FirstOrDefaultAsync();

                if (data == null)
                {
                    return NotFound("Project not found.");
                }

                List<UserAppDtoOnlyNameId> users = new List<UserAppDtoOnlyNameId>();
                if (!string.IsNullOrWhiteSpace(data.UserIds))
                {
                    List<string> userIds = data.UserIds.Split(';', StringSplitOptions.RemoveEmptyEntries).Select(u => u.Trim()).ToList();
                    users = await _userManager.Users.Where(e => userIds.Contains(e.Id)).Select(e => new UserAppDtoOnlyNameId
                    {
                        Id = e.Id,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        UserName = e.UserName
                    }).ToListAsync();
                }
                return users;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpGet("GetProjectUsersWithPhoto")]
        public async Task<ActionResult<List<UserAppDto>>> GetProjectUsersWithPhoto([FromQuery] string projectId, [FromQuery] int page = 1)
        {
            try
            {
                var data = await _ticketProjectsService
                    .Where(e => e.Id == Guid.Parse(projectId))
                    .FirstOrDefaultAsync();

                if (data == null)
                {
                    return NotFound("Project not found.");
                }

                List<UserAppDto> userList = new List<UserAppDto>();

                if (!string.IsNullOrWhiteSpace(data.UserIds))
                {
                    List<string> userIds = data.UserIds
                        .Split(';', StringSplitOptions.RemoveEmptyEntries)
                        .Select(u => u.Trim())
                        .ToList();

                    var usersQuery = _userManager.Users
                        .Where(e => userIds.Contains(e.Id))
                        .OrderBy(e => e.FirstName);

                    int pageSize = 5;
                    var pagedUsers = await usersQuery
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();

                    userList = _mapper.Map<List<UserAppDto>>(pagedUsers);
                }

                return Ok(userList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        // Proje yönetimi sayfası için
        [HttpGet("GetTasksByUser")]
        public async Task<ActionResult<List<string>>> GetTasksByUser([FromQuery] string userId, [FromQuery] string projectId)
        {
            try
            {
                var data = await _ticketProjectsService.Where(e => e.Id == Guid.Parse(projectId)).FirstOrDefaultAsync();

                if (data == null)
                {
                    return NotFound("Project not found.");
                }

                // Proje çalışanı var ise
                if (!string.IsNullOrWhiteSpace(data.UserIds))
                {
                    List<string> userIds = data.UserIds.Split(';', StringSplitOptions.RemoveEmptyEntries).Select(u => u.Trim()).ToList();

                    //İstenen çalışan projede var ise
                    if (userIds.Contains(userId))
                    {
                        var tasks = await _projectTasksService
                         .Where(e => e.ProjectId == Guid.Parse(projectId)
                                     && !string.IsNullOrEmpty(e.UserIds)
                                     && e.UserIds.Contains(userId))
                         .Select(e => e.Name)
                         .ToListAsync();
                        return tasks;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        // Kullanıcının projelerini görebilmesi için
        [HttpGet("GetUserTasks")]
        public async Task<ActionResult<List<ProjectInfoDto>>> GetUserTasks([FromQuery] string userId)
        {
            try
            {

                var data = await _projectTasksService
                           .Where(e => e.UserIds != null && e.UserIds.Contains(userId))
                           .Include(e => e.TicketProjects)
                           .ThenInclude(e => e.WorkCompany)
                           .Select(e => new ProjectInfoDto
                           {
                               Id = e.Id,
                               CompanyName = e.TicketProjects.WorkCompany.Name,
                               ProjectName = e.TicketProjects.Name +
              (string.IsNullOrWhiteSpace(e.TicketProjects.SubProjectName) ? "" : " - " + e.TicketProjects.SubProjectName),

                               TaskName = e.Name,
                               Notes = e.Notes,
                               StartDate = e.StartDate,
                               Duration = e.Duration,
                               Progress = e.Progress,
                           })
                           .ToListAsync();

                return data;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        // Kullanıcıların projelerini görebilir mi?
        [HttpGet("HasPerm")]
        public async Task<ActionResult<bool>> HasPerm()
        {
            try
            {
                var loginName = User.Identity.Name;
                var user = await _userManager.Users.Where(e => e.Email == loginName).Select(e => new { e.Id }).FirstOrDefaultAsync();
                if (user == null) return false;

                bool perm = false;
                if (_tenantContext?.CurrentTenantId != null)
                {
                    var ut = await _userTenantService.GetByUserAndTenantAsync(user.Id, _tenantContext.CurrentTenantId.Value);
                    perm = ut != null && ut.HasOtherDeptCalendarPerm;
                }
                // tenant context yoksa tenant-bazlı izin değerlendirilemez

                return perm;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }


        [HttpPost]
        public async Task<IActionResult> InsertTask(ProjectTasksInsertDto dto)
        {
            try
            {
                if (string.IsNullOrEmpty(dto.Name) || dto.ProjectId == Guid.Empty || dto.StartDate == null)
                {
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(400, "Zorunlu alanları doldurunuz."));
                }

                string? users = null;
                if (dto.Users != null && dto.Users.Any())
                {
                    users = string.Join(";", dto.Users.Select(u => u.Id));
                }
                var newTask = new ProjectTasks
                {
                    Name = dto.Name,
                    StartDate = dto.StartDate,
                    taskId = dto.taskId,
                    Duration = dto.Duration,
                    Progress = dto.Progress,
                    Predecessor = dto.Predecessor,
                    ParentId = dto.ParentId,
                    Milestone = dto.Milestone,
                    Notes = dto.Notes,
                    IsManual = dto.IsManual,
                    ProjectId = dto.ProjectId,
                    UserIds = users,
                };
                await _projectTasksService.AddAsync(newTask);

                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, ex.Message));
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTask(ProjectTasksUpdateDto dto)
        {
            try
            {
                if (string.IsNullOrEmpty(dto.Name) || dto.ProjectId == Guid.Empty || dto.StartDate == null)
                {
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(400, "Zorunlu alanları doldurunuz."));
                }
                var data = await _projectTasksService.Where(e => e.Id == dto.Id).FirstOrDefaultAsync();

                if (data == null)
                {
                    return NotFound("Project task bulunamadı.");
                }

                string? users = null;
                if (dto.Users != null && dto.Users.Any())
                {
                    users = string.Join(";", dto.Users.Select(u => u.Id));
                }

                data.Name = dto.Name;
                data.ProjectId = dto.ProjectId;
                data.StartDate = dto.StartDate;
                data.Duration = dto.Duration;
                data.Progress = dto.Progress;
                data.Predecessor = dto.Predecessor;
                data.ParentId = dto.ParentId;
                data.Milestone = dto.Milestone;
                data.Notes = dto.Notes;
                data.IsManual = dto.IsManual;
                data.taskId = dto.taskId;
                data.UserIds = users;

                await _projectTasksService.UpdateAsync(data);

                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the ticket project: {ex.Message}");
            }
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteTask(List<string> ids)
        {
            try
            {
                var data = await _projectTasksService.Where(e => ids.Contains(e.Id.ToString())).ToListAsync();

                if (data == null)
                {
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "Projeler bulunamadı"));
                }

                await _projectTasksService.RemoveRangeAsync(data);

                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, ex.Message));
            }
        }


    }
}
