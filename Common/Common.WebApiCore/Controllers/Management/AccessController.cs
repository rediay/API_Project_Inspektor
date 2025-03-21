using Common.DTO;
using Common.DTO.Management;
using Common.Services.Infrastructure.Management;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Common.WebApiCore.Controllers.Management
{
    [Route("AccessController")]
    public class AccessController : BaseApiController
    {

        protected readonly IAccessService _accessService;
        protected readonly IAccessSubModulesService _accessSubModulesService;

        public AccessController(IAccessService accessService, IAccessSubModulesService accessSubModulesService)
        {
            this._accessService = accessService;
            this._accessSubModulesService = accessSubModulesService;
        }

        [HttpGet]
        [Route(nameof(AccessController.GetByCompany))]

        public async Task<IActionResult> GetByCompany()
        {
            var thirdPartyType = await _accessService.GetAccesByCompany();
            return Ok(thirdPartyType);
        }

        [HttpGet]
        [Route(nameof(AccessController.GetByIdCompany))]
        [ValidateIdCompany]

        public async Task<IActionResult> GetByIdCompany(int IdCompany)
        {
            var thirdPartyType = await _accessService.GetAccesByIdCompany(IdCompany);
            return Ok(thirdPartyType);
        }

        [HttpPost]
        [Route(nameof(AccessController.Update))]
        public async Task<IActionResult> Update(AccessDTO accessDTO)
        {

            var result = await _accessService.Edit(accessDTO);
            if (result != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [Route(nameof(AccessController.Create))]
        public async Task<IActionResult> Create(AccessDTO accessDTO)
        {

            var result = await _accessService.Edit(accessDTO);
            if (result != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpPost]
        [Route(nameof(AccessController.CreateAccessJson))]
        public async Task<IActionResult> CreateAccessJson(AccessSubModulesDTO accessSubModulesDTO)
        {
            AccessDTO accessDTO = new AccessDTO();
            accessDTO.Name = accessSubModulesDTO.nameAccess;

            var result = await _accessService.Edit(accessDTO);
            if (result != null)
            {
                accessSubModulesDTO.accessId = result.Id;
                var resUpd = UpdateAccess(accessSubModulesDTO);
                if (resUpd != null)
                {
                    return Ok();
                }
                else

                {
                    Delete(result.Id.ToString());
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpPost]
        [Route(nameof(AccessController.Delete))]
        public async Task<IActionResult> Delete(string id)
        {

            bool result = await _accessService.Delete(Convert.ToInt32(id));

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
        [Route(nameof(AccessController.UpdateAccess))]
        public async Task<IActionResult> UpdateAccess(AccessSubModulesDTO accessSubModulesDTO)
        {

            var result = await _accessSubModulesService.Update(accessSubModulesDTO);
            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest(null);



        }

        [HttpGet]
        [Route(nameof(AccessController.GetPermissionsByAccessId))]
        public async Task<IActionResult> GetPermissionsByAccessId(int AccessId)
        {

            var result = await _accessSubModulesService.GetAccessJson(AccessId);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(null);
        }





    }
}
