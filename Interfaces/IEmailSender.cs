using LucidiaIT.Models.HomeViewModels;
using SendGrid.Helpers.Mail;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LucidiaIT.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task SendEmail(SendGridMessage message);
        Task SendEmail(ContactUsViewModel contact, SendGridMessage message);
    }
}
