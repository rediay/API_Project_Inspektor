/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.DTO;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Mail;
using Common.Services.Infrastructure.Repositories.Management;
using Common.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Common.WebApiCore.Controllers
{
    [Route("EMail")]
    public class EMailController : BaseApiController
    {
        protected readonly IEMailService _mailService;
        protected readonly JwtManage jwtManager;
        protected readonly IAuthService authService;

        public EMailController(IEMailService mailService)
        {
            this._mailService = mailService;
            
            
        }

        [HttpPost]
        [Route(nameof(EMailController.SendMail))]
        public async Task<IActionResult> SendMail(EmailMessageRequestDto emailMessageRequestDto)
        {
            var user = await _mailService.SendMail(emailMessageRequestDto);
            return Ok(user);
        }

     

    }
}