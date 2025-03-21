/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.DTO;
using Common.DTO.Relations_Countrys;
using Common.Services;
using Common.Services.Infrastructure.Mail;
using Common.Services.Infrastructure.Services.Relations_Countrys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.WebApiCore.Controllers
{
    [Route("Cities")]
    public class CitiesController : BaseApiController
    {
        protected readonly ICitiesService _citiesService;
      

        public CitiesController(ICitiesService citiesService)
        {
            this._citiesService = citiesService;
            
            
        }

        [HttpGet]
        [Route(nameof(GetCitiesbyId))]
        public async Task<IActionResult> GetCitiesbyId(int idcountry, int stateid)
        {
            var countriesResponse = await _citiesService.GetCitiesbyId(idcountry,stateid);         
            return Ok(countriesResponse);
        }

        [HttpGet]
        [Route(nameof(StatesController.Getall))]
        public async Task<IActionResult> Getall([FromQuery] PaginationFilterDTO paginationFilterDto)
        {
            paginationFilterDto.query = paginationFilterDto.query.IsNullOrEmpty() ? "" : paginationFilterDto.query;
            var users = await _citiesService.GetAll(paginationFilterDto);
            return Ok(users);
        }

        [HttpPost]
        [Route(nameof(StatesController.Create))]
        public async Task<IActionResult> Create(List<CitiesDTO> dtos)
        {
            var result = await _citiesService.BulkCreate(dtos);
            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut]
        [Route(nameof(StatesController.Edit))]
        public async Task<IActionResult> Edit(int id, CitiesDTO dto)
        {
            var result = await _citiesService.Edit(dto);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete]
        [Route(nameof(StatesController.Delete))]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _citiesService.Delete(id);
            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }



    }
}