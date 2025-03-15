using Microsoft.IdentityModel.Tokens;
using Opuspac.TechnicalTest.Application.DTOs;
using Opuspac.TechnicalTest.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Opuspac.TechnicalTest.Infrastructure
{
    public class TokenService : ITokenService
    {
        public string GenerateToken(UserDTO userDTO)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ViniciusMoreiraTesteTecnicoOpuspac"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Name, userDTO.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: "opuspac",
                audience: "opuspac_portal",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public UserDTO ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("ViniciusMoreiraTesteTecnicoOpuspac");

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = "opuspac",
                ValidateAudience = true,
                ValidAudience = "opuspac_portal",
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                var email = principal.FindFirst(JwtRegisteredClaimNames.Name)?.Value;

                if (string.IsNullOrEmpty(email))
                    throw new Exception("Token inválido");

                return new UserDTO { Email = email };
            }
            catch (Exception)
            {
                throw new Exception("Token inválido");
            }
        }

    }
}
