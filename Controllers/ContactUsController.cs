﻿using System;
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

        public IActionResult SendEmail(ContactUsViewModel contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _emailSender.SendEmail(contact, _messageBuilder.BuildContactMessage(contact));
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