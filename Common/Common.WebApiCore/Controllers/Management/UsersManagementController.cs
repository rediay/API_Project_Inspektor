using System;
using System.Threading.Tasks;
using Common.DTO;
using Common.DTO.Users;
using Common.Services.Infrastructure.Services.Users;
using Common.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Common.WebApiCore.Controllers.Users
{
    [Route("management/users")]
    public class UserManagementController : BaseApiController
    {
        private readonly IUserManagementService _userManagementService;

        public UserManagementController(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilterDTO paginationFilterDto)
        {
            paginationFilterDto.query = paginationFilterDto.query.IsNullOrEmpty() ? "" : paginationFilterDto.query;
            var users = await _userManagementService.GetAll(paginationFilterDto);
            return Ok(users);
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _userManagementService.GetById(id);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost]
        [Authorize]
        [ValidateIdCompany]
        public async Task<IActionResult> Create(UserManagementDto userDto)
        {
            try
            {                 
                var result = await _userManagementService.Create(userDto);

                if (result.Succeeded)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route(nameof(UserManagementController.CreateByAdmin))]
        [Authorize]
        [ValidateIdCompany]
        public async Task<IActionResult> CreateByAdmin(UserManagementDto userDto)
        {
            try
            {
                var result = await _userManagementService.CreateByAdmin(userDto);

                if (result.Succeeded)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize]
        [ValidateIdCompany]
        public async Task<IActionResult> Edit(int id, UserManagementDto dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var result = await _userManagementService.Edit(dto);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpPut]        
        [Route(nameof(UserManagementController.ResetPassword) + "/{id:int}")]
        [Authorize]
        [ValidateIdCompany]
        public async Task<IActionResult> ResetPassword(int id, UserManagementDto dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var result = await _userManagementService.ResetPassword(dto);

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
            var result = await _userManagementService.Delete(id);
            return Ok(result);
        }

        [HttpGet]
        [Route(nameof(UserManagementController.GetAllByCompanyId))]
        [Authorize]
        [ValidateIdCompany]
        public async Task<IActionResult> GetAllByCompanyId(int CompanyId)
        {            
            var users = await _userManagementService.GetAllByCompanyId(CompanyId);
            return Ok(users);
        }
    }
}