using LucidiaIT.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LucidiaIT.Services
{
    public class SmtpBuilder : ISmtpBuilder
    {
        private readonly IConfiguration _configuration;

        public SmtpBuilder (IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SmtpClient GetSmtpClient() => new SmtpClient(_configuration["EmailSettings:Host"], Int32.Parse(_configuration["EmailSettings:Port"]))
        {
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new System.Net.NetworkCredential(_configuration["EmailSettings:Username"], _configuration["EmailSettings:Password"]),
            DeliveryMethod = SmtpDeliveryMethod.Network
        };
    }
}
