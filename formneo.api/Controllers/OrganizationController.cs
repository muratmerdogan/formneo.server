using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Graph.Models;
using Microsoft.OpenApi.Validations;
using NLayer.Core.Services;
using System.Runtime.ConstrainedExecution;
using formneo.core.DTOs;
using formneo.core.DTOs.DepartmentUserDto;
using formneo.core.DTOs.Ticket.TicketDepartments;
using formneo.core.Models;
using formneo.core.Models.Ticket;
using formneo.core.Services;

namespace formneo.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrganizationController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IServiceWithDto<TicketDepartment, TicketDepartmensListDto> _ticketDepartments;
        private readonly UserManager<UserApp> _userManager;
        public OrganizationController(IMapper mapper, IServiceWithDto<TicketDepartment, TicketDepartmensListDto> Service, IUserService userService, UserManager<UserApp> userManager)
        {
            _mapper = mapper;
            _ticketDepartments = Service;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<OrganizationDto>> GetAll()
        {
            try
            {
                var loginName = User.Identity.Name;
                var user = await _userManager.Users.Where(e => e.Email == loginName).Include(e => e.WorkCompany).FirstOrDefaultAsync();

                var service = await _ticketDepartments.Include();
                var departments = service.Where(e => e.WorkCompanyId == Guid.Parse("2e5c2ba5-3eb8-414d-8bc7-08dd44716854"))
                    .Include(e => e.Manager)
                    .ThenInclude(e => e.Positions)
                    .ToList();

                OrganizationDto result = new();

                #region Statik Eklenen Alan (formneo şirketi, genel müdürlük)
                // GENEL MÜDÜRLÜK EKLENDİ
                var firstDepartment = departments.Where(e => e.Id == Guid.Parse("358da3ff-ea80-44da-bb42-33c849631456")).FirstOrDefault();
                var firstDepartmentDto = new OrganizationDto
                {
                    Id = firstDepartment?.Id.ToString(),
                    Name = firstDepartment?.DepartmentText,
                    Expanded = true,
                    Type = "department",
                    Children = new List<OrganizationDto>(),
                };
                // formneo DANIŞMANLIK EKLENDİ
                var companyDto = new OrganizationDto
                {
                    Id = "2e5c2ba5-3eb8-414d-8bc7-08dd44716854",
                    Name = "formneo Danışmanlık",
                    Expanded = true,
                    Type = "department",
                    Children = new List<OrganizationDto>(),
                };
                result = companyDto;
                result.Children.Add(firstDepartmentDto);

                #endregion
                var resultDto = await Recursive(firstDepartment.Id, result, departments);
                return resultDto;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }

        }
        private async Task<OrganizationDto> Recursive(Guid parentDptId, OrganizationDto result, List<TicketDepartment> departments)
        {
            try
            {
                var service = await _ticketDepartments.Include();

                // Üst departmanı parentDptId olan tüm departmanları al
                var childDpts = departments
                       .Where(e => e.ParentDepartmentId == parentDptId)
                       .Select(e => new OrganizationDto
                       {
                           Id = e.Id.ToString(),
                           Name = e.DepartmentText,
                           Expanded = true,
                           Type = "department",
                           Children = new List<OrganizationDto>()
                       }).ToList();

                // Departman yöneticisini bul ve child olarak ekle
                var parentDptDto = FindById(result, parentDptId.ToString());
                var managerDto = departments.Where(e => e.Id == parentDptId).Select(e => new OrganizationDto
                {
                    Id = e.ManagerId,
                    Name = $"{e.Manager?.FirstName} {e.Manager?.LastName}".Trim(),
                    Title = e.Manager?.Positions?.Name,
                    Email = e.Manager?.Email,
                    Photo = e.Manager?.photo,
                    Expanded = true,
                    ClassName = "manager-node",
                    Children = new List<OrganizationDto>()
                }).FirstOrDefault();

                if (parentDptDto != null)
                {
                    if (parentDptDto.Children == null)
                        parentDptDto.Children = new List<OrganizationDto>();

                    parentDptDto.Children.Add(managerDto);
                }

                // Eğer alt departman yoksa
                if (!childDpts.Any())
                {
                    if (parentDptDto != null && managerDto != null)
                    {
                        // Departmanı bu parent olan kullanıcıları bul ve manager a child olarak ekle
                        var depUsers = await _userManager.Users
                         .Where(e => e.TicketDepartmentId == Guid.Parse(parentDptDto.Id) && e.Id != managerDto.Id)
                         .Include(e => e.Positions)
                         .ToListAsync();
                        foreach (var user in depUsers)
                        {
                            var userDto = new OrganizationDto
                            {
                                Id = user.Id.ToString(),
                                Name = $"{user.FirstName} {user.LastName}".Trim(),
                                Title = user.Positions?.Name,
                                Email = user.Email,
                                Photo = user.photo,
                                ClassName = user.Email == "muhammed.kadan@formneo.com" ? "ceo-node" :
                                           (user.Email == "veysel.karani@formneo.com" || user.Email == "bolat.ciftci@formneo.com") ? "executive-node" :
                                           "employee-node",
                                Children = new List<OrganizationDto>()
                            };

                            if (managerDto.Children == null)
                            {
                                managerDto.Children = new List<OrganizationDto>();
                            }
                            managerDto.Children.Add(userDto);
                        }
                    }
                   
                }
                else
                {
                    foreach (var item in childDpts)
                    {
                        // Alt departmanı ekle
                        if (managerDto != null)
                        {
                            if (managerDto.Children == null)
                                managerDto.Children = new List<OrganizationDto>();
                            managerDto.Children.Add(item);
                        }
                        await Recursive(Guid.Parse(item.Id), result, departments);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
          
        }
        private static OrganizationDto? FindById(OrganizationDto node, string id)
        {
            if (node.Id == id)
                return node;

            if (node.Children != null && node.Children.Any())
            {
                foreach (var child in node.Children)
                {
                    var found = FindById(child, id);
                    if (found != null)
                        return found;
                }
            }

            return null;
        }


        [HttpGet("GetByDepartment")]
        public async Task<ActionResult<OrganizationDto>> GetByDepartment([FromQuery] Guid dptId)
        {
            try
            {
                // Kullanıcı bilgisi (gerekirse kullanılır)
                var loginName = User.Identity.Name;
                var user = await _userManager.Users
                    .Where(e => e.Email == loginName)
                    .Include(e => e.WorkCompany)
                    .FirstOrDefaultAsync();

                // Tüm departmanları yükle
                var service = await _ticketDepartments.Include();
                var departments = service                    
                    .Where(e => e.WorkCompanyId == Guid.Parse("2e5c2ba5-3eb8-414d-8bc7-08dd44716854"))
                    .Include(e => e.Manager)
                    .ThenInclude(m => m.Positions)
                    .ToList();

                // Başlangıç departmanını bul
                var rootDepartment = departments.Where(d => d.Id == dptId).FirstOrDefault();
                if (rootDepartment == null)
                    return NotFound("Department not found");

                // Root departman DTO'su oluştur
                var rootDto = new OrganizationDto
                {
                    Id = rootDepartment.Id.ToString(),
                    Name = rootDepartment.DepartmentText,
                    Expanded = true,
                    Type = "department",
                    Children = new List<OrganizationDto>()
                };

                // Hiyerarşiyi oluştur
                var result = await Recursive(rootDepartment.Id, rootDto, departments);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }
    }
}
