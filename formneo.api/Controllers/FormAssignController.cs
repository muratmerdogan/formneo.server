using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLayer.Core.Services;
using formneo.api.Helper;
using formneo.core.DTOs;
using formneo.core.DTOs.FormAssign;
using formneo.core.DTOs.Ticket.Tickets;
using formneo.core.EnumExtensions;
using formneo.core.Models;
using formneo.core.Models.FormEnums;
using formneo.core.Models.Ticket;
using formneo.core.Services;
using formneo.service.Services;

namespace formneo.api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FormAssignController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IServiceWithDto<FormAssign, FormAssignDto> _formAssignService;
        private readonly IFormService _formService;
        private readonly DbNameHelper _dbNameHelper;

        private readonly UserManager<UserApp> _userManager;
        public FormAssignController(DbNameHelper dbNameHelper,UserManager<UserApp> userManager,IMapper mapper, IServiceWithDto<FormAssign, FormAssignDto> formAssignService, IUserService userService, IFormService formService)
        {
            _mapper = mapper;
            _userService = userService;
            _formAssignService = formAssignService;
            _formService = formService;
            _userManager = userManager;
            _dbNameHelper = dbNameHelper;
        }

        [HttpGet]
        public async Task<List<FormAssignDto>> All()
        {
            var forms = await _formAssignService.GetAllAsync();

            return forms.Data.ToList();
        }

        [HttpPost]
        public async Task<IActionResult> Save(FormAssignDto dto)
        {

            var result = await _formAssignService.AddAsync(dto);

            return CreateActionResult(CustomResponseDto<FormAssignDto>.Success(204));

        }

        [HttpPut]
        public async Task<IActionResult> Update(FormAssignDto formDto)
        {
            await _formAssignService.UpdateAsync(formDto);

            return CreateActionResult(CustomResponseDto<FormAssignDto>.Success(204));
        }

        [HttpGet("UserForms")]
        public async Task<List<FormAssignDto>> GetUserForms([FromQuery] string[]? statues = null)
        {
            var userId = await _userService.GetUserByEmailAsync(User.Identity.Name);

            var forms = await _formAssignService.Include();
            forms = forms.Include(e => e.Form);

            var query = forms.Where(e => e.UserAppId == userId.Data.Id);

            if (statues != null && statues.Length > 0)
            {
                var statusList = statues
                    .Select(s => Enum.TryParse<FormStatus>(s, true, out var status) ? (FormStatus?)status : null)
                    .Where(s => s.HasValue)
                    .Select(s => s.Value)
                    .ToList();

                if (statusList.Count > 0)
                {
                    query = query.Where(e => statusList.Contains(e.Status));
                }
            }

            var formlist = query.Select(e => new FormAssignDto
            {
                Id = e.Id,
                FormId = e.FormId,
                FormName = e.Form.FormName,
                UserAppId = e.UserAppId,
                Status = e.Status,
                StatusText = e.Status!.GetDescription(),
                CreatedDate = e.CreatedDate,
                FormRunTimeId = e.FormRunTimeId,

            }).OrderByDescending(e => e.CreatedDate).ToList();

            return formlist;
        }

        [HttpGet("GetById")]
        public async Task<FormAssignDto> GetById(Guid id)
        {
            var form = await _formAssignService.Include();
            form = form.Where(e => e.Id == id);

            var toSendForm = _mapper.Map<FormAssignDto>(form.FirstOrDefault());

            return toSendForm;
        }

        [HttpGet("FormStatus")]
        public IActionResult GetFormStatus()
        {
            var values = Enum.GetValues(typeof(FormStatus))
                             .Cast<FormStatus>()
                             .Select(e => new
                             {
                                 Id = (int)e,
                                 Name = e.ToString(),
                                 Description = e.GetDescription()
                             });

            return Ok(values);
        }


        [HttpGet("CreateFormAssign")]
        public async Task<IActionResult> CreateFormAssign()
        {
            DateTime startOfDay = DateTime.Today;
            DateTime endOfDay = startOfDay.AddDays(1);

            var forms = await _formAssignService.Include();

            var formList = forms.Include(e => e.Form).Where(e => e.CreatedDate >= startOfDay && e.CreatedDate < endOfDay).ToList();

            if (formList.Count == 0)
            {
                var newDto = new FormAssignDto
                {
                    FormId = new Guid("F02BB493-0412-44ED-B027-CF342697A857"),
                    UserAppId = "920463d8-3e29-4a2c-bba8-9f625f11eb2c",
                    Status = FormStatus.waiting
                };
                var result = await _formAssignService.AddAsync(newDto);
            }

            return CreateActionResult(CustomResponseDto<FormAssignDto>.Success(204));
        }

        [HttpPost("AssignFormByMail")]
        public async Task<IActionResult> AssignFormByMail(string username, string formId)
        {
            var user = await _userService.GetUserByEmailAsync(username);

            var form = await _formService.GetByIdStringGuidAsync(new Guid(formId));

            var newDto = new FormAssignDto
            {
                FormId = new Guid(formId),
                UserAppId = user.Data.Id,
                Status = FormStatus.waiting
            };
            var result = await _formAssignService.AddAsync(newDto);

            List<string> tolist = new List<string>();
            tolist.Add(username);

            string mailContent =
                $"<p style=\"font-size:14px; margin-bottom:10px;\"><strong>{form.FormName}</strong> isimli form size yönlendirilmiştir.</p>\r\n <p style=\"font-size:14px; margin-bottom:10px;\">Gerekli aksiyonları zamanında almanızı rica ederiz.</p>\r\n";
            SendMailFormAssign(form.FormName, tolist, mailContent);


            return CreateActionResult(CustomResponseDto<FormAssignDto>.Success(204));

        }

        [AllowAnonymous]
        [HttpPut("UpdateExpiredForms")]
        public async Task<IActionResult> UpdateExpiredForms()
        {
            DateTime today = DateTime.Today;

            var forms = await _formAssignService.Include();

            var expiredForms = forms.Where(e => e.Status == FormStatus.waiting && e.CreatedDate.Date <= today.AddDays(-2))
                .ToList();

            if (expiredForms.Count == 0)
            {
                return Ok("Güncellenecek form bulunamadı.");
            }

            foreach (var form in expiredForms)
            {
                form.Status = FormStatus.expired;
                var toSendForm = _mapper.Map<FormAssignDto>(form);
                await _formAssignService.UpdateAsync(toSendForm);

                var formMain = await _formService.GetByIdStringGuidAsync(form.FormId);
                var user = await _userManager.FindByIdAsync(form.UserAppId);
                List<string> tolist = new List<string>();
                tolist.Add(user.Email);


                string mailContent =
               $"<p style=\"font-size:14px; margin-bottom:10px;\">Size yönlendirilen <strong>{formMain.FormName}</strong> isimli formun süresi geçmiştir.</p>\r\n <p style=\"font-size:14px; margin-bottom:10px;\">Bilgilerinize.</p>\r\n";
                SendMailFormAssign(formMain.FormName, tolist, mailContent);
            }

            return CreateActionResult(CustomResponseDto<FormAssignDto>.Success(204));
        }

        private async Task SendMailFormAssign(string formName, List<string> tolist, string mailContent)
        {
            string dbName = _dbNameHelper.GetDatabaseName();

            string emailBody = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <title>Form Bilgilendirme</title>
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
                            {mailContent}
                            <br>
                            <p style=""color:#0073e6;""><strong>Formlara ulaşmak için: https://support.formneo-tech.com/userFormList</strong></p>
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


            utils.Utils.SendMail("Form Bilgilendirme", emailBody, tolist);

        }


    }
}
