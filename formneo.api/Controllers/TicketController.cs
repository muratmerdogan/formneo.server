using AutoMapper;
using Azure.Identity;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using HtmlAgilityPack;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.xmp.impl;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.Graph.Core;
using Microsoft.Graph.Models;
using Microsoft.Graph.Models.ExternalConnectors;
using Microsoft.Graph.Models.IdentityGovernance;
using Microsoft.Identity.Client;
using NLayer.Core.Services;
using OfficeOpenXml;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Security.Claims;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using vesa.api.Controllers.utils;
using vesa.api.Helper;
using vesa.core.Configuration;
using vesa.core.DTOs;
using vesa.core.DTOs.Budget.NormCodeRequest;
using vesa.core.DTOs.Ticket;
using vesa.core.DTOs.Ticket.TicketAssigne;
using vesa.core.DTOs.Ticket.TicketComment;
using vesa.core.DTOs.Ticket.TicketDepartments;
using vesa.core.DTOs.Ticket.TicketNotifications;
using vesa.core.DTOs.Ticket.TicketRuleEngine;
using vesa.core.DTOs.Ticket.Tickets;
using vesa.core.DTOs.Ticket.TicketTeams;
using vesa.core.EnumExtensions;
using vesa.core.Models;
using vesa.core.Models.BudgetManagement;
using vesa.core.Models.TaskManagement;
using vesa.core.Models.Ticket;
using vesa.core.Operations;
using vesa.core.RuleEngine;
using vesa.core.Services;
using vesa.core.UnitOfWorks;
using vesa.repository.UnitOfWorks;
using vesa.service.Services;
using vesa.ticket;
using vesa.workflow;
using static OfficeOpenXml.ExcelErrorValue;

