using Common.DTO;
using Common.Entities.Auth;
using Common.Services.Infrastructure.Repositories.Management;
using Common.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Common.Services.Infrastructure;

namespace Common.Services.Management
{
    public class AuthService<TUser> : IAuthService
        where TUser : Entities.User, new()
    {
        protected readonly UserManager<TUser> userManager;
        protected readonly JwtManage jwtManager;
        protected readonly IUserService _userService;

        public AuthService(JwtManage jwtManager, UserManager<TUser> userManager, IUserService userService)
        {
            this.userManager = userManager;
            this.jwtManager = jwtManager;
            _userService = userService;
        }

        public async Task<AuthResult<Token>> Login(LoginDTO loginDto)
        {
            if (loginDto == null || string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password))
                return AuthResult<Token>.UnvalidatedResult;

            var user = await userManager.FindByEmailAsync(loginDto.Email);

            if (user != null && user.Id > 0)
            {
                var validateCompany = _userService.ValidateCompanyIsActive(user.Id);

                if (!user.IsActive) return AuthResult<Token>.BlokedUserResult;

                if (!validateCompany.Result) return AuthResult<Token>.BlokedCompanyResult;

                if (await userManager.CheckPasswordAsync(user, loginDto.Password))
                {
                    var token = jwtManager.GenerateToken(user);
                    return AuthResult<Token>.TokenResult(token);
                }
            }

            return AuthResult<Token>.UnauthorizedResult;
        }

        public async Task<string> GetHashPassword(string email, string newPassword)
        {
            var user = await userManager.FindByEmailAsync(email);
            return userManager.PasswordHasher.HashPassword(user, newPassword);

            //String hashedNewPassword = userManager.PasswordHasher.HashPassword(user, newPassword);
            //await userManager.RemovePasswordAsync(user);
            //var resp1 = await userManager.HasPasswordAsync(user);
            //var resp2 = await userManager.AddPasswordAsync(user, newPassword);

            //string token = await userManager.GeneratePasswordResetTokenAsync(user);
            //var token = jwtManager.GenerateToken(user);
            //var result1 = await userManager.ResetPasswordAsync(user, token, newPassword);
            //var result = await userManager.UpdateAsync(user);
            //var result2 = await userManager.ChangePasswordAsync(user, null, hashedNewPassword);

            //var result = await userManager.AddPasswordAsync(user, newPassword);
            //ApplicationUser cUser = await store.FindByIdAsync(userId);
            //UserStore<ApplicationUser> store = new UserStore<ApplicationUser>();
            //store.SetPasswordHashAsync(cUser, hashedNewPassword);
        }

        public async Task<AuthResult<Token>> LoginExternal(LoginDTO loginDto)
        {
            if (loginDto == null || string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password))
                return AuthResult<Token>.UnvalidatedResult;

            var user = await userManager.FindByEmailAsync(loginDto.Email);

            if (user != null && user.Id > 0)
            {
                var validateCompany = _userService.ValidateCompanyIsActive(user.Id);

                if (!validateCompany.Result) return AuthResult<Token>.BlokedCompanyResult;

                if (await userManager.CheckPasswordAsync(user, loginDto.Password))
                {
                    var isExternal = true;
                    var token = jwtManager.GenerateToken(user, isExternal);
                    return AuthResult<Token>.TokenResult(token);
                }
            }

