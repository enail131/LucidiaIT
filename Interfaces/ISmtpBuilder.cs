﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LucidiaIT.Interfaces
{
    public interface ISmtpBuilder
    {
        SmtpClient GetSmtpClient();
    }
}