using System.Threading.Tasks;
using Common.DTO;
using Common.DTO.RestrictiveLists;
using Common.Services.Infrastructure.Services.RestrictiveLists;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Common.WebApiCore.Controllers.RestrictiveLists
{
    [Route("restrictive-lists/list-groups")]
    public class ListGroupController : BaseApiController
    {
        private readonly IListGroupService _listGroupService;

        public ListGroupController(IListGroupService listGroupService)
        {
            _listGroupService = listGroupService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilterDTO paginationFilterDto)
        {
            paginationFilterDto.query = paginationFilterDto.query.IsNullOrEmpty() ? "" : paginationFilterDto.query;
            var results = await _listGroupService.GetAll(paginationFilterDto);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _listGroupService.GetById(id);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> Create(ListGroupDTO dto)
        {
            var result = await _listGroupService.Create(dto);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> Edit(int id, ListGroupDTO dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var result = await _listGroupService.Edit(dto);

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
            var result =  _listGroupService.Delete(id);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            
            return BadRequest(result);
        }
    }
}