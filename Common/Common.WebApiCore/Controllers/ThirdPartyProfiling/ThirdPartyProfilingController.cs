using System;
using System.IO;
using System.Threading.Tasks;
using Common.DTO;
using Common.DTO.ThirdPartyProfiling;
using Common.Services.Infrastructure.Services.ThirdPartyProfiling;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml;

namespace Common.WebApiCore.Controllers.ThirdPartyProfiling
{
    [Route("third-party-profiling")]
    public class ThirdPartyProfilingController : BaseApiController
    {
        private readonly IParameterizeVariableService _parameterizeVariableService;
        private readonly IThirdPartyProfilingService _thirdPartyProfilingService;

        public ThirdPartyProfilingController(IParameterizeVariableService parameterizeVariableService,
            IThirdPartyProfilingService thirdPartyProfilingService)
        {
            _parameterizeVariableService = parameterizeVariableService;
            _thirdPartyProfilingService = thirdPartyProfilingService;
        }

        [HttpGet]
        [Route("risk-profile-variables")]
        [Authorize]
        public async Task<IActionResult> GetAllRiskProfileVariables([FromQuery] PaginationFilterDTO paginationFilterDto)
        {
            paginationFilterDto.query = paginationFilterDto.query.IsNullOrEmpty() ? "" : paginationFilterDto.query;
            var list = await _parameterizeVariableService.GetAllRiskProfileVariables(paginationFilterDto);
            return Ok(list);
        }

        [HttpGet]
        [Route("risk-profile-variables/{riskProfileId:int}/category-variables/person-type/{personTypeId:int}")]
        [Authorize]
        public async Task<IActionResult> GetAllCategoryVariablesByRiskProfileVariableId(
            [FromQuery] PaginationFilterDTO paginationFilterDto, int riskProfileId, int personTypeId)
        {
            paginationFilterDto.query = paginationFilterDto.query.IsNullOrEmpty() ? "" : paginationFilterDto.query;
            var list = await _parameterizeVariableService.GetAllCategoryVariablesByRiskProfileVariableId(
                paginationFilterDto, riskProfileId, personTypeId);
            return Ok(list);
        }

        [HttpPut]
        [Route("risk-profile-variables/{id:int}")]
        [Authorize]
        public async Task<IActionResult> Edit(int id, RiskProfileVariableDTO dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var result = await _parameterizeVariableService.EditRiskProfileVariable(dto);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut]
        [Route("category-variables/{id:int}")]
        [Authorize]
        public async Task<IActionResult> Edit(int id, CategoryVariableDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            if (dto.Condition.Type == "between" && dto.Condition.FromValue > dto.Condition.ToValue)
            {
                throw new Exception("El valor inicial no puede ser mayor al valor final");
            }

            var result = await _parameterizeVariableService.EditCategoryVariable(dto);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpGet]
        [Route("risk-profiles")]
        [Authorize]
        public async Task<IActionResult> GetAllRiskProfiles([FromQuery] PaginationFilterDTO paginationFilterDto)
        {
            paginationFilterDto.query = paginationFilterDto.query.IsNullOrEmpty() ? "" : paginationFilterDto.query;
            var list = await _parameterizeVariableService.GetAllRiskProfiles(paginationFilterDto);
            return Ok(list);
        }

        [HttpPut]
        [Route("risk-profiles/{id:int}")]
        [Authorize]
        public async Task<IActionResult> Edit(int id, RiskProfileDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            if (dto.StartValue > dto.EndValue)
            {
                throw new Exception("El valor inicial no puede ser mayor al valor final");
            }

            var result = await _parameterizeVariableService.EditRiskProfile(dto);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet]
        [Route("detail")]
        [Authorize]
        public async Task<IActionResult> GetAllThirdPartyProfiling([FromQuery] PaginationFilterDTO paginationFilterDto)
        {
            //return Ok("casas");
            paginationFilterDto.query = paginationFilterDto.query.IsNullOrEmpty() ? "" : paginationFilterDto.query;
            var list = await _thirdPartyProfilingService.GetAll(paginationFilterDto);
            return Ok(list);
        }

        [HttpPost]
        [Route("import")]
        [Authorize]
        public async Task<IActionResult> ImportThirdPartyProfiling()
        {
            var templateFile = Request.Form.Files[0];
            var response = await _thirdPartyProfilingService.Import(templateFile);
            return Ok(response);
        }

        [HttpGet]
        [Route("export/template")]
        [Authorize]
        public async Task<IActionResult> ExportTemplate()
        {
            var cellNames = await _thirdPartyProfilingService.GetExportTemplateColumns();
            var memoryStream = new MemoryStream();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var excelPackage = new ExcelPackage(memoryStream);

            excelPackage.Workbook.Properties.Author = "Risk Consulting";
            excelPackage.Workbook.Properties.Title = "Perfilamiento Tipo Tercero";

            var worksheet = excelPackage.Workbook.Worksheets.Add("Plantilla");
            worksheet.Name = "Template";

            for (var i = 0; i < cellNames.Count; i++)
            {
                var cellName = cellNames[i];
                var cell = worksheet.Cells[1, i + 1];
                cell.Value = cellName;
                cell.AutoFitColumns();
            }

            await excelPackage.SaveAsync();

            const string fileName = "PerfilamientoTipoTerceroPlantilla.xlsx";

            return File(memoryStream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName);
        }
    }
}