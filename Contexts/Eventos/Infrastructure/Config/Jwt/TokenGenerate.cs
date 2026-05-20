using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IngressoJa.Contexts.Eventos.Application.Interfaces.User;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace IngressoJa.Contexts.Eventos.Infrastructure.Config.Jwt
{
    public class TokenGenerate : ITokenGenerate
    
    {
        private readonly IConfiguration _configuration;
        public TokenGenerate(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(Guid userId, string email)
        {
            var tokenHandler = new JsonWebTokenHandler(); // Cria um manipulador de tokens JWT
            var secret = _configuration["JwtSettings:SecretKey"] ?? _configuration["Jwt:SecretKey"];
            if (string.IsNullOrEmpty(secret))
                throw new ArgumentNullException("JWT Secret não configurada!");

            var key = System.Text.Encoding.ASCII.GetBytes(secret); // Obtém a chave secreta do arquivo de configuração
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new System.Security.Claims.Claim("id", userId.ToString()), // Adiciona o ID do usuário como uma reivindicação
                    new System.Security.Claims.Claim("email", email) // Adiciona o email do usuário como uma reivindicação
                }),
                Expires = DateTime.UtcNow.AddHours(1), // TODO: ver se realmente precisa de expiração
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature) // Define as credenciais de assinatura usando a chave secreta e o algoritmo HMAC SHA256
            };
                    return tokenHandler.CreateToken(tokenDescriptor); // Cria o token JWT com base na descrição do token
        }
        
    }
}