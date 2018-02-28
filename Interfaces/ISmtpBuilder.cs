using System.Net.Mail;

namespace LucidiaIT.Interfaces
{
    public interface ISmtpBuilder
    {
        SmtpClient GetSmtpClient();
    }
}
