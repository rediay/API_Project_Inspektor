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
    [Route("States")]
    public class StatesController : BaseApiController
    {
        protected readonly IStatesService statesService;
      

        public StatesController(IStatesService statesService)
        {
            this.statesService = statesService;
            
            
        }

        [HttpGet]
        [Route(nameof(StatesController.GetStatesbyId))]
        public async Task<IActionResult> GetStatesbyId(int idcountry)
        {
            var countriesResponse = await statesService.GetStatesbyId(idcountry);         
            return Ok(countriesResponse);
        }

        [HttpGet]
        [Route(nameof(StatesController.Getall))]
        public async Task<IActionResult> Getall([FromQuery] PaginationFilterDTO paginationFilterDto)
        {
            paginationFilterDto.query = paginationFilterDto.query.IsNullOrEmpty() ? "" : paginationFilterDto.query;
            var users = await statesService.GetAll(paginationFilterDto);
            return Ok(users);
        }

        [HttpPost]
        [Route(nameof(StatesController.Create))]
        public async Task<IActionResult> Create(List<StatesDTO> dtos)
        {
            var result = await statesService.BulkCreate(dtos);
            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut]
        [Route(nameof(StatesController.Edit))]
        public async Task<IActionResult> Edit(int id, StatesDTO dto)
        {
            var result = await statesService.Edit(dto);

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
            var result = await statesService.Delete(id);
            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }




    }
}