using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using IngressoJa.Contexts.Eventos.Application.Interfaces.User;
using Microsoft.IdentityModel.Tokens;

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
            var tokenHandler = new JwtSecurityTokenHandler(); // Cria um manipulador de tokens JWT
            var key = System.Text.Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]); // Obtém a chave secreta do arquivo de configuração
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
            var token = tokenHandler.CreateToken(tokenDescriptor); // Cria o token JWT com base na descrição do token
            return tokenHandler.WriteToken(token); 
        }
        
    }
}