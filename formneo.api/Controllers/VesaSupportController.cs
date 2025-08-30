using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System.Net;
using System.Net.Mail;
using vesa.ticket;

namespace vesa.api.Controllers
{

    [Route("api/[controller]")]

    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class VesaSupportController : CustomBaseController
    {
 
        [HttpGet]
        public async Task<IActionResult> All()
        {

            TicketCore core = new TicketCore();
            var list= core.GetTaskList();
            string reportname = $"User_Wise_{Guid.NewGuid():N}.xlsx";

            using ExcelPackage pack = new ExcelPackage();
            ExcelWorksheet ws = pack.Workbook.Worksheets.Add(reportname);
           

            ws.Cells["E1"].Style.Numberformat.Format = "mm-dd-yy";//or m/d/yy h:mm
            ws.Cells["A1"].LoadFromCollection(list, true, TableStyles.Light1);

            ws.Cells.AutoFitColumns();
            var result= pack.GetAsByteArray();


            string senderEmail = "murat.merdogan@vesacons.com";
            string senderPassword = "Ecm2634@!";

            // E-posta alıcısının adresi
            string toEmail = "muhammed.kadan@vesacons.com";

            // E-posta başlığı ve içeriği
            string subject = "Fiori 9. Hafta -- Noreply";
            string body = "Sistem tarafından otomatik gönderilmiştir.";

            // SMTP (Simple Mail Transfer Protocol) ayarları
            SmtpClient smtpClient = new SmtpClient("smtp.office365.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                EnableSsl = true,
            };


            // E-posta oluştur
            //MailMessage mail = new MailMessage(senderEmail, toEmail, subject, body);

            //try
            //{
            //    // E-postayı gönder
            //    //mail.To.Add(new MailAddress("veysel.yilmaz@vesacons.com"));
            //    //mail.To.Add(new MailAddress("hasibe.avcioglu@vesacons.com"));
            //    //mail.To.Add(new MailAddress("murat.merdogan@vesacons.com"));

            //    using (MemoryStream ms = new MemoryStream(result))
            //    {
            //        Attachment attachment = new Attachment(ms, "Fiori-9.Hafta.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            //        mail.Attachments.Add(attachment);

            //        // E-postayı gönder
            //        smtpClient.Send(mail);
            //        Console.WriteLine("E-posta başarıyla gönderildi.");
            //    }

            //    Console.WriteLine("E-posta başarıyla gönderildi.");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("E-posta gönderme sırasında bir hata oluştu: " + ex.Message);
            //}


            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", reportname);

        }



        [HttpGet("[action]")]
        public async Task<List<ExcelList>> GetAll()
        {

            TicketCore core = new TicketCore();
            var list = core.GetTaskListAll();

            return list;

        }
        [HttpGet("[action]")]
        public async Task<List<AllList>> GetSelectList()
        {

            TicketCore core = new TicketCore();
            var list = core.GetSelectList();

            return list;

        }

    }
}
