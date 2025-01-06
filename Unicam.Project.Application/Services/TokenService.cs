using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Unicam.Project.Application.Abstractions.Services;
using Unicam.Project.Application.Models.Requests;
using Unicam.Project.Application.Options;

namespace Unicam.Project.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtAuthenticationOption _jwtAuthOption;

        public TokenService(IOptions<JwtAuthenticationOption> jwtAuthOption)
        {
            _jwtAuthOption = jwtAuthOption.Value;
        }

        public string CreateToken(CreateTokenRequest request)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("id_utente", request.IdUtente.ToString()),
                new Claim("Email", request.Email),
                new Claim("Nome", request.Nome),
                new Claim("Cognome", request.Cognome)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtAuthOption.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken(
                issuer: _jwtAuthOption.Issuer,
                expires: DateTime.Now.AddMinutes(30),
                claims: claims,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

    }

}
