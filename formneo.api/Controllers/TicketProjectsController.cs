using AutoMapper;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using vesa.core.DTOs;
using vesa.core.DTOs.TaskManagement;
using vesa.core.DTOs.TicketProjects;
using vesa.core.Models;
using vesa.core.Models.Ticket;
using vesa.core.Services;
using WorkflowCore.Primitives;

namespace vesa.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TicketProjectsController : CustomBaseController
    {
        private readonly ITicketProjectsService _ticketProjectsService;
        private readonly IMapper _mapper;
        private readonly ITicketServices _ticketService;
        private readonly UserManager<UserApp> _userManager;
        private readonly IUserService _userService;
        private readonly IProjectTasksService _projectTasksService;
        public TicketProjectsController(IProjectTasksService projectTasksService, IUserService userService, UserManager<UserApp> userManager, ITicketProjectsService ticketProjectsService, IMapper mapper, ITicketServices ticketService)
        {
            _ticketProjectsService = ticketProjectsService;
            _mapper = mapper;
            _ticketService = ticketService;
            _userManager = userManager;
            _userService = userService;
            _projectTasksService = projectTasksService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TicketProjectsListDto>>> GetAll([FromQuery] Guid? workCompanyId)
        {
            try
            {
                IQueryable<TicketProjects> query = _ticketProjectsService.Where(e => true).Include(e => e.ProjectCategory);

                if (workCompanyId.HasValue)
                {
                    query = query.Where(e => e.WorkCompanyId == workCompanyId);
                }

                query = query.Include(e => e.WorkCompany);

                var list = await query.ToListAsync();

                List<TicketProjectsListDto> resultList = new List<TicketProjectsListDto>();

                foreach (var data in list)
                {
                    List<string>? userIds = null;
                    List<UserApp>? users = null;
                    if (!string.IsNullOrEmpty(data.UserIds))
                    {
                        userIds = data.UserIds.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList();
                        users = await _userManager.Users.Where(e => userIds.Contains(e.Id)).ToListAsync();
                    }

                    var res = new TicketProjectsListDto
                    {
                        Id = data.Id,
                        Name = data.Name,
                        SubProjectName = data.SubProjectName,
                        Description = data.Description,
                        IsActive = data.IsActive,
                        WorkCompanyId = data.WorkCompanyId,
                        WorkCompany = data.WorkCompany,
                        ManagerId = data.ManagerId,
                        Manager = data.Manager != null ? new UserApp
                        {
                            Id = data.Manager.Id,
                            FirstName = data.Manager.FirstName,
                            LastName = data.Manager.LastName,
                        } : null,
                        UserIds = userIds,
                        CreatedDate = data.CreatedDate,
                        ProjectCategoryId = data.ProjectCategoryId,
                        ProjectCategory = _mapper.Map<ProjectCategoriesListDto>(data.ProjectCategory),
                        Users = users != null && users.Any()
                                ? users
                                    .Select(u => new UserAppDto
                                    {
                                        Id = u.Id,
                                        FirstName = u.FirstName,
                                        LastName = u.LastName,
                                        Email = u.Email,
                                        //photo = u.photo,
                                    })
                                    .ToList()
                                : new List<UserAppDto>(),
                        Risks = data.Risks,
                        ReportsUrl = data.ReportsUrl
                    };

                    resultList.Add(res);
                }

                return resultList.OrderBy(e => e.Name).ToList();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpGet("GetActiveProjects")]
        public async Task<ActionResult<List<TicketProjectsListDto>>> GetActiveProjects([FromQuery] Guid? workCompanyId)
        {
            try
            {
                IQueryable<TicketProjects> query = _ticketProjectsService.Where(e => e.IsActive == true).Include(e => e.ProjectCategory).Include(e => e.Manager);

                if (workCompanyId.HasValue)
                {
                    query = query.Where(e => e.WorkCompanyId == workCompanyId);
                }

                query = query.Include(e => e.WorkCompany);

                var list = await query.ToListAsync();

                List<TicketProjectsListDto> resultList = new List<TicketProjectsListDto>();

                foreach (var data in list)
                {
                    List<string>? userIds = null;
                    List<UserApp>? users = null;
                    if (!string.IsNullOrEmpty(data.UserIds))
                    {
                        userIds = data.UserIds.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList();
                        users = await _userManager.Users
                                .Where(e => userIds.Contains(e.Id))
                                .Select(u => new UserApp
                                {
                                    Id = u.Id,
                                    FirstName = u.FirstName,
                                    LastName = u.LastName,
                                    Email = u.Email

                                })
                                .ToListAsync();
                    }

                    var res = new TicketProjectsListDto
                    {
                        Id = data.Id,
                        Name = data.Name,
                        SubProjectName = data.SubProjectName,
                        Description = data.Description,
                        IsActive = data.IsActive,
                        WorkCompanyId = data.WorkCompanyId,
                        WorkCompany = data.WorkCompany,
                        ManagerId = data.ManagerId,
                        Manager = data.Manager != null ? new UserApp
                        {
                            Id = data.Manager.Id,
                            FirstName = data.Manager.FirstName,
                            LastName = data.Manager.LastName,
                        } : null,
                        UserIds = userIds,
                        CreatedDate = data.CreatedDate,
                        ProjectCategoryId = data.ProjectCategoryId,
                        ProjectCategory = _mapper.Map<ProjectCategoriesListDto>(data.ProjectCategory),
                        Users = users != null && users.Any()
                                ? users
                                    .Select(u => new UserAppDto
                                    {
                                        Id = u.Id,
                                        FirstName = u.FirstName,
                                        LastName = u.LastName,
                                        Email = u.Email,
                                        //photo = u.photo,
                                    })
                                    .ToList()
                                : new List<UserAppDto>(),
                        Risks = data.Risks,
                        ReportsUrl = data.ReportsUrl
                    };

                    resultList.Add(res);
                }

                return resultList.OrderBy(e => e.Name).ToList();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TicketProjectsListDto>> GetById(Guid id)
        {
            try
            {
                var data = await _ticketProjectsService.Where(e => e.Id == id).Include(e => e.WorkCompany).Include(e => e.ProjectCategory).Include(e => e.Manager).FirstOrDefaultAsync();

                if (data == null)
                {
                    return NotFound("Ticket project not found.");
                }



                List<string>? userIds = null;
                List<UserApp>? users = null;
                if (!string.IsNullOrEmpty(data.UserIds))
                {
                    userIds = data.UserIds.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList();
                    users = await _userManager.Users.Where(e => userIds.Contains(e.Id)).ToListAsync();
                }

                var res = new TicketProjectsListDto
                {
                    Id = data.Id,
                    Name = data.Name,
                    SubProjectName = data.SubProjectName,
                    Description = data.Description,
                    IsActive = data.IsActive,
                    WorkCompanyId = data.WorkCompanyId,
                    WorkCompany = data.WorkCompany,
                    ManagerId = data.ManagerId,
                    Manager = data.Manager,
                    UserIds = userIds,
                    CreatedDate = data.CreatedDate,
                    ProjectCategory = _mapper.Map<ProjectCategoriesListDto>(data.ProjectCategory),
                    ProjectCategoryId = data.ProjectCategoryId,
                    Users = users != null && users.Any()
                            ? users
                                .Select(u => new UserAppDto
                                {
                                    Id = u.Id,
                                    FirstName = u.FirstName,
                                    LastName = u.LastName,
                                    Email = u.Email,
                                    photo = u.photo,
                                })
                                .ToList()
                            : new List<UserAppDto>(),
                    Risks = data.Risks,
                    ReportsUrl = data.ReportsUrl
                };

                return res;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertProject(TicketProjectsInsertDto dto)
        {
            try
            {

                if (string.IsNullOrEmpty(dto.Name) || !dto.WorkCompanyId.HasValue)
                {
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(400, "Proje adı ve şirket zorunludur."));
                }

                string? users = null;
                if (dto.UserIds != null && dto.UserIds.Any())
                {
                    users = string.Join(";", dto.UserIds);
                }

                var newProject = new TicketProjects
                {
                    Name = dto.Name,
                    SubProjectName = dto.SubProjectName,
                    Description = dto.Description,
                    IsActive = dto.IsActive ?? true,
                    WorkCompanyId = dto.WorkCompanyId.Value,
                    ManagerId = dto.ManagerId, // Sadece ID atıyoruz
                    UserIds = users,
                    Risks = dto.Risks,
                    ProjectCategoryId = dto.ProjectCategoryId,
                    ReportsUrl = dto.ReportsUrl,
                };

                var addedProject = await _ticketProjectsService.AddAsync(newProject);

                #region Task Kopyalama İşlemleri
                // Task kopyalama istendiyse
                if (!string.IsNullOrEmpty(dto.CopiedProjectId))
                {
                    // Kopyalanan projenin taskı var mı?
                    var hasData = await _projectTasksService.AnyAsync(e => e.ProjectId == Guid.Parse(dto.CopiedProjectId));

                    if (hasData)
                    {
                        // Kopyalanan projenin tasklarını al
                        var data = await _projectTasksService.Where(e => e.ProjectId == Guid.Parse(dto.CopiedProjectId)).ToListAsync();

                        // Task çalışanlarını da kopyalamak istediyse
                        if (dto.IsUserCopied == true)
                        {
                            // Kopyalanan projenin çalışanlarını al
                            var copiedProjectUsers = await _ticketProjectsService.Where(e => e.Id == Guid.Parse(dto.CopiedProjectId)).Select(e => e.UserIds).FirstOrDefaultAsync();

                            // Yeni projenin çalışanlarını ve kopyalanan projenin çalışanlarını kıyasla,
                            // eğer kopyalanan projede eksik kişi varsa ekle. Yoksa tasklar doğru çalışmaz.
                            var addedUserIds = !string.IsNullOrWhiteSpace(addedProject?.UserIds)
                                ? addedProject.UserIds.Split(';', StringSplitOptions.RemoveEmptyEntries).ToHashSet()
                                : new HashSet<string>();

                            var copiedUserIds = !string.IsNullOrWhiteSpace(copiedProjectUsers)
                                ? copiedProjectUsers.Split(';', StringSplitOptions.RemoveEmptyEntries)
                                : Array.Empty<string>();
                            // Eksik kişiler 
                            var missingUserIds = copiedUserIds
                                .Where(userId => !string.IsNullOrWhiteSpace(userId) && !addedUserIds.Contains(userId))
                                .ToList();

                            // EĞER KOPYALANAN PROJEDE OLUP YENİ PROJEDE OLMAYAN VARSA YENİYE EKLE
                            if (missingUserIds != null && missingUserIds.Any())
                            {
                                var existingUserIds = string.IsNullOrEmpty(addedProject.UserIds)
                                    ? ""
                                    : addedProject.UserIds + ";";

                                addedProject.UserIds = existingUserIds + string.Join(";", missingUserIds);
                            }

                        }

                        // Kopyalanan taskları kaydet
                        List<ProjectTasks> newTasks = new();

                        foreach (var item in data)
                        {
                            var newTask = new ProjectTasks
                            {
                                Name = item.Name,
                                StartDate = item.StartDate,
                                taskId = item.taskId,
                                Duration = item.Duration,
                                Progress = 0,
                                Predecessor = item.Predecessor,
                                ParentId = item.ParentId,
                                Milestone = item.Milestone,
                                Notes = item.Notes,
                                IsManual = item.IsManual,
                                ProjectId = addedProject.Id,
                                UserIds = null,
                            };

                            // Task çalışanlarını da kopyalamak istediyse VE kopyalanan proje taskında çalışan varsa
                            if (dto.IsUserCopied == true && !string.IsNullOrEmpty(item.UserIds))
                            {
                                newTask.UserIds = item.UserIds;
                            }
                            newTasks.Add(newTask);
                        }

                        await _projectTasksService.AddRangeAsync(newTasks);
                    }
                }
                #endregion

                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, ex.Message));
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProject(TicketProjectsUpdateDto dto)
        {
            try
            {
                if (string.IsNullOrEmpty(dto.Name) || !dto.WorkCompanyId.HasValue)
                {
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(400, "Proje adı ve şirket zorunludur."));
                }
                var existing = await _ticketProjectsService.Where(e => e.Id == dto.Id).FirstOrDefaultAsync();

                if (existing == null)
                {
                    return NotFound("Ticket project not found.");
                }

                existing.Name = dto.Name;
                existing.Description = dto.Description;
                existing.WorkCompanyId = dto.WorkCompanyId;
                existing.IsActive = dto.IsActive;
                existing.ManagerId = dto.ManagerId;
                existing.SubProjectName = dto.SubProjectName;
                existing.Risks = dto.Risks;
                existing.ProjectCategoryId = dto.ProjectCategoryId;
                existing.ReportsUrl = dto.ReportsUrl;

                string? users = null;
                if (dto.UserIds != null && dto.UserIds.Any())
                {
                    users = string.Join(";", dto.UserIds);
                    existing.UserIds = users;
                }

                // Değişiklikleri kaydet
                await _ticketProjectsService.UpdateAsync(existing);

                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                return StatusCode(500, $"An error occurred while updating the ticket project: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            try
            {
                var data = await _ticketProjectsService.GetByIdStringGuidAsync(id);

                if (data == null)
                {
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "Proje bulunamadı"));
                }

                //Ticket da bu projeyi foreign key olarak alanları null yap
                var tickets = await _ticketService.Where(e => e.TicketProjectId == id).ToListAsync();

                foreach (var ticket in tickets)
                {
                    ticket.TicketProjectId = null;
                    await _ticketService.UpdateAsync(ticket);
                }

                await _ticketProjectsService.RemoveAsync(data);

                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, ex.Message));
            }
        }

        [HttpGet("GetActiveProjectsOnlyName")]
        public async Task<ActionResult<List<TicketProjectsListDto>>> GetActiveProjectsOnlyName()
        {
            try
            {
                var data = await _ticketProjectsService.Where(e => e.IsActive == true)
                    .Select(e => new TicketProjectsListDto
                    {
                        Id = e.Id,
                        Name = e.Name,
                        SubProjectName = e.SubProjectName,
                    })
                    .ToListAsync();

                return data;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpGet("GetChangedProjects")]
        public async Task<ActionResult<List<ChangedTaskListDto>>> GetChangedProjects([FromQuery] DateTime startDate, DateTime endDate, bool? isNewProject)
        {
            try
            {

                // Taskı olan projelerin idleri
                var projectsWithTasks = await _projectTasksService.Where(e => true).Select(e => e.ProjectId).Distinct().ToListAsync();
                // Tüm projelerin idleri
                var allProjectIds = await _ticketProjectsService.Where(e => true).Select(p => p.Id).ToListAsync();


                // Taskı olmayan projelerin getirilmesi istendi mi?
                var result1 = new List<ChangedTaskListDto>();
                if (isNewProject == true)
                {
                    // Taskı olmayan projeler
                    var projectsWithoutTasks = allProjectIds.Where(id => !projectsWithTasks.Contains(id)).ToList();

                    // Taskı olmayan ve belirtilen tarihler arasında eklenen proje idler
                    var data1 = await _ticketProjectsService
                        .Where(p => projectsWithoutTasks.Contains(p.Id)
                                 && p.CreatedDate.Date >= startDate.Date
                                 && p.CreatedDate.Date <= endDate.Date)
                        .Select(e => e.Id)
                        .ToListAsync();

                    result1 = await _ticketProjectsService.Where(e => data1.Contains(e.Id))
                      .Include(e => e.WorkCompany)
                      .Include(e => e.Manager)
                      .Select(e => new ChangedTaskListDto
                      {
                          TaskId = null,
                          TaskName = null,
                          Progress = null,
                          ProjectName = string.IsNullOrEmpty(e.SubProjectName)
                            ? e.Name
                            : $"{e.Name} - {e.SubProjectName}",
                          CompanyName = e.WorkCompany != null ? e.WorkCompany.Name : "",
                          ManagerName = e.Manager != null
                        ? e.Manager.FirstName + " " + e.Manager.LastName : "",
                          ChangeType = "Proje eklendi.",
                          DateOfChange = e.CreatedDate,
                          AssignUserIds = null,
                          AssignUsers = null,
                      }).ToListAsync();
                }

                // Belirtilen tarihler arasında task değişikliği olan projeler
                var result2 = await _projectTasksService
                    .Where(e => (e.UpdatedDate.HasValue && e.UpdatedDate.Value.Date >= startDate.Date && e.UpdatedDate.Value.Date <= endDate.Date) || (e.CreatedDate.Date >= startDate.Date && e.CreatedDate.Date <= endDate.Date))
                    .Include(e => e.TicketProjects)
                    .ThenInclude(e => e.WorkCompany)
                    .Include(e => e.TicketProjects.Manager)
                    .Select(e => new ChangedTaskListDto
                    {
                        TaskId = e.Id,
                        TaskName = e.Name,
                        Progress = e.Progress,
                        ProjectName = string.IsNullOrEmpty(e.TicketProjects.SubProjectName)
                        ? e.TicketProjects.Name
                        : $"{e.TicketProjects.Name} - {e.TicketProjects.SubProjectName}",
                        CompanyName = e.TicketProjects.WorkCompany != null ? e.TicketProjects.WorkCompany.Name : "",
                        ManagerName = e.TicketProjects.Manager != null
                        ? e.TicketProjects.Manager.FirstName + " " + e.TicketProjects.Manager.LastName : "",
                        ChangeType = "Görev güncellendi.",
                        DateOfChange = e.UpdatedDate,
                        AssignUserIds = string.IsNullOrEmpty(e.UserIds)
                        ? new List<string>()
                        : e.UserIds.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList(),
                        AssignUsers = null,
                    }).ToListAsync();

                // Kullanıcı bilgilerini doldur
                var allUserIds = result2
                    .SelectMany(x => x.AssignUserIds)
                    .Distinct()
                    .ToList();

                var users = await _userManager.Users.Where(e => allUserIds.Contains(e.Id)).Select(e => new UserAppDtoOnlyNameId
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    UserName = e.UserName,
                }).ToListAsync();

                foreach (var task in result2)
                {
                    task.AssignUsers = users
                        .Where(u => task.AssignUserIds?.Contains(u.Id) == true)
                        .ToList();
                }

                var combinedList = result1.Concat(result2).OrderBy(x => x.CompanyName).ThenBy(x => x.ProjectName).ToList();

                return combinedList;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpGet("GetProjectsByUser")]
        public async Task<ActionResult<List<TicketProjectsListDto>>> GetActiveProjectsByUser([FromQuery] string userId)
        {
            try
            {
                var data = await _ticketProjectsService
                 .Where(e => e.IsActive == true
                             && !string.IsNullOrEmpty(e.UserIds)
                             && e.UserIds.Contains(userId))
                 .Include(e => e.Manager)
                 .Include(e => e.ProjectCategory)
                 .Include(e => e.WorkCompany)
                 .Select(e => new TicketProjectsListDto
                 {
                     Id = e.Id,
                     WorkCompany = e.WorkCompany != null ? new WorkCompany
                     {
                         Name = e.WorkCompany.Name
                     } : null,
                     Name = e.Name,
                     SubProjectName = e.SubProjectName,
                     Manager = e.Manager != null ? new UserApp
                     {
                         FirstName = e.Manager.FirstName,
                         LastName = e.Manager.LastName,
                     } : null,
                     ProjectCategory = e.ProjectCategory != null ? new ProjectCategoriesListDto
                     {
                         Name = e.ProjectCategory.Name,
                     } : null,
                     CreatedDate = e.CreatedDate,
                 })
                 .ToListAsync();

                return data;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

    }
}
