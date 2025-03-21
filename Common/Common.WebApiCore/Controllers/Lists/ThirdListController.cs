/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.DTO;
using Common.DTO.Lists;
using Common.DTO.Relations_Countrys;
using Common.DTO.RestrictiveLists;
using Common.Services;
using Common.Services.Infrastructure.Mail;
using Common.Services.Infrastructure.Services.Lists;
using Common.Services.Infrastructure.Services.Relations_Countrys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.WebApiCore.Controllers
{
    [Route("ThirdList")]
    public class ThirdListController : BaseApiController
    {
        protected readonly IThirdListsService _thirdListsService;
      

        public ThirdListController(IThirdListsService thirdListsService)
        {
            this._thirdListsService = thirdListsService;
            
            
        }

        [HttpGet]
        [Route(nameof(GetListById))]
        public async Task<IActionResult> GetListById(string id)
        {
            var countriesResponse = await _thirdListsService.GetListById(id);         
            return Ok(countriesResponse);
        }

        [HttpGet]
        [Route(nameof(Getall))]
        public async Task<IActionResult> Getall([FromQuery] PaginationFilterDTO paginationFilterDto)
        {
            paginationFilterDto.query = paginationFilterDto.query.IsNullOrEmpty() ? "" : paginationFilterDto.query;
            var users = await _thirdListsService.GetAll(paginationFilterDto);
            return Ok(users);
        }


        [HttpGet]
        [Route(nameof(GetAllQuery))]
        public async Task<IActionResult> GetAllQuery([FromQuery] ListPaginationFilterThirdDTO paginationFilter)
        {
            // return Ok(paginationFilter);
            var results = await _thirdListsService.GetAllQuery(paginationFilter);
            return Ok(results);
        }

        [HttpGet]
        [Route(nameof(GetAllToVerify))]
        public async Task<IActionResult> GetAllToVerify([FromQuery] ListPaginationFilterThirdDTO paginationFilter)
        {
            // return Ok(paginationFilter);
            var results = await _thirdListsService.GetAllToVerify(paginationFilter);
            return Ok(results);
        }


        [HttpPost]
        [Route(nameof(Create))]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> Create(List<ThirdListsDTO> dtos)
        {
            var result = await _thirdListsService.BulkCreate(dtos);
            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost]
        [Route(nameof(ValidateRegister))]
        public async Task<IActionResult> ValidateRegister(List<int> ids)
        {
            var result = await _thirdListsService.ValidateRegister(ids);
            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut]
        [Route(nameof(Edit))]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> Edit(int id, ThirdListsDTO dto)
        {
            var result = await _thirdListsService.Edit(dto);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete]
        [Route(nameof(Delete))]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _thirdListsService.Delete(id);
            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }



    }
}