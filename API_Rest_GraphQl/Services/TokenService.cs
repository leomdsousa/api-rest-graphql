using API_Rest_GraphQl.Models.Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using API_Rest_GraphQl.Models.AppSettings;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using API_Rest_GraphQl.Services.Interfaces;

namespace API_Rest_GraphQl.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GerarToken(Usuario usuario)
        {
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings").GetSection("Configuration").GetSection("Secret").Value);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                     {
                         new Claim(ClaimTypes.Name, usuario.Nome),
                         new Claim(ClaimTypes.Role, usuario.Role.ToString())
                     }),
                Expires = DateTime.Now.AddHours(5),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);                

            return tokenHandler.WriteToken(token);
        }

    }
}
