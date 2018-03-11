using LucidiaIT.Interfaces;
using Microsoft.Extensions.Configuration;
using SendGrid;

namespace LucidiaIT.Services
{
    public class SendGridBuilder : ISendGridBuilder
    {
        public readonly IConfiguration _config;

        public SendGridBuilder(IConfiguration config)
        {
            _config = config;
        }

        public SendGridClient GetSendGridClient() => new SendGridClient(_config["EmailSettings:LucidiaSendGridKey"]);
    }
}
