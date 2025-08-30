using AutoMapper;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph.Models;
using vesa.api.Helper;
using vesa.core.DTOs;
using vesa.core.DTOs.Kanban;
using vesa.core.DTOs.TicketProjects;
using vesa.core.Models;
using vesa.core.Models.NewFolder;
using vesa.core.Services;
using vesa.service.Services;
using vesa.workflow;

namespace vesa.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class KanbanController : CustomBaseController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly UserManager<UserApp> _userManager;
        private readonly IKanbanService _kanbanService;
        private readonly DbNameHelper _dbNameHelper;

        public KanbanController(DbNameHelper dbNameHelper,IUserService userService, IMapper mapper, UserManager<UserApp> userManager, IKanbanService kanbanService)
        {
            _userService = userService;
            _mapper = mapper;
            _userManager = userManager;
            _kanbanService = kanbanService;
            _dbNameHelper = dbNameHelper;
        }

        [HttpGet]
        public async Task<ActionResult<List<KanbanTasksListDto>>> GetAll()
        {
            try
            {
                var data = await _kanbanService.Where(e => true).ToListAsync(); // Get the initial data

                var dtos = new List<KanbanTasksListDto>();
                foreach (var item in data)
                {
                    //var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == item.AssigneId.ToString()); // Assuming item.AssigneeId exists

                    dtos.Add(new KanbanTasksListDto
                    {
                        //AssigneId = item.AssigneId,
                        //Assignee = new UserAppDtoOnlyNameId
                        //{
                        //    FirstName = user?.FirstName,
                        //    LastName = user?.LastName,
                        //    Id = user?.Id, // gerekli değil frontta kullanılmayacak 
                        //    UserName = user?.UserName
                        //},
                        Description = item.Description,
                        Id = item.Id,
                        Priority = item.Priority,
                        RankId = item.RankId,
                        Status = item.Status,
                        Summary = item.Summary,
                        Tags = item.Tags,
                        Type = item.Type
                    });
                }

                //var data = await _kanbanService.Where(e => true).Include(e => e.Assignee).Select(e => new KanbanTasksListDto
                //{
                //    AssigneId = e.AssigneId,
                //    Assignee = new UserAppDtoOnlyNameId
                //    {
                //        FirstName = e.Assignee.FirstName,
                //        LastName = e.Assignee.LastName,
                //        UserName = e.Assignee.UserName
                //    },
                //    Description = e.Description,
                //    Priority = e.Priority,
                //    RankId = e.RankId,
                //    Id = e.Id,
                //    Status = e.Status,
                //    Summary = e.Summary,



                //}).ToListAsync(); // Get the initial data


                return dtos;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<KanbanTasksListDto>> GetById(Guid id)
        {
            try
            {
                var data = await _kanbanService.Where(e => e.Id == id).Select(e=> new KanbanTasksListDto
                {
                    Id = e.Id,
                    //AssigneId = e.AssigneId,
                    //Assignee = new UserAppDtoOnlyNameId
                    //{
                    //    Id = e.Assignee.Id,
                    //    FirstName = e.Assignee.FirstName,
                    //    LastName = e.Assignee.LastName,                       
                    //    UserName = e.Assignee.UserName
                    //},
                    Description = e.Description,
                    Priority = e.Priority,
                    RankId = e.RankId,
                    Status = e.Status,
                    Summary = e.Summary,
                    Tags = e.Tags,
                    Type = e.Type
                }).FirstOrDefaultAsync();
                          
                return data;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTask(KanbanTasksUpdateDto dto)
        {
            try
            {

                if (string.IsNullOrEmpty(dto.AssigneId.ToString()) || string.IsNullOrEmpty(dto.Id.ToString()))
                {
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(400, "Proje Id ya da kişi id zorunlu"));
                }

                var edittedTask = new Kanban
                {
                    //AssigneId = dto.AssigneId,
                    Description = dto.Description,
                    Id = dto.Id,
                    Priority = dto.Priority,
                    Type = dto.Type,
                    RankId = dto.RankId,
                    Status = dto.Status,
                    Summary = dto.Summary,
                    Tags = dto.Tags
                };

                await _kanbanService.UpdateAsync(edittedTask);


                // Assignee ye update bildirimi gitsin 
                List<string> toMails = new List<string>();
                var user = await _userManager.Users.Where(e => e.Id == dto.AssigneId).FirstOrDefaultAsync();

                var subject = "Kanban Güncelleme Bildirimi";

                if (user != null)
                {
                    toMails.Add(user.Email);
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
                                                        <td style=""border:1px solid #ddd; padding:8px;"">Açıklama</td>
                                                        <td style=""border:1px solid #ddd; padding:8px;"">{dto.Description}</td>
                                                    </tr>                                    
                                                    <tr>
                                                        <td style=""border:1px solid #ddd; padding:8px;"">Kullanıcı</td>
                                                        <td style=""border:1px solid #ddd; padding:8px;"">{user.FirstName} {user.LastName}</td>
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
                    utils.Utils.SendMail($"Vesacons Bilgilendirme E-postası", emailBody, toMails, null);

                }
               


                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, ex.Message));
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTask(Guid Id)
        {
            try
            {
                var data = await _kanbanService.GetByIdStringGuidAsync(Id);

                if (data != null)
                {
                    await _kanbanService.RemoveAsync(data);
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
                }

                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, "hata"));
            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertTask(KanbanTasksInsertDto dto)
        {
            try
            {
                var newTask = new Kanban
                {
                    //AssigneId = dto.AssigneId,
                    Priority = dto.Priority,
                    RankId = dto.RankId,
                    Status = dto.Status,
                    Summary = dto.Summary,
                    Tags = dto.Tags,
                    Type = dto.Type,
                    Description = dto.Description,
                };
                await _kanbanService.AddAsync(newTask);

                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, ex.Message));
            }
        }
    }
}
