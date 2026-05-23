using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IngressoJa.shared.jwt.interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace IngressoJa.shared.jwt
{
    

    public class JwtValidatorUseCase : IJwtValidatorUseCase
    {
        
        private readonly IConfiguration _configuration;
        public JwtValidatorUseCase(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<bool> ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JsonWebTokenHandler();
                var secret = _configuration["JwtSettings:SecretKey"] ?? _configuration["Jwt:SecretKey"];
                if (string.IsNullOrEmpty(secret))
                    throw new ArgumentNullException("JWT Secret não configurada!");

                var key = System.Text.Encoding.ASCII.GetBytes(secret);
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                var result = tokenHandler.ValidateToken(token, validationParameters);
                return Task.FromResult(result.IsValid);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }
        
        
    }
}