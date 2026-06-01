using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;

namespace BackEnd.Contexts.Eventos.Adapters.DTOs.Request.User
{
    public record UserUpdateRequestDTO(
        string? FirstName, 
        string? LastName, 
        EmailVO? Email, 
        PhotoProfileVO? PhotoProfile, 
        PasswordVO? Password);
    
}