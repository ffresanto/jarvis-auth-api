﻿using JarvisAuth.Core.Messages;
using JarvisAuth.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JarvisAuth.Application.Security
{
    public class JwtTokenSecurity(IConfiguration configuration)
    {

        IConfigurationSection? _jwtSettings = configuration.GetSection("JwtSettings");

        public string GenerateJwtToken(User user, Domain.Models.ApplicationWithPermissions permissions)
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("userId", user.Id.ToString()),
                new Claim("IsAdmin", user.IsAdmin.ToString()),
                new Claim("userEmail", user.Email),
                new Claim("Application", permissions.Application),
                new Claim("Permissions", JsonConvert.SerializeObject(permissions.Permissions))
            };

            var token = new JwtSecurityToken(issuer: _jwtSettings["Issuer"],
                                             audience: _jwtSettings["Audience"],
                                             claims: claims,
                                             expires: DateTime.Now.AddMinutes(double.Parse(_jwtSettings["ExpiresInMinutes"])),
                                             signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshJwtToken(User user, Domain.Models.ApplicationWithPermissions permissions)
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("userId", user.Id.ToString()),
                new Claim("isAdmin", user.IsAdmin.ToString()),
                new Claim("Application", permissions.Application),
                new Claim("Permissions", JsonConvert.SerializeObject(permissions.Permissions))
            };

            var token = new JwtSecurityToken(issuer: _jwtSettings["Issuer"],
                                             audience: _jwtSettings["Audience"],
                                             claims: claims,
                                             expires: DateTime.UtcNow.AddDays(double.Parse(_jwtSettings["RefreshTokenExpiresInDays"])),
                                             signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal ValidateJwtToken(string token, bool validateLifetime)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings["Key"])),
                ValidateLifetime = validateLifetime,
                ValidIssuer = _jwtSettings["Issuer"],
                ValidAudience = _jwtSettings["Audience"]
            };

            ClaimsPrincipal claims;

            try
            {
                claims = new JwtSecurityTokenHandler().ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

                if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    throw new SecurityTokenException(GlobalMessages.INVALID_TOKEN_OR_REFRESH_TOKEN);
            }
            catch (Exception)
            {
                throw new SecurityTokenException(GlobalMessages.INVALID_TOKEN_OR_REFRESH_TOKEN);
            }

            return claims;
        }

    }
}
