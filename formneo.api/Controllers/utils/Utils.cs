using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Net.Mime;

namespace vesa.api.Controllers.utils
{
    public static class Utils
    {
        public static string ConvertToSapFormattedDate(DateTime dateTime)
        {
            // DateTime'ı Unix epoch zamanına çevirme
            DateTimeOffset dateTimeOffset = new DateTimeOffset(dateTime);
            long unixTimeMilliseconds = dateTimeOffset.ToUnixTimeMilliseconds();
            string sapFormattedDate = $"/Date({unixTimeMilliseconds})/";

            return sapFormattedDate;
        }


        public static List<string> GetMails()
        {
            List<string> tolist = new List<string>();
            tolist.Add("murat.merdogan@vesacons.com");
            tolist.Add("kardelen.baltaci@vesacons.com");
            tolist.Add("muratmerdogan@gmail.com");
            tolist.Add("busraydemir.26@gmail.com");

            return tolist;
        }
        public static void SendMail(string mailSubject, string mailText, List<string> toList, List<string>? ccList = null,bool? isAttachment=null)
        {
            string senderEmail = "support@vesacons.com";
            string senderPassword = "Sifre2634@!!";

            // E-posta başlığı ve içeriği
            string subject = mailSubject;
            string body = mailText;

            // SMTP (Simple Mail Transfer Protocol) ayarları
            SmtpClient smtpClient = new SmtpClient("smtp.office365.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                EnableSsl = true,
            };
            // E-posta oluştur
            //MailMessage mail = new MailMessage(senderEmail, toList[0], subject, body);
            
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(senderEmail);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            if (isAttachment == true)
            {
                string base64WordFile = VesaLogo.ticketSystemDocument;
                string fileName = "vesatickets.docx";
                byte[] fileBytes = Convert.FromBase64String(base64WordFile);
                MemoryStream ms = new MemoryStream(fileBytes);
                Attachment attachment = new Attachment(ms, fileName, MediaTypeNames.Application.Octet);
                mail.Attachments.Add(attachment);
            }
           
            foreach (var item in toList)
            {
                mail.To.Add(new MailAddress(item));
            }
            //mail.Bcc.Add("murat.merdogan@vesacons.com");
            //mail.Bcc.Add("busra.aydemir@vesacons.com");
            if (ccList != null)
            {
                foreach (var item in ccList)
                {
                    mail.CC.Add(new MailAddress(item));
                }
            }
            try
            {
                // E-postayı gönder
                smtpClient.Send(mail);
                Console.WriteLine("E-posta başarıyla gönderildi.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("E-posta gönderme sırasında bir hata oluştu: " + ex.Message);
            }
        }
    }
}
