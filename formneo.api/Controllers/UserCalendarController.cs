using AutoMapper;
using Azure;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NLayer.Core.Services;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using formneo.api.Helper;
using formneo.core.DTOs;
using formneo.core.DTOs.PositionsDtos;
using formneo.core.DTOs.TaskManagement;
using formneo.core.DTOs.Ticket.TicketDepartments;
using formneo.core.DTOs.UserCalendar;
using formneo.core.EnumExtensions;
using formneo.core.Models;
using formneo.core.Models.TaskManagement;
using formneo.core.Models.Ticket;
using formneo.core.Services;
using formneo.service.Services;

namespace formneo.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserCalendarController : CustomBaseController
    {
        private readonly IServiceWithDto<UserCalendar, UserCalendarListDto> _userCalendarService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ITenantContext _tenantContext;
        private readonly IUserTenantService _userTenantService;
        private readonly UserManager<UserApp> _userManager;
        private readonly EmployeeLeaveApiService _leaveservice;
        private readonly IServiceWithDto<TicketDepartment, TicketDepartmensListDto> _ticketDepartments;
        private readonly DbNameHelper _dbNameHelper;
        List<Guid> result;
        public UserCalendarController(DbNameHelper dbNameHelper,IServiceWithDto<TicketDepartment, TicketDepartmensListDto> Service, IServiceWithDto<UserCalendar, UserCalendarListDto> userCalendarService, IMapper mapper, IUserService userService, UserManager<UserApp> userManager, EmployeeLeaveApiService leaveservice, ITenantContext tenantContext, IUserTenantService userTenantService)
        {
            _ticketDepartments = Service;
            _userCalendarService = userCalendarService;
            _mapper = mapper;
            _userService = userService;
            _userManager = userManager;
            _leaveservice = leaveservice;
            _dbNameHelper = dbNameHelper;
            _tenantContext = tenantContext;
            _userTenantService = userTenantService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserCalendarListDto>>> GetAllTasks()
        {
            try
            {
                var service = await _userCalendarService.Include();
                var list = service
                 .Select(e => new UserCalendarListDto
                 {
                     Id = e.Id,
                     Name = e.Name,
                     Description = e.Description,
                     StartDate = e.StartDate,
                     EndDate = e.EndDate,
                     CustomerRef = e.CustomerRef,
                     CustomerRefId = e.CustomerRefId.ToString(),
                     UserAppDto = _mapper.Map<UserAppDto>(e.UserApp),
                     UserAppId = e.UserAppId,
                     Color = e.Percentage != null
                            ? (int.Parse(e.Percentage) < 25 ? "success" :
                               int.Parse(e.Percentage) < 50 ? "#fdd835" :
                               int.Parse(e.Percentage) < 75 ? "warning" :
                               "error")
                            : "gray",
                     Percentage = e.Percentage,
                 }).ToList();

                //return Ok(_mapper.Map<List<TaskMngListDto>>(list));
                return list;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpGet("department/{id}")]
        public async Task<ActionResult<List<UserCalendarListDto>>> GetAllTasksByDepartmantId(Guid id)
        {
            try
            {
                var service = await _userCalendarService.Include();
                var list = service
                   .Where(e => e.UserApp != null && e.UserApp.TicketDepartmentId == id)

                 .Select(e => new UserCalendarListDto
                 {
                     Id = e.Id,
                     Name = e.Name,
                     Description = e.Description,
                     StartDate = e.StartDate,
                     EndDate = e.EndDate,
                     CustomerRef = e.CustomerRef,
                     CustomerRefId = e.CustomerRefId.ToString(),
                     UserAppDto = _mapper.Map<UserAppDto>(e.UserApp),
                     UserAppId = e.UserAppId,
                     Color = e.Percentage != null
                            ? (int.Parse(e.Percentage) < 25 ? "success" :
                               int.Parse(e.Percentage) < 50 ? "#fdd835" :
                               int.Parse(e.Percentage) < 75 ? "warning" :
                               "error")
                            : "gray",
                     Percentage = e.Percentage,
                     WorkLocation = e.WorkLocation
                 }).ToList();

                return list;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpGet("getByUsers")]
        public async Task<ActionResult<List<UserCalendarListDto>>> GetTasksByUsersByMonthly([FromQuery] int year, [FromQuery] int month, [FromQuery] List<string>? users = null)
        {
            try
            {
                if (users != null)
                {
                    #region İlk Ve Son Gün Hesaplama
                    DateTime monthStart = new DateTime(year, month, 1);
                    DateTime monthEnd = monthStart.AddMonths(1).AddDays(-1);
                    #endregion

                    var service = await _userCalendarService.Include();
                    var list = service
                             .Where(e => e.UserApp != null &&
                             (users.Contains(e.UserApp.Id)) &&
                            (
                                (e.StartDate >= monthStart && e.StartDate <= monthEnd) ||
                                (e.EndDate != null && e.EndDate >= monthStart && e.EndDate <= monthEnd) ||
                                (e.StartDate <= monthStart && e.EndDate != null && e.EndDate >= monthEnd)
                            ))
                               .Select(e => new UserCalendarListDto
                               {
                                   Id = e.Id,
                                   Name = e.Name,
                                   Description = e.Description,
                                   StartDate = e.StartDate,
                                   EndDate = e.EndDate,
                                   CustomerRef = e.CustomerRef,
                                   CustomerRefId = e.CustomerRefId.ToString(),
                                   UserAppDto = _mapper.Map<UserAppDto>(e.UserApp),
                                   UserAppId = e.UserAppId,
                                   Color = e.Percentage != null
                                            ? (int.Parse(e.Percentage) <= 25 ? "success" :
                                               int.Parse(e.Percentage) <= 50 ? "#fdd835" :
                                               int.Parse(e.Percentage) <= 75 ? "warning" :
                                               "error")
                                            : "gray",
                                   Percentage = e.Percentage,
                                   WorkLocation = e.WorkLocation,
                                   IsAvailable = e.IsAvailable,
                               }).ToList();

                    return list;
                }
                return null;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<UserCalendarListDto>> GetTaskById(Guid id)
        {
            try
            {
                var service = await _userCalendarService.Include();
                var task = service
                 .Where(e => e.Id == id)
                 .Select(e => new UserCalendarListDto
                 {
                     Id = e.Id,
                     Name = e.Name,
                     Description = e.Description,
                     StartDate = e.StartDate,
                     EndDate = e.EndDate,
                     CustomerRef = e.CustomerRef,
                     CustomerRefId = e.CustomerRefId.ToString(),
                     UserAppDto = _mapper.Map<UserAppDto>(e.UserApp),
                     UserAppId = e.UserAppId,
                     Color = e.Percentage != null
                            ? (int.Parse(e.Percentage) < 25 ? "success" :
                               int.Parse(e.Percentage) < 50 ? "#ffeb3b" :
                               int.Parse(e.Percentage) < 75 ? "warning" :
                               "error")
                            : "gray",
                     Percentage = e.Percentage,
                     IsAvailable = e.IsAvailable,
                 }).FirstOrDefault();

                if (task == null)
                {
                    return NotFound("Task not found.");
                }

                return task;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertTask(UserCalendarInsertDto dto)
        {
            try
            {
                switch (Convert.ToInt32(dto.Percentage))
                {
                    case 0:
                        dto.Percentage = "0";
                        break;
                    case 1:
                        dto.Percentage = "25";
                        break;
                    case 2:
                        dto.Percentage = "50";
                        break;
                    case 3:
                        dto.Percentage = "75";
                        break;
                    case 4:
                        dto.Percentage = "100";
                        break;
                    default:
                        dto.Percentage = "0";
                        break;
                }
                var newTask = await _userCalendarService.AddAsync(_mapper.Map<UserCalendarListDto>(dto));


                // Task sahibine, proje yönetim ekibine ve ekip yöneticisine mail gönder
                var user = await _userManager.Users.Where(e => e.Id == dto.UserAppId).Select(e => new { e.TicketDepartmentId, e.Email }).FirstOrDefaultAsync();

                var departments = await _ticketDepartments.Include();
                departments = departments.Include(x => x.SubDepartments);
                var managerEmail = await departments.Where(e => e.Id == user.TicketDepartmentId).Select(e => e.Manager.Email).FirstOrDefaultAsync();

                List<string> toList = new List<string>();

                // Task sahibini ekle
                toList.Add(user.Email);
                // Ekip yöneticisini ekle
                if (!toList.Contains(managerEmail))
                {
                    toList.Add(managerEmail);
                }
                // Proje yönetimini ekle
                var managementdept = departments.Where(e => e.DeparmentCode == "#78A12888").Select(e => e.Id).FirstOrDefault();
                var mngUsers = await _userManager.Users.Where(e => e.TicketDepartmentId == managementdept).Select(e => e.Email).ToListAsync();
                foreach (var email in mngUsers)
                {
                    if (!toList.Contains(email))
                    {
                        toList.Add(email);
                    }
                }

                var service = await _userCalendarService.Include();
                var task = service
                 .Where(e => e.Id == newTask.Data.Id)
                 .Select(e => new UserCalendarListDto
                 {
                     Id = e.Id,
                     Name = e.Name,
                     StartDate = e.StartDate,
                     EndDate = e.EndDate,
                     CustomerRef = e.CustomerRef,
                     CustomerRefId = e.CustomerRefId.ToString(),
                     UserAppDto = _mapper.Map<UserAppDto>(e.UserApp),
                     UserAppId = e.UserAppId,
                     IsAvailable = e.IsAvailable,
                 }).FirstOrDefault();

                var subject = "Ekip Planlama - Yeni Görev Bildirimi";
                await SendTaskMail(subject, toList, task);



                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, ex.Message));
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTask(UserCalendarUpdateDto dto)
        {
            try
            {
                var service = await _userCalendarService.Include();
                var task = await service.Where(e => e.Id == dto.Id).FirstOrDefaultAsync();

                if (task == null)
                {
                    return NotFound("Task not found.");
                }
                switch (Convert.ToInt32(dto.Percentage))
                {
                    case 0:
                        dto.Percentage = "0";
                        break;
                    case 1:
                        dto.Percentage = "25";
                        break;
                    case 2:
                        dto.Percentage = "50";
                        break;
                    case 3:
                        dto.Percentage = "75";
                        break;
                    case 4:
                        dto.Percentage = "100";
                        break;
                    default:
                        dto.Percentage = "0";
                        break;
                }
                task.Name = dto.Name;
                task.Description = dto.Description;
                task.StartDate = dto.StartDate;
                task.EndDate = dto.EndDate;
                task.CustomerRefId = string.IsNullOrWhiteSpace(dto.CustomerRefId)
                ? (Guid?)null
                : Guid.Parse(dto.CustomerRefId);
                task.UserAppId = dto.UserAppId;
                task.Percentage = dto.Percentage;
                task.WorkLocation = dto.WorkLocation;
                task.IsAvailable = dto.IsAvailable ?? false;

                await _userCalendarService.UpdateAsync(_mapper.Map<UserCalendarListDto>(task));

                return Ok("Updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating task: {ex.Message}");
            }
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveTask(Guid id)
        {
            try
            {
                var existTask = await _userCalendarService.GetByIdGuidAsync(id);

                if (existTask == null)
                {
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "Task bilgisi bulunamadı"));
                }

                await _userCalendarService.RemoveAsyncByGuid(id);

                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, ex.Message));
            }
        }

        [HttpGet("getUsersByDepartmentAndLevel")]
        public async Task<ActionResult<List<UserAppDto>>> GetUsersByDepartmentAndLevel([FromQuery] Guid? ticketDepartmentId = null, [FromQuery] int? level = null, [FromQuery] List<string>? userId = null)
        {
            try
            {
                var loginName = User.Identity.Name;
                var loginUser = await _userService.GetUserByNameAsync(loginName);

                if (loginUser == null)
                {
                    return NotFound(new { message = "Kullanıcı bulunamadı" });
                }

                var query = _userManager.Users.AsQueryable();

                var departments = await _ticketDepartments.Include();
                departments = departments.Include(x => x.SubDepartments);


                //Giriş yapan kullanıcının takvimde tüm departmanları görme yetkisi var ise
                bool perm = false;
                if (_tenantContext?.CurrentTenantId != null)
                {
                    var ut = await _userTenantService.GetByUserAndTenantAsync(loginUser.Data.Id, _tenantContext.CurrentTenantId.Value);
                    perm = ut != null && ut.HasOtherDeptCalendarPerm;
                }
                // tenant context yoksa tenant-bazlı izin değerlendirilemez

                if (perm == true)
                {


                    if (ticketDepartmentId != null)
                    {
                        query = query.Where(e => e.TicketDepartmentId == ticketDepartmentId);
                    }

                    if (level != null)
                    {
                        var userLevel = (UserLevel)level;
                        query = query.Where(e => e.UserLevel == userLevel);
                    }

                    if (userId != null && userId.Any())
                    {
                        query = query.Where(e => userId.Contains(e.Id));
                    }

                    var list = query.Where(e=>e.isBlocked != true && e.isTestData != true).ToList();

                    return _mapper.Map<List<UserAppDto>>(list);
                }

                //Giriş yapan kullanıcının takvimde tüm departmanları görme yetkisi yok ise
                else
                {

                    //Kullanıcının yönetici mi?
                    var managerDepartments = await departments.Where(e => e.ManagerId == loginUser.Data.Id).Include(e => e.SubDepartments).ToListAsync();

                    //yönetici ise ticketDepartmentid değeri yukarıdaki departmanlara ait olan tüm kullanıcılar
                    if (managerDepartments.Count() > 0)
                    {
                        result = new List<Guid>();
                        var allDepartments = await GetAllSubDepartmentIds(departments, managerDepartments, result);

                        var loginUserDto = _userManager.Users.Where(e => e.Email == loginName).FirstOrDefault();

                        if (level != null)
                        {
                            var userLevel = (UserLevel)level;

                            query = query
                           .Where(user => user.TicketDepartmentId.HasValue &&
                           allDepartments.Contains(user.TicketDepartmentId.Value)
                           && user.UserLevel == userLevel);

                            var list = query.Where(e => e.isBlocked != true && e.isTestData != true).ToList();
                            if (!list.Any(x => x.Id == loginUserDto.Id))
                            {
                                list.Add(loginUserDto);
                            }

                            return _mapper.Map<List<UserAppDto>>(list);
                        }
                        else
                        {
                            query = query.Where(user => user.TicketDepartmentId.HasValue &&
                           allDepartments.Contains(user.TicketDepartmentId.Value));

                            var list = query.Where(e => e.isBlocked != true && e.isTestData != true).ToList();
                            if (!list.Any(x => x.Id == loginUserDto.Id))
                            {
                                list.Add(loginUserDto);
                            }

                            return _mapper.Map<List<UserAppDto>>(list);
                        }

                    }

                    //yönetici değil ise sadece kendisi
                    else
                    {
                        query = query
                           .Where(user => user.Id == loginUser.Data.Id);

                        var list = query.Where(e => e.isBlocked != true && e.isTestData != true).ToList();

                        return _mapper.Map<List<UserAppDto>>(list);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        private async Task<List<Guid>> GetAllSubDepartmentIds(IQueryable<TicketDepartment> data, List<TicketDepartment> departments, List<Guid> result)
        {
            foreach (var department in departments)
            {
                result.Add(department.Id);

                var newDepartment = await data.Where(e => e.Id == department.Id).FirstOrDefaultAsync();

                if (newDepartment.SubDepartments != null && newDepartment.SubDepartments.Any())
                {
                    await GetAllSubDepartmentIds(data, newDepartment.SubDepartments, result);
                }
            }

            return result;
        }


        [HttpGet("WorkLocations")]
        public IActionResult GetWorkLocations()
        {
            var values = Enum.GetValues(typeof(WorkLocation))
                             .Cast<WorkLocation>()
                             .Select(e => new
                             {
                                 Id = (int)e,
                                 Name = e.ToString(),
                                 Description = e.GetDescription()
                             });

            return Ok(values);
        }

        [HttpGet("GetTasksByWeekly")]
        public async Task<ActionResult<List<UserWeeklyTasksDto>>> GetTasksByWeekly([FromQuery] int? yil, [FromQuery] int? hafta, [FromQuery] List<string>? users = null, [FromQuery] Guid? ticketDepartmentId = null, [FromQuery] int? userLevel = null, [FromQuery] List<string>? daysOfWeek = null, [FromQuery] List<int>? percentage = null, [FromQuery] bool? isGetAll = false)
        {
            try
            {
                #region Hafta Hesaplama
                DateTime bugun = DateTime.Today;
                Calendar takvim = CultureInfo.InvariantCulture.Calendar;

                // Geçerli yıl ve hafta hesapla (gelen null ise)
                int gecerliYil = yil ?? bugun.Year;
                int gecerliHafta = hafta ?? takvim.GetWeekOfYear(
                    bugun,
                    CalendarWeekRule.FirstFourDayWeek,
                    DayOfWeek.Monday);

                // 4 Ocak'ı baz alarak Pazartesi gününü bul
                DateTime ocakDord = new DateTime(gecerliYil, 1, 4);
                DateTime haftaninPazartesi = ocakDord
                    .AddDays(-((int)ocakDord.DayOfWeek == 0 ? 6 : (int)ocakDord.DayOfWeek - 1)) // Pazartesi'ye çek
                    .AddDays((gecerliHafta - 1) * 7);

                DateTime haftaninPazar = haftaninPazartesi.AddDays(6);
                #endregion


                var service = await _userCalendarService.Include();

                // Ortak tarih aralığı filtresi
                Expression<Func<UserCalendar, bool>> dateFilter = e =>
                    (e.StartDate >= haftaninPazartesi && e.StartDate <= haftaninPazar) ||
                    (e.EndDate >= haftaninPazartesi && e.EndDate <= haftaninPazar) ||
                    (e.StartDate <= haftaninPazartesi && e.EndDate >= haftaninPazar);

                var query = service.Where(dateFilter);


                //KULLANICI YETKİLİ Mİ KONTROLÜ
                var loginName = User.Identity.Name;
                var loginUser = await _userService.GetUserByNameAsync(loginName);
                bool perm2 = false;
                if (_tenantContext?.CurrentTenantId != null)
                {
                    var ut2 = await _userTenantService.GetByUserAndTenantAsync(loginUser.Data.Id, _tenantContext.CurrentTenantId.Value);
                    perm2 = ut2 != null && ut2.HasOtherDeptCalendarPerm;
                }
                // tenant context yoksa tenant-bazlı izin değerlendirilemez

                if (perm2 == true)
                {
                    // Departman filtresi
                    if (ticketDepartmentId != null)
                        query = query.Where(e => e.UserApp.TicketDepartmentId == ticketDepartmentId);
                }

                // Test departmanını dahil etme
                query = query.Where(e => e.UserApp.TicketDepartmentId != Guid.Parse("9c2a54d3-87f3-47d2-b2fb-4c543a69707e"));

                // Kullanıcı filtresi
                if (users != null && users.Any())
                    query = query.Where(e => users.Contains(e.UserAppId));

                // Kullanıcı seviyesi filtresi
                if (userLevel != null)
                {
                    var level = (UserLevel)userLevel;
                    if (level != null)
                        query = query.Where(e => e.UserApp.UserLevel == level);
                }

                // Select ve ToListAsync
                var rawTasks = await query
                    .Select(e => new
                    {
                        e.Id,
                        e.Name,
                        e.Description,
                        e.StartDate,
                        e.EndDate,
                        e.CustomerRef,
                        e.CustomerRefId,
                        e.UserApp,
                        e.UserAppId,
                        e.Percentage,
                        e.WorkLocation,
                        e.IsAvailable
                    })
                    .ToListAsync();

                var tasks = await Task.WhenAll(rawTasks.Select(async e => new UserCalendarListDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    CustomerRef = e.CustomerRef,
                    CustomerRefId = e.CustomerRefId.ToString(),
                    //UserAppDtoWithoutPhoto = _mapper.Map<UserAppDtoWithoutPhoto>(e.UserApp),
                    UserAppDtoWithoutPhoto = new UserAppDtoWithoutPhoto
                    {
                        Id = e.UserAppId,
                        Email = e.UserApp.Email,
                        FirstName = e.UserApp.FirstName,
                        LastName = e.UserApp.LastName,
                        TicketDepartmentId = e.UserApp.TicketDepartmentId,
                        PositionId = e.UserApp.PositionId,
                    },
                    UserAppId = e.UserAppId,
                    Color = e.Percentage != null
                            ? (int.Parse(e.Percentage) < 25 ? "success" :
                               int.Parse(e.Percentage) < 50 ? "info" :
                               int.Parse(e.Percentage) < 75 ? "warning" :
                               "error")
                            : "gray",
                    Percentage = e.Percentage,
                    WorkLocation = e.WorkLocation,
                    IsAvailable = e.IsAvailable,
                    DaysOfWeek = Enumerable.Range(0, 7)
                    .Select(i =>
                    {
                        var currentDate = haftaninPazartesi.AddDays(i);
                        return (e.StartDate.HasValue && e.EndDate.HasValue &&
                                e.StartDate.Value.Date <= currentDate && e.EndDate.Value.Date >= currentDate);
                    })
                    .ToList()
                }).ToList());

                var groupedTasks = tasks
                  .GroupBy(t => Guid.Parse(t.UserAppDtoWithoutPhoto.Id))

                   .Select(g => new UserWeeklyTasksDto
                   {
                       UserId = g.Key,
                       Tasks = g.ToList()
                   })
                   .ToList();

                // Gün ve yoğunluk girildiyse
                if (daysOfWeek != null && percentage != null)
                {
                    List<DateTime> parsedDaysOfWeek = daysOfWeek
                                    .Select(dayStr =>
                                    {
                                        if (int.TryParse(dayStr, out int day))
                                        {
                                            return haftaninPazartesi.AddDays(day);
                                        }
                                        else
                                        {
                                            return (DateTime?)null;
                                        }
                                    })
                                    .Where(date => date.HasValue)
                                    .Select(date => date.Value)
                                    .ToList();

                    // Kullanıcıya göre günlük task gruplaması
                    var groupedTasks2 = tasks
                     .GroupBy(t => Guid.Parse(t.UserAppId))
                     .Select(g => new
                     {
                         UserId = g.Key,
                         DailyTasks = parsedDaysOfWeek
                             .Select(day => new
                             {
                                 Date = day,
                                 Tasks = g.Where(t => t.StartDate.Value.Date <= day && t.EndDate.Value.Date >= day).ToList()
                             })
                             .Where(dt => dt.Tasks.Count > 0)
                             .ToList()
                     })
                     .ToList();

                    Func<int, int, bool> isInRange = (value, range) =>
                    {
                        return range switch
                        {
                            1 => value >= 0 && value <= 25,
                            2 => value > 25 && value <= 50,
                            3 => value > 50 && value <= 99,
                            4 => value > 99,
                            _ => false
                        };
                    };

                    //Kullanıcıya göre günlük percentage hesaplaması
                    var userDailyPercentageSums = groupedTasks2
                      .Select(user => new
                      {
                          UserId = user.UserId,
                          DailyPercentages = user.DailyTasks
                              .Select(day => new
                              {
                                  Date = day.Date,
                                  TotalPercentage = day.Tasks.Sum(t => Convert.ToInt32(t.Percentage))
                              })
                              .Where(dp => percentage.Any(range => isInRange(dp.TotalPercentage, range)))
                              .ToList()
                      })
                      .ToList();


                    var userss = userDailyPercentageSums.Where(e => e.DailyPercentages.Count >= daysOfWeek.Count()).Select(e => e.UserId).ToList();

                    var newGroupedTasks = tasks
                      .Where(e => userss.Contains(Guid.Parse(e.UserAppId)))
                      .GroupBy(e => e.UserAppId)
                      .Select(g => new UserWeeklyTasksDto
                      {
                          UserId = Guid.Parse(g.Key),
                          Tasks = g.ToList()
                      })
                      .ToList();
                    //HERKESİ GETİR E TIKLANMADIYSA
                    if (isGetAll == false)
                    {
                        newGroupedTasks = newGroupedTasks.OrderBy(x => (x.Tasks != null && x.Tasks.Count > 0)
                           ? x.Tasks[0]?.UserAppDtoWithoutPhoto?.FirstName
                           : null)
                       .ToList();
                        return newGroupedTasks;
                    }
                    //HERKESİ GETİR E TIKLANDIYSA
                    else
                    {
                        var kullanicilar = await _userService.GetAllUserWithOutPhoto();
                        var kullaniciIds = kullanicilar.Data.Where(e => e.WorkCompanyId == "2e5c2ba5-3eb8-414d-8bc7-08dd44716854").Select(e => e.Id);
                        var servicee = await _userCalendarService.Include();

                        // Ortak tarih aralığı filtresi
                        Expression<Func<UserCalendar, bool>> dateFilterr = e =>
                            (e.StartDate >= haftaninPazartesi && e.StartDate <= haftaninPazar) ||
                            (e.EndDate >= haftaninPazartesi && e.EndDate <= haftaninPazar) ||
                            (e.StartDate <= haftaninPazartesi && e.EndDate >= haftaninPazar);

                        var queryy = servicee.Where(dateFilterr);
                        queryy.ToList();
                        var mevcutKullaniciIds = queryy.Select(q => q.UserAppId).Distinct();
                        // kullaniciIds içinde olup, mevcutKullaniciIds içinde olmayanları filtrele
                        var olmayanKullanicilar = kullaniciIds.Except(mevcutKullaniciIds).ToList();
                        //                var olanKullanicilar = kullaniciIds.Intersect(mevcutKullaniciIds).ToList();

                        //                var olanKullanicilarDetayli = kullanicilar.Data
                        //.Where(k => olanKullanicilar.Contains(k.Id))
                        //.ToList();
                        // Önce olmayan kullanıcılar için boş UserWeeklyTasksDto nesneleri oluştur
                        //var user = kullanicilar.Data.Where(e => e.isBlocked == false).FirstOrDefault(u => u.Id == id);
                        var eksikKullaniciDtos = olmayanKullanicilar
                         .Select(id =>
                         {
                             var user = kullanicilar.Data.FirstOrDefault(u => !u.isBlocked && u.Id == id);
                             return new UserWeeklyTasksDto
                             {
                                 UserId = Guid.Parse(id),
                                 FirstName = user?.FirstName,
                                 LastName = user?.LastName,
                                 Email = user?.Email,
                                 TicketDepartmentId = user?.TicketDepartmentId.ToString(),
                                 PositionId = user?.PositionId.ToString(),
                                 Tasks = null
                             };
                         })
                         .OrderBy(x => x.FirstName)
                         .ToList();

                        // Sonra mevcut listeyle birleştir
                        newGroupedTasks = newGroupedTasks
                       .OrderBy(x => (x.Tasks != null && x.Tasks.Count > 0)
                           ? x.Tasks[0]?.UserAppDtoWithoutPhoto?.FirstName
                           : null)
                       .ToList();
                        newGroupedTasks.AddRange(eksikKullaniciDtos);
                        return newGroupedTasks;
                    }

                }
                //HERKESİ GETİR E TIKLANMADIYSA
                if (isGetAll == false)
                {
                    groupedTasks = groupedTasks.OrderBy(x => (x.Tasks != null && x.Tasks.Count > 0)
                        ? x.Tasks[0]?.UserAppDtoWithoutPhoto?.FirstName
                        : null)
                    .ToList();
                    return groupedTasks;
                }
                //HERKESİ GETİR E TIKLANDIYSA
                else
                {
                    var kullanicilar = await _userService.GetAllUserWithOutPhoto();
                    var kullaniciIds = kullanicilar.Data.Where(e => e.WorkCompanyId == "2e5c2ba5-3eb8-414d-8bc7-08dd44716854" || e.WorkCompanyText == "formneo Danışmanlık").Select(e => e.Id);
                    var servicee = await _userCalendarService.Include();

                    // Ortak tarih aralığı filtresi
                    Expression<Func<UserCalendar, bool>> dateFilterr = e =>
                        (e.StartDate >= haftaninPazartesi && e.StartDate <= haftaninPazar) ||
                        (e.EndDate >= haftaninPazartesi && e.EndDate <= haftaninPazar) ||
                        (e.StartDate <= haftaninPazartesi && e.EndDate >= haftaninPazar);

                    var queryy = servicee.Where(dateFilterr);
                    queryy.ToList();
                    var mevcutKullaniciIds = queryy.Select(q => q.UserAppId).Distinct();
                    // kullaniciIds içinde olup, mevcutKullaniciIds içinde olmayanları filtrele
                    var olmayanKullanicilar = kullaniciIds.Except(mevcutKullaniciIds).ToList();
                    //                var olanKullanicilar = kullaniciIds.Intersect(mevcutKullaniciIds).ToList();

                    //                var olanKullanicilarDetayli = kullanicilar.Data
                    //.Where(k => olanKullanicilar.Contains(k.Id))
                    //.ToList();
                    // Önce olmayan kullanıcılar için boş UserWeeklyTasksDto nesneleri oluştur

                    var eksikKullaniciDtos = olmayanKullanicilar
                         .Select(id =>
                         {
                             var user = kullanicilar.Data.FirstOrDefault(u => !u.isBlocked && u.Id == id);
                             return new UserWeeklyTasksDto
                             {
                                 UserId = Guid.Parse(id),
                                 FirstName = user?.FirstName,
                                 LastName = user?.LastName,
                                 TicketDepartmentId = user?.TicketDepartmentId.ToString(),
                                 PositionId = user?.PositionId.ToString(),
                                 Email = user?.Email,
                                 Tasks = null
                             };
                         })
                         .OrderBy(x => x.FirstName)
                         .ToList();

                    // Sonra mevcut listeyle birleştir
                    groupedTasks = groupedTasks
                    .OrderBy(x => (x.Tasks != null && x.Tasks.Count > 0)
                        ? x.Tasks[0]?.UserAppDtoWithoutPhoto?.FirstName
                        : null)
                    .ToList();
                    groupedTasks.AddRange(eksikKullaniciDtos);
                    return groupedTasks;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }


        [HttpGet("GetEmployeeLeavesByWeekly")]
        public async Task<ActionResult<HolidaysAndLeavesDto>> GetEmployeeLeavesByWeekly([FromQuery] int year, [FromQuery] int week, [FromQuery] List<string>? userMails)
        {
            try
            {
                #region İlk Ve Son Gün Hesaplama
                DateTime jan4 = new DateTime(year, 1, 4);
                int daysOffset = DayOfWeek.Monday - jan4.DayOfWeek;
                if (daysOffset > 0) daysOffset -= 7;
                DateTime firstMonday = jan4.AddDays(daysOffset);
                DateTime weekStart = firstMonday.AddDays((week - 1) * 7);
                DateTime weekEnd = weekStart.AddDays(6);
                #endregion

                var dto = new LeaveRequestDto
                {
                    Mails = userMails,
                    Begda = weekStart.ToString("yyyyMMdd"),
                    Endda = weekEnd.ToString("yyyyMMdd")
                };

                var resultLeaves = await _leaveservice.GetEmployeeLeaves(dto);
                var resultHolidays = await _leaveservice.GetPublicHolidays(weekStart, weekEnd);


                var results = new HolidaysAndLeavesDto
                {
                    leaves = resultLeaves,
                    holidays = resultHolidays
                };

                return results != null ? Ok(results) : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }

        }

        [HttpGet("GetEmployeeLeavesByMonthly")]
        public async Task<ActionResult<HolidaysAndLeavesDto>> GetEmployeeLeavesByMonthly([FromQuery] int year, [FromQuery] int month, [FromQuery] List<string>? userMails)
        {
            try
            {
                #region İlk Ve Son Gün Hesaplama
                DateTime monthStart = new DateTime(year, month, 1);
                DateTime monthEnd = monthStart.AddMonths(1).AddDays(-1);
                #endregion

                var dto = new LeaveRequestDto
                {
                    Mails = userMails,
                    Begda = monthStart.ToString("yyyyMMdd"),
                    Endda = monthEnd.ToString("yyyyMMdd")
                };

                var resultLeaves = await _leaveservice.GetEmployeeLeaves(dto);
                var resultHolidays = await _leaveservice.GetPublicHolidays(monthStart, monthEnd);


                var results = new HolidaysAndLeavesDto
                {
                    leaves = resultLeaves,
                    holidays = resultHolidays
                };

                return results != null ? Ok(results) : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }

        }

        [HttpGet("check-otherDeptperm")]
        public async Task<ActionResult<TicketPermDto>> CheckHaveOtherDeptPermAsync()
        {
            var loginName = User.Identity.Name;
            var loginUser = _userManager.Users.Where(e => e.Email == loginName).Select(e => new { e.Id, e.FirstName, e.LastName }).FirstOrDefault();

            if (loginUser == null)
            {
                return NotFound(new { message = "Kullanıcı bulunamadı" });
            }

            bool perm = false;
            if (_tenantContext?.CurrentTenantId != null)
            {
                var ut = await _userTenantService.GetByUserAndTenantAsync(loginUser.Id, _tenantContext.CurrentTenantId.Value);
                if (ut != null)
                {
                    perm = ut.HasOtherDeptCalendarPerm;
                }
            }
            // tenant context yoksa tenant-bazlı izin değerlendirilemez

            var sendData = new TicketPermDto
            {
                Id = loginUser.Id,
                Name = $"{loginUser.FirstName} {loginUser.LastName}",
                Perm = perm
            };

            return sendData;
        }

        [HttpGet("check-userIsManager")]
        public async Task<ActionResult<TicketPermDto>> CheckUserIsManagerAsync()
        {
            var loginName = User.Identity.Name;
            var loginUser = _userManager.Users.Where(e => e.Email == loginName).Select(e => new { e.Id, e.FirstName, e.LastName }).FirstOrDefault();

            if (loginUser == null)
            {
                return NotFound(new { message = "Kullanıcı bulunamadı" });
            }

            var departments = await _ticketDepartments.Include();
            departments = departments.Include(x => x.SubDepartments);
            var managerCount = (await departments.Where(e => e.ManagerId == loginUser.Id).ToListAsync()).Count();

            var sendData = new TicketPermDto
            {
                Id = loginUser.Id,
                Name = $"{loginUser.FirstName} {loginUser.LastName}",
                Perm = managerCount > 0 ? true : false
            };

            return sendData;
        }

        private async Task SendTaskMail(string subject, List<string> emails, UserCalendarListDto newTask)
        {
            var startDate = newTask.StartDate.Value.ToString("dd.MM.yyyy");
            var endDate = newTask.EndDate.Value.ToString("dd.MM.yyyy");

            string customerRow = string.Empty;
            if (newTask.CustomerRef != null)
            {
                customerRow = $@"
        <tr>
            <td style=""border:1px solid #ddd; padding:8px;"">Müşteri</td>
            <td style=""border:1px solid #ddd; padding:8px;"">{newTask.CustomerRef.Name}</td>
        </tr>";
            }
            string availabilityRow = string.Empty;
            if (newTask.IsAvailable)
            {
                availabilityRow = $@"
        <tr>
            <td style=""border:1px solid #ddd; padding:8px;"">Müsaitlik Durumu</td>
            <td style=""border:1px solid #ddd; padding:8px;"">Müsait</td>
        </tr>";
            }
            string nameRow = string.Empty;
            if (!string.IsNullOrWhiteSpace(newTask.Name))
            {
                availabilityRow = $@"
        <tr>
            <td style=""border:1px solid #ddd; padding:8px;"">Görev Başlığı</td>
            <td style=""border:1px solid #ddd; padding:8px;"">{newTask.Name}</td>
        </tr>";
            }
            string dbName = _dbNameHelper.GetDatabaseName();
            string emailBody = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset=""UTF-8"">
    <title>{subject}</title>
</head>
<body style=""margin:0; padding:0; font-family: Arial, sans-serif; background-color:#f4f7fc;"">
    <table role=""presentation"" width=""100%"" cellspacing=""0"" cellpadding=""0"" border=""0"" style=""background-color:#f4f7fc; padding:20px;"">
        <tr>
            <td align=""center"">
                <table role=""presentation"" width=""800"" cellspacing=""0"" cellpadding=""0"" border=""0"" style=""background-color:#ffffff; border-radius:8px; box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.1);"">
                    <!-- HEADER -->
                    <td style=""background-color:white;"">
                        <table style=""width: 100%; table-layout: fixed; display: inline-table;"">
                            <tr>
                                <td style=""background-color: white; padding:12px; width: auto;"">
                                    <img src=""{formneoLogo.Logo}"" alt=""Logo"" width=""100"" height=""60"" style=""display: block; width: 100%; height: auto;"">
                                </td>
                                <td style=""background-color: white; padding:12px; width: auto;"">
                                    <img src=""{formneoLogo.ColorImg}"" alt=""Logo"" width=""650"" height=""20"" style=""display: block; width: 100%; height: auto;"">
                                </td>
                            </tr>
                        </table>
                    </td>

                    <!-- CONTENT -->
                    <tr>
                        <td style=""padding:20px;"">
                            <h2 style="" font-size:20px; margin-bottom:10px;"">{subject}</h2>
                            <table width=""100%"" cellspacing=""0"" cellpadding=""10"" border=""0"" style=""border-collapse: collapse;"">
                                <tr>
                                    <td style=""border:1px solid #ddd; padding:8px;"">Kullanıcı</td>
                                    <td style=""border:1px solid #ddd; padding:8px;"">{newTask.UserAppDto.FirstName} {newTask.UserAppDto.LastName}</td>
                                </tr>
                                {customerRow}
                                {nameRow}
                                {availabilityRow}
                                <tr>
                                    <td style=""border:1px solid #ddd; padding:8px;"">Başlangıç Tarihi</td>
                                    <td style=""border:1px solid #ddd; padding:8px;"">{startDate}</td>
                                </tr>
                                <tr>
                                    <td style=""border:1px solid #ddd; padding:8px;"">Bitiş Tarihi</td>
                                    <td style=""border:1px solid #ddd; padding:8px;"">{endDate}</td>
                                </tr>
                            </table>
                            <p style=""color:#0073e6;""><strong>Destek Sistemine Giriş için: https://support.formneo-tech.com/</strong></p>
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
</html>
";

            if (emails != null)
            {
                utils.Utils.SendMail($"formneo Bilgilendirme E-postası", emailBody, emails, null);
            }
        }
    }
}
