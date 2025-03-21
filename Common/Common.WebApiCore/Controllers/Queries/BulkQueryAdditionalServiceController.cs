using Common.DTO.Queries;
using Common.Services.Infrastructure.Services.Queries;
using Common.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Common.WebApiCore.Controllers.Queries
{
    [Route("BulkQueryAdditionalServiceController")]
    public class BulkQueryAdditionalServiceController : BaseApiController
    {
        protected readonly IBulkQueryAdditionalService _bulkQueryAdditionalService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        IConfiguration _configuration;
        public BulkQueryAdditionalServiceController(IBulkQueryAdditionalService bulkQueryAdditionalService, IWebHostEnvironment WebHostEnvironment, IConfiguration configuration)
        {
            _bulkQueryAdditionalService = bulkQueryAdditionalService;
            _webHostEnvironment = WebHostEnvironment;
            _configuration = configuration;
        }

        [HttpPost]
        [Authorize]
        [Route(nameof(BulkQueryAdditionalServiceController.BulkQueryServicesAdditional))]
        public async Task<IActionResult> BulkQueryServicesAdditional()
        {
            if (!Request.HasFormContentType)
            {
                return BadRequest();
            }

            var messageError = _configuration.GetSection("MessageError");
            var countFileExcel = _configuration.GetSection("CountFile");
            var countFileExcelBulkQuery = countFileExcel["countFileBulkQueryAddiionalService"];
            FilesHelper filesHelper = new FilesHelper();


            if (!filesHelper.ValidateExcel(Request.Form.Files[0], countFileExcelBulkQuery))
            {
                return BadRequest(messageError["errorCountFileBulkQueryAdditionalQuery"]);
            }

            BulkQueryServicesAdditionalRequestDTO bulkQueryRequestDTO = new BulkQueryServicesAdditionalRequestDTO
            {
                File = Request.Form.Files[0],
                hasProcuraduria = Convert.ToBoolean(Request.Form["hasProcuraduria"]),
                hasRamaJudicial = Convert.ToBoolean(Request.Form["hasRamaJudicial"]),
                hasRamaJudicialJEMPS = Convert.ToBoolean(Request.Form["hasRamaJudicialJEMPS"])
            };

            //Thread hilo = new Thread(() =>
            //{
            //    var threadValidation = true;
            //    while (threadValidation)
            //    {
            //        _bulkQueryAdditionalService.BulkQueryAdditionalAsync(HttpContext.RequestAborted, bulkQueryRequestDTO);
            //        // Enviar status 200 al front-end aquí
            //        Thread.Sleep(5000); // Esperar 5 segundos
            //        threadValidation = false;
            //    }
            //});

            //hilo.Start();

            _bulkQueryAdditionalService.BulkQueryAdditionalAsync(bulkQueryRequestDTO);

            //await Task.Run(async () => _bulkQueryService.ExecuteAsync(HttpContext.RequestAborted));


            //Task.Run(() => _bulkQueryAdditionalService.BulkQueryAdditionalAsync(HttpContext.RequestAborted, bulkQueryRequestDTO));

            return Ok("Procesando Consulta.");

        }

        [HttpGet("getBulkQueryServicesAdditionalTable")]
        [Authorize]
        public async Task<IActionResult> BulkQueryServicesAdditionalTable()
        {

            var result = await _bulkQueryAdditionalService.getBulkQueryServiceAdditionalTable();

            return Ok(result);

        }

        [HttpGet("getReportAditionalService/{QueryId}")]
        [Authorize]
        public async Task<IActionResult> getReportAditionalService(int QueryId)
        {
            var result = await _bulkQueryAdditionalService.getBulkQueryServiceAdditional(QueryId);

            if (result != null)
            {
                string contentRootPath = _webHostEnvironment.ContentRootPath;
                var makeReportHelper = new MakeReportHelper();

                var resp = makeReportHelper.makeReportPDFBulkQueryServiceAdditional(result, Response, contentRootPath);

                byte[] bytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    resp.Save(ms);
                    bytes = ms.ToArray();
                }
                return File(bytes, "application/pdf");
            }

            return NotFound();
        }

        [HttpGet]
        [Route(nameof(BulkQueryAdditionalServiceController.getReportExcelAditionalService))]
        [Authorize]
        public async Task<IActionResult> getReportExcelAditionalService(int QueryId)
        {
            var result = await _bulkQueryAdditionalService.getBulkQueryServiceAdditional(QueryId);
            var FileHelper = new FilesHelper();

            if (result != null)
            {
                var BulkQueryServiceAdditionalDataExcels = result.ListDataExcels.MapTo<List<BulkQueryServiceAdditionalDataExcelDTO>>();
                List<dynamic> data = new List<dynamic>();

                data.Add(BulkQueryServiceAdditionalDataExcels);

                List<string> names = new List<string>()
                {
                    "consulta_masiva"
                };

                return File(FileHelper.TableToExcel(data, names, null),
                                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                    "NotEnviadas" + DateTime.Now + ".xlsx");
            }
            return NotFound();
        }
    }
}
