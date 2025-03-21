using System.Threading.Tasks;
using Common.DTO;
using Common.Services.Infrastructure.Services.Extras;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Common.WebApiCore.Controllers.Extras
{
    [Route("extras/event-roi")]
    public class EventRoiController : BaseApiController
    {
        private readonly IEventRoiService _eventRoiService;

        public EventRoiController(IEventRoiService eventRoiService)
        {
            _eventRoiService = eventRoiService;
        }
        [HttpGet]
        [Route("operation/types")]
        [Authorize]
        public async Task<IActionResult> GetOperationTypes()
        {
            var result = await _eventRoiService.GetAllOperationTypes();
            return Ok(result);
        }
        [HttpGet]
        [Route("operation/statuses")]
        [Authorize]
        public async Task<IActionResult> GetOperationStatuses()
        {
            var result = await _eventRoiService.GetAllOperationStatuses();
            return Ok(result);
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(EventRoiDTO roiDto)
        {
            var result = await _eventRoiService.Create(roiDto);
            return Ok(result);
        }
    }
}