            return AuthResult<Token>.UnauthorizedResult;
        }
        public async Task<AuthResult<Token>> ChangePassword(ChangePasswordDTO changePasswordDto, int currentUserId)
        {
            if (changePasswordDto == null ||
                string.IsNullOrEmpty(changePasswordDto.ConfirmPassword) ||
                string.IsNullOrEmpty(changePasswordDto.Password) ||
                changePasswordDto.Password != changePasswordDto.ConfirmPassword
            )
                return AuthResult<Token>.UnvalidatedResult;

            if (currentUserId > 0)
            {
                var user = await userManager.FindByIdAsync(currentUserId.ToString());
                var result = await userManager.ChangePasswordAsync(user, null, changePasswordDto.Password);
                if (result.Succeeded)
                    return AuthResult<Token>.SucceededResult;
            }

            return AuthResult<Token>.UnauthorizedResult;
        }

        public async Task<AuthResult<Token>> SignUp(SignUpDTO signUpDto)
        {
            if (signUpDto == null ||
                string.IsNullOrEmpty(signUpDto.Email) ||
                string.IsNullOrEmpty(signUpDto.Password) ||
                string.IsNullOrEmpty(signUpDto.ConfirmPassword) ||
                string.IsNullOrEmpty(signUpDto.FullName) ||
                signUpDto.Password != signUpDto.ConfirmPassword
            )
                return AuthResult<Token>.UnvalidatedResult;

            var newUser = new TUser { Login = signUpDto.FullName, Email = signUpDto.Email };

            var result = await userManager.CreateAsync(newUser, signUpDto.Password);

            if (result.Succeeded)
            {
                if (newUser.Id > 0)
                {
                    await userManager.AddToRoleAsync(newUser, "User");
                    var token = jwtManager.GenerateToken(newUser);
                    return AuthResult<Token>.TokenResult(token);
                }
            }

            return AuthResult<Token>.UnauthorizedResult;
        }

        public async Task<AuthResult<string>> RequestPassword(RequestPasswordDTO requestPasswordDto)
        {
            if (requestPasswordDto == null ||
                string.IsNullOrEmpty(requestPasswordDto.Email))
                return AuthResult<string>.UnvalidatedResult;

            var user = await userManager.FindByEmailAsync(requestPasswordDto.Email);

            if (user != null && user.Id > 0)
            {
                var passwordResetToken = await userManager.GeneratePasswordResetTokenAsync(user);
                return AuthResult<string>.TokenResult(passwordResetToken);
            }

            return AuthResult<string>.UnvalidatedResult;
        }

        public async Task<AuthResult<Token>> RestorePassword(RestorePasswordDTO restorePasswordDto)
        {
            if (restorePasswordDto == null ||
                string.IsNullOrEmpty(restorePasswordDto.Email) ||
                string.IsNullOrEmpty(restorePasswordDto.Token) ||
                string.IsNullOrEmpty(restorePasswordDto.NewPassword) ||
                string.IsNullOrEmpty(restorePasswordDto.ConfirmPassword) ||
                string.IsNullOrEmpty(restorePasswordDto.ConfirmPassword) ||
                restorePasswordDto.ConfirmPassword != restorePasswordDto.NewPassword
            )
                return AuthResult<Token>.UnvalidatedResult;

            var user = await userManager.FindByEmailAsync(restorePasswordDto.Email);

            if (user != null && user.Id > 0)
            {
                var result = await userManager.ResetPasswordAsync(user, restorePasswordDto.Token, restorePasswordDto.NewPassword);

                if (result.Succeeded)
                {
                    var token = jwtManager.GenerateToken(user);
                    return AuthResult<Token>.TokenResult(token);
                }
            }

            return AuthResult<Token>.UnvalidatedResult;
        }

        public Task<AuthResult<Token>> SignOut()
        {
            throw new System.NotImplementedException();
        }

        public async Task<AuthResult<Token>> RefreshToken(RefreshTokenDTO refreshTokenDto)
        {
            var refreshToken = refreshTokenDto?.Token?.Refresh_token;
            if (string.IsNullOrEmpty(refreshToken))
                return AuthResult<Token>.UnvalidatedResult;

            var isExternal = ValidateTokenUserRol(refreshToken);

            try
            {
                var principal = jwtManager.GetPrincipal(refreshToken, isAccessToken: false);
                var userId = principal.GetUserId();
                var user = await userManager.FindByIdAsync(userId.ToString());

                if (user != null && user.Id > 0)
                {
                    var token = jwtManager.GenerateToken(user, isExternal);
                    return AuthResult<Token>.TokenResult(token);
                }
            }
            catch (Exception)
            {
                return AuthResult<Token>.UnauthorizedResult;
            }

            return AuthResult<Token>.UnauthorizedResult;
        }

        private bool ValidateTokenUserRol(string refreshToken)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwt = tokenHandler.ReadJwtToken(refreshToken);

                string role = jwt.Claims.FirstOrDefault(claim => claim.Type == "role")?.Value;

                bool isExternal = role == "external" ? true : false;

                return isExternal;
            }
            catch (Exception EX)
            {
                throw;
            }            
        }

        public async Task<Token> GenerateToken(int userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());

            if (user != null && user.Id > 0)
            {
                return jwtManager.GenerateToken(user);
            }

            return null;
        }
    }
}
