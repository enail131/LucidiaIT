using LucidiaIT.Models.HomeViewModels;
using System;
using System.Net.Mail;

namespace LucidiaIT.Interfaces
{
    public interface IMessageBuilder
    {
        MailMessage BuildErrorMessage(Exception e);
        MailMessage BuildContactMessage(ContactUsViewModel contact);
    }
}
