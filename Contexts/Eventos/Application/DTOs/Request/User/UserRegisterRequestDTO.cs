using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IngressoJa.Contexts.Eventos.Application.DTOs.Request
{
    public record UserRegisterRequestDTO(
        String CompleteName, 
        String Email, 
        String Password, 
        DateTime DateBirth
        );
}