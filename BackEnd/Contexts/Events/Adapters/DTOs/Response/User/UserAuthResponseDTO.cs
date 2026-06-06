using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IngressoJa.Contexts.Eventos.Domain.Entities.Enums;
using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;

namespace IngressoJa.Contexts.Eventos.Application.DTOs.Response
{
    public record UserAuthResponseDTO(
        Guid Id,
        RoleEnum role,
        String Token,
        String FistName,
        PhotoProfileVO PhotoProfile
        );
}