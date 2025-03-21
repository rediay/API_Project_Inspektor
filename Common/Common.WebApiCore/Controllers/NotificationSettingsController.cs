

using Common.Services.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Common.DTO;

namespace Common.WebApiCore.Controllers
{
    [Route("NotificationSettings")]
    public class NotificationSettingsController : BaseApiController
    {
        protected readonly INotificationSettingsService notificationSettingsService;
        public NotificationSettingsController(INotificationSettingsService notificationSettingsService)
        {
            this.notificationSettingsService = notificationSettingsService;
        }
        [HttpGet]
        [Authorize]
        [ValidateIdCompany]
        public async Task<IActionResult> Get(int CompanyId)
        {
           // return Ok(CompanyId);
            var notificationSettings = await notificationSettingsService.GetByCompanyId(CompanyId);
            return Ok(notificationSettings);
        }
        [HttpPut]
        [Authorize]
        [ValidateIdCompany]
        public async Task<IActionResult> Edit( NotificationSettingsDTO notificationSettingsDto)
        {

            var result = await notificationSettingsService.Edit(notificationSettingsDto);
            return Ok(result);
        }

    }
}
