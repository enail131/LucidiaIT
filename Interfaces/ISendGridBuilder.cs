using SendGrid;

namespace LucidiaIT.Interfaces
{
    public interface ISendGridBuilder
    {
        SendGridClient GetSendGridClient();
    }
}
