using LucidiaIT.Interfaces;
using LucidiaIT.Models.HomeViewModels;
using Microsoft.Extensions.Configuration;
using SendGrid.Helpers.Mail;
using System;
using System.Net.Mail;

namespace LucidiaIT.Services
{
    public class MessageBuilder : IMessageBuilder
    {
        private readonly IConfiguration _configuration;

        public MessageBuilder(IConfiguration configuration) => _configuration = configuration;

        public SendGridMessage BuildErrorMessage(Exception e)
        {
            string subject = "New error reported";
            string body = $"Error message: {e.Message} \n\n Error stack: {e.StackTrace}";
            return BuildEmailMessage(_configuration["EmailSettings:MyEmail"], subject, body);
        }

        public SendGridMessage BuildContactMessage(ContactUsViewModel contact)
        {
            var subject = "New contact message";
            var body = $"Name: {contact.Name} \n\nEmail: {contact.EmailAddress} \n\nMessage: \n{contact.Message}";
            return BuildEmailMessage(_configuration["EmailSettings:LucidiaEmail"], subject, body);
        }
        
        private SendGridMessage BuildEmailMessage(string toEmail, string subject, string body)
        {
            EmailAddress from = new EmailAddress(_configuration["EmailSettings:LucidiaEmail"], "Lucidia IT");
            EmailAddress to = new EmailAddress(toEmail, "Lucidia IT");
            return MailHelper.CreateSingleEmail(from, to, subject, body, null);
        }
    }
}
