using Common.DTO;
using Common.DTO.OwnLists;
using Common.Services.Infrastructure.Management;
using Common.Services.Infrastructure.OwnLists;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Common.WebApiCore.Controllers.OwnLists
{
    [Route("OwnLists")]
    public class OwnListsController : BaseApiController
    {
        protected readonly IOwnListsService _ownListsService ;
        
        public OwnListsController(IOwnListsService ownListsService )
        {
            _ownListsService= ownListsService;
        }

        [HttpGet]
        [Route(nameof(OwnListsController.GetOwnLists))]
        [ValidateIdCompany]
        public async Task<IActionResult> GetOwnLists(int CompanyId)
        {
            var companyList = await _ownListsService.GetOwnLists(CompanyId);
            return Ok(companyList);
        }

        [HttpPost]
        [Route(nameof(OwnListsController.UpdateOwnLists))]
        [ValidateIdCompany]
        public async Task<IActionResult> UpdateOwnLists(OwnListDTO ownListDTO)
        {
            bool result = await _ownListsService.UpdateOwnList(ownListDTO);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpPost]
        [Route(nameof(OwnListsController.CreateOwnList))]
        public async Task<IActionResult> CreateOwnList(OwnListDTO ownListsDTO)
        {
            bool result = await _ownListsService.CreateOwnList(ownListsDTO);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [Route(nameof(OwnListsController.DeleteOwnList))]
        public async Task<IActionResult> DeleteOwnList(string id)
        {
            bool result = await _ownListsService.DeleteOwnList(Convert.ToInt32(id));

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }
    }
}
