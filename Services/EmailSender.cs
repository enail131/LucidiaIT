using LucidiaIT.Interfaces;
using LucidiaIT.Models.HomeViewModels;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LucidiaIT.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        private readonly ISendGridBuilder _sendGridBuilder;
        private SendGridClient _client;

        public EmailSender(ISendGridBuilder sendGridBuilder)
        {
            _sendGridBuilder = sendGridBuilder;
            _client = _sendGridBuilder.GetSendGridClient();
        }

        public Task SendEmailAsync(string email, string subject, string message) => Task.CompletedTask;

        public async Task SendEmail(SendGridMessage message) => await _client.SendEmailAsync(message);

        public async Task SendEmail(ContactUsViewModel contact, SendGridMessage message) => await _client.SendEmailAsync(message);
    }
}
