using LucidiaIT.Interfaces;
using LucidiaIT.Models.HomeViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LucidiaIT.Services
{
    public class MessageBuilder : IMessageBuilder
    {
        private readonly IConfiguration _configuration;
        
        public MessageBuilder(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public MailMessage BuildErrorMessage(Exception e)
        {
            MailMessage message = BuildMailMessage();
            message.Subject = "New error reported";
            message.Body = $"Error message: {e.Message} \n\n Error stack: {e.StackTrace}";
            return message;
        }

        public MailMessage BuildContactMessage(ContactUsViewModel contact)
        {
            MailMessage message = BuildMailMessage();
            message.Subject = "New contact message";
            message.Body = $"Name: {contact.Name} \n\nEmail: {contact.EmailAddress} \n\nMessage: \n{contact.Message}";
            return message;
        }

        private MailMessage BuildMailMessage()
        {
            MailMessage message = new MailMessage();
            message.To.Add(_configuration["EmailSettings:EmailAddress"]);
            message.From = new MailAddress(_configuration["EmailSettings:EmailAddress"], _configuration["EmailSettings:Title"]);
            return message;
        }
    }
}
