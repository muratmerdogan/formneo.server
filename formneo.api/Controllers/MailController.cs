using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using formneo.core.Services;
using formneo.api.Helper;

namespace formneo.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
     [Authorize]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mailService;

        public MailController(IMailService mailService)
        {
            _mailService = mailService;
        }

        /// <summary>
        /// Test e-postası gönder (muratmerdogan@gmail.com'a)
        /// </summary>
        [HttpPost("send-test")]
        public async Task<IActionResult> SendTestEmail()
        {
            try
            {
                var result = await _mailService.SendTestEmailAsync();
                
                if (result)
                {
                    return Ok(new { 
                        message = "Test e-postası başarıyla gönderildi!", 
                        to = "muratmerdogan@gmail.com",
                        timestamp = DateTime.Now
                    });
                }
                else
                {
                    return BadRequest(new { 
                        message = "E-posta gönderilemedi. Lütfen logları kontrol edin." 
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    message = "E-posta gönderme sırasında hata oluştu", 
                    error = ex.Message 
                });
            }
        }

        /// <summary>
        /// Özel e-posta gönder
        /// </summary>
        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] SendEmailRequest request)
        {
            if (!ValidationHelper.IsValidOrReturnError(ModelState, out var validationResult))
                return validationResult;

            try
            {
                var result = await _mailService.SendEmailAsync(
                    request.To, 
                    request.Subject, 
                    request.Body, 
                    request.IsHtml
                );
                
                if (result)
                {
                    return Ok(new { 
                        message = "E-posta başarıyla gönderildi!", 
                        to = request.To,
                        subject = request.Subject,
                        timestamp = DateTime.Now
                    });
                }
                else
                {
                    return BadRequest(new { 
                        message = "E-posta gönderilemedi. Lütfen logları kontrol edin." 
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    message = "E-posta gönderme sırasında hata oluştu", 
                    error = ex.Message 
                });
            }
        }

        /// <summary>
        /// Toplu e-posta gönder
        /// </summary>
        [HttpPost("send-bulk")]
        public async Task<IActionResult> SendBulkEmail([FromBody] SendBulkEmailRequest request)
        {
            if (!ValidationHelper.IsValidOrReturnError(ModelState, out var validationResult))
                return validationResult;

            try
            {
                var result = await _mailService.SendEmailAsync(
                    request.To, 
                    request.Subject, 
                    request.Body, 
                    request.IsHtml
                );
                
                if (result)
                {
                    return Ok(new { 
                        message = "Toplu e-posta başarıyla gönderildi!", 
                        to = request.To,
                        count = request.To.Length,
                        subject = request.Subject,
                        timestamp = DateTime.Now
                    });
                }
                else
                {
                    return BadRequest(new { 
                        message = "E-posta gönderilemedi. Lütfen logları kontrol edin." 
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    message = "E-posta gönderme sırasında hata oluştu", 
                    error = ex.Message 
                });
            }
        }
    }

    public class SendEmailRequest
    {
        [Required(ErrorMessage = "Alıcı e-posta adresi zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        public string To { get; set; }

        [Required(ErrorMessage = "E-posta konusu zorunludur")]
        [StringLength(200, ErrorMessage = "Konu en fazla 200 karakter olabilir")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "E-posta içeriği zorunludur")]
        public string Body { get; set; }

        public bool IsHtml { get; set; } = true;
    }

    public class SendBulkEmailRequest
    {
        [Required(ErrorMessage = "Alıcı e-posta adresleri zorunludur")]
        [MinLength(1, ErrorMessage = "En az bir alıcı e-posta adresi gereklidir")]
        public string[] To { get; set; }

        [Required(ErrorMessage = "E-posta konusu zorunludur")]
        [StringLength(200, ErrorMessage = "Konu en fazla 200 karakter olabilir")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "E-posta içeriği zorunludur")]
        public string Body { get; set; }

        public bool IsHtml { get; set; } = true;
    }
}