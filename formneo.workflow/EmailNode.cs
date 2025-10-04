using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models;

namespace formneo.workflow
{
    public class EmailNode
    {
        public EmailNode(Dictionary<string, object> message, WorkflowItem item)
        {

            var result = ConvertToMailConfiguration(message);

            SendMail(item.NodeDescription);

        }
        static MailConfiguration ConvertToMailConfiguration(Dictionary<string, object> message)
        {
            // Use Newtonsoft.Json to convert the dictionary to JSON and then deserialize to MailConfiguration
            string json = JsonConvert.SerializeObject(message);
            return JsonConvert.DeserializeObject<MailConfiguration>(json);
        }

        private void SendMail(string mailSubject)
        {
            string senderEmail = "murat.merdogan@formneo.com";
            string senderPassword = "Ecm2634@!";

            // E-posta alıcısının adresi
            string toEmail = "murat.merdogan@formneo.com";


            // E-posta başlığı ve içeriği
            string subject = mailSubject;
            string body = mailSubject;

            // SMTP (Simple Mail Transfer Protocol) ayarları
            SmtpClient smtpClient = new SmtpClient("smtp.office365.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                EnableSsl = true,
            };

            // E-posta oluştur
            MailMessage mail = new MailMessage(senderEmail, toEmail, subject, body);

            mail.To.Add(new MailAddress("muratmerdogan@gmail.com"));

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

    public class MailConfiguration
    {
        public string SubjectTemplate { get; set; }
        public string ReceiversTemplate { get; set; }
        public string TextTemplate { get; set; }
        public string ResultPath { get; set; }
    }
}
