using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Common.WebApiCore.Controllers.Users
{
    public class UsersPasswordController : BaseApiController 
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;

        public UsersPasswordController(UserManager<User> userManager, IUserService userService)
        {
            _userService = userService;
            _userManager = userManager;
        }

        [HttpPut]
        [Route("password/update")]
        public async Task<IActionResult> Edit(PasswordDto passwordDto)
        {
            var email= User.GetUserEmail();

            if (email == null) return BadRequest();
            
            var user = await _userManager.FindByEmailAsync(email);
            
            if (!await _userManager.CheckPasswordAsync(user, passwordDto.CurrentPassword)) return BadRequest();
            
            var result = await _userManager.ChangePasswordAsync(user, passwordDto.CurrentPassword, passwordDto.NewPassword);

            if (result.Succeeded)
            {
                user.HasResetPassword = false;
                await _userManager.UpdateAsync(user);
                return Ok(true);
            }

            return BadRequest();
        }
    }
}