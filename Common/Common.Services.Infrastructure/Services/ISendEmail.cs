﻿using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.Infrastructure.Services
{
    public interface ISendEmail
    {
        bool Send(EmailMessageRequestDto emailMessage);
    }
}
