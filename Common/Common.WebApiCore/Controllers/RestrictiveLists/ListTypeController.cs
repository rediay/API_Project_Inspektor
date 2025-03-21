using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.DTO.Lists;
using Common.DTO.RestrictiveLists;
using Common.Services.Infrastructure.Services;
using Common.Services.Infrastructure.Services.Lists;
using Common.Services.Infrastructure.Services.Relations_Countrys;
using Common.Services.Infrastructure.Services.RestrictiveLists;
using Common.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ListTypeDTO = Common.DTO.RestrictiveLists.ListTypeDTO;

namespace Common.WebApiCore.Controllers.RestrictiveLists
{
    [Route("restrictive-lists/list-types")]
    public class ListTypeController : BaseApiController
    {
        private readonly IListGroupService _listGroupService;
        private readonly ICountryService _countryService;
        private readonly IListPeriodicityService _lListPeriodicityService;
        private readonly IListTypeService _listTypeService;

        public ListTypeController(IListGroupService listGroupService, ICountryService countryService,
            IListPeriodicityService lListPeriodicityService, IListTypeService listTypeService)
        {
            _listGroupService = listGroupService;
            _countryService = countryService;
            _lListPeriodicityService = lListPeriodicityService;
            _listTypeService = listTypeService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilterDTO paginationFilterDto)
        {
            paginationFilterDto.query = paginationFilterDto.query.IsNullOrEmpty() ? "" : paginationFilterDto.query;
            var results = await _listTypeService.GetAll(paginationFilterDto);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            var paginationFilterDto = new PaginationFilterDTO {query = "", ShowAll = true};

            var groupResponse = await _listGroupService.GetAll(paginationFilterDto);
            var countriesResponse = await _countryService.GetAll(paginationFilterDto);
            var periodicitiesResponse = await _lListPeriodicityService.GetAll(paginationFilterDto);

            var groups = groupResponse.Data;
            var countries = countriesResponse.Data;
            var periodicities = periodicitiesResponse.Data;

            var result = await _listTypeService.GetById(id);
            
            result.Data.ListGroups = groups;
            result.Data.Countries = countries.MapTo<List<CountryDTO>>();
            result.Data.Periodicities = periodicities.MapTo<List<PeriodicityDTO>>();

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> Create(ListTypeDTO dto)
        {
            var result = await _listTypeService.Create(dto);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet]
        [Route("create")]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> Create()
        {
            var paginationFilterDto = new PaginationFilterDTO {query = "", ShowAll = true};

            var groupResponse = await _listGroupService.GetAll(paginationFilterDto);
            var countriesResponse = await _countryService.GetAll(paginationFilterDto);
            var periodicitiesResponse = await _lListPeriodicityService.GetAll(paginationFilterDto);

            var groups = groupResponse.Data;
            var countries = countriesResponse.Data;
            var periodicities = periodicitiesResponse.Data;

            var emptyListType = new ListTypeDTO
            {
                ListGroups = groupResponse.Data,
                Countries = countriesResponse.Data.MapTo<List<CountryDTO>>(),
                Periodicities = periodicitiesResponse.Data.MapTo<List<PeriodicityDTO>>()
            };

            var response = new ResponseDTO<ListTypeDTO>(emptyListType);

            return Ok(response);
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> Edit(int id, ListTypeDTO dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var result = await _listTypeService.Edit(dto);

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
            var result = _listTypeService.Delete(id);
            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}