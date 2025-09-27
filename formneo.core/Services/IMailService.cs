using System.Threading.Tasks;

namespace vesa.core.Services
{
    public interface IMailService
    {
        Task<bool> SendEmailAsync(string to, string subject, string body, bool isHtml = true);
        Task<bool> SendEmailAsync(string[] to, string subject, string body, bool isHtml = true);
        Task<bool> SendTestEmailAsync();
    }
}
