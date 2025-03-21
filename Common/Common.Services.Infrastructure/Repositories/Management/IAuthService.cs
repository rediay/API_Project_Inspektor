using Common.DTO;
using Common.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.Infrastructure.Repositories.Management
{
    public interface IAuthService
    {
        Task<AuthResult<Token>> Login(LoginDTO loginDto);
        Task<AuthResult<Token>> LoginExternal(LoginDTO loginDto);
        Task<AuthResult<Token>> ChangePassword(ChangePasswordDTO changePasswordDto, int currentUserId);
        Task<AuthResult<Token>> SignUp(SignUpDTO signUpDto);
        Task<AuthResult<string>> RequestPassword(RequestPasswordDTO requestPasswordDto);
        Task<AuthResult<Token>> RestorePassword(RestorePasswordDTO restorePasswordDto);
        Task<AuthResult<Token>> SignOut();
        Task<AuthResult<Token>> RefreshToken(RefreshTokenDTO refreshTokenDto);
        Task<Token> GenerateToken(int userId);
        Task<string> GetHashPassword(string email, string newPassword);
    }
}
