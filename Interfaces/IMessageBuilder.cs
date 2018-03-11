using LucidiaIT.Models.HomeViewModels;
using SendGrid.Helpers.Mail;
using System;
using System.Net.Mail;

namespace LucidiaIT.Interfaces
{
    public interface IMessageBuilder
    {
        SendGridMessage BuildErrorMessage(Exception e);
        SendGridMessage BuildContactMessage(ContactUsViewModel contact);
    }
}
