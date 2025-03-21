using Common.DTO;
using Common.Services.Infrastructure.Services.Relations_Countrys;
using Common.Services.Infrastructure.Services.Extras;
using Common.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Entities.Relations_Countrys;
using Common.Services.Infrastructure.Services.Lists;

namespace Common.WebApiCore.Controllers.Extras
{
    [Route("extras/news")]
    public class NewsController : BaseApiController
    {
        private readonly IContentService _contentService;
        private readonly ICountryService _countryService;
        private readonly IContentCategoryService _contentCategoryService;
        private readonly IContentTypeService _contentTypeService;

        public NewsController(IContentService contentService, ICountryService countryService, IContentCategoryService contentCategoryService, IContentTypeService contentTypeService)
        {
            _contentService = contentService;
            _countryService = countryService;
            _contentCategoryService = contentCategoryService;
            _contentTypeService = contentTypeService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] ContentPaginationFilterDTO paginationFilterDto)
        {

            var news = await _contentService.GetAll(paginationFilterDto);
            return Ok(news);
        }

        [HttpGet]
        [Route("filters")]
        [Authorize]
        public async Task<IActionResult> GetSearchFilters()
        {
            var paginationFilterDto = new PaginationFilterDTO { query = "", ShowAll = true };

            var countriesResponse = await _countryService.GetAll(paginationFilterDto);
            var categoriesResponse = await _contentCategoryService.GetAll(paginationFilterDto);
            //var typesResponse = await _newsTypeService.GetAll(paginationFilterDto);

            var countries = countriesResponse.Data;
            var categories = categoriesResponse.Data;
            //var types = typesResponse.Data;

            var data = new ContentDTO
            {
                Countries = countries,
                ContentCategories = categories,
                //NewsTypes = types
            };

            var response = new ResponseDTO<ContentDTO>(data);

            return Ok(response);
        }

        [HttpGet]
        [Route(nameof(NewsController.ExportExcel))]
        [Authorize]
        public async Task<IActionResult> ExportExcel([FromQuery] ContentPaginationFilterDTO paginationFilterDto)
        {

            var FileHelper = new FilesHelper();
            paginationFilterDto.query = string.IsNullOrEmpty(paginationFilterDto.query) ? "" : paginationFilterDto.query;

            var news = await _contentService.GetAllToExcel(paginationFilterDto);
            if (news == null)
                return BadRequest();
            else
            {
                List<dynamic> data = new List<dynamic>();
                data.Add(news);
                List<string> names = new List<string>()
                {
                    "Noticias",
                };
                return File(FileHelper.TableToExcel(data, names, null),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "NotEnviadas" + DateTime.Now + ".xlsx");
            }
        }
    }
}