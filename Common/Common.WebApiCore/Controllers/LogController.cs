using System.Linq;
using System.Threading.Tasks;
using Common.DTO;
using Common.DTO.Log;
using Common.DTO.RestrictiveLists;
using Common.DTO.Users;
using Common.Entities;
using Common.Services.Infrastructure.Management;
using Common.Services.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Common.WebApiCore.Controllers
{
    [Route("log")]
    public class LogController: BaseApiController
    {
        private readonly ILogService _logService;
        private readonly IPermissionsService _permissionsService;

        public LogController(ILogService logService, IPermissionsService permissionsService )
        {
            _logService = logService;
            _permissionsService = permissionsService;
        }
        
        [HttpGet]
        [Route("users")]
        [Authorize]
        public async Task<IActionResult> GetAllUsers([FromQuery] LogPaginationFilterDTO paginationFilter)
        {
            var results = await _logService.GetAll<User, UserManagementDto>(paginationFilter);

            for (int i = 0; i < results.Data.Count; i++)
            {
                if (results.Data[i].CurrentRecord.Permissions == null || results.Data[i].CurrentRecord.Permissions.Length == 0)
                {
                    results.Data[i].CurrentRecord.roleUser = await _permissionsService.GetPermissionsByUserId(results.Data[i].Record.Id);
                }
                results.Data[i].Record= OrganicePermissions(results.Data[i].Record);                
            }            
            
            return Ok(results);
        }

        private UserManagementDto OrganicePermissions(UserManagementDto userManagementDto)            
        {
            userManagementDto.roleUser = _permissionsService.PermissionsToRoleUserDTO(userManagementDto.Permissions.ToList());
            return userManagementDto;
        }

        [HttpGet]
        [Route("additional-company-services")]
        [Authorize]
        public async Task<IActionResult> GetAllAdditionalCompanyService([FromQuery] LogPaginationFilterDTO paginationFilter)
        {
            var results = await _logService.GetAll<AdditionalCompanyService, AdditionalCompanyServiceDTO>(paginationFilter);
            return Ok(results);
        }
        
        [HttpGet]
        [Route("lists")]
        [Authorize]
        public async Task<IActionResult> GetAllLists([FromQuery] LogPaginationFilterDTO paginationFilter)
        {
            var results = await _logService.GetAll<List, ListDTO>(paginationFilter);
            return Ok(results);
        }
        
        [HttpGet]
        [Route("plans")]
        [Authorize]
        public async Task<IActionResult> GetAllPlans([FromQuery] LogPaginationFilterDTO paginationFilter)
        {
            var results = await _logService.GetAll<Plan, PlanDTO>(paginationFilter);
            return Ok(results);
        }
    }
}