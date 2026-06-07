using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;

namespace IngressoJa.Contexts.Eventos.Application.DTOs.Response.User
{
    public record UserRecordedResponseDTO(
        Guid Id,
        String FirstName,
        String LastName,
        CpfVO Cpf,
        EmailVO Email,
        PhotoProfileVO PhotoProfile,
        DateOnly DateBirth
    );
}