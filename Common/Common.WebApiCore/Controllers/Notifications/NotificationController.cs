using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.Services.Infrastructure.Services;
using Common.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Common.WebApiCore.Controllers
{
    [Route("notifications/sent")]
    public class NotificationController : BaseApiController
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] NotificationPaginationFilterDTO paginationFilterDto)
        {
            paginationFilterDto.query = paginationFilterDto.query.IsNullOrEmpty() ? "" : paginationFilterDto.query;
            var notifications = await _notificationService.GetAll(paginationFilterDto);
            return Ok(notifications);

        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _notificationService.GetById(id);
            return Ok(response);
        }
        [HttpGet]
        [Route(nameof(NotificationController.ExportExcel))]
        public async Task<IActionResult> ExportExcel([FromQuery] NotificationPaginationFilterDTO paginationFilterDto)
        {
            var FileHelper = new FilesHelper();
            paginationFilterDto.query = paginationFilterDto.query.IsNullOrEmpty() ? "" : paginationFilterDto.query;
            var notificationSentDTOs = await _notificationService.GetExcelReport(paginationFilterDto);
            if (notificationSentDTOs == null)
                return BadRequest();
            else
            {
                List<dynamic> data = new List<dynamic>();
                data.Add(notificationSentDTOs);
                List<string> names = new List<string>()
                {
                   "Notificaciones Enviadas"
                };

                List<Dictionary<int, string>> list_columns = new List<Dictionary<int, string>>()
                {
                    DictionariesExcel.NoticationSent()
                };
                return File(FileHelper.TableToExcel(data, names, list_columns),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "NotEnviadas" + DateTime.Now + ".xlsx");
            }
        }
    }
}