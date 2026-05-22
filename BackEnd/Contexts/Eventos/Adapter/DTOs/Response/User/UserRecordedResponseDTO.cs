using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IngressoJa.Contexts.Eventos.Application.DTOs.Response.User
{
    public record UserRecordedResponseDTO(
        Guid Id,
        String CompleteName,
        String Email,
        DateTime DateBirth
    );
}