using Common.DTO;
using Common.DTO.IndividualQueryExternal;
using Common.Services.Infrastructure;
using Common.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;


namespace Common.WebApiCore.Controllers.Queries
{
    [Route("IndivdualQuery")]
    public class IndivdualQueryController : BaseApiController
    {
        protected readonly IIndividualQueryService individualQueryService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public IndivdualQueryController(IIndividualQueryService individualQueryService, IWebHostEnvironment WebHostEnvironment)
        {
            this.individualQueryService = individualQueryService;
            _webHostEnvironment = WebHostEnvironment;
        }

        [HttpPost]
        [Authorize]
        [ValidateIdCompany]
        //[Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> makeQuery(IndividualQueryParamsDTO individualQueryParamsDTO)
        {

            //if (CompanyId != notificationSettingsDto.CompanyId)
            //    return BadRequest();

            var result = await individualQueryService.makeQuery(individualQueryParamsDTO);
            if (result != null)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("query")]
        [Authorize(Roles = "external")]
        public async Task<IActionResult> query(IndividualQueryExternalParamsEsDTO individualQueryExternalParamsEsDTO)
        {
            var individualQueryExternalParamsDTO = individualQueryExternalParamsEsDTO.MapTo<IndividualQueryExternalParamsDTO>();

            //if (CompanyId != notificationSettingsDto.CompanyId)
            //    return BadRequest();

            var result = await individualQueryService.makeQueryExternal(individualQueryExternalParamsDTO);
            if (result != null)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("previusQuery")]
        [Authorize]
        [ValidateIdCompany]
        //[Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> previusQuery(IndividualQueryParamsDTO individualQueryParamsDTO)
        {
            //if (CompanyId != notificationSettingsDto.CompanyId)
            //    return BadRequest();

            var result = await individualQueryService.previusQuery(individualQueryParamsDTO);
            if (result != null)
                return Ok(result);
            return NotFound();
        }

        [HttpGet]
        [Route("{QueryId:int}")]
        [Authorize]
        //[Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Get(int QueryId)
        {
            // return Ok(CompanyId);
            var result = await individualQueryService.getQuery(QueryId);
            if (result != null)
                return Ok(result);
            return NotFound();
        }

        [HttpGet("getReport/{QueryId}")]
        //[Route("{QueryId:int}")]
        [Authorize]
        //[Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> getReport(int QueryId)
        {
            // return Ok(CompanyId);
            var result = await individualQueryService.getQuery(QueryId);

            string contentRootPath = _webHostEnvironment.ContentRootPath;
            var makeReportHelper = new MakeReportHelper();

            object[] resp = makeReportHelper.makeReport(result, Response, contentRootPath);

            if (resp != null)
            {
                byte[] bytes = resp[0] as byte[];
                string mimeType = resp[1] as string;
                string namepdf = resp[2] as string;
                string extension = resp[3] as string;
                return File(bytes, mimeType, namepdf + "." + extension);
            }

            //if (result != null)
            //{
            //    var file = new ViewAsPdf("/Views/Queries/IndividualQuery/pdf.cshtml", result)
            //    {
            //        FileName = "consulta_individual.pdf",
            //        PageMargins = { Left = 4, Bottom = 4, Right = 4, Top = 1 },
            //        CustomSwitches = $"--debug-javascript --enable-javascript --no-stop-slow-scripts --javascript-delay 1000 --footer-center \"Pagina: [page] de [toPage]\" --footer-line --footer-font-size \"10\" --footer-font-name \"Segoe UI\" --header-html \"/Views/Queries/HederFooter/HeaderPDF.cshtml\" ",
            //    };

            //    return file;
            //    //return File(bytesFile, "application/pdf");
            //}
            // return Ok(result);
            return NotFound();
        }

    }
}
