using Common.DTO;
using Common.Services.Infrastructure.Services;
using Common.Services.Infrastructure.Services.Notifications;
using Common.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.WebApiCore.Controllers.Notifications
{
    [Route("notifications/monitoring")]
    public class NotificationMonitoringController : BaseApiController
    {
        private readonly INotificationMonitoringService _notificationMonitoringService;

        public NotificationMonitoringController(INotificationMonitoringService notificationMonitoringService)
        {
            _notificationMonitoringService = notificationMonitoringService;
        }

        [HttpGet]
        [Authorize]
        [Route(nameof(NotificationMonitoringController.GetAll))]
        public async Task<IActionResult> GetAll([FromQuery] NotificationPaginationFilterDTO paginationFilterDto)
        {
            paginationFilterDto.query = paginationFilterDto.query.IsNullOrEmpty() ? "" : paginationFilterDto.query;
            var notifications = await _notificationMonitoringService.GetAll(paginationFilterDto);
            return Ok(notifications);
        }
        [Authorize]
        [HttpGet]
        [Route(nameof(NotificationMonitoringController.ExportExcel))]
        public async Task<IActionResult> ExportExcel([FromQuery] NotificationPaginationFilterDTO paginationFilterDto)
        {
            var FileHelper = new FilesHelper();
            paginationFilterDto.query = paginationFilterDto.query.IsNullOrEmpty() ? "" : paginationFilterDto.query;
            var notificationsMonitoringDTOs = await _notificationMonitoringService.GetExcelReport(paginationFilterDto);
            if (notificationsMonitoringDTOs == null)
                return BadRequest();
            else
            {
                List<dynamic> data = new List<dynamic>();
                data.Add(notificationsMonitoringDTOs);
                List<string> names = new List<string>()
                {
                   "Notificaciones Monitoreo"
                };

                List<Dictionary<int, string>> list_columns = new List<Dictionary<int, string>>()
                {
                    DictionariesExcel.Monitoring()
                };

                return File(FileHelper.TableToExcel(data, names, list_columns),
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "NotMonitoreo" + DateTime.Now + ".xlsx");
            }
        }

    }
}