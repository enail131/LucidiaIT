using LucidiaIT.Models.HomeViewModels;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LucidiaIT.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
        void SendEmail(MailMessage message);
        void SendEmail(ContactUsViewModel contact, MailMessage message);
    }
}
