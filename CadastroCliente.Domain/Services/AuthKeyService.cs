using CadastroCliente.Domain.Interfaces;
using CadastroCliente.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CadastroCliente.Domain.Services
{
    public class AuthKeyService : IAuthKeyService
    {
        private readonly IAuthKeyRepository _authKeyRepository;
        private readonly IConfiguration _config;

        public AuthKeyService(IAuthKeyRepository authKeyRepository, IConfiguration config)
        {
            _authKeyRepository = authKeyRepository;
            _config = config;
        }

        public async Task<string?> Authenticate(string companyName, string apiKey)
        {
            try
            {
                var authKey = await _authKeyRepository.GetByCompanyNameAndKey(companyName, apiKey);
                if (authKey == null) return null;

                return GenerateJwt(authKey);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private string GenerateJwt(AuthKey authKey)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, authKey.CompanyName),
            new Claim("ApiKey", authKey.Key)
        };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