namespace vesa.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TicketController : CustomBaseController
    {
        private readonly ITicketServices _ticketService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IServiceWithDto<Tickets, TicketListDto> _ticketGenericService;
        private readonly IServiceWithDto<TicketFile, TicketFileDto> _ticketFileGenericService;
        private readonly IServiceWithDto<TicketAssigne, TicketAssigneDto> _ticketAssigneService;
        private readonly IWorkFlowService _workFlowservice;
        private readonly IWorkFlowItemService _workFlowItemservice;
        private readonly IApproveItemsService _approveItemsService;
        private readonly IServiceWithDto<WorkFlowDefination, WorkFlowDefinationDto> _workFlowDefinationDto;
        private readonly IServiceWithDto<WorkflowHead, WorkFlowHeadDto> _workFlowHeadService;
        private readonly IServiceWithDto<WorkCompany, WorkCompanyDto> _workCompanyService;
        private readonly IServiceWithDto<TicketDepartment, TicketDepartmensListDto> _ticketDepartments;
        private readonly IServiceWithDto<TicketTeam, TicketTeamListDto> _tickeTeamService;
        private readonly IServiceWithDto<TicketRuleEngine, TicketRuleEngineListDto> _ticketRuleEngineService;
        private readonly UserManager<UserApp> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DbNameHelper _dbNameHelper;

        private readonly IServiceWithDto<TicketNotifications, TicketNotificationsListDto> _ticketNotificationService;

        IUnitOfWork _unitOfWork;

        private readonly IConfiguration _configuration;
        private readonly ITenantContext _tenantContext;
        private readonly IUserTenantService _userTenantService;
        public TicketController(DbNameHelper dbNameHelper,IConfiguration configuration, IUserService userService, IMapper mapper, ITicketServices ticketServices, IUnitOfWork unitOfWork, IServiceWithDto<Tickets, TicketListDto> ticketGenericService, IServiceWithDto<TicketFile, TicketFileDto> ticketFileGenericService, IServiceWithDto<TicketAssigne, TicketAssigneDto> ticketAssigneService, IWorkFlowService workFlowService, IWorkFlowItemService workFlowItemservice, IApproveItemsService approveItemsService, IServiceWithDto<WorkFlowDefination, WorkFlowDefinationDto> definationdto, IServiceWithDto<WorkflowHead, WorkFlowHeadDto> workFlowHeadService, IServiceWithDto<WorkCompany, WorkCompanyDto> workcompanyService, IServiceWithDto<TicketDepartment, TicketDepartmensListDto> ticketDepartments, IServiceWithDto<TicketTeam, TicketTeamListDto> tickeTeamService, UserManager<UserApp> userManager, IServiceWithDto<TicketNotifications, TicketNotificationsListDto> ticketNotificationService, IServiceWithDto<TicketRuleEngine, TicketRuleEngineListDto> ticketRuleEngineService, IHttpContextAccessor httpContextAccessor, ITenantContext tenantContext, IUserTenantService userTenantService)
        {
            _ticketService = ticketServices;
            _mapper = mapper;
            _userService = userService;
            _unitOfWork = unitOfWork;
            _ticketGenericService = ticketGenericService;
            _ticketFileGenericService = ticketFileGenericService;
            _ticketAssigneService = ticketAssigneService;
            _dbNameHelper = dbNameHelper;
            _workFlowDefinationDto = definationdto;
            _workFlowItemservice = workFlowItemservice;
            _approveItemsService = approveItemsService;
            _workFlowHeadService = workFlowHeadService;
            _workFlowservice = workFlowService;
            _workCompanyService = workcompanyService;
            _ticketDepartments = ticketDepartments;
            _tickeTeamService = tickeTeamService;
            _userManager = userManager;
            _ticketRuleEngineService = ticketRuleEngineService;
            _ticketNotificationService = ticketNotificationService;
            _httpContextAccessor = httpContextAccessor;
            _tenantContext = tenantContext;
            _userTenantService = userTenantService;

            _configuration = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> CreaTicket(TicketInsertDto createUserDto, int createEnvironment = 1, [FromQuery] List<string?>? ccRecipients = null)
        {

            _unitOfWork.BeginTransaction();

            if (string.IsNullOrEmpty(createUserDto.WorkCompanyId))
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(400, "Şirket Bilgisi Boş Bırakılamaz"));
            }
            //if (string.IsNullOrEmpty(createUserDto.CustCompanyId))
            //{
            //    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(400, "Müşteri Bilgisi Boş Bırakılamaz"));
            //}

            if (string.IsNullOrEmpty(createUserDto.UserAppId))
            {

                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(400, "Kullanıcı Boş Bırakılamaz"));
            }

            if (string.IsNullOrEmpty(createUserDto.WorkCompanySystemInfoId))
            {
                createUserDto.WorkCompanySystemInfoId = null;
            }
            if (string.IsNullOrEmpty(createUserDto.CustomerRefId))
            {
                createUserDto.CustomerRefId = null;
            }

            createUserDto.TicketCode = Guid.NewGuid().ToString();
            var ticket = _mapper.Map<Tickets>(createUserDto);


            //ticket.TicketDepartmentId = new Guid("E0BB054C-8885-49B1-9693-67D2DE9BA3FD");

            //if (createUserDto.TicketSubject == TicketSubject.ABAP)
            //{
            //    ticket.TicketDepartmentId = new Guid("911e0f7f-e133-4fa1-a6e1-0c79c674bb61");
            //}
            //else if (createUserDto.TicketSubject == TicketSubject.FioriBtp)
            //{
            //    ticket.TicketDepartmentId = new Guid("1ff822c6-0fbc-4d12-aa20-e0a0d9d8aec3");
            //}
            //else if (createUserDto.TicketSubject == TicketSubject.ITHelpDesk)
            //{
            //    ticket.TicketDepartmentId = new Guid("66DF64D4-9251-45B7-85F1-B2E8E6448028");
            //}

            //else
            //{
            //    ticket.TicketDepartmentId = new Guid("E0BB054C-8885-49B1-9693-67D2DE9BA3FD");
            //}

            ticket.TicketDepartmentId = new Guid("E0BB054C-8885-49B1-9693-67D2DE9BA3FD");

            //1-> Vesa Destek sisteminden olusturulmus
            //2 ->Mailden olusturulmus
            var newDto = new ChechRuleDto
            {
                insertDto = createUserDto,
                updateDto = null
            };
            var rules = createUserDto.isSend != false ? await CheckRuleEngine(newDto, createEnvironment) : null;


            if (rules != null)
            {
                if (rules.Value.AssignedDepartmentId != Guid.Empty)
                    ticket.TicketDepartmentId = rules.Value.AssignedDepartmentId;
            }




            if (createUserDto.isSend == true)
            {
                ticket.Status = TicketStatus.Open;
            }
            else if (createUserDto.isSend == false)
            {
                ticket.Status = TicketStatus.Draft;
            }
            //var workCompany = await ConditionApprove(createUserDto.WorkCompanyId);

            //string definationId = workCompany.WorkFlowDefinationId;

            string definationId = Guid.Empty.ToString();
            if (rules != null)
            {
                if (rules.Value.WorkflowId != Guid.Empty)
                    definationId = rules.Value.WorkflowId.ToString();
            }


            var tickets = await _ticketService.AddAsync(ticket);


            var dto = await GetById(tickets.Id);

            //mailden ilk defa olustuysa createdby defaultUser geldigi icin bu sekilde degistirildi
            //if (createEnvironment == 2)
            //{
            //    var userr = await _userManager.FindByEmailAsync(dto.UserAppUserName);
            //    tickets.CreatedBy = userr.Email;
            //    if (tickets.TicketComment != null && tickets.TicketComment.Count > 0)
            //    {
            //        tickets.TicketComment[0].CreatedBy = userr.Email;
            //    }
            //}

            bool sendApprove = false;
            if (definationId != Guid.Empty.ToString())
            {

                WorkFlowStartApiDto workFlowApiDto = new WorkFlowStartApiDto();
                WorkFlowExecute execute = new WorkFlowExecute();
                WorkFlowDto workFlowDto = new WorkFlowDto();

                WorkFlowParameters parameters = new WorkFlowParameters();
                parameters.workFlowService = _workFlowservice;
                parameters.workFlowItemService = _workFlowItemservice;
                parameters._workFlowDefination = _workFlowDefinationDto;
                parameters._ticketService = _ticketService;
                workFlowApiDto.UserName = User.Identity.Name;

                workFlowDto.WorkFlowDefinationId = new Guid(definationId);
                workFlowDto.UserName = workFlowApiDto.UserName;
                workFlowDto.WorkFlowInfo = "Yeni Ticket Talebi: " + ticket.UniqNumber + " " + dto.WorkCompanyName;

                string json = JsonSerializer.Serialize(createUserDto);
                if (ticket.Status != TicketStatus.Draft)
                {
                    var result = await execute.StartAsync(workFlowDto, parameters, json);
                    var mapResult = _mapper.Map<WorkFlowHeadDtoResultStartOrContinue>(result);

                    ticket.WorkflowHeadId = new Guid(mapResult.Id);
                    ticket.Status = TicketStatus.InApprove;
                    dto.WorkFlowHeadId = ticket.WorkflowHeadId.ToString();
                    sendApprove = true;

                }
            }



            var ticketId = tickets.Id;

            //query atama icin
            if (rules != null)
            {


                if (rules.Value.AssignedTeamId != Guid.Empty || rules.Value.AssignedUserId != Guid.Empty)
                {
                    var asg = new TicketAssigne();
                    asg.Description = "Sistem Ataması";
                    asg.TicketsId = tickets.Id;
                    asg.Status = tickets.Status;

                    asg.TicketTeamID = rules.Value.AssignedTeamId != Guid.Empty ? rules.Value.AssignedTeamId : null;
                    asg.UserAppId = rules.Value.AssignedUserId != Guid.Empty ? rules.Value.AssignedUserId.ToString() : null;


                    var resAsg = await _ticketAssigneService.AddAsync(_mapper.Map<TicketAssigneDto>(asg));
                    ticket.TicketAssigneId = resAsg.Data.Id;

                    await _ticketService.UpdateAsync(ticket);
                }
            }

            _unitOfWork.Commit();

            if (ticket.Status != TicketStatus.Draft)
            {
                //departman yoneticisi
                List<string> tolist = new List<string>();
                var resultDpt = await _ticketDepartments.Include();
                var resData = resultDpt.Where(e => e.Id == new Guid(dto.TicketDepartmentId)).Select(e => new { e.Manager.Email }).FirstOrDefault();

                if (resData != null)
                {
                    if (resData.Email != null && resData.Email != "")
                    {
                        tolist.Add(resData.Email);
                    }
                }



                SendTicketMailAsync(dto, tolist, "Yeni Talep Oluşturuldu");

                //talebi olusturan kisiye bilgi maili
                if (dto.CreatedBy != null && dto.CreatedBy != "")
                {
                    tolist = new List<string>();
                    tolist.Add(dto.CreatedBy);
                    if (sendApprove)
                        SendTicketMailAsync(dto, tolist, "Talebiniz Onaya Gönderilmiştir");
                    else
                    {
                        var depId = await _userManager.Users.Where(e => e.Id == createUserDto.UserAppId).Select(e => e.TicketDepartmentId).FirstOrDefaultAsync();
                        List<string> depUserMails;
                        if (_tenantContext?.CurrentTenantId != null)
                        {
                            var candidates = await _userManager.Users.Where(e => e.TicketDepartmentId == depId && e.Id != createUserDto.UserAppId).Select(e => new { e.Id, e.Email }).ToListAsync();
                            depUserMails = new List<string>();
                            foreach (var c in candidates)
                            {
                                var ut = await _userTenantService.GetByUserAndTenantAsync(c.Id, _tenantContext.CurrentTenantId.Value);
                                if (ut != null && ut.HasDepartmentPermission && !string.IsNullOrEmpty(c.Email))
                                {
                                    depUserMails.Add(c.Email);
                                }
                            }
                        }
                        else
                        {
                            // Tenant id yoksa tenant-bazlı izinleri değerlendiremeyiz, bu durumda CC listesine departman maili eklemeyelim
                            depUserMails = new List<string>();
                        }

                        if (depUserMails != null && depUserMails.Count > 0)
                        {
                            ccRecipients ??= new List<string>();
                            ccRecipients.AddRange(depUserMails);
                        }

                        var addedMails = createUserDto.AddedMailAddresses.Split(';', StringSplitOptions.RemoveEmptyEntries);
                        if (addedMails != null && addedMails.Length > 0)
                        {
                            if (ccRecipients == null)
                            {
                                ccRecipients = new List<string>();
                            }

                            foreach (var addedMail in addedMails)
                            {
                                if (!ccRecipients.Contains(addedMail))
                                {
                                    ccRecipients.Add(addedMail);
                                }
                            }
                        }


                        SendTicketMailAsync(dto, tolist, "Talebiniz Oluşturulmuştur", ccRecipients);
                    }

                }

                //queryden atama yapilanlara bildirim maili
                if (rules != null)
                {
                    if (rules.Value?.AssignedTeamId != null && rules.Value.AssignedTeamId != Guid.Empty)
                    {
                        var teamValue = await _tickeTeamService.GetByIdGuidAsync(rules.Value.AssignedTeamId);
                        dto.TicketAssigneText = teamValue.Data.Name;
                    }
                    if (rules.Value?.AssignedUserId != null && rules.Value.AssignedUserId != Guid.Empty)
                    {
                        var user = await _userManager.FindByIdAsync(rules.Value.AssignedUserId.ToString());
                        dto.TicketAssigneText = user.FirstName + " " + user.LastName;

                    }
                    await SendQueryMail(rules, dto);
                }
            }

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
            //return CreateActionResult(await _ticketService.CreateTicketAsync(createUserDto));
        }

        private async Task<WorkCompanyDto> ConditionApprove(string workCompanyId)
        {

            var result = await _workCompanyService.Where(e => e.Id == new Guid(workCompanyId));

            var company = result.Data.FirstOrDefault();
            if (company != null)
            {
                if (company.WorkFlowDefinationId != null)
                {

                    return company;

                }

            }
            return null;

        }

        [HttpPost("UpdateStartTicket")]
        public async Task<IActionResult> UpdateStartTicket(TicketUpdateDto updateDto, int createEnvironment = 1, bool isEdit = false)
        {

            _unitOfWork.BeginTransaction();

            if (string.IsNullOrEmpty(updateDto.WorkCompanySystemInfoId))
            {
                updateDto.WorkCompanySystemInfoId = null;
            }
            if (string.IsNullOrEmpty(updateDto.CustomerRefId))
            {
                updateDto.CustomerRefId = null;
            }

            var ticket = _mapper.Map<Tickets>(updateDto);
            if (updateDto.isSend == true)
            {
                ticket.Status = TicketStatus.Open;
            }
            else if (updateDto.isSend == false)
            {
                ticket.Status = TicketStatus.Draft;
            }

            //if (updateDto.TicketSubject == TicketSubject.ABAP)
            //{
            //    ticket.TicketDepartmentId = new Guid("911e0f7f-e133-4fa1-a6e1-0c79c674bb61");
            //}
            //else if (updateDto.TicketSubject == TicketSubject.FioriBtp)
            //{
            //    ticket.TicketDepartmentId = new Guid("1ff822c6-0fbc-4d12-aa20-e0a0d9d8aec3");
            //}
            //else
            //{
            //    ticket.TicketDepartmentId = new Guid("E0BB054C-8885-49B1-9693-67D2DE9BA3FD");
            //}

            //taslaktan talep olusturulduysa sorgulari kontrol et

            var newDto = new ChechRuleDto
            {
                insertDto = null,
                updateDto = updateDto
            };
            ActionResult<TicketRuleEngineListDto> rules = null;
            if (updateDto.isSend == true && isEdit == false)
            {
                rules = await CheckRuleEngine(newDto, createEnvironment);
                //query atama icin
                if (rules != null)
                {
                    if (rules.Value.AssignedTeamId != Guid.Empty || rules.Value.AssignedUserId != Guid.Empty)
                    {
                        var asg = new TicketAssigne();
                        asg.Description = "Sistem Ataması";
                        asg.TicketsId = ticket.Id;
                        asg.Status = ticket.Status;

                        asg.TicketTeamID = rules.Value.AssignedTeamId != Guid.Empty ? rules.Value.AssignedTeamId : null;
                        asg.UserAppId = rules.Value.AssignedUserId != Guid.Empty ? rules.Value.AssignedUserId.ToString() : null;


                        var resAsg = await _ticketAssigneService.AddAsync(_mapper.Map<TicketAssigneDto>(asg));
                        ticket.TicketAssigneId = resAsg.Data.Id;

                        ticket.TicketDepartmentId = rules.Value.AssignedDepartmentId != Guid.Empty ? rules.Value.AssignedDepartmentId : ticket.TicketDepartmentId;

                    }
                }
            }


            await _ticketService.UpdateAsync(ticket);

            //var workCompany = await ConditionApprove(updateDto.WorkCompanyId);


            var dto = await GetById(ticket.Id);

            string definationId = Guid.Empty.ToString();
            if (rules != null)
            {
                if (rules.Value.WorkflowId != Guid.Empty)
                    definationId = rules.Value.WorkflowId.ToString();
            }

            bool sendApprove = false;
            if (definationId != Guid.Empty.ToString())
            {
                WorkFlowStartApiDto workFlowApiDto = new WorkFlowStartApiDto();
                WorkFlowExecute execute = new WorkFlowExecute();
                WorkFlowDto workFlowDto = new WorkFlowDto();

                WorkFlowParameters parameters = new WorkFlowParameters();
                parameters.workFlowService = _workFlowservice;
                parameters.workFlowItemService = _workFlowItemservice;
                parameters._workFlowDefination = _workFlowDefinationDto;
                parameters._ticketService = _ticketService;
                workFlowApiDto.UserName = User.Identity.Name;

                workFlowDto.WorkFlowDefinationId = new Guid(definationId);
                workFlowDto.UserName = workFlowApiDto.UserName;
                workFlowDto.WorkFlowInfo = "Yeni Ticket Talebi: " + ticket.UniqNumber + " " + dto.WorkCompanyName;

                if (ticket.Status != TicketStatus.Draft)
                {

                    string json = JsonSerializer.Serialize(updateDto);
                    var result = await execute.StartAsync(workFlowDto, parameters, json);

                    sendApprove = true;
                    var mapResult = _mapper.Map<WorkFlowHeadDtoResultStartOrContinue>(result);

                    ticket.WorkflowHeadId = new Guid(mapResult.Id);
                    ticket.Status = TicketStatus.InApprove;

                }
            }
            _unitOfWork.Commit();

            if (ticket.Status != TicketStatus.Draft)
            {
                //departman yoneticisi
                List<string> tolist = new List<string>();
                var resultDpt = await _ticketDepartments.Include();
                var resData = resultDpt.Where(e => e.Id == new Guid(dto.TicketDepartmentId)).Select(e => new { e.Manager.Email }).FirstOrDefault();

                if (resData != null)
                {
                    if (resData.Email != null && resData.Email != "")
                    {
                        tolist.Add(resData.Email);
                    }
                }
                SendTicketMailAsync(dto, tolist, isEdit == false ? "Yeni Talep Oluşturuldu." : "Talep Güncellendi.");

                //talebi olusturan kisiye bilgi maili
                if (dto.CreatedBy != null && dto.CreatedBy != "")
                {
                    tolist = new List<string>();
                    tolist.Add(dto.CreatedBy);

                    if (sendApprove)
                        SendTicketMailAsync(dto, tolist, "Talebiniz Onaya Gönderilmiştir.");
                    else
                        SendTicketMailAsync(dto, tolist, isEdit == false ? "Talebiniz Oluşturulmuştur." : "Talebiniz Güncellendi.");

                }

                //queryden atama yapilanlara bildirim maili
                if (rules != null)
                {
                    if (rules.Value?.AssignedTeamId != null && rules.Value.AssignedTeamId != Guid.Empty)
                    {
                        var teamValue = await _tickeTeamService.GetByIdGuidAsync(rules.Value.AssignedTeamId);
                        dto.TicketAssigneText = teamValue.Data.Name;
                    }
                    if (rules.Value?.AssignedUserId != null && rules.Value.AssignedUserId != Guid.Empty)
                    {
                        var user = await _userManager.FindByIdAsync(rules.Value.AssignedUserId.ToString());
                        dto.TicketAssigneText = user.FirstName + " " + user.LastName;

                    }
                    await SendQueryMail(rules, dto);
                }
            }

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
            //return CreateActionResult(await _ticketService.CreateTicketAsync(createUserDto));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTicket(TicketUpdateDto updateDto)
        {

            _unitOfWork.BeginTransaction();

            if (string.IsNullOrEmpty(updateDto.WorkCompanySystemInfoId))
            {
                updateDto.WorkCompanySystemInfoId = null;
            }
            if (string.IsNullOrEmpty(updateDto.CustomerRefId))
            {
                updateDto.CustomerRefId = null;
            }

            var ticket = _mapper.Map<Tickets>(updateDto);
            if (updateDto.isSend == true)
            {
                ticket.Status = TicketStatus.Open;
            }
            else if (updateDto.isSend == false)
            {
                ticket.Status = TicketStatus.Draft;
            }

            if (updateDto.TicketSubject == TicketSubject.ABAP)
            {
                ticket.TicketDepartmentId = new Guid("911e0f7f-e133-4fa1-a6e1-0c79c674bb61");
            }
            else if (updateDto.TicketSubject == TicketSubject.FioriBtp)
            {
                ticket.TicketDepartmentId = new Guid("1ff822c6-0fbc-4d12-aa20-e0a0d9d8aec3");
            }
            else
            {
                ticket.TicketDepartmentId = new Guid("E0BB054C-8885-49B1-9693-67D2DE9BA3FD");
            }




            await _ticketService.UpdateAsync(ticket);

            _unitOfWork.Commit();

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
            //return CreateActionResult(await _ticketService.CreateTicketAsync(createUserDto));
        }
        [HttpPost("AddComment")]
        public async Task<IActionResult> AddComment(TicketCommentInsertDto newDto, Guid id)
        {

            _unitOfWork.BeginTransaction();

            var service = await _ticketGenericService.Include();

            var ticket = await service.Include(e => e.TicketComment).Include(e => e.TicketAssigne).Where(e => e.Id == id).FirstOrDefaultAsync();

            var addedMails = (ticket.AddedMailAddresses ?? string.Empty).Split(';', StringSplitOptions.RemoveEmptyEntries).ToList();

            ticket.TicketComment.Add(_mapper.Map<TicketComment>(newDto));

            await _ticketService.UpdateAsync(ticket);

            _unitOfWork.Commit();

            //bilgilendirme mailleri
            var ticketComments = ticket.TicketComment;
            List<string> tolist = new List<string>();

            foreach (var comment in ticketComments)
            {
                var email = comment.CreatedBy;
                if (!tolist.Contains(email))
                {
                    tolist.Add(email);
                }
                if (addedMails.Any())
                {
                    foreach (var addedMail in addedMails)
                    {
                        if (!tolist.Contains(addedMail))
                        {
                            tolist.Add(addedMail);
                        }
                    }
                }

            }

            //atanan kisi ise  onu, takimsa takimdaki herkesi maile ekle
            if (!ticket.isTeam)
            {
                if (ticket.TicketAssigne != null)
                {
                    if (ticket.TicketAssigne!.UserAppId != null)
                    {
                        var selecteduser = _userManager.Users.Where(e => e.Id == ticket.TicketAssigne!.UserAppId).Select(e => new { e.Id, e.FirstName, e.LastName, e.Email }).FirstOrDefault();

                        if (!tolist.Contains(selecteduser.Email))
                        {
                            tolist.Add(selecteduser.Email);
                        }

                    }
                }

            }
            else
            {
                if (ticket.TicketAssigne != null)
                {
                    if (ticket.TicketAssigne!.TicketTeamID != null)
                    {
                        var query = await _tickeTeamService.Include();
                        query = query.Include(x => x.TeamList).ThenInclude(e => e.UserApp);
                        var result = await query.Where(e => e.Id == ticket.TicketAssigne!.TicketTeamID).FirstOrDefaultAsync();

                        foreach (var item in result.TeamList)
                        {
                            var selecteduser = _userManager.Users.Where(e => e.Id == item.UserAppId).Select(e => new { e.Id, e.FirstName, e.LastName, e.Email }).FirstOrDefault();

                            if (!tolist.Contains(selecteduser.Email))
                            {
                                tolist.Add(selecteduser.Email);
                            }
                        }
                    }
                }

            }

            //ilgili kisiler varsa onlari da ekle
            var notifications = await _ticketNotificationService.Where(e => e.TicketId == ticket.Id);
            var relPers = notifications.Data.ToList();
            if (relPers.Count > 0)
            {
                foreach (var rel in relPers)
                {
                    if (rel.UserAppId != null)
                    {
                        var selecteduser = _userManager.Users.Where(e => e.Id == rel.UserAppId).Select(e => new { e.Id, e.FirstName, e.LastName, e.Email }).FirstOrDefault();

                        if (!tolist.Contains(selecteduser.Email))
                        {
                            tolist.Add(selecteduser.Email);
                        }
                    }
                }
            }

            if (tolist.Count > 0)
            {
                foreach (var email in tolist)
                {
                    List<string> emailList = new List<string>();
                    emailList.Add(email);
                    SendInfoCommentMail(ticket.UniqNumber.ToString(), ticket.Title, newDto.Body, emailList, "Yeni Mesaj Bildirimi");
                }

            }



            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
            //return CreateActionResult(await _ticketService.CreateTicketAsync(createUserDto));
        }

        [HttpGet("GetFile")]
        public async Task<TicketFileDto> GetFile(Guid id)
        {
            var dto = await _ticketFileGenericService.GetByIdGuidAsync(id);
            if (dto.Data.Base64 == "")
            {
                dto.Data.Base64 = GetFileBase64(dto.Data.FilePath);
            }
            return _mapper.Map<TicketFileDto>(dto.Data);
        }

        [HttpGet("{id}")]
        public async Task<TicketListDto> GetById(Guid id)
        {
            try
            {
                var service = await _ticketGenericService.Include();


                var result = await service.Include(td => td.UserApp).Include(e => e.WorkCompany).Include(e => e.WorkCompanySystemInfo).Include(e => e.TicketComment).ThenInclude(tc => tc.Files)
                     .Include(ticket => ticket.TicketAssigne)
                     .ThenInclude(ticket => ticket.UserApp)
                     .Include(ticket => ticket.TicketAssigne)
                     .ThenInclude(ticket => ticket.TicketTeam)
                     .Select(td => new
                     {
                         td.Id,
                         //UserApp=td.UserApp,
                         UserApp = td.UserApp != null
            ? new { td.UserApp.Id, td.UserApp.FirstName, td.UserApp.UserName, td.UserApp.LastName }
            : null,
                         td.AddedMailAddresses,
                         td.EstimatedDeadline,
                         td.TicketProjectId,
                         td.TicketProject,
                         td.UniqNumber,
                         td.WorkCompany,
                         td.WorkCompanySystemInfo,
                         td.TicketCode,
                         td.Type,
                         td.ApproveStatus,
                         td.UserAppId,
                         td.WorkCompanyId,
                         td.CustomerRefId,
                         td.CustomerRef,
                         td.CreatedDate,
                         td.Title,
                         td.Status,
                         td.IsFilePath,
                         td.FilePath,
                         td.TicketSubject,
                         td.CreatedBy,
                         td.Priority,
                         td.UpdatedBy,
                         td.IsFromEmail,
                         td.WorkCompanySystemInfoId,
                         td.TicketSLA,
                         TicketAssigne = td.TicketAssigne != null ? new
                         {
                             UserApp = td.TicketAssigne != null ? td.TicketAssigne.UserApp != null
                                     ? new { td.TicketAssigne.UserApp.Id, td.TicketAssigne.UserApp.UserName, td.TicketAssigne.UserApp.LastName, td.TicketAssigne.UserApp.NormalizedUserName, td.TicketAssigne.UserApp.FirstName }
                                    : null : null,
                             TicketTeam = td.TicketAssigne != null ? td.TicketAssigne.TicketTeam != null
                                     ? new { td.TicketAssigne.TicketTeam.Name }
                                 : null : null,
                             Description = td.TicketAssigne != null ? td.TicketAssigne.Description : ""
                         } : null,
                         //td.TicketAssigne,
                         //td.TicketAssigne = new
                         //{

                         //    UserApp = td.TicketAssigne != null ? td.TicketAssigne.UserApp != null
                         //        ? new { td.TicketAssigne.UserApp.Id, td.TicketAssigne.UserApp.NormalizedUserName, td.TicketAssigne.UserApp.FirstName, td.TicketAssigne.UserApp.LastName }
                         //       : null : null,
                         //    TicketTeam = td.TicketAssigne != null ? td.TicketAssigne.TicketTeam != null
                         //        ? new { td.TicketAssigne.TicketTeam.Name }
                         //    : null : null
                         //},
                         td.TicketDepartment,
                         td.TicketDepartmentId,
                         TicketComments = td.TicketComment.Select(tc => new TicketCommentDto
                         {
                             FilePath = tc.FilePath,
                             id = tc.Id.ToString(),
                             Body = td.IsFilePath == true ? ReadMailBodyFromPath(tc.FilePath) : tc.Body,
                             TicketId = td.Id.ToString(),
                             CreatedDate = tc.CreatedDate,
                             CreatedBy = tc.CreatedBy,

                             Files = tc.Files.Select(f => new TicketFileDto
                             {
                                 TicketCommentId = tc.Id.ToString(),
                                 id = f.Id.ToString(),
                                 FileName = f.FileName,
                                 FilePath = td.IsFilePath == true ? f.FilePath : null,
                                 Base64 = td.IsFilePath == true ? GetFileBase64(f.FilePath) : f.Base64
                             }).ToList()
                         }).OrderBy(e => e.CreatedDate).ToList()
                     })
    .FirstOrDefaultAsync(td => td.Id == id);

                if (result == null)
                {
                    return null;
                }

                var ticketNotifications = await _ticketNotificationService.Include();
                var relPersons = ticketNotifications.Where(e => e.TicketId == result.Id).ToList();
                List<TicketNotificationsListDto> listDto = new List<TicketNotificationsListDto>();
                foreach (var item in relPersons)
                {
                    var toAdd = _mapper.Map<TicketNotificationsListDto>(item);
                    var user = _userManager.Users.Where(e => e.Id == item.UserAppId).Select(e => new { e.Id, e.FirstName, e.LastName }).FirstOrDefault();

                    toAdd.User = new UserApp
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Id = user.Id
                    };

                    listDto.Add(toAdd);
                }


                var ticketDto = new TicketListDto
                {
                    TicketNotificationsListDto = listDto,
                    TicketCode = result.TicketCode,
                    Id = result.Id,
                    TicketNumber = result.UniqNumber,
                    Type = result.Type,
                    TypeText = result.Type.GetDescription(),
                    ApproveStatus = result.ApproveStatus,
                    ApproveStatusText = result.ApproveStatus.GetDescription(),
                    UserAppId = result.UserAppId,
                    UserAppName = result.UserApp?.FirstName + " " + result.UserApp?.LastName,
                    UserAppUserName = result.UserApp?.UserName,
                    WorkCompanyId = result.WorkCompanyId,
                    WorkCompanyName = result.WorkCompany?.Name,
                    CustomerRefId = result.CustomerRefId != null ? (Guid)result.CustomerRefId : null,
                    CustomerRefName = result.CustomerRef != null ? result.CustomerRef.Name : "",
                    WorkCompanySystemInfoId = result.WorkCompanySystemInfoId != null ? result.WorkCompanySystemInfoId : null,
                    WorkCompanySystemName = result.WorkCompanySystemInfo != null ? result.WorkCompanySystemInfo.Name : "",
                    CreatedDate = result.CreatedDate,
                    CreatedBy = result.CreatedBy,
                    UpdatedBy = result.UpdatedBy,
                    Title = result.Title,
                    Status = result.Status,
                    StatusText = result.Status.GetDescription(),
                    TicketSubject = result.TicketSubject,
                    TicketSubjectText = result.TicketSubject.GetDescription(),
                    PriorityText = result.Priority!.GetDescription(),
                    Priority = result.Priority,
                    TicketSLAText = result.TicketSLA.GetDescription(),
                    TicketSLA = result.TicketSLA,
                    TicketComment = result.TicketComments,
                    TicketDepartmentId = result.TicketDepartmentId!.ToString(),
                    TicketDepartmentText = result.TicketDepartment!.DepartmentText,
                    TicketAssigneText = result.TicketAssigne != null ? result.TicketAssigne.UserApp != null
                    ? result.TicketAssigne.UserApp.FirstName + " " + result.TicketAssigne.UserApp.LastName
                    : result.TicketAssigne.TicketTeam != null
                    ? result.TicketAssigne.TicketTeam.Name // Team'den alacağınız alan
                  : "Atama Yok" : "Atama Yok", // H
                    CanEdit = result.Status == TicketStatus.InApprove ||
                    result.Status == TicketStatus.Closed ||
                    result.Status == TicketStatus.Resolved ||
                    result.Status == TicketStatus.Cancelled ? false : true,
                    AssigneDescription = result.TicketAssigne != null ? result.TicketAssigne.Description : "",
                    AddedMailAddresses = result.AddedMailAddresses,
                    IsFromEmail = result.IsFromEmail,
                    EstimatedDeadline = result.EstimatedDeadline,
                    TicketProjectId = result.TicketProjectId,
                    TicketprojectName = result.TicketProject?.Name,
                    // Diğer alanlar
                };
                return ticketDto;
            }
            catch (Exception ex)
            {
                return null;

            }
        }

        private static string? GetFileBase64(string? filePath)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                    return null;

                // Mutlak dosya yolunu tam olarak belirtmeniz gerekebilir.
                // Örneğin: Path.Combine(Environment.WebRootPath, filePath) gibi

                if (!System.IO.File.Exists(filePath))
                    return null;

                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                return Convert.ToBase64String(fileBytes);
            }
            catch (Exception ex)
            {
                // Hata loglama yapılabilir
                return null;
            }
        }
        private static string? ReadMailBodyFromPath(string? folderPath)
        {
            try
            {
                if (string.IsNullOrEmpty(folderPath))
                    return null;

                var mailBodyPath = Path.Combine(folderPath, "Mailbody.html");

                if (!System.IO.File.Exists(mailBodyPath))
                    return null;

                return System.IO.File.ReadAllText(mailBodyPath);
            }
            catch (Exception ex)
            {
                // Hata loglama yapılabilir
                return null;
            }
        }



        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TicketListDto>))]
        public async Task<List<TicketListDto>> GetAllTickets()
        {

            var values = await _ticketService.GetAllTicketsWithEnumDescriptionsAsync(User.Identity.Name);
            return values.TicketList.OrderByDescending(e => e.CreatedDate).ToList();

        }
        [HttpGet("GetAssignTickets")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TicketListDto>))]
        public async Task<List<TicketListDto>> GetAssignTickets()
        {

            var values = await _ticketService.GetAllAssignTicketsWithEnumDescriptionsAsync(User.Identity.Name);
            return values.TicketList.OrderByDescending(e => e.CreatedDate).ToList();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _ticketService.GetByIdStringGuidAsync(id);

                if (result.Status != TicketStatus.Draft)
                {
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(400, "Bu kayıt silmeye uygun değil"));

                }

                await _ticketGenericService.SoftDeleteAsync(id);

                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, ex.Message));
            }
        }

        [HttpPost("Assign")]
        public async Task<IActionResult> Assign(TicketManagerUpdateDto dto)
        {
            if (dto.assigngDto!.UserAppId != null && dto.assigngDto.TicketTeamID != null)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(400, "Aynı anda iki gruba atama yapılamaz"));
            }


            if (String.IsNullOrEmpty(dto.managerDto.TicketDepartmentId))
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(400, "Atanılacak Departman Boş Olamaz"));
            }
            _unitOfWork.BeginTransaction();


            var serviceGeneric = await _ticketGenericService.Include();

            var ticketResult = serviceGeneric.Include(e => e.TicketAssigne).Where(e => e.Id == new Guid(dto.managerDto.Id));



            var ticket = ticketResult.FirstOrDefault();


            bool reassing = false;

            //atanma islemi olmadan talep iptal edilirse


            if (dto.assigngDto!.UserAppId == null && dto.assigngDto.TicketTeamID == null)
            {

                if (ticket.TicketAssigne != null)
                {
                    if (ticket.TicketAssigne.UserAppId != null)
                    {
                        reassing = true;
                        dto.assigngDto.UserAppId = new Guid(ticket.TicketAssigne.UserAppId);

                    }
                    else if (ticket.TicketAssigne.TicketTeamID != null)
                    {
                        reassing = true;
                        dto.assigngDto.TicketTeamID = ticket.TicketAssigne!.TicketTeamID;
                    }
                }
                else
                {

                    var result = await _userManager.FindByNameAsync(User.Identity.Name);

                    dto.assigngDto.UserAppId = new Guid(result.Id);
                }
            }

            List<string> tolist = new List<string>();
            bool hasAssigg = false;
            string subject = "";
            if (dto.assigngDto!.UserAppId != null || dto.assigngDto.TicketTeamID != null)
            {



                if (dto.assigngDto.TicketTeamID != null)
                {
                    hasAssigg = true;
                    dto.assigngDto.UserAppId = null;
                    //ticket.TicketAssigneId = dto.assigngDto.TicketTeamID.ToString();

                    //utils.Utils.SendMail("Yeni İş Kodu Oluşturuldu Bilgilendirme", htmlBody, tolist);
                    ticket.isTeam = true;

                    var service = await _tickeTeamService.Include();
                    var result = service.Where(e => dto.assigngDto.TicketTeamID != null
                                  && e.Id == Guid.Parse(dto.assigngDto.TicketTeamID.ToString())).Include(e => e.TeamList).ThenInclude(e => e.UserApp).FirstOrDefault();

                    if (reassing == false)
                    {
                        subject = "Takımınıza Yeni Talep Atandı";
                    }
                    else
                    {
                        subject = "Takımınıza Atanan Talepte Değişiklik Oldu";
                    }


                    foreach (var item in result.TeamList)
                    {

                        if (item.UserApp != null)
                        {
                            tolist.Add(item.UserApp.Email!);

                        }
                    }
                }
                else
                {

                    hasAssigg = true;
                    //   dto.assigngDto.TicketTeamID = null;

                    var result = await _userManager.FindByIdAsync(dto.assigngDto!.UserAppId!.ToString());

                    tolist.Add(result.Email);

                    // TAKIM LİDERİNE DE BİLDİRİM MAİLİ GİTMELİ
                    var dept = await _ticketDepartments.Where(e => e.Id == result.TicketDepartmentId);
                    var managerId = dept.Data.Select(e => e.ManagerId).FirstOrDefault();

                    if (!string.IsNullOrEmpty(managerId))
                    {
                        var managerUser = await _userManager.FindByIdAsync(managerId);

                        if (managerUser != null && !tolist.Contains(managerUser.Email))
                        {
                            tolist.Add(managerUser.Email);
                        }
                    }

                    if (reassing == true)
                    {
                        subject = "Size Atanılan Talepte Değişiklik Oldu";
                    }
                    else
                    {
                        subject = "Size Talep atandı";
                    }
                    //ticket.TicketAssigneId = dto.assigngDto.UserAppId.ToString();
                    ticket.isTeam = false;
                }

                ticket.TicketAssigne = _mapper.Map<TicketAssigne>(dto.assigngDto);

                ticket.TicketAssigne.Status = dto.managerDto.Status;

                ticket.TicketAssigne.Description = dto.assigngDto.Description;

            }



            ticket.Status = dto.managerDto.Status;
            //Tahmini bitiş zamanını güncelle
            ticket.EstimatedDeadline = dto.managerDto.EstimatedDeadline;
            ticket.TicketProjectId = dto.managerDto.TicketProjectId;



            bool isChangeDepartment = false;
            if (ticket.TicketDepartmentId.ToString() != dto.managerDto.TicketDepartmentId)
            {
                isChangeDepartment = true;
            }
            ticket.TicketDepartmentId = new Guid(dto.managerDto.TicketDepartmentId);



            await _ticketService.UpdateAsync(ticket);
            var resultmail = await GetById(ticket.Id);

            if (isChangeDepartment)
            {

                var service = await _ticketDepartments.Include();
                var result = service.Include(e => e.Manager).FirstOrDefault();
                var managerEmail = result.Manager.Email;
                List<string> managertolist = new List<string>();
                tolist.Add(managerEmail);


                SendTicketMailAsync(resultmail, tolist, "Bölümünüze Talep Yönlendirildi");
            }



            List<string> createMailList = new List<string>();
            var createUser = await _userManager.FindByNameAsync(ticket.CreatedBy);
            createMailList.Add(createUser.Email);

            // CC kişilerine de düzenleme maili gider
            if (ticket.AddedMailAddresses != null)
            {
                var addedMails = ticket.AddedMailAddresses.Split(';', StringSplitOptions.RemoveEmptyEntries);
                if (addedMails != null && addedMails.Length > 0)
                {
                    foreach (var addedMail in addedMails)
                    {
                        if (!createMailList.Contains(addedMail))
                        {
                            createMailList.Add(addedMail);
                        }
                    }
                }
            }


            SendTicketMailAsync(resultmail, createMailList, "Talebiniz üzerinde güncelleme");



            //SendTicketMailAsync(resultmail, tolist, subject);


            //ilgili kisiler duzenlemesi
            await AddRelPersonsAsync(dto.notificationsInsertDtos, dto.assigngDto.TicketsId);
            //ilgili kisilere mail gonderme
            List<string> relPersonMailList = new List<string>();
            foreach (var item in dto.notificationsInsertDtos)
            {
                var relPerson = await _userManager.FindByIdAsync(item.UserAppId);
                relPersonMailList.Add(relPerson.Email);
            }
            if (relPersonMailList.Count > 0)
            {
                //SendTicketMailAsync(resultmail, relPersonMailList, "Talep Bilginize Sunulmuştur");
            }


            SendTicketMailAsync(resultmail, tolist, subject, relPersonMailList);

            //_ticketAssigneService.AddAsync(dto.assigngDto)
            _unitOfWork.Commit();

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));
            //var dto = await _ticketFileGenericService.GetByIdGuidAsync(id);
            //return _mapper.Map<TicketFileDto>(dto.Data);

        }

        //[HttpGet("GetTicketSubjectList")]
        //public List<TicketSourceListDto> GetTicketSubjectList()
        //{
        //    List<TicketSourceListDto> list = new List<TicketSourceListDto>();

        //    list.Add(new TicketSourceListDto { Id = 1, Name = "Genel" });
        //    list.Add(new TicketSourceListDto { Id = 2, Name = "Bordro" });
        //    list.Add(new TicketSourceListDto { Id = 3, Name = "Pozitif Zaman Yönetimi" });
        //    list.Add(new TicketSourceListDto { Id = 4, Name = "Success Factors" });
        //    list.Add(new TicketSourceListDto { Id = 5, Name = "ABAP" });
        //    list.Add(new TicketSourceListDto { Id = 6, Name = "Fiori Btp2" });
        //    return list;
        //}


        //[HttpGet("GetTicketSla")]
        //public List<TicketSourceListDto> GetTicketSla()
        //{
        //    List<TicketSourceListDto> list = new List<TicketSourceListDto>();

        //    list.Add(new TicketSourceListDto { Id = 1, Name = "Genel" });
        //    list.Add(new TicketSourceListDto { Id = 2, Name = "Bordro" });
        //    list.Add(new TicketSourceListDto { Id = 3, Name = "Pozitif Zaman Yönetimi" });
        //    list.Add(new TicketSourceListDto { Id = 4, Name = "Success Factors" });
        //    list.Add(new TicketSourceListDto { Id = 5, Name = "ABAP" });
        //    list.Add(new TicketSourceListDto { Id = 6, Name = "Fiori Btp2" });
        //    return list;
        //}
        [HttpGet("TicketSubject")]
        public IActionResult GetTicketSubject()
        {
            var values = Enum.GetValues(typeof(TicketSubject))
                             .Cast<TicketSubject>()
                             .Select(e => new
                             {
                                 Id = (int)e,
                                 Name = e.ToString(),
                                 Description = GetEnumDescription(e)
                             });

            return Ok(values);
        }
        [HttpGet("TicketSLA")]
        public IActionResult GetTicketSLA()
        {
            var values = Enum.GetValues(typeof(TicketSLA))
                             .Cast<TicketSLA>()
                             .Select(e => new
                             {
                                 Id = (int)e,
                                 Name = e.ToString(),
                                 Description = GetEnumDescription(e)
                             });

            return Ok(values);
        }

        [HttpGet("TicketType")]
        public IActionResult GetTicketType()
        {
            var values = Enum.GetValues(typeof(TicketType))
                             .Cast<TicketType>()
                             .Select(e => new
                             {
                                 Id = (int)e,
                                 Name = e.ToString(),
                                 Description = GetEnumDescription(e)
                             });

            return Ok(values);
        }
        [HttpGet("ticket-priorities")]
        public IActionResult GetTicketPriorities()
        {
            var values = Enum.GetValues(typeof(TicketPriority))
                             .Cast<TicketPriority>()
                             .Select(e => new
                             {
                                 Id = (int)e,
                                 Name = e.ToString(),
                                 Description = GetEnumDescription(e)
                             });

            return Ok(values);
        }
        [HttpGet("ticket-status")]
        public IActionResult GetTicketStatus()
        {
            var allowedStatuses = new[] {
                TicketStatus.Draft,
                TicketStatus.Open,
                TicketStatus.InProgress,
                TicketStatus.InternalTesting,
                TicketStatus.CustomerTesting ,
                TicketStatus.WaitingForCustomer ,
                TicketStatus.Resolved ,
                TicketStatus.Cancelled ,
                TicketStatus.ConsultantWaiting,
                TicketStatus.InApprove
            };

            var values = Enum.GetValues(typeof(TicketStatus))
                             .Cast<TicketStatus>()
                             .Where(e => allowedStatuses.Contains(e)) // Sadece belirlenenleri filtrele
                             .Select(e => new
                             {
                                 Id = (int)e,
                                 Name = e.ToString(),
                                 Description = GetEnumDescription(e)
                             });

            return Ok(values);
        }
        [HttpGet("check-perm")]
        public async Task<ActionResult<TicketPermDto>> CheckHavePermAsync()
        {
            var loginName = User.Identity.Name;
            //var loginUser = await _userService.GetUserByEmailAsync(loginName);
            var loginUser = _userManager.Users.Where(e => e.Email == loginName).Select(e => new { e.Id, e.FirstName, e.LastName }).FirstOrDefault();

            if (loginUser == null)
            {
                return NotFound(new { message = "Kullanıcı bulunamadı" });
            }

            bool hasTicketPerm = false;
            bool canEdit = false;
            if (_tenantContext?.CurrentTenantId != null)
            {
                var ut = await _userTenantService.GetByUserAndTenantAsync(loginUser.Id, _tenantContext.CurrentTenantId.Value);
                if (ut != null)
                {
                    hasTicketPerm = ut.HasTicketPermission;
                    canEdit = ut.canEditTicket;
                }
            }
            // Tenant context yoksa, tenant-bazlı izinleri değerlendiremeyiz

            var sendData = new TicketPermDto
            {
                Id = loginUser.Id,
                Name = $"{loginUser.FirstName} {loginUser.LastName}",
                Perm = hasTicketPerm,
                CanEditTicket = canEdit
            };

            return sendData;
        }
        [HttpGet("check-othercompanyperm")]
        public async Task<ActionResult<TicketPermDto>> CheckHaveOtherCompanyPermAsync()
        {
            var loginName = User.Identity.Name;
            //var loginUser = await _userService.GetUserByEmailAsync(loginName);
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
                    perm = ut.HasOtherCompanyPermission;
                }
            }
            // Tenant context yoksa, tenant-bazlı izinleri değerlendiremeyiz

            var sendData = new TicketPermDto
            {
                Id = loginUser.Id,
                Name = $"{loginUser.FirstName} {loginUser.LastName}",
                Perm = perm
            };

            return sendData;
        }
        [HttpGet("GetAssingList")]
        public async Task<ActionResult<List<TicketAssigneListDto>>> GetAssingList(string ticketId)
        {
            var loginName = User.Identity.Name;


            var service = await _ticketAssigneService.Include();

            var list = service
              .Where(e => e.TicketsId == new Guid(ticketId))
              .Select(e => new
              {
                  e.Id,
                  e.CreatedDate,
                  e.Status,
                  // Ticket'tan ihtiyacınız olan diğer alanlar
                  UserApp = e.UserApp != null ? new
                  {
                      e.UserApp.Id,
                      e.UserApp.NormalizedUserName,
                      e.UserApp.FirstName,
                      e.UserApp.LastName
                  } : null,
                  TicketTeam = e.TicketTeam != null ? new
                  {
                      e.TicketTeam.Id,
                      e.TicketTeam.Name
                      // TicketTeam'den ihtiyacınız olan diğer alanlar
                  } : null,
                  Description = e.Description != null ? e.Description : "",
                  CreatedBy = e.CreatedBy != null ? e.CreatedBy : ""
              })
              .ToList();

            var result = list.Select(ticket => new TicketAssigneListDto
            {
                CreateDate = ticket.CreatedDate,
                StatusId = ticket.Status,
                Status = ticket.Status.GetDescription(),
                Name = ticket.UserApp != null
              ? $"{ticket.UserApp.FirstName} {ticket.UserApp.LastName}" // UserApp null değilse ad ve soyad
            : ticket.TicketTeam != null
                ? ticket.TicketTeam.Name // UserApp null ise, TicketTeam null değilse takım adı
                : "No Team or User", // H
                Description = ticket.Description,
                CreatedBy = ticket.CreatedBy
            }).ToList();

            foreach (var item in result)
            {
                var user = _userManager.Users.Where(e => e.Email == item.CreatedBy).Select(e => new { e.FirstName, e.LastName }).FirstOrDefault();
                var username = $@"{user.FirstName} {user.LastName}";
                item.CreatedBy = username;
            }

            return result.OrderByDescending(c => c.CreateDate).ToList();
        }
        private async Task SendTicketMailAsync(TicketListDto dto, List<string> tolist, string subject, List<string>? cclist = null)
        {
            string dbName = _dbNameHelper.GetDatabaseName();
            var sendMail = "";
            string talepEden = dto.CreatedBy;
            string sirketAdi = dto.WorkCompanyName;
            string musteriAdi = dto.CustomerRefName;
            string department = dto.TicketDepartmentText;
            string musteriSistemBilgisi = dto.WorkCompanySystemName;
            string onemDerecesi = dto.PriorityText;
            string musteriTipi = "";
            string ticketDurumu = dto.StatusText;
            string talepDetayi = dto.TicketComment[0].Body;
            string olusturmaTarihi = dto.CreatedDate.ToString();
            string atamaYapan = dto.UpdatedBy;
            string number = dto.TicketNumber.ToString();
            string atanmayapilan = dto.TicketAssigneText.ToString();
            string not = dto.AssigneDescription;
            string title = dto.Title;
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
                                <!-- Logo ve Başlık birlikte yer alacak -->
                                <td style=""background-color: white; padding:12px; width: auto;"">
                                    <img src=""{VesaLogo.Logo}"" alt=""Logo"" width=""100"" height=""60"" style=""display: block; width: 100%; height: auto;"">
                                </td>
                                <td style=""background-color: white; padding:12px; width: auto;"">
                                    <img src=""{VesaLogo.ColorImg}"" alt=""Logo"" width=""650"" height=""20"" style=""display: block; width: 100%; height: auto;"">
                                </td>
                            </tr>
                        </table>
                    </td>

                    <!-- CONTENT -->
                    <tr>
                        <td style=""padding:20px;"">
                            <h2 style="" font-size:20px; margin-bottom:10px;"">{subject}</h2>
                            <table width=""100%"" cellspacing=""0"" cellpadding=""10"" border=""0"" style=""border-collapse: collapse;"">
                                <tr >
                                    <th style=""border:1px solid #ddd; background-color:#f0f4f8; text-align:left; padding:12px; font-size:14px;"">Talep Numarası</th>
                                    <td style=""border:1px solid #ddd; padding:12px; font-size:14px;"">{number}</td>
                                </tr>
                                <tr >
                                    <th style=""border:1px solid #ddd; background-color:#f0f4f8; text-align:left; padding:12px; font-size:14px;"">Başlık</th>
                                    <td style=""border:1px solid #ddd; padding:12px; font-size:14px;"">{title}</td>
                                </tr>
                                <tr>
                                    <th style=""border:1px solid #ddd; background-color:#f0f4f8; text-align:left; padding:12px; font-size:14px;"">Talep Sahibi</th>
                                    <td style=""border:1px solid #ddd; padding:12px; font-size:14px;"">{talepEden}</td>
                                </tr>
                                <tr>
                                    <th style=""border:1px solid #ddd; background-color:#f0f4f8; text-align:left; padding:12px; font-size:14px;"">Müşteri Adı</th>
                                    <td style=""border:1px solid #ddd; padding:12px; font-size:14px;"">{musteriAdi}</td>
                                </tr>
                                <tr>
                                    <th style=""border:1px solid #ddd; background-color:#f0f4f8; text-align:left; padding:12px; font-size:14px;"">Müşteri Sistem Bilgisi</th>
                                    <td style=""border:1px solid #ddd; padding:12px; font-size:14px;"">{musteriSistemBilgisi}</td>
                                </tr>
                                <tr>
                                    <th style=""border:1px solid #ddd; background-color:#f0f4f8; text-align:left; padding:12px; font-size:14px;"">Bölüm</th>
                                    <td style=""border:1px solid #ddd; padding:12px; font-size:14px;"">{department}</td>
                                </tr>
                                <tr>
                                    <th style=""border:1px solid #ddd; background-color:#f0f4f8; text-align:left; padding:12px; font-size:14px;"">Öncelik</th>
                                    <td style=""border:1px solid #ddd; padding:12px; font-size:14px;"">{onemDerecesi}</td>
                                </tr>
                                <tr>
                                    <th style=""border:1px solid #ddd; background-color:#f0f4f8; text-align:left; padding:12px; font-size:14px;"">Durum</th>
                                    <td style=""border:1px solid #ddd; padding:12px; font-size:14px;"">{ticketDurumu}</td>
                                </tr>
                                <tr>
                                    <th style=""border:1px solid #ddd; background-color:#f0f4f8; text-align:left; padding:12px; font-size:14px;"">Açıklama</th>
                                    <td style=""border:1px solid #ddd; padding:12px; font-size:14px;"">{talepDetayi}</td>
                                </tr>
                                <tr >
                                    <th style=""border:1px solid #ddd; background-color:#f0f4f8; text-align:left; padding:12px; font-size:14px;"">Atanan Kişi</th>
                                    <td style=""border:1px solid #ddd; padding:12px; font-size:14px;"">{atanmayapilan}</td>
                                </tr>
                                <tr>
                                    <th style=""border:1px solid #ddd; background-color:#f0f4f8; text-align:left; padding:12px; font-size:14px;"">Not</th>
                                    <td style=""border:1px solid #ddd; padding:12px; font-size:14px;"">{not}</td>
                                </tr>
                            </table>
                            <p style=""color:#0073e6;""><strong>Destek Sistemine Giriş için: https://support.vesa-tech.com/</strong></p>
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

            if (cclist != null)
            {
                utils.Utils.SendMail($"Talep Numarası : {number} - {title}", emailBody, tolist, cclist);
            }
            else
            {
                utils.Utils.SendMail($"Talep Numarası : {number} - {title}", emailBody, tolist);
            }
            //departman yoneticisi
            //var result = await _ticketDepartments.Include();
            //var resData = result.Where(e => e.Id == new Guid(dto.TicketDepartmentId)).Select(e => new { e.Manager.Email }).FirstOrDefault();

            //if (resData != null)
            //{
            //    if (resData.Email != null && resData.Email != "")
            //    {
            //        tolist.Add(resData.Email);

            //        //tolist.Add("murat.merdogan@vesacons.com");
            //        utils.Utils.SendMail("Yeni Talep Oluşturuldu", emailBody, tolist);
            //    }
            //}


        }
        private async Task SendInfoCommentMail(string ticketNo, string ticketTitle, string comment, List<string> tolist, string subject)
        {
            var user = _userManager.Users.Where(e => e.Email == User.Identity.Name).Select(e => new { e.FirstName, e.LastName, e.WorkCompanyId, e.WorkCompany }).FirstOrDefault();
            var username = $@"{user.FirstName} {user.LastName}";
            var userCompany = user.WorkCompany.Name;
            string dbName = _dbNameHelper.GetDatabaseName();
            string emailBody = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <title>Şifre Sıfırlama</title>
</head>
<body style=""margin:0; padding:0; font-family: Arial, sans-serif; background-color:#f4f7fc;"">
     <table role=""presentation"" width=""100%"" cellspacing=""0"" cellpadding=""0"" border=""0"" style=""background-color:#f4f7fc; padding:20px;"">
        <tr>
            <td align=""center"">
                <table role=""presentation"" width=""800"" cellspacing=""0"" cellpadding=""0"" border=""0"" style=""background-color:#ffffff; box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.1);"">
                    <!-- HEADER -->
                    <td style=""background-color:white;"">
                        <table style=""width: 100%; table-layout: fixed; display: inline-table;"">
                            <tr>
                                <!-- Logo ve Başlık birlikte yer alacak -->
                                <td style=""background-color: white; padding:12px; width: auto;"">
                                    <img src=""{VesaLogo.Logo}"" alt=""Logo"" width=""100"" height=""60"" style=""display: block; width: 100%; height: auto;"">
                                </td>
                                <td style=""background-color: white; padding:12px; width: auto;"">
                                    <img src=""{VesaLogo.ColorImg}"" alt=""Logo"" width=""650"" height=""20"" style=""display: block; width: 100%; height: auto;"">
                                </td>
                            </tr>
                        </table>
                    </td>

                    <!-- CONTENT -->
                    <tr>
                        <td style=""padding:20px;"">
                            <h2 style=""font-size:18px; margin-bottom:10px;"">{subject}</h2>
                            <p><strong>""{ticketNo} - {ticketTitle}""</strong> talebi için yeni bir mesaj eklenmiştir:</p>
                            <p><i><strong>({userCompany})</strong> {username} :</i></p>
                            <p><i>{comment}</i></p>
                            </br>
                            <p>Bilgilerinize</p>
                            </br>
                            <p style=""color:#0073e6;""><strong>Destek Sistemine Giriş için: https://support.vesa-tech.com/</strong></p>
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
</html>";
            utils.Utils.SendMail($"Talep Numarası : {ticketNo} - {ticketTitle}", emailBody, tolist);


        }
        private async Task SendMailTicketInfo(string subject, string email)
        {
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
                                <!-- Logo ve Başlık birlikte yer alacak -->
                                <td style=""background-color: white; padding:12px; width: auto;"">
                                    <img src=""{VesaLogo.Logo}"" alt=""Logo"" width=""100"" height=""60"" style=""display: block; width: 100%; height: auto;"">
                                </td>
                                <td style=""background-color: white; padding:12px; width: auto;"">
                                    <img src=""{VesaLogo.ColorImg}"" alt=""Logo"" width=""650"" height=""20"" style=""display: block; width: 100%; height: auto;"">
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
                                    <th style=""border:1px solid #ddd; background-color:#f0f4f8; text-align:left; padding:12px; font-size:14px;"">Merhaba, e-posta adresiniz sistemimizde kayıtlı olmadığı için yukarıda belirtilen konulu talebiniz oluşturulamamıştır. Lütfen departman yöneticisi ile iletişime geçiniz.</th>
                                </tr>
                            </table>
                            <p style=""color:#0073e6;""><strong>Destek Sistemine Giriş için: https://support.vesa-tech.com/</strong></p>
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

            if (email != null)
            {
                var recipients = new List<string> { email };
                utils.Utils.SendMail($"Vesacons Bilgilendirme E-postası", emailBody, recipients, null);
            }

        }
        [HttpGet("FilteredAllTickets")]
        public async Task<TicketDtoResult> GetFilteredAllTickets(int skip = 0, int top = 50, string? pageDesc = "solveAllTicket", [FromQuery] string[]? statues = null, string? workCompanyId = null, string? assignedUser = null, string? assignedTeam = null, string? type = null, string? endDate = null, string? startDate = null, string? creator = null, string? customer = null, bool closeInc = false, string? title = null, [FromQuery] List<string>? departmentId = null, [FromQuery] List<string>? ticketProjectId = null)
        {
            TicketDtoResult result = new TicketDtoResult();
            List<TicketListDto> values = [];
            var _count = 0;

            TicketFilters filters = new TicketFilters
            {
                workCompanyId = String.IsNullOrEmpty(workCompanyId) ? null : workCompanyId,
                assignedUser = String.IsNullOrEmpty(assignedUser) ? null : assignedUser,
                assignedTeam = String.IsNullOrEmpty(assignedTeam) ? null : assignedTeam,
                type = String.IsNullOrEmpty(type) ? null : type,
                endDate = String.IsNullOrEmpty(endDate) ? null : endDate,
                startDate = String.IsNullOrEmpty(startDate) ? null : startDate,
                creator = String.IsNullOrEmpty(creator) ? null : creator,
                customer = String.IsNullOrEmpty(customer) ? null : customer,
                talepBaslik = String.IsNullOrEmpty(title) ? null : title,
                talepNo = null,
                departmentId = (departmentId == null || departmentId.Count == 0) ? null : departmentId,
                ticketProjectId = (ticketProjectId == null || ticketProjectId.Count == 0) ? null : ticketProjectId,
            };



            if (pageDesc == "solveAllTicket")
            {
                if (statues == null || statues.Length == 0)
                {
                    var res = await _ticketService.GetAllAssignTicketsWithEnumDescriptionsAsync(User.Identity.Name, skip, top, null, filters);
                    values = res.TicketList;
                    _count = res.Count;


                }
                else
                {
                    var statusValues = statues
                        .Where(s => !string.IsNullOrEmpty(s) && int.TryParse(s, out _))
                        .Select(int.Parse)
                        .ToList();

                    var res = await _ticketService.GetAllAssignTicketsWithEnumDescriptionsAsync(User.Identity.Name, skip, top, statusValues, filters);
                    values = res.TicketList;
                    _count = res.Count;
                }
            }
            else
            {
                if (statues == null || statues.Length == 0)
                {
                    var res = await _ticketService.GetAllTicketsWithEnumDescriptionsAsync(User.Identity.Name, skip, top, null, filters);
                    values = res.TicketList;
                    _count = res.Count;
                }
                else
                {
                    var statusValues = statues
                        .Where(s => !string.IsNullOrEmpty(s) && int.TryParse(s, out _))
                        .Select(int.Parse)
                        .ToList();

                    var res = await _ticketService.GetAllTicketsWithEnumDescriptionsAsync(User.Identity.Name, skip, top, statusValues, filters);
                    values = res.TicketList;
                    _count = res.Count;
                }

            }


            result.TicketList = values;
            result.Count = _count;
            return result;
        }
        [HttpGet("SearchTicket")]
        public async Task<TicketDtoResult> SearchTicket(string? pageDesc = "solveAllTicket", string? talepNo = null, int skip = 0, int top = 50)
        {
            TicketDtoResult result = new TicketDtoResult();
            List<TicketListDto> values = [];
            var _count = 0;

            TicketFilters filters = new TicketFilters
            {
                workCompanyId = null,
                assignedUser = null,
                assignedTeam = null,
                type = null,
                endDate = null,
                startDate = null,
                creator = null,
                customer = null,
                talepBaslik = null,
                talepNo = talepNo
            };

            if (pageDesc == "solveAllTicket")
            {
                var res = await _ticketService.GetAllAssignTicketsWithEnumDescriptionsAsync(User.Identity.Name, skip, top, null, filters);
                values = res.TicketList;
                _count = res.Count;
            }
            else
            {
                var res = await _ticketService.GetAllTicketsWithEnumDescriptionsAsync(User.Identity.Name, skip, top, null, filters);
                values = res.TicketList;
                _count = res.Count;
            }

            result.TicketList = values;
            result.Count = _count;
            return result;
        }

        [HttpGet("getTicketPdf/{id}")]
        public async Task<string> getTicketPdf(Guid id)
        {

            var service = await _ticketGenericService.Include();
            var data = await service.Include(td => td.UserApp).Include(e => e.WorkCompany).Include(e => e.WorkCompanySystemInfo).Include(e => e.TicketComment).ThenInclude(tc => tc.Files)
                      .Include(ticket => ticket.TicketAssigne)
                      .ThenInclude(ticket => ticket.UserApp)
                      .Include(ticket => ticket.TicketAssigne)
                      .ThenInclude(ticket => ticket.TicketTeam)
                      .Select(td => new
                      {
                          td.Id,
                          //UserApp=td.UserApp,
                          UserApp = td.UserApp != null
             ? new { td.UserApp.Id, td.UserApp.FirstName, td.UserApp.UserName, td.UserApp.LastName }
             : null,
                          td.AddedMailAddresses,

                          td.UniqNumber,
                          td.WorkCompany,
                          td.WorkCompanySystemInfo,
                          td.TicketCode,
                          td.Type,
                          td.ApproveStatus,
                          td.UserAppId,
                          td.WorkCompanyId,
                          td.CustomerRefId,
                          td.CustomerRef,
                          td.CreatedDate,
                          td.Title,
                          td.Status,
                          td.IsFilePath,
                          td.FilePath,
                          td.TicketSubject,
                          td.CreatedBy,
                          td.Priority,
                          td.UpdatedBy,
                          td.IsFromEmail,
                          td.WorkCompanySystemInfoId,
                          td.TicketSLA,
                          TicketAssigne = td.TicketAssigne != null ? new
                          {
                              UserApp = td.TicketAssigne != null ? td.TicketAssigne.UserApp != null
                                      ? new { td.TicketAssigne.UserApp.Id, td.TicketAssigne.UserApp.UserName, td.TicketAssigne.UserApp.LastName, td.TicketAssigne.UserApp.NormalizedUserName, td.TicketAssigne.UserApp.FirstName }
                                     : null : null,
                              TicketTeam = td.TicketAssigne != null ? td.TicketAssigne.TicketTeam != null
                                      ? new { td.TicketAssigne.TicketTeam.Name }
                                  : null : null,
                              Description = td.TicketAssigne != null ? td.TicketAssigne.Description : ""
                          } : null,
                          //td.TicketAssigne,
                          //td.TicketAssigne = new
                          //{

                          //    UserApp = td.TicketAssigne != null ? td.TicketAssigne.UserApp != null
                          //        ? new { td.TicketAssigne.UserApp.Id, td.TicketAssigne.UserApp.NormalizedUserName, td.TicketAssigne.UserApp.FirstName, td.TicketAssigne.UserApp.LastName }
                          //       : null : null,
                          //    TicketTeam = td.TicketAssigne != null ? td.TicketAssigne.TicketTeam != null
                          //        ? new { td.TicketAssigne.TicketTeam.Name }
                          //    : null : null
                          //},
                          td.TicketDepartment,
                          td.TicketDepartmentId,
                          TicketComments = td.TicketComment.Select(tc => new TicketCommentDto
                          {
                              FilePath = tc.FilePath,
                              id = tc.Id.ToString(),
                              Body = td.IsFilePath == true ? ReadMailBodyFromPath(tc.FilePath) : tc.Body,
                              TicketId = td.Id.ToString(),
                              CreatedDate = tc.CreatedDate,
                              CreatedBy = tc.CreatedBy,

                              Files = tc.Files.Select(f => new TicketFileDto
                              {
                                  TicketCommentId = tc.Id.ToString(),
                                  id = f.Id.ToString(),
                                  FileName = f.FileName,
                                  FilePath = td.IsFilePath == true ? f.FilePath : null,
                                  Base64 = td.IsFilePath == true ? GetFileBase64(f.FilePath) : f.Base64
                              }).ToList()
                          }).OrderBy(e => e.CreatedDate).ToList()
                      })
     .FirstOrDefaultAsync(td => td.Id == id);

            if (data != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    // 📄 PDF Oluşturma
                    iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 25, 25, 30, 30);
                    PdfWriter.GetInstance(pdfDoc, memoryStream);
                    pdfDoc.AddAuthor("VESA");
                    pdfDoc.AddTitle("Talep Detay Raporu");
                    pdfDoc.Open();

                    // Kurumsal Renkler
                    BaseColor corporateBlue = new BaseColor(58, 127, 62);   // Koyu Mavi
                    BaseColor corporateGray = new BaseColor(88, 89, 91);   // Kurumsal Gri
                    BaseColor lightGray = new BaseColor(254, 251, 246);    // Açık Gri (arka plan için)

                    // Font dosyasının yolu
                    string fontPath = Path.Combine(Directory.GetCurrentDirectory(), "Fonts", "Roboto-Regular.ttf");
                    // Fontu yükle
                    BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    iTextSharp.text.Font titleFont = new iTextSharp.text.Font(baseFont, 18, iTextSharp.text.Font.BOLD, corporateGray); // 20pt ve kalın font

                    string base64 = VesaLogo.Logo;
                    if (base64.Contains(","))
                    {
                        base64 = base64.Substring(base64.IndexOf(",") + 1);
                    }
                    byte[] logoBytes = Convert.FromBase64String(base64);
                    using (MemoryStream ms = new MemoryStream(logoBytes))
                    {
                        iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(ms);
                        logo.Alignment = Element.ALIGN_CENTER;
                        logo.ScaleToFit(120f, 120f);
                        logo.SpacingAfter = 40f;
                        pdfDoc.Add(logo);
                    }
                    pdfDoc.Add(new iTextSharp.text.Paragraph(" ", new iTextSharp.text.Font(baseFont, 8)) { SpacingAfter = 10f });

                    // Başlık
                    iTextSharp.text.Paragraph title = new iTextSharp.text.Paragraph("Talep Detay Raporu", titleFont);
                    title.Alignment = Element.ALIGN_CENTER;
                    title.SpacingAfter = 30f;
                    pdfDoc.Add(title);

                    iTextSharp.text.Font infoFont = new iTextSharp.text.Font(baseFont, 12, iTextSharp.text.Font.NORMAL, corporateGray);

                    // Tablo oluştur (2 sütunlu)
                    PdfPTable table = new PdfPTable(2);

                    // Sütun genişliklerini belirle
                    float[] widths = new float[] { 1f, 2f };
                    table.SetWidths(widths);
                    table.WidthPercentage = 100f;

                    // Talep Numarası
                    PdfPCell cell = new PdfPCell(new Phrase("Talep Numarası:", infoFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;  // Hücre sınırlarını kaldır
                    cell.PaddingTop = 5f;  // Üstten boşluk
                    cell.PaddingBottom = 5f;  // Alttan boşluk
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(data.UniqNumber.ToString(), infoFont)); // UniqNumber'ı string'e çevir
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    cell.PaddingTop = 5f;  // Üstten boşluk
                    cell.PaddingBottom = 5f;  // Alttan boşluk
                    table.AddCell(cell);

                    // Talep Başlığı
                    cell = new PdfPCell(new Phrase("Talep Başlığı:", infoFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    cell.PaddingTop = 5f;  // Üstten boşluk
                    cell.PaddingBottom = 5f;  // Alttan boşluk
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(data.Title.ToString(), infoFont)); // Title'ı string'e çevir
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    cell.PaddingTop = 5f;  // Üstten boşluk
                    cell.PaddingBottom = 5f;  // Alttan boşluk
                    table.AddCell(cell);

                    // Şirket Adı
                    cell = new PdfPCell(new Phrase("Şirket:", infoFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    cell.PaddingTop = 5f;  // Üstten boşluk
                    cell.PaddingBottom = 5f;  // Alttan boşluk
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(data.WorkCompany.Name.ToString(), infoFont)); // Name'i string'e çevir
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    cell.PaddingTop = 5f;  // Üstten boşluk
                    cell.PaddingBottom = 5f;  // Alttan boşluk
                    table.AddCell(cell);

                    // Müşteri Adı
                    cell = new PdfPCell(new Phrase("Müşteri:", infoFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    cell.PaddingTop = 5f;  // Üstten boşluk
                    cell.PaddingBottom = 5f;  // Alttan boşluk
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(data.CustomerRef.Name.ToString(), infoFont)); // Name'i string'e çevir
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    cell.PaddingTop = 5f;  // Üstten boşluk
                    cell.PaddingBottom = 5f;  // Alttan boşluk
                    table.AddCell(cell);

                    // Oluşturan Kişi
                    cell = new PdfPCell(new Phrase("Oluşturan Kişi:", infoFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    cell.PaddingTop = 5f;  // Üstten boşluk
                    cell.PaddingBottom = 5f;  // Alttan boşluk
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(data.CreatedBy.ToString(), infoFont)); // CreatedBy'i string'e çevir
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    cell.PaddingTop = 5f;  // Üstten boşluk
                    cell.PaddingBottom = 5f;  // Alttan boşluk
                    table.AddCell(cell);

                    // Oluşturulma Tarihi
                    cell = new PdfPCell(new Phrase("Oluşturulma Tarihi:", infoFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    cell.PaddingTop = 5f;  // Üstten boşluk
                    cell.PaddingBottom = 5f;  // Alttan boşluk
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(data.CreatedDate.ToString(), infoFont)); // CreatedDate'i string'e çevir
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    cell.PaddingTop = 5f;  // Üstten boşluk
                    cell.PaddingBottom = 5f;  // Alttan boşluk
                    table.AddCell(cell);

                    // Yardım Konusu
                    cell = new PdfPCell(new Phrase("Yardım Konusu:", infoFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    cell.PaddingTop = 5f;  // Üstten boşluk
                    cell.PaddingBottom = 5f;  // Alttan boşluk
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(data.TicketSubject.ToString(), infoFont)); // TicketSubject'i string'e çevir
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    cell.PaddingTop = 5f;  // Üstten boşluk
                    cell.PaddingBottom = 5f;  // Alttan boşluk
                    table.AddCell(cell);

                    // Talep Önceliği
                    cell = new PdfPCell(new Phrase("Talep Önceliği:", infoFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    cell.PaddingTop = 5f;  // Üstten boşluk
                    cell.PaddingBottom = 5f;  // Alttan boşluk
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(data.Priority.ToString(), infoFont)); // Priority'i string'e çevir
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    cell.PaddingTop = 5f;  // Üstten boşluk
                    cell.PaddingBottom = 5f;  // Alttan boşluk
                    table.AddCell(cell);

                    // Talep Tipi
                    cell = new PdfPCell(new Phrase("Talep Tipi:", infoFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    cell.PaddingTop = 5f;  // Üstten boşluk
                    cell.PaddingBottom = 5f;  // Alttan boşluk
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(data.Type.ToString(), infoFont)); // Type'ı string'e çevir
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    cell.PaddingTop = 5f;  // Üstten boşluk
                    cell.PaddingBottom = 5f;  // Alttan boşluk
                    table.AddCell(cell);

                    // Talep SLA
                    cell = new PdfPCell(new Phrase("Talep SLA:", infoFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    cell.PaddingTop = 5f;  // Üstten boşluk
                    cell.PaddingBottom = 5f;  // Alttan boşluk
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(data.TicketSLA.ToString(), infoFont)); // TicketSLA'yı string'e çevir
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    cell.PaddingTop = 5f;  // Üstten boşluk
                    cell.PaddingBottom = 5f;  // Alttan boşluk
                    table.AddCell(cell);

                    // PDF'e tabloyu ekle
                    pdfDoc.Add(table);

                    // Başlık
                    iTextSharp.text.Paragraph title2 = new iTextSharp.text.Paragraph("Açıklamalar", titleFont);
                    title2.Alignment = Element.ALIGN_CENTER;
                    title2.SpacingAfter = 20f;
                    pdfDoc.Add(title2);

                    // Yorumlar
                    if (data.TicketComments != null && data.TicketComments.Any())
                    {
                        foreach (var comment in data.TicketComments)
                        {
                            // Yorum Başlığı (Tarih ve Kullanıcı)
                            iTextSharp.text.Paragraph header = new iTextSharp.text.Paragraph(
                                $"{comment.CreatedDate:dd.MM.yyyy HH:mm} - {comment.CreatedBy}", infoFont);
                            header.SpacingAfter = 3f;
                            pdfDoc.Add(header);

                            // Yorum Metni
                            string plainText = "";

                            if (data.IsFilePath == true)
                            {
                                HtmlDocument htmlDoc = new HtmlDocument();
                                htmlDoc.LoadHtml(comment.Body);
                                // Görsel ve stil kodlarını da ayıklamak isterseniz
                                foreach (var node in htmlDoc.DocumentNode.SelectNodes("//style|//script|//img|//comment()") ?? Enumerable.Empty<HtmlNode>())
                                {
                                    node.Remove();
                                }

                                // Temiz metni al
                                plainText = htmlDoc.DocumentNode.InnerText;
                                int index = plainText.IndexOf("&nbsp", StringComparison.OrdinalIgnoreCase);
                                if (index >= 0)
                                {
                                    plainText = plainText.Substring(0, index).Trim();
                                }

                            }
                            else
                            {
                                plainText = Regex.Replace(comment.Body, "<.*?>", string.Empty);
                            }

                            iTextSharp.text.Paragraph body = new iTextSharp.text.Paragraph(plainText, infoFont);
                            body.IndentationLeft = 10f;
                            body.SpacingAfter = 3f;
                            pdfDoc.Add(body);

                            //if (comment.Files != null && comment.Files.Any())
                            //{
                            //    Paragraph fileHeader = new Paragraph("Ek Dosyalar:", infoFont);
                            //    fileHeader.IndentationLeft = 10f;
                            //    fileHeader.SpacingAfter = 5f;
                            //    pdfDoc.Add(fileHeader);

                            //    foreach (var file in comment.Files)
                            //    {
                            //        Guid fileId = Guid.Parse(file.id);
                            //        var dto = await _ticketFileGenericService.GetByIdGuidAsync(fileId);

                            //        try
                            //        {
                            //            // "data:image/png;base64,..." gibi başlık varsa ayıkla
                            //            string base64Data = dto.Data.Base64.Contains(",")
                            //                ? dto.Data.Base64.Split(',')[1]
                            //                : dto.Data.Base64;

                            //            byte[] imageBytes = Convert.FromBase64String(base64Data);

                            //            using (MemoryStream ms = new MemoryStream(imageBytes))
                            //            {
                            //                var image = iTextSharp.text.Image.GetInstance(ms);
                            //                image.ScaleToFit(400f, 400f); // Gerekirse boyut ayarla
                            //                image.Alignment = Element.ALIGN_LEFT;
                            //                image.SpacingAfter = 15f;
                            //                image.IndentationLeft = 20f;

                            //                pdfDoc.Add(image); // PDF'e resmi ekle
                            //            }
                            //        }
                            //        catch (Exception ex)
                            //        {
                            //            Paragraph error = new Paragraph($"Görsel eklenemedi: {ex.Message}", infoFont);
                            //            error.SpacingAfter = 5f;
                            //            pdfDoc.Add(error);
                            //        }
                            //    }
                            //}

                            // Yorumlar arasında boşluk bırak
                            iTextSharp.text.Paragraph spacer = new iTextSharp.text.Paragraph(" ", infoFont);
                            spacer.SpacingAfter = 10f;
                            pdfDoc.Add(spacer);
                        }
                    }
                    else
                    {
                        iTextSharp.text.Paragraph noComment = new iTextSharp.text.Paragraph("Açıklama bulunmamaktadır.", infoFont);
                        noComment.SpacingAfter = 5f;
                        pdfDoc.Add(noComment);
                    }

                    pdfDoc.Close();
                    byte[] pdfBytes = memoryStream.ToArray();
                    return $"data:application/pdf;base64,{Convert.ToBase64String(pdfBytes)}";
                }
            }
            return null;
        }

        private string GetEnumDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = field?.GetCustomAttribute<DescriptionAttribute>();

            return attribute?.Description ?? value.ToString();
        }
        private async Task<IActionResult> AddRelPersonsAsync(List<TicketNotificationsInsertDto> dto, Guid ticketId)
        {
            try
            {

                var notifications = await _ticketNotificationService.Include();

                //ilgili ticketdaki kayitlari sil.
                var toDels = notifications.Where(e => e.TicketId == ticketId).ToList();
                foreach (var toDel in toDels)
                {
                    var result = await _ticketNotificationService.RemoveAsyncByGuid(toDel.Id);
                }

                foreach (var item in dto)
                {
                    //yeni kisileri ekle
                    var listDto = _mapper.Map<TicketNotificationsListDto>(item);
                    var res = await _ticketNotificationService.AddAsync(listDto);

                }

                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));


            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, ex.Message));
            }
        }
        [HttpGet("ExcelExport")]
        public async Task<ExcelAndGraphicData> ExcelExport(string? pageDesc = "solveAllTicket", [FromQuery] string[]? statues = null, string? workCompanyId = null, string? assignedUser = null, string? assignedTeam = null, string? type = null, string? endDate = null, string? startDate = null, string? creator = null, string? customer = null, bool closeInc = false, string? title = null, [FromQuery] List<string>? departmentId = null, [FromQuery] List<string>? ticketProjectId = null)
        {
            List<TicketListDto> values = [];
            var _count = 0;

            TicketFilters filters = new TicketFilters
            {
                workCompanyId = String.IsNullOrEmpty(workCompanyId) ? null : workCompanyId,
                assignedUser = String.IsNullOrEmpty(assignedUser) ? null : assignedUser,
                assignedTeam = String.IsNullOrEmpty(assignedTeam) ? null : assignedTeam,
                type = String.IsNullOrEmpty(type) ? null : type,
                endDate = String.IsNullOrEmpty(endDate) ? null : endDate,
                startDate = String.IsNullOrEmpty(startDate) ? null : startDate,
                creator = String.IsNullOrEmpty(creator) ? null : creator,
                customer = String.IsNullOrEmpty(customer) ? null : customer,
                talepNo = null,
                talepBaslik = title,
                departmentId = (departmentId == null || departmentId.Count == 0) ? null : departmentId,
                ticketProjectId = (ticketProjectId == null || ticketProjectId.Count == 0) ? null : ticketProjectId,
            };
            if (pageDesc == "solveAllTicket")
            {
                if (statues == null || statues.Length == 0)
                {
                    var res = await _ticketService.GetAllAssignTicketsWithEnumDescriptionsAsync(User.Identity.Name, 0, 0, null, filters);
                    values = res.TicketList;


                }
                else
                {
                    var statusValues = statues
                        .Where(s => !string.IsNullOrEmpty(s) && int.TryParse(s, out _))
                        .Select(int.Parse)
                        .ToList();

                    var res = await _ticketService.GetAllAssignTicketsWithEnumDescriptionsAsync(User.Identity.Name, 0, 0, statusValues, filters);
                    values = res.TicketList;
                }
            }
            else
            {

                if (statues == null || statues.Length == 0)
                {
                    var res = await _ticketService.GetAllTicketsWithEnumDescriptionsAsync(User.Identity.Name, 0, 0, null, filters);
                    values = res.TicketList;


                }
                else
                {
                    var statusValues = statues
                        .Where(s => !string.IsNullOrEmpty(s) && int.TryParse(s, out _))
                        .Select(int.Parse)
                        .ToList();

                    var res = await _ticketService.GetAllTicketsWithEnumDescriptionsAsync(User.Identity.Name, 0, 0, statusValues, filters);
                    values = res.TicketList;
                }
            }





            TicketCore core = new TicketCore();
            //var list = core.GetTaskList();
            string reportname = $"TicketList.xlsx";

            using ExcelPackage pack = new ExcelPackage();
            ExcelWorksheet ws = pack.Workbook.Worksheets.Add(reportname);

            List<ExcelListNew> list = new List<ExcelListNew>();
            for (int i = 0; i < values.Count; i++)
            {
                ExcelListNew item = new ExcelListNew();
                item.TicketNumber = values[i].TicketNumber.ToString();
                item.StatusText = values[i].StatusText;
                item.Title = values[i].Title;
                item.CustomerRefName = values[i].CustomerRefName;
                item.TicketAssigneText = values[i].TicketAssigneText;
                item.UserAppName = values[i].UserAppName;
                item.CreatedDate = values[i].CreatedDate.ToString();
                item.TicketDepartmentText = values[i].TicketDepartmentText;


                list.Add(item);


            }
            list = list.OrderByDescending(e => e.CreatedDate).ToList();

            ws.Cells["A1"].Value = "Talep No";
            ws.Cells["B1"].Value = "Durum";
            ws.Cells["C1"].Value = "Başlık";
            ws.Cells["D1"].Value = "Müşteri";
            ws.Cells["E1"].Value = "Atanan (Kişi/Takım)";
            ws.Cells["F1"].Value = "Oluşturan";
            ws.Cells["G1"].Value = "Tarih";
            ws.Cells["H1"].Value = "Departman";

            //ws.Cells["E1"].Style.Numberformat.Format = "mm-dd-yy";//or m/d/yy h:mm
            ws.Cells["A2"].LoadFromCollection(list, false);

            ws.Cells.AutoFitColumns();
            var result = pack.GetAsByteArray();

            string base64String = Convert.ToBase64String(result);

            string fileName = $"TicketList.xlsx";

            //return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);

            var fileResult = new FileContentResult(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = fileName
            };

            var senddata = new ExcelAndGraphicData
            {
                ExcelData = fileResult,
                GraphicData = values
            };

            return senddata;

        }
        [HttpPatch("CheckRuleEngine")]
        public async Task<ActionResult<TicketRuleEngineListDto>> CheckRuleEngine([FromQuery] ChechRuleDto? chechRule, int createEnvironment)
        {
            var rules = await _ticketRuleEngineService.GetAllAsync();
            //rules.Data = rules.Data.OrderByDescending(e => e.Order).ToList();
            rules.Data = rules.Data.OrderBy(e => e.Order).Where(e => e.createEnvironment == createEnvironment).ToList();


            var defaultSbj = TicketSubject.General;

            if (chechRule.insertDto != null)
            {
                defaultSbj = chechRule.insertDto.TicketSubject;

                foreach (var rule in rules.Data)
                {
                    bool isValid = RuleEvaluator.IsDtoValidByRules(chechRule.insertDto, rule.RuleJson);

                    if (isValid)
                    {
                        return rule;
                    }

                }
            }
            else if (chechRule.updateDto != null)
            {
                defaultSbj = chechRule.updateDto.TicketSubject;

                foreach (var rule in rules.Data)
                {
                    bool isValid = RuleEvaluator.IsDtoValidByRules(chechRule.updateDto, rule.RuleJson);

                    if (isValid)
                    {
                        return rule;
                    }

                }
            }

            var abapDpt = new Guid("911e0f7f-e133-4fa1-a6e1-0c79c674bb61");
            var fioriDpt = new Guid("1ff822c6-0fbc-4d12-aa20-e0a0d9d8aec3");
            var itHelpDpt = new Guid("66DF64D4-9251-45B7-85F1-B2E8E6448028");
            var elseDpt = new Guid("E0BB054C-8885-49B1-9693-67D2DE9BA3FD");


            var defaultRule = new TicketRuleEngineListDto
            {
                Id = "99999999999"
            };


            if (defaultSbj == TicketSubject.ABAP)
            {
                defaultRule.AssignedDepartmentId = abapDpt;
            }
            else if (defaultSbj == TicketSubject.FioriBtp)
            {
                defaultRule.AssignedDepartmentId = fioriDpt;
            }
            else if (defaultSbj == TicketSubject.ITHelpDesk)
            {
                defaultRule.AssignedDepartmentId = itHelpDpt;
            }
            else
            {
                defaultRule.AssignedDepartmentId = elseDpt;
            }

            return defaultRule;
        }


        [AllowAnonymous]
        [HttpGet("ReadMails")]
        public async Task<ActionResult<bool>> ReadMails()
        {
            #region Mail Okuma İşlemleri

            var tenantId = _configuration["AzureAd:TenantId"];
            var clientId = _configuration["AzureAd:ClientId"];
            var clientSecret = "";
            var userEmail = "support@vesacons.com";

    

            string connectionString = _configuration.GetConnectionString("SqlServerConnection");
            var builder = new SqlConnectionStringBuilder(connectionString);
            string databaseName = builder.InitialCatalog;

            if (databaseName == "vesa_erp_test")
            {
                clientSecret = "";
            }
            else if (databaseName == "vesa_erp")
            {
                clientSecret = "";
            }

            // Kimlik bilgisi oluştur
            var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);

            // Graph istemcisi oluştur
            var graphClient = new GraphServiceClient(credential);

            // Gelen kutusundan son 5 e-postayı al
            var messages = await graphClient.Users[userEmail]
                .MailFolders["Inbox"]
                .Messages
                .GetAsync(config =>
                {

                    config.QueryParameters.Filter = "isRead eq false";
                    config.QueryParameters.Orderby = new[] { "receivedDateTime desc" };
                });

            // E-postaları yazdır
            foreach (var message in messages.Value)
            {
                var updateMessage = new Message
                {
                    IsRead = true
                };

                var fullMessage = await graphClient.Users[userEmail]
              .Messages[message.Id]
              .GetAsync(config =>
              {
                  config.QueryParameters.Select = new[]
                  {
                        "subject", "from", "toRecipients", "ccRecipients",
                        "body", "bodyPreview", "internetMessageId",
                        "conversationId", "ReplyTo", "hasAttachments", "attachments","internetMessageHeaders"
                  };
              });

                string formattedMailBody = "";
                string[] parsed = Array.Empty<string>();
                Microsoft.Graph.Models.AttachmentCollectionResponse? attachmentsSign = null;

                // Eğer mail HTML ise ve cid içeren görseller varsa
                if (fullMessage?.Body?.ContentType == Microsoft.Graph.Models.BodyType.Html)
                {
                    parsed = fullMessage?.Body?.Content?.Split("From:");

                    attachmentsSign = await graphClient.Users[userEmail]
                        .Messages[message.Id]
                        .Attachments
                        .GetAsync();

                    foreach (var attachment in attachmentsSign.Value)
                    {
                        if (attachment is FileAttachment fileAttachment && fileAttachment.IsInline == true)
                        {
                            var contentId = fileAttachment.ContentId;
                            var base64Image = Convert.ToBase64String(fileAttachment.ContentBytes);

                            for (int i = 0; i < parsed.Length; i++)
                            {
                                parsed[i] = parsed[i].Replace(
                                    $"cid:{contentId}",
                                    $"data:{fileAttachment.ContentType};base64,{base64Image}"
                                );
                            }
                        }
                    }

                    formattedMailBody = string.Join("", parsed);
                }
                else if (fullMessage.Body.ContentType == Microsoft.Graph.Models.BodyType.Text)
                {
                    // Sadece düz metin varsa, HTML'e uygun formatla
                    formattedMailBody = "<pre>" + System.Net.WebUtility.HtmlEncode(fullMessage.Body.Content) + "</pre>";
                }


                //var parsed = fullMessage.Body.Content.Split("From:");
                //var formattedMailBody = "";

                //var attachmentsSign = await graphClient.Users[userEmail]
                //    .Messages[message.Id]
                //    .Attachments
                //    .GetAsync();

                //foreach (var attachment in attachmentsSign.Value)
                //{
                //    if (attachment is FileAttachment fileAttachment && fileAttachment.IsInline == true)
                //    {
                //        var contentId = fileAttachment.ContentId; // "image004.png@01DBAD29.8E5F8290"
                //        var base64Image = Convert.ToBase64String(fileAttachment.ContentBytes);

                //        for (int i = 0; i < parsed.Count(); i++)
                //        {
                //            parsed[i] = parsed[i].Replace(
                //                $"cid:{contentId}",
                //                $"data:{fileAttachment.ContentType};base64,{base64Image}"
                //            );
                //        }
                //        formattedMailBody = string.Join("", parsed);
                //    }
                //}

                // ✅ Gönderen kim? Sistemde kayıtlı değil ise mail gönder ve döngüye devam
                var from = fullMessage?.From?.EmailAddress?.Address;

                // ✅ Konu
                var subject = fullMessage?.Subject;

                var user = await _userManager.FindByEmailAsync(from);

                if (user == null)
                {
                    SendMailTicketInfo(subject, from);

                    await graphClient.Users[userEmail]
                       .Messages[message.Id]
                       .PatchAsync(updateMessage);

                    continue;
                }

                // ✅ Alıcılar
                var toRecipients = fullMessage?.ToRecipients?.Select(r => r.EmailAddress.Address).ToList();
                var ccRecipients = fullMessage?.CcRecipients?.Select(r => r.EmailAddress.Address).ToList();

                // ✅ Gövde (HTML veya Text olabilir)
                //var bodyContent = fullMessage?.Body?.Content;
                //var bodyContent = parsed[0];
                var bodyContent = parsed;

                //bodyContent = Regex.Replace(bodyContent, @"<table[\s\S]*?</table>", "", RegexOptions.IgnoreCase);
                //bodyContent = Regex.Replace(bodyContent, @"<img[^>]*>", "", RegexOptions.IgnoreCase);
                //bodyContent = Regex.Replace(bodyContent, "<[^>]+>", "");

                var bodyType = fullMessage?.Body?.ContentType;

                // ✅ Benzersiz Kimlikler
                var internetMessageId = fullMessage?.InternetMessageId;
                var conversationId = fullMessage?.ConversationId;
                var inReplyTo = fullMessage?.ReplyTo;

                var claims = new List<Claim>
                 {
                     new Claim(ClaimTypes.Name, user.UserName),
                     new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                 };

                var identity = new ClaimsIdentity(claims, "EmailBasedAuth");
                var principal = new ClaimsPrincipal(identity);

                _httpContextAccessor.HttpContext.User = principal;
                #endregion

                string targetFolderPath = _configuration["TicketSettings:TargetFolderPath"];


                if (!Directory.Exists(targetFolderPath))
                {
                    Directory.CreateDirectory(targetFolderPath);
                }

                // Bir comment nesnesi oluştur
                var ticketComment = new TicketCommentInsertDto();

                // Bu mail reply bir mail mi?
                bool isReply = fullMessage.InternetMessageHeaders?.Any(header => header.Name.Equals("In-Reply-To", StringComparison.OrdinalIgnoreCase)) == true;

                // Talep numarası ile bir eşleşme var mı? Varsa direkt yorum ekle.
                var match = Regex.Match(subject, @"Talep\s+Numarası\s*:\s*(\d+)");

                if (match.Success)
                {
                    string talepNo = match.Groups[1].Value;
                    var ticketObj = _ticketService.Where(e => e.UniqNumber == Convert.ToInt32(talepNo)).AsNoTracking().FirstOrDefault();

                    if (ticketObj == null)
                    {
                        await graphClient.Users[userEmail].Messages[message.Id].DeleteAsync();
                        continue;
                        //return NotFound("Talep numarasına ait talep bulunamadı");
                    }

                    // TİCKET IN DOSYA YOLUNU GETİR
                    var ticketFolderPath = ticketObj.FilePath;

                    if (ticketFolderPath != "" && ticketFolderPath != null)
                    {
                        // ONUN ALTINA DOSYA ADI GUİD OLAN BİR COMMENT DOSYASI OLUŞTUR
                        var commentFolderName = Guid.NewGuid().ToString();
                        var commentFolderPath = Path.Combine(ticketFolderPath, commentFolderName);
                        Directory.CreateDirectory(commentFolderPath);

                        // ONUN ALTINA MAİLBODY Yİ KAYDET VE "ATTACHMENTS" DİYE BİR DOSYA OLUŞTUR
                        var mailBodyFilePath = Path.Combine(commentFolderPath, "Mailbody.html");
                        System.IO.File.WriteAllText(mailBodyFilePath, parsed[0]);

                        var attachmentsFolderPath = Path.Combine(commentFolderPath, "Attachments");
                        Directory.CreateDirectory(attachmentsFolderPath);

                        // EKLERİ KAYDET
                        List<TicketFileInsertDto> ticketFiles = new();

                        if (fullMessage?.HasAttachments == true && attachmentsSign.Value != null)
                        {
                            foreach (var attachment in attachmentsSign.Value)
                            {
                                if (attachment is FileAttachment file && file.ContentBytes != null && attachment.IsInline == false)
                                {
                                    var safeFileName = Path.GetFileName(file.Name);
                                    var attachmentFilePath = Path.Combine(attachmentsFolderPath, safeFileName);
                                    System.IO.File.WriteAllBytes(attachmentFilePath, file.ContentBytes);

                                    ticketFiles.Add(new TicketFileInsertDto
                                    {
                                        FileName = file.Name,
                                        FileType = Path.GetExtension(file.Name), // .pdf, .jpg vs.
                                        Base64 = "",
                                        FilePath = attachmentFilePath,

                                    });
                                }
                            }
                        }

                        // YORUMU OLUŞTUR
                        ticketComment = new TicketCommentInsertDto
                        {
                            Body = "",
                            FilePath = commentFolderPath,
                            Files = ticketFiles,
                            isNew = true
                        };

                        await AddComment(ticketComment, ticketObj.Id);
                    }
                    else
                    {
                        // Ekleri dönüştür
                        List<TicketFileInsertDto> ticketFilesOld = new();

                        if (fullMessage?.HasAttachments == true && attachmentsSign.Value != null)
                        {
                            foreach (var attachment in attachmentsSign.Value)
                            {
                                if (attachment is FileAttachment file && file.ContentBytes != null && attachment.IsInline == false)
                                {
                                    ticketFilesOld.Add(new TicketFileInsertDto
                                    {
                                        FileName = file.Name,
                                        FileType = Path.GetExtension(file.Name), // .pdf, .jpg vs.
                                        Base64 = Convert.ToBase64String(file.ContentBytes)
                                    });
                                }
                            }
                        }

                        var ticketCommentOld = new TicketCommentInsertDto
                        {
                            Body = parsed[0],
                            Files = ticketFilesOld,
                            isNew = true
                        };

                        await AddComment(ticketCommentOld, ticketObj.Id);

                    }



                }
                else
                {
                    if (isReply)
                    {
                        var ticketObj = _ticketService
                            .Where(e => e.MailConversationId == conversationId)
                            .AsNoTracking()
                            .FirstOrDefault();

                        // Reply edilen mail zinciri daha önceden ticket olarak eklendiyse
                        if (ticketObj != null)
                        {
                            // TİCKET IN DOSYA YOLUNU GETİR
                            var ticketFolderPath = ticketObj.FilePath;


                            if (ticketFolderPath != "" && ticketFolderPath != null)
                            {
                                // ONUN ALTINA DOSYA ADI GUİD OLAN BİR COMMENT DOSYASI OLUŞTUR
                                var commentFolderName = Guid.NewGuid().ToString();
                                var commentFolderPath = Path.Combine(ticketFolderPath, commentFolderName);
                                Directory.CreateDirectory(commentFolderPath);

                                // ONUN ALTINA MAİLBODY Yİ KAYDET VE "ATTACHMENTS" DİYE BİR DOSYA OLUŞTUR
                                var mailBodyFilePath = Path.Combine(commentFolderPath, "Mailbody.html");
                                System.IO.File.WriteAllText(mailBodyFilePath, parsed[0]);

                                var attachmentsFolderPath = Path.Combine(commentFolderPath, "Attachments");
                                Directory.CreateDirectory(attachmentsFolderPath);

                                // EKLERİ KAYDET
                                List<TicketFileInsertDto> ticketFiles = new();

                                if (fullMessage?.HasAttachments == true && attachmentsSign.Value != null)
                                {
                                    foreach (var attachment in attachmentsSign.Value)
                                    {
                                        if (attachment is FileAttachment file && file.ContentBytes != null && attachment.IsInline == false)
                                        {
                                            var safeFileName = Path.GetFileName(file.Name);
                                            var attachmentFilePath = Path.Combine(attachmentsFolderPath, safeFileName);
                                            System.IO.File.WriteAllBytes(attachmentFilePath, file.ContentBytes);

                                            ticketFiles.Add(new TicketFileInsertDto
                                            {
                                                FileName = file.Name,
                                                FileType = Path.GetExtension(file.Name), // .pdf, .jpg vs.
                                                Base64 = "",
                                                FilePath = attachmentFilePath,

                                            });
                                        }
                                    }
                                }

                                // YORUMU OLUŞTUR
                                ticketComment = new TicketCommentInsertDto
                                {
                                    Body = "",
                                    FilePath = commentFolderPath,
                                    Files = ticketFiles,
                                    isNew = true
                                };

                                await AddComment(ticketComment, ticketObj.Id);
                            }
                            else
                            {
                                // Ekleri dönüştür
                                List<TicketFileInsertDto> ticketFilesOld = new();

                                if (fullMessage?.HasAttachments == true && attachmentsSign.Value != null)
                                {
                                    foreach (var attachment in attachmentsSign.Value)
                                    {
                                        if (attachment is FileAttachment file && file.ContentBytes != null && attachment.IsInline == false)
                                        {
                                            ticketFilesOld.Add(new TicketFileInsertDto
                                            {
                                                FileName = file.Name,
                                                FileType = Path.GetExtension(file.Name), // .pdf, .jpg vs.
                                                Base64 = Convert.ToBase64String(file.ContentBytes)
                                            });
                                        }
                                    }
                                }

                                var ticketCommentOld = new TicketCommentInsertDto
                                {
                                    Body = parsed[0],
                                    Files = ticketFilesOld,
                                    isNew = true
                                };

                                await AddComment(ticketCommentOld, ticketObj.Id);

                            }

                        }

                        // Reply edilen mail zinciri ilk defa ticket olarak ekleniyorsa
                        else
                        {
                            // TALEP İÇİN KLASÖR OLUŞTUR
                            var ticketFolderName = Guid.NewGuid().ToString();
                            var ticketFolderPath = Path.Combine(targetFolderPath, ticketFolderName);
                            Directory.CreateDirectory(ticketFolderPath);


                            // ONUN ALTINA YENİ BİR YORUM DOSYASI OLUŞTUR
                            var commentFolderName = Guid.NewGuid().ToString();
                            var commentFolderPath = Path.Combine(ticketFolderPath, commentFolderName);
                            Directory.CreateDirectory(commentFolderPath);


                            // ONUN ALTINA MAİLBODY Yİ KAYDET VE EKLER VARSA EKLER DİYE BİR DOSYA OLUŞTUR
                            var mailBodyFilePath = Path.Combine(commentFolderPath, "Mailbody.html");
                            System.IO.File.WriteAllText(mailBodyFilePath, formattedMailBody);

                            var attachmentsFolderPath = Path.Combine(commentFolderPath, "Attachments");
                            Directory.CreateDirectory(attachmentsFolderPath);

                            // EKLERİ KAYDET
                            List<TicketFileInsertDto> ticketFiles = new();

                            if (fullMessage?.HasAttachments == true && attachmentsSign.Value != null)
                            {
                                foreach (var attachment in attachmentsSign.Value)
                                {
                                    if (attachment is FileAttachment file && file.ContentBytes != null && attachment.IsInline == false)
                                    {
                                        var safeFileName = Path.GetFileName(file.Name);
                                        var attachmentFilePath = Path.Combine(attachmentsFolderPath, safeFileName);
                                        System.IO.File.WriteAllBytes(attachmentFilePath, file.ContentBytes);

                                        ticketFiles.Add(new TicketFileInsertDto
                                        {
                                            FileName = file.Name,
                                            FileType = Path.GetExtension(file.Name), // .pdf, .jpg vs.
                                            Base64 = "",
                                            FilePath = attachmentFilePath,

                                        });
                                    }
                                }
                            }

                            // YORUMU OLUŞTUR
                            ticketComment = new TicketCommentInsertDto
                            {
                                Body = "",
                                FilePath = commentFolderPath,
                                Files = ticketFiles,
                                isNew = true
                            };

                            // Ticket'i doldur
                            var dto = new TicketInsertDto
                            {
                                TicketCode = null,
                                Title = subject ?? "(Konu yok)",
                                Description = $"Gönderen: {from}\nAlıcılar: {string.Join(",", toRecipients)}\nCc: {string.Join(",", ccRecipients)}",
                                WorkCompanyId = user.WorkCompanyId.ToString(),
                                WorkCompanySystemInfoId = "",
                                UserAppId = user.Id,
                                Type = TicketType.Feature,
                                TicketSLA = TicketSLA.Standart,
                                TicketSubject = TicketSubject.General,
                                Priority = TicketPriority.Medium,
                                TicketComment = new List<TicketCommentInsertDto> { ticketComment },
                                isSend = true,
                                CustomerRefId = user.WorkCompanyId.ToString(),
                                IsFromEmail = true,
                                MailConversationId = conversationId,
                                AddedMailAddresses = string.Join(";", ccRecipients),
                                FilePath = ticketFolderPath,
                                IsFilePath = true,
                            };
                            await CreaTicket(dto, 2, ccRecipients);
                        }
                    }
                    else
                    {
                        // TALEP İÇİN KLASÖR OLUŞTUR
                        var ticketFolderName = Guid.NewGuid().ToString();
                        var ticketFolderPath = Path.Combine(targetFolderPath, ticketFolderName);
                        Directory.CreateDirectory(ticketFolderPath);


                        // ONUN ALTINA YENİ BİR YORUM DOSYASI OLUŞTUR
                        var commentFolderName = Guid.NewGuid().ToString();
                        var commentFolderPath = Path.Combine(ticketFolderPath, commentFolderName);
                        Directory.CreateDirectory(commentFolderPath);


                        // ONUN ALTINA MAİLBODY Yİ KAYDET VE EKLER VARSA EKLER DİYE BİR DOSYA OLUŞTUR
                        var mailBodyFilePath = Path.Combine(commentFolderPath, "Mailbody.html");
                        System.IO.File.WriteAllText(mailBodyFilePath, formattedMailBody);

                        var attachmentsFolderPath = Path.Combine(commentFolderPath, "Attachments");
                        Directory.CreateDirectory(attachmentsFolderPath);

                        // EKLERİ KAYDET
                        List<TicketFileInsertDto> ticketFiles = new();

                        if (fullMessage?.HasAttachments == true && attachmentsSign.Value != null)
                        {
                            foreach (var attachment in attachmentsSign.Value)
                            {
                                if (attachment is FileAttachment file && file.ContentBytes != null && attachment.IsInline == false)
                                {
                                    var safeFileName = Path.GetFileName(file.Name);
                                    var attachmentFilePath = Path.Combine(attachmentsFolderPath, safeFileName);
                                    System.IO.File.WriteAllBytes(attachmentFilePath, file.ContentBytes);

                                    ticketFiles.Add(new TicketFileInsertDto
                                    {
                                        FileName = file.Name,
                                        FileType = Path.GetExtension(file.Name), // .pdf, .jpg vs.
                                        Base64 = "",
                                        FilePath = attachmentFilePath,

                                    });
                                }
                            }
                        }

                        // YORUMU OLUŞTUR
                        ticketComment = new TicketCommentInsertDto
                        {
                            Body = "",
                            FilePath = commentFolderPath,
                            Files = ticketFiles,
                            isNew = true

                        };

                        // Ticket'i doldur
                        var dto = new TicketInsertDto
                        {
                            TicketCode = null,
                            Title = subject ?? "(Konu yok)",
                            Description = $"Gönderen: {from}\nAlıcılar: {string.Join(",", toRecipients)}\nCc: {string.Join(",", ccRecipients)}",
                            WorkCompanyId = user.WorkCompanyId.ToString(),
                            WorkCompanySystemInfoId = "",
                            UserAppId = user.Id,
                            Type = TicketType.Feature,
                            TicketSLA = TicketSLA.Standart,
                            TicketSubject = TicketSubject.General,
                            Priority = TicketPriority.Medium,
                            TicketComment = new List<TicketCommentInsertDto> { ticketComment },
                            isSend = true,
                            CustomerRefId = user.WorkCompanyId.ToString(),
                            IsFromEmail = true,
                            MailConversationId = conversationId,
                            AddedMailAddresses = string.Join(";", ccRecipients),
                            FilePath = ticketFolderPath,
                            IsFilePath = true,
                        };
                        await CreaTicket(dto, 2, ccRecipients);
                    }
                }

                await graphClient.Users[userEmail].Messages[message.Id].DeleteAsync();

            }
            return false;
        }

        [AllowAnonymous]
        [HttpGet("DeleteAllSentEmails")]
        public async Task DeleteAllSentEmails()
        {
            var tenantId = _configuration["AzureAd:TenantId"];
            var clientId = _configuration["AzureAd:ClientId"];
            var clientSecret = "";
            var userEmail = "support@vesacons.com";

            string connectionString = _configuration.GetConnectionString("SqlServerConnection");
            var builder = new SqlConnectionStringBuilder(connectionString);
            string databaseName = builder.InitialCatalog;

            if (databaseName == "vesa_erp_test")
            {
                clientSecret = "";
            }
            else if (databaseName == "vesa_erp")
            {
                clientSecret = "";
            }
            var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);

            // Graph istemcisi oluştur
            var _graphClient = new GraphServiceClient(credential);
            await DeleteMessagesRecursivelyAsync(_graphClient, userEmail, null);


        }
        private async Task DeleteMessagesRecursivelyAsync(GraphServiceClient graphClient, string userEmail, string? nextPageLink)
        {
            MessageCollectionResponse page;

            if (string.IsNullOrEmpty(nextPageLink))
            {
                // İlk sayfa verisi çekiliyor
                page = await graphClient.Users[userEmail]
                    .MailFolders["SentItems"]
                    .Messages
                    .GetAsync(config =>
                    {
                        config.QueryParameters.Top = 100;
                        config.QueryParameters.Select = new[] { "id" };
                        config.QueryParameters.Orderby = new[] { "sentDateTime desc" };
                    });
            }
            else
            {
                // Sonraki sayfa URL'si ile veri çekiliyor
                page = await new GraphServiceClient(graphClient.RequestAdapter)
                    .Users[userEmail]
                    .MailFolders["SentItems"]
                    .Messages
                    .WithUrl(nextPageLink)
                    .GetAsync();
            }

            if (page?.Value == null || page.Value.Count == 0)
                return;

            foreach (var message in page.Value)
            {
                await graphClient.Users[userEmail]
                    .Messages[message.Id]
                    .DeleteAsync();
            }

            if (!string.IsNullOrEmpty(page.OdataNextLink))
            {
                // Recursive olarak bir sonraki sayfaya geç
                await DeleteMessagesRecursivelyAsync(graphClient, userEmail, page.OdataNextLink);
            }
        }



        private async Task SendQueryMail(ActionResult<TicketRuleEngineListDto> rules, TicketListDto dto)
        {
            List<string> tolist = new List<string>();
            var subject = "";

            if (rules != null)
            {
                if (rules.Value.AssignedTeamId != Guid.Empty || rules.Value.AssignedUserId != Guid.Empty)
                {
                    if (rules.Value.AssignedTeamId != Guid.Empty)
                    {
                        var service = await _tickeTeamService.Include();
                        var result = service.Where(e => rules.Value.AssignedTeamId != null
                                      && e.Id == rules.Value.AssignedTeamId).Include(e => e.TeamList).ThenInclude(e => e.UserApp).FirstOrDefault();

                        subject = "Takımınıza Talep Atandı";

                        foreach (var item in result.TeamList)
                        {
                            if (item.UserApp != null)
                            {
                                tolist.Add(item.UserApp.Email!);
                            }
                        }
                    }
                    else if (rules.Value.AssignedUserId != Guid.Empty)
                    {
                        try
                        {
                            var result = await _userManager.FindByIdAsync(rules.Value.AssignedUserId.ToString());

                            tolist.Add(result.Email);

                            // TAKIM LİDERİNE DE BİLDİRİM MAİLİ GİTMELİ
                            var dept = await _ticketDepartments.Where(e => e.Id == result.TicketDepartmentId);
                            var managerId = dept.Data.Select(e => e.ManagerId).FirstOrDefault();

                            if (!string.IsNullOrEmpty(managerId))
                            {
                                var managerUser = await _userManager.FindByIdAsync(managerId);

                                if (managerUser != null && !tolist.Contains(managerUser.Email))
                                {
                                    tolist.Add(managerUser.Email);
                                }
                            }
                            subject = "Size Talep atandı";
                        }
                        catch (Exception e)
                        {
                            var a = e;
                        }


                    }
                }

                if (tolist.Count > 0)
                {
                    SendTicketMailAsync(dto, tolist, subject);
                }
            }

        }


    }
    public class ExcelAndGraphicData
    {
        public FileContentResult? ExcelData { get; set; }
        public List<TicketListDto>? GraphicData { get; set; }
    }

    public class TicketSourceListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }

    public class TicketPermDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Perm { get; set; }
        public bool CanEditTicket { get; set; }
    }

    public class ExcelListNew
    {
        public string TicketNumber { get; set; }
        public string StatusText { get; set; }
        public string Title { get; set; }
        public string CustomerRefName { get; set; }
        public string TicketAssigneText { get; set; }
        public string UserAppName { get; set; }
        public string CreatedDate { get; set; }
        public string TicketDepartmentText { get; set; }


    }

    public class ChechRuleDto
    {
        public TicketInsertDto? insertDto { get; set; }
        public TicketUpdateDto? updateDto { get; set; }
    }

}
