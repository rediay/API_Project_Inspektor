using Common.DTO.Queries;
using Common.Services.Infrastructure.Queries;
using Common.Services.Infrastructure.Services.Queries;
using Common.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.WebApiCore.Controllers.Queries
{
    [Route("BulkQueryController")]
    public class BulkQueryController : BaseApiController
    {
        protected readonly IBulkQueryService _bulkQueryService;
        protected readonly IBulkQueryAdditionalService _bulkQueryAdditionalService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        IConfiguration _configuration;

        public BulkQueryController(IBulkQueryService bulkQueryService, IBulkQueryAdditionalService bulkQueryAdditionalService, IWebHostEnvironment WebHostEnvironment, IConfiguration configuration)
        {
            this._bulkQueryService = bulkQueryService;
            _bulkQueryAdditionalService = bulkQueryAdditionalService;
            _webHostEnvironment = WebHostEnvironment;
            _configuration = configuration;
        }

        [HttpPost]
        //[Authorize(Policy = "AdminOnly")]
        [Authorize]
        [Route(nameof(BulkQueryController.BulkQuery))]
        public async Task<IActionResult> BulkQuery()
        {            

            if (!Request.HasFormContentType)
            {
                return BadRequest();
            }

            var messageError = _configuration.GetSection("MessageError");
            var countFileExcel = _configuration.GetSection("CountFile");
            var countFileExcelBulkQuery = countFileExcel["countFileBulkQuery"];
            FilesHelper filesHelper = new FilesHelper();

            if (!filesHelper.ValidateExcel(Request.Form.Files[0], countFileExcelBulkQuery))
            {
                return BadRequest(messageError["errorCountFileBulkQuery"]);
            }

            BulkQueryRequestDTO bulkQueryRequestDTO = new BulkQueryRequestDTO
            {
                File = Request.Form.Files[0],
                NWords = Convert.ToInt32(Request.Form["NWords"]),
            };

            var result = await _bulkQueryService.BulkQuery(bulkQueryRequestDTO);
            return Ok(result);


        }

        [HttpGet("getReport/{QueryId}")]
        //[Route(nameof(BulkQueryController.getReport))]
        //[Authorize(Policy = "AdminOnly")]
        [Authorize]
        public async Task<IActionResult> getReport(int QueryId)
        {
            // return Ok(CompanyId);
            var result = await _bulkQueryService.getQuery(QueryId);

            string contentRootPath = _webHostEnvironment.ContentRootPath;
            var makeReportHelper = new MakeReportHelper();

            Object[] resp = makeReportHelper.makeReportBulk(result, Response, contentRootPath);

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

            //    var file = new ViewAsPdf("/Views/Queries/BulkQuery/pdf.cshtml", result)
            //    {
            //        FileName = "consulta_individual.pdf",
            //        PageSize = Rotativa.AspNetCore.Options.Size.A3,
            //        PageMargins = { Left = 4, Bottom = 4, Right = 4, Top = 1 },
            //        CustomSwitches = "--debug-javascript  --enable-javascript --no-stop-slow-scripts --javascript-delay 1000 --footer-center \"Pagina: [page] de [toPage]\" --footer-line --footer-font-size \"10\" --footer-font-name \"Segoe UI\"",

            //    };
            //Response.Headers.ContentDisposition= "inline; filename=consulta_individual.pdf";

            //return file;
            //return File(bytesFile, "application/pdf");
            //}
            // return Ok(result);
            return NotFound();
        }

        [HttpGet]
        [Route(nameof(BulkQueryController.GetBulkQuery))]
        [Authorize]
        public async Task<IActionResult> GetBulkQuery(int QueryId)
        {
            // return Ok(CompanyId);
            var result = await _bulkQueryService.getQuery(QueryId);
            if (result != null)
                return Ok(result);
            return NotFound();
        }


        [HttpGet]
        [Route(nameof(BulkQueryController.getReportExcel))]
        [Authorize]
        public async Task<IActionResult> getReportExcel(int QueryId)
        {
            // return Ok(CompanyId);
            var result = await _bulkQueryService.getQuery(QueryId);
            var FileHelper = new FilesHelper();

            if (result != null)
            {

                var bulkQuery = result.lists.MapTo<List<BulkQueryListExcelDTO>>();
                bulkQuery.ForEach(x => x.Consulta_empresa = result.query.IdQueryCompany.ToString());

                var bulkQueryListCoincidence = result.QueryDetails.MapTo<List<BulkQueryListExcelCoincidenceDTO>>();

                var bulkOwnList = result.ownLists.MapTo<List<BulkQueryOwnListExcelDTO>>();

                List<dynamic> data = new List<dynamic>();

                data.Add(bulkQuery);
                data.Add(bulkOwnList);
                data.Add(bulkQueryListCoincidence);

                List<string> names = new List<string>()
                {
                    "consulta_masiva",
                    "listas propias",
                    "lista coincidencias",
                };

                return File(FileHelper.TableToExcel(data, names, null),
                                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                    "NotEnviadas" + DateTime.Now + ".xlsx");
                //return File(bytesFile, "application/pdf");
            }
            // return Ok(result);
            return NotFound();
        }

    }
}
