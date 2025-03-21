using Common.DTO;
using Common.Entities;
using Common.Utils.Setup;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils
{
    public class JwtManage
    {
        private readonly JwtOption jwtOptions;

        public JwtManage(IOptions<JwtOption> jwtOptions)
        {
            this.jwtOptions = jwtOptions.Value;
        }

        public Token GenerateToken<TUser>(TUser user, bool isExternal = false) where TUser : User
        {
            var token = new Token
            {
                Expires_in = jwtOptions.AccessValidFor.TotalMilliseconds,
                Access_token = CreateToken(user, jwtOptions.AccessExpiration, jwtOptions.AccessSigningCredentials, isExternal),
                Refresh_token = CreateToken(user, jwtOptions.RefreshExpiration, jwtOptions.RefreshSigningCredentials, isExternal)
            };

            return token;
        }
        private string CreateToken<TUser>(TUser user, DateTime expiration, SigningCredentials credentials, bool isExternal) where TUser : User
        {
            var identity = GenerateClaimsIdentity(user, isExternal);
            var jwt = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                claims: identity.Claims,
                notBefore: jwtOptions.NotBefore,
                expires: expiration,
                signingCredentials: credentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        private ClaimsIdentity GenerateClaimsIdentity<TUser>(TUser user, bool isExternal) where TUser : User
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("CompanyId", user.CompanyId.ToString()),
            };

            if (user.UserRoles != null)
            {
                claims.AddRange(isExternal ? new[] { new Claim("role", "external") } : user.UserRoles.Select(x => new Claim("role", x.Role.Name)));
            }

            if (user.Claims != null)
            {
                claims.AddRange(user.Claims.Select(x => new Claim(x.ClaimType, x.ClaimValue)));
            }

            return new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }

        public ClaimsPrincipal GetPrincipal(string token, bool isAccessToken = true)
        {
            var key = new SymmetricSecurityKey(isAccessToken ? jwtOptions.AccessSecret : jwtOptions.RefreshSecret);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }
}
