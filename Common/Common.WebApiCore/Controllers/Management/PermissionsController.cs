using Common.DTO;
using Common.DTO.Management;
using Common.Services.Infrastructure.Management;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.WebApiCore.Controllers.Management
{
    [Route("management/Permissions")]
    public class PermissionsController : BaseApiController
    {

        protected readonly IPermissionsService _permissionsService;

        public PermissionsController(IPermissionsService permissionsService)
        {
            _permissionsService = permissionsService;
        }


        [HttpPost]
        [Route(nameof(PermissionsController.Update))]
        public async Task<IActionResult> Update(RoleUserDTO roleUserDTOs)
        {

            var result = await _permissionsService.Update(roleUserDTOs);
            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest(null);
        }

        [HttpGet]
        [Route(nameof(PermissionsController.GetPermissionsByUser))]
        public async Task<IActionResult> GetPermissionsByUser(int UserId)
        {

            var result = await _permissionsService.GetPermissionsByUserId(UserId);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(null);
        }




    }
}
