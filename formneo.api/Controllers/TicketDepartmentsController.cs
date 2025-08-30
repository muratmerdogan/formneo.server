using AutoMapper;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.ExtendedProperties;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NLayer.Core.Services;
using System.ComponentModel.Design;
using vesa.core.DTOs;
using vesa.core.DTOs.DepartmentUserDto;
using vesa.core.DTOs.Menu;
using vesa.core.DTOs.TaskManagement;
using vesa.core.DTOs.Ticket;
using vesa.core.DTOs.Ticket.TicketDepartments;
using vesa.core.DTOs.Ticket.TicketTeams;
using vesa.core.Models;
using vesa.core.Models.Ticket;
using vesa.core.Services;


namespace vesa.api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TicketDepartmentsController : CustomBaseController
    {

        private readonly IMapper _mapper;

        private readonly IUserService _userService;
        private readonly IMemoryCache _memoryCache;
        private readonly IServiceWithDto<TicketDepartment, TicketDepartmensListDto> _ticketDepartments;
        private readonly IServiceWithDto<DepartmentUser, DepartmentUserListDto> _departmentUserService;
        private readonly UserManager<UserApp> _userManager;
        private readonly IServiceWithDto<vesa.core.Models.WorkCompanyTicketMatris, WorkCompanyTicketMatrisListDto> _workCompanyMatrisService;
        private readonly ITenantContext _tenantContext;
        private readonly IUserTenantService _userTenantService;
        public TicketDepartmentsController(IMapper mapper, IServiceWithDto<TicketDepartment, TicketDepartmensListDto> Service, IUserService userService, IMemoryCache memoryCache, IServiceWithDto<DepartmentUser, DepartmentUserListDto> departmentUserService, UserManager<UserApp> userManager, IServiceWithDto<vesa.core.Models.WorkCompanyTicketMatris, WorkCompanyTicketMatrisListDto> workCompanyMatrisService, ITenantContext tenantContext, IUserTenantService userTenantService)
        {

            _ticketDepartments = Service;
            _mapper = mapper;
            _userService = userService;
            _memoryCache = memoryCache;
            _departmentUserService = departmentUserService;
            _userManager = userManager;
            _workCompanyMatrisService = workCompanyMatrisService;
            _tenantContext = tenantContext;
            _userTenantService = userTenantService;
        }
        [HttpGet]
        public async Task<List<TicketDepartmensListDto>> All()
        {

            //if (_memoryCache.TryGetValue("allDepartment", out var cachedValue))
            //{
            //    // Değer zaten önbellekte var, cachedValue'yu kullanabilirsiniz
            //    return cachedValue as List<TicketDepartmensListDto>;
            //}



            var departments = await _ticketDepartments.Include();
            //departments = departments.Include(e => e.Manager).Include(e => e.WorkCompany).Include(e => e.DepartmentUsers);
            departments = departments.Include(x => x.SubDepartments);
            departments = departments
                .OrderBy(e => e.DepartmentText)
                .Select(e => new TicketDepartment
                {
                    Id = e.Id,
                    DeparmentCode = e.DeparmentCode,
                    DepartmentText = e.DepartmentText,
                    IsActive = e.IsActive,
                    ManagerId = e.ManagerId,
                    WorkCompanyId = e.WorkCompanyId,
                    WorkCompany = e.WorkCompany,
                    ParentDepartmentId = e.ParentDepartmentId,
                    SubDepartments = e.SubDepartments.Where(y => y.ParentDepartmentId == e.Id).Select(y => new TicketDepartment
                    {
                        DepartmentText = y.DepartmentText,
                        DeparmentCode = y.DeparmentCode,
                        Id = y.Id,
                    }).ToList(),
                    IsVisibleInList = e.IsVisibleInList,
                    Manager = e.Manager != null ? new UserApp
                    {
                        Id = e.Manager.Id,
                        FirstName = e.Manager.FirstName,
                        LastName = e.Manager.LastName,
                    } : null
                });



            var departmentsList = _mapper.Map<List<TicketDepartmensListDto>>(departments);


            //_memoryCache.Set("allDepartment", departmentsList.ToList(),
            //     new MemoryCacheEntryOptions()
            //     .SetAbsoluteExpiration(TimeSpan.FromDays(1)));

            return departmentsList.ToList();
        }

        [HttpGet("AllOnlyName")]
        public async Task<List<TicketDepartmensListDto>> AllOnlyName()
        {

            //if (_memoryCache.TryGetValue("AllOnlyName", out var cachedValue))
            //{
            //    // Değer zaten önbellekte var, cachedValue'yu kullanabilirsiniz
            //    return cachedValue as List<TicketDepartmensListDto>;
            //}
            var departments = await _ticketDepartments.GetAllAsync();
            var listDepartments = departments.Data.Where(e=>e.IsActive == true && e.IsVisibleInList == true).OrderBy(e => e.DepartmentText).ToList();

            _memoryCache.Set("AllOnlyName", listDepartments.ToList(),
                 new MemoryCacheEntryOptions()
                 .SetAbsoluteExpiration(TimeSpan.FromDays(1)));

            return listDepartments.ToList();
        }

        [HttpGet("AllFilteredCompany")]
        public async Task<List<TicketDepartmensListDto>> AllFilteredCompany(string companyId)
        {

            //if (_memoryCache.TryGetValue("AllOnlyName", out var cachedValue))
            //{
            //    // Değer zaten önbellekte var, cachedValue'yu kullanabilirsiniz
            //    return cachedValue as List<TicketDepartmensListDto>;
            //}



            var departments = await _ticketDepartments.Include();
            departments = departments.Include(x => x.SubDepartments);
            departments = departments
                 .OrderBy(e => e.DepartmentText)
                .Where(e => e.WorkCompanyId == Guid.Parse(companyId) && e.IsVisibleInList == true && e.IsActive == true)
                .Select(e => new TicketDepartment
                {
                    Id = e.Id,
                    DeparmentCode = e.DeparmentCode,
                    DepartmentText = e.DepartmentText,
                    IsActive = e.IsActive,
                    ManagerId = e.ManagerId,
                    WorkCompanyId = e.WorkCompanyId,
                    WorkCompany = e.WorkCompany,
                    ParentDepartmentId = e.ParentDepartmentId,
                    SubDepartments = e.SubDepartments.Where(y => y.ParentDepartmentId == e.Id).Select(y => new TicketDepartment
                    {
                        DepartmentText = y.DepartmentText,
                        DeparmentCode = y.DeparmentCode,
                        Id = y.Id,
                    }).ToList(),
                    IsVisibleInList = e.IsVisibleInList,
                    Manager = e.Manager != null ? new UserApp
                    {
                        Id = e.Manager.Id,
                        FirstName = e.Manager.FirstName,
                        LastName = e.Manager.LastName,
                    } : null
                });



            var departmentsList = _mapper.Map<List<TicketDepartmensListDto>>(departments);

            return departmentsList;
        }


        [HttpPost]
        public async Task<IActionResult> Save(TicketDepartmensInsertDto dto)
        {
            try
            {

                // Aynı departman kodundan var mı kontrolü
                var existingDepartment = (await _ticketDepartments.Include()).Any(x => x.DeparmentCode == dto.DeparmentCode);

                if (existingDepartment)
                {
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(400, "Bu departman kodu zaten kullanılmakta."));
                }

                var result = await _ticketDepartments.AddAsync(_mapper.Map<TicketDepartmensListDto>(dto));

                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, ex.Message));
            }
        }
        [HttpPut]
        public async Task<ActionResult<TicketDepartmensUpdateDto>> Update(TicketDepartmensUpdateDto dto)
        {

            var ticketDptService = await _ticketDepartments.Include();


            var existingDpt = await ticketDptService.Include(e => e.DepartmentUsers).Where(e => e.Id == dto.Id).FirstOrDefaultAsync();

            if (existingDpt == null)
            {
                return NotFound("Department not found.");
            }

            // TicketTeam özelliklerini güncelle
            existingDpt.DeparmentCode = dto.DeparmentCode;
            existingDpt.DepartmentText = dto.DepartmentText;
            existingDpt.WorkCompanyId = new Guid(dto.WorkCompanyId);
            existingDpt.ManagerId = dto.ManagerId;
            existingDpt.IsActive = dto.IsActive;
            existingDpt.ParentDepartmentId = dto.ParentDepartmentId;
            existingDpt.IsVisibleInList = dto.IsVisibleInList;



            foreach (var item in existingDpt.DepartmentUsers)
            {
                await _departmentUserService.RemoveAsyncByGuid(item.Id);
            }

            existingDpt.DepartmentUsers.Clear();

            foreach (var teamMemberId in dto.DepartmentUsers)
            {

                existingDpt.DepartmentUsers.Add(new DepartmentUser
                {
                    UserId = teamMemberId.UserId.ToString(),
                }); ;
            }

            var result = await _ticketDepartments.UpdateAsync(_mapper.Map<TicketDepartmensListDto>(existingDpt));


            return dto;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var department = await _ticketDepartments.GetByIdGuidAsync(id);

                if (department == null)
                {
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "Departman bulunamadı"));
                }

                await _ticketDepartments.RemoveAsyncByGuid(id);

                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, ex.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TicketDepartmensListDto>> GetById(Guid id)
        {
            try
            {
                var departments = await _ticketDepartments.Include();


                //    var department = await departments
                //.Include(td => td.Manager)
                //.Include(td => td.DepartmentUsers)
                //    .ThenInclude(du => du.User)
                //.FirstOrDefaultAsync(td => td.Id == id);
                var department = departments
                   .Select(e => new TicketDepartment
                   {
                       Id = e.Id,
                       DeparmentCode = e.DeparmentCode,
                       DepartmentText = e.DepartmentText,
                       IsActive = e.IsActive,
                       ManagerId = e.ManagerId,
                       WorkCompanyId = e.WorkCompanyId,
                       WorkCompany = e.WorkCompany,
                       ParentDepartmentId = e.ParentDepartmentId,
                       SubDepartments = departments
                            .Where(sub => sub.ParentDepartmentId == e.Id)
                            .Select(sub => new TicketDepartment
                            {
                                Id = sub.Id,
                                DeparmentCode = sub.DeparmentCode,
                                DepartmentText = sub.DepartmentText,
                                IsActive = sub.IsActive,
                                ManagerId = sub.ManagerId
                            }).ToList(),
                       IsVisibleInList = e.IsVisibleInList,
                       DepartmentUsers = e.DepartmentUsers.Select(du => new DepartmentUser
                       {
                           Id = du.Id,
                           TicketDepartmentId = du.TicketDepartmentId,
                           TicketDepartment = du.TicketDepartment,
                           UserId = du.UserId,
                           User = new UserApp
                           {
                               Id = du.User.Id,
                               FirstName = du.User.FirstName,
                               LastName = du.User.LastName,
                               photo = du.User.photo
                           }
                       }).ToList(),
                       Manager = e.Manager != null ? new UserApp
                       {
                           Id = e.Manager.Id,
                           FirstName = e.Manager.FirstName,
                           LastName = e.Manager.LastName,
                       } : null
                   }).Where(td => td.Id == id).FirstOrDefault();



                if (department == null)
                {
                    return null;
                }

                // Department'ı DTO'ya dönüştür
                var departmentDto = _mapper.Map<TicketDepartmensListDto>(department);



                return departmentDto;
            }
            catch (Exception ex)
            {
                return null;

            }
        }

        [HttpGet("getAllWithUsers")]
        public async Task<List<TicketDepartmensListDto>> GetAllWithUsers()
        {
            var departments = await _ticketDepartments.Include();
            //departments = departments.Include(e => e.Manager).Include(e => e.WorkCompany).Include(e => e.DepartmentUsers);
            departments = departments.Include(x => x.SubDepartments);
            departments = departments
                 .OrderBy(e => e.DepartmentText)
                .Select(e => new TicketDepartment
                {
                    Id = e.Id,
                    DeparmentCode = e.DeparmentCode,
                    DepartmentText = e.DepartmentText,
                    DepartmentUsers = e.DepartmentUsers.Select(du => new DepartmentUser
                    {
                        Id = du.Id,
                        TicketDepartmentId = du.TicketDepartmentId,
                        TicketDepartment = du.TicketDepartment,
                        UserId = du.UserId,
                        User = new UserApp
                        {
                            Id = du.User.Id,
                            FirstName = du.User.FirstName,
                            LastName = du.User.LastName,
                            photo = du.User.photo,

                        }
                    }).ToList(),
                    IsActive = e.IsActive,
                    ManagerId = e.ManagerId,
                    WorkCompanyId = e.WorkCompanyId,
                    WorkCompany = e.WorkCompany,
                    ParentDepartmentId = e.ParentDepartmentId,
                    IsVisibleInList = e.IsVisibleInList,
                    SubDepartments = e.SubDepartments.Where(y => y.ParentDepartmentId == e.Id).Select(y => new TicketDepartment
                    {
                        DepartmentText = y.DepartmentText,
                        DeparmentCode = y.DeparmentCode,
                        Id = y.Id,
                    }).ToList(),
                    Manager = e.Manager != null ? new UserApp
                    {
                        Id = e.Manager.Id,
                        FirstName = e.Manager.FirstName,
                        LastName = e.Manager.LastName,
                    } : null
                });


            var departmentsList = _mapper.Map<List<TicketDepartmensListDto>>(departments);


            //_memoryCache.Set("allDepartment", departmentsList.ToList(),
            //     new MemoryCacheEntryOptions()
            //     .SetAbsoluteExpiration(TimeSpan.FromDays(1)));

            return departmentsList.ToList();
        }

        [HttpGet("getUsersByDepartmentId/{id}")]
        public async Task<List<UserAppDto>> GetUsersByDepartmentId(Guid id)
        {

            var list = _userManager.Users.Where(e => e.TicketDepartmentId == id).ToList();

            if (list == null)
                return null;

            return _mapper.Map<List<UserAppDto>>(list);

        }

        [HttpGet("getAllVisibleDepartments")]
        public async Task<List<TicketDepartmensListDto>> AllVisibleDepartments()
        {
            var loginName = User.Identity.Name;
            var user = await _userManager.Users.Where(e => e.Email == loginName).Select(e => new { e.Id, e.isSystemAdmin, e.WorkCompanyId }).FirstOrDefaultAsync();

            bool otherCompanyPerm = false;
            if (_tenantContext?.CurrentTenantId != null)
            {
                var ut = await _userTenantService.GetByUserAndTenantAsync(user.Id, _tenantContext.CurrentTenantId.Value);
                otherCompanyPerm = ut != null && ut.HasOtherCompanyPermission;
            }

            if (user.isSystemAdmin || otherCompanyPerm)
            {
                var departments = await _ticketDepartments.Include();
                departments = departments.Include(x => x.SubDepartments);
                departments = departments
                     .OrderBy(e => e.DepartmentText)
                    .Where(e => e.IsVisibleInList == true && e.IsActive == true)
                    .Select(e => new TicketDepartment
                    {
                        Id = e.Id,
                        DeparmentCode = e.DeparmentCode,
                        DepartmentText = e.DepartmentText,
                        IsActive = e.IsActive,
                        ManagerId = e.ManagerId,
                        WorkCompanyId = e.WorkCompanyId,
                        WorkCompany = e.WorkCompany,
                        ParentDepartmentId = e.ParentDepartmentId,
                        SubDepartments = e.SubDepartments.Where(y => y.ParentDepartmentId == e.Id).Select(y => new TicketDepartment
                        {
                            DepartmentText = y.DepartmentText,
                            DeparmentCode = y.DeparmentCode,
                            Id = y.Id,
                        }).ToList(),
                        IsVisibleInList = e.IsVisibleInList,
                        Manager = e.Manager != null ? new UserApp
                        {
                            Id = e.Manager.Id,
                            FirstName = e.Manager.FirstName,
                            LastName = e.Manager.LastName,
                        } : null
                    })
                    .OrderBy(e => e.DepartmentText);

                var departmentsList = _mapper.Map<List<TicketDepartmensListDto>>(departments);

                return departmentsList.ToList();
            }
            else
            {

                var company = await _workCompanyMatrisService.Where(e => e.FromCompanyId == user.WorkCompanyId);
                var companies = company.Data.Select(e => e.ToCompaniesIds).FirstOrDefault();

                var companyIds = new List<string>();
                companyIds.Add(user.WorkCompanyId.ToString());
                if (companies != null)
                {
                    foreach (var id in companies)
                    {
                        companyIds.Add(id.ToString());
                    }
                }

                var departments = await _ticketDepartments.Include();
                departments = departments.Include(x => x.SubDepartments);
                departments = departments
                    .Where(e => e.IsVisibleInList == true && e.IsActive == true && companyIds.Contains(e.WorkCompanyId.ToString()))
                    .Select(e => new TicketDepartment
                    {
                        Id = e.Id,
                        DeparmentCode = e.DeparmentCode,
                        DepartmentText = e.DepartmentText,
                        IsActive = e.IsActive,
                        ManagerId = e.ManagerId,
                        WorkCompanyId = e.WorkCompanyId,
                        WorkCompany = e.WorkCompany,
                        ParentDepartmentId = e.ParentDepartmentId,
                        SubDepartments = e.SubDepartments.Where(y => y.ParentDepartmentId == e.Id).Select(y => new TicketDepartment
                        {
                            DepartmentText = y.DepartmentText,
                            DeparmentCode = y.DeparmentCode,
                            Id = y.Id,
                        }).ToList(),
                        IsVisibleInList = e.IsVisibleInList,
                        Manager = e.Manager != null ? new UserApp
                        {
                            Id = e.Manager.Id,
                            FirstName = e.Manager.FirstName,
                            LastName = e.Manager.LastName,
                        } : null
                    })
                    .OrderBy(e => e.DepartmentText);

                var departmentsList = _mapper.Map<List<TicketDepartmensListDto>>(departments);

                return departmentsList.ToList();
            }

        }

        [HttpGet("GetOnlyVesaDepartments")]
        public async Task<List<TicketDepartmensListDto>> GetOnlyVesaDepartments()
        {
            var departments = await _ticketDepartments.Include();
            var dto = departments
                 .OrderBy(e => e.DepartmentText)
                .Where(e => e.IsVisibleInList == true && e.IsActive == true && e.WorkCompanyId == Guid.Parse("2e5c2ba5-3eb8-414d-8bc7-08dd44716854"))
                .Select(e => new TicketDepartmensListDto
                {
                    Id = e.Id.ToString(),
                    DepartmentText = e.DepartmentText,
                }).ToList();

            return dto;
        }

    }
}
