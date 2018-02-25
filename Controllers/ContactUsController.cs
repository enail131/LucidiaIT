using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LucidiaIT.Models.HomeViewModels;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using LucidiaIT.Services;
using LucidiaIT.Interfaces;

namespace LucidiaIT.Controllers
{
    public class ContactUsController : Controller
    {
        private static IConfiguration _configuration;
        private readonly ISmtpBuilder _client;

        public ContactUsController(IConfiguration configuration, ISmtpBuilder client)
        {
            _configuration = configuration;
            _client = client;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SendEmail(ContactUsViewModel contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    SmtpClient client = _client.GetSmtp();
                    client.Send(GetMessage(contact));
                    return PartialView("_Success", contact);
                }
                catch
                {
                    return PartialView("_Failure", contact);
                }
            }
            else
            {
                return PartialView("_Failure", contact);
            }
        }

        /// <summary>
        /// Get MailMessage object
        /// </summary>
        /// <param name="contact">
        /// Accepts ContactFormModel to build the message
        /// </param>
        /// <returns></returns>
        private MailMessage GetMessage(ContactUsViewModel contact)
        {
            MailMessage message = new MailMessage();
            message.To.Add(_configuration["EmailSettings:EmailAddress"]);
            message.From = new MailAddress(_configuration["EmailSettings:EmailAddress"], _configuration["EmailSettings:Title"]);
            message.Subject = "New contact message";
            message.Body = $"Name: {contact.Name} \n\nEmail: {contact.EmailAddress} \n\nMessage: \n{contact.Message}";

            return message;
        }
    }
}