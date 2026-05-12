using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;

namespace IngressoJa.Contexts.Eventos.Application.DTOs.Request
{
    public record UserRegisterRequestDTO(
        String CompleteName, 
        EmailVO Email, 
        PasswordVO Password, 
        DateTime DateBirth
        );
}