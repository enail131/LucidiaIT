using LucidiaIT.Interfaces;
using LucidiaIT.Models.HomeViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LucidiaIT.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        private readonly ISmtpBuilder _smtpBuilder;
        private SmtpClient _client;

        public EmailSender(ISmtpBuilder smtpBuilder)
        {
            _smtpBuilder = smtpBuilder;
            _client = _smtpBuilder.GetSmtpClient();
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.CompletedTask;
        }

        public void SendEmail(ContactUsViewModel contact, MailMessage message) 
        {
             _client.Send(message);
        }

        public void SendEmail(MailMessage message)
        {
            _client.Send(message);
        }
    }
}
