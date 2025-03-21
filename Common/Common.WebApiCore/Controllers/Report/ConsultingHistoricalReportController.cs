using System.Threading.Tasks;
using Common.DTO;
using Common.Services.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Common.WebApiCore.Controllers.Report
{
    [Route("report")]
    public class ConsultingHistoricalReportController : BaseApiController
    {
        private readonly IReportService _reportService;

        public ConsultingHistoricalReportController(IReportService reportService)
        {
            _reportService = reportService;
        }
        
        [HttpGet]
        [Route("consulting-historicals")]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] ReportPaginationFilterDTO paginationFilter)
        {
            var results = await _reportService.GetAll(paginationFilter);
            return Ok(results);
        }
        
        [HttpGet]
        [Route("consulting-historicals/{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetQueryLists(int id)
        {
            var results = _reportService.GetQueryLists(id);
            return Ok(results);
        }
    }
}