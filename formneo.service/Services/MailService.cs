using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using formneo.core.Services;

namespace formneo.service.Services
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<MailService> _logger;

        public MailService(IConfiguration configuration, ILogger<MailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<bool> SendEmailAsync(string to, string subject, string body, bool isHtml = true)
        {
            return await SendEmailAsync(new[] { to }, subject, body, isHtml);
        }

        public async Task<bool> SendEmailAsync(string[] to, string subject, string body, bool isHtml = true)
        {
            try
            {
                using var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("noreply@formneo.com", "fwxdhcpanvblxysl")
                };

                using var mailMessage = new MailMessage
                {
                    From = new MailAddress("noreply@formneo.com", "FormNeo System"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = isHtml
                };

                foreach (var email in to)
                {
                    mailMessage.To.Add(email);
                }

                await client.SendMailAsync(mailMessage);
                
                _logger.LogInformation($"Email başarıyla gönderildi: {string.Join(", ", to)}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Email gönderme hatası: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SendTestEmailAsync()
        {
            var subject = "FormNeo Test Email";
            var body = @"
                <html>
                <body>
                    <h2>🚀 FormNeo Test Email</h2>
                    <p>Bu bir test e-postasıdır.</p>
                    <p><strong>Gönderim Zamanı:</strong> " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + @"</p>
                    <p><strong>Sistem:</strong> FormNeo API</p>
                    <br/>
                    <p style='color: #666;'>Bu e-posta otomatik olarak gönderilmiştir.</p>
                </body>
                </html>";

            return await SendEmailAsync("muratmerdogan@gmail.com", subject, body, true);
        }
    }
}
