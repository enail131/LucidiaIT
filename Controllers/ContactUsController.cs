using Microsoft.AspNetCore.Mvc;
using LucidiaIT.Models.HomeViewModels;
using LucidiaIT.Interfaces;
using System;
using System.Threading.Tasks;

namespace LucidiaIT.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly IEmailSender _emailSender;
        private IMessageBuilder _messageBuilder;

        public ContactUsController(
            IEmailSender emailSender,
            IMessageBuilder messageBuilder)
        {
            _emailSender = emailSender;
            _messageBuilder = messageBuilder;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SendEmail(ContactUsViewModel contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _emailSender.SendEmail(contact, _messageBuilder.BuildContactMessage(contact));
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
    }
}