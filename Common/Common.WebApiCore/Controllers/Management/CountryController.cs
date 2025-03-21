using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.DTO.Lists;
using Common.DTO.Relations_Countrys;
using Common.Entities.Relations_Countrys;
using Common.Services.Infrastructure.Services.Lists;
using Common.Services.Infrastructure.Services.Relations_Countrys;
using Common.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Common.WebApiCore.Controllers.Management
{
    [Route("management/countries")]
    public class CountryController : BaseApiController
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilterDTO paginationFilterDto)
        {
            paginationFilterDto.query = paginationFilterDto.query.IsNullOrEmpty() ? "" : paginationFilterDto.query;
            var users = await _countryService.GetAll(paginationFilterDto);
            return Ok(users);
        }

        [HttpPost]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> Create(CountryDTO dto)
        {
            var result = await _countryService.Edit(dto);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> Edit(int id, CountryDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var result = await _countryService.Edit(dto);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = _countryService.Delete(id);
            if (result.IsCompleted)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        
        [HttpPost]
        [Route("upload")]
        [Authorize]
        public async Task<IActionResult> Upload(List<CountryDTO> dtos)
        {
            var result = await _countryService.BulkCreate(dtos);
            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        //[HttpPost]
        [HttpGet]
        [Route("template/download")]
        [Authorize]
        //[Authorize(Policy = "AdminOnly")] 
        public async Task<IActionResult> TemplateDownload()
        {
            var filePath = DownloadMasiveUploadTemplate.Download<Countries>();
            
            // try
            // {
            //     var result = new HttpResponseMessage(HttpStatusCode.OK);
            //     //var filePath = $"{MyRootPath}/{name}";
            //     var bytes = File.ReadAllBytes(filePath );
            //
            //     result.Content = new ByteArrayContent(bytes);
            //
            //     var mediaType = "application/octet-stream";
            //     result.Content.Headers.ContentType = new  System.Net.Http.Headers.MediaTypeHeaderValue(mediaType);
            //     return result;
            // }
            // catch (Exception ex)
            // {
            //     throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.ToString()));
            // }
            
            return Ok(filePath);
            /*var result = await _countryService.BulkCreate(dtos);
            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result);*/
        }
    }
}