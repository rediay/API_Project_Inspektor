using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.DTO.RestrictiveLists;
using Common.Entities;
using Common.Services.Infrastructure.Services.RestrictiveLists;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Common.WebApiCore.Controllers.RestrictiveLists
{
    [Route("restrictive-lists/list-validation")]
    public class ListValidationController : BaseApiController
    {
        private readonly IListService _listService;

        public ListValidationController(IListService listService)
        {
            _listService = listService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] ListPaginationFilterDTO paginationFilterDto)
        {
            var results = await _listService.GetAllByValidation(paginationFilterDto);
            return Ok(results);
        }


        [HttpPut]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> Edit(IEnumerable<int> listIds)
        {

            var resp = await _listService.ValidateRecords(listIds);            

            var response = new ResponseDTO<Boolean>(resp);

            return Ok(response);
        }
    }
}