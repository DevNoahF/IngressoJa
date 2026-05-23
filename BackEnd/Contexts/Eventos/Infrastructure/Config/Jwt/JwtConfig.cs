using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IngressoJa.Contexts.Eventos.Infrastructure.Config.Jwt
{
    public static class JwtConfig
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var secretKey = configuration.GetSection("JwtSettings:SecretKey").Value //  le uma info no appsettings, "Jwtsettings" 
                                                                                    // é o nome da seção, "SecretKey" é a chave dentro dessa seção
        
                            ?? throw new ArgumentNullException("JWT Secret não configurada!");
            var key = Encoding.ASCII.GetBytes(secretKey); // converte a chave secreta para bytes

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // define o esquema de autenticação padrão como JWT
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // define o esquema de desafio padrão como JWT
            }).AddJwtBearer(t =>
            {
                t.RequireHttpsMetadata = false; // desativa exigencia de https
                t.SaveToken = true;
                t.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true, // valida a chave de assinatura do so emissor(api)
                    IssuerSigningKey = new SymmetricSecurityKey(key), // define a chave de assinatura, que nesse caso é digitos byte.lenght == 8
                    ValidateIssuer = false, // não valida o emissor(quem emitiu)
                    ValidateAudience = false // não valida a audiência
                };
            });

            return services;
        
        }   
        
    }
}