using API_Rest_GraphQl.Models.Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using API_Rest_GraphQl.Models.AppSettings;
using System.Security.Claims;

namespace API_Rest_GraphQl.Services
{
    public static class TokenService
    {
        private static AppSettings _settings;

        public static string GerarToken(Usuario usuario)
        {
            var key = Encoding.ASCII.GetBytes(_settings.Configuration.Secret);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                     {
                         new Claim(ClaimTypes.Name, usuario.Nome),
                         new Claim(ClaimTypes.Role, usuario.Role.ToString())
                     }),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);                

            return tokenHandler.WriteToken(token);
        }

    }
}
