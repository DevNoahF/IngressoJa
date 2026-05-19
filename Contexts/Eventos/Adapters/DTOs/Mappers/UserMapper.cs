using IngressoJa.Contexts.Eventos.Application.DTOs.Response.User;
using IngressoJa.Contexts.Eventos.Domain.Entities;
using IngressoJa.Data.Model;
using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;

namespace IngressoJa.Contexts.Eventos.Application.DTOs.Mappers;

public static class UserMapper
{
    /// <summary>
    /// Converte UserEntity para UserModel
    /// </summary>
    public static UserModel ToModel(this UserEntity entity)
    {
        return new UserModel
        {
            Id = entity.Id,
            Role = entity.Role,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Cpf = entity.Cpf,
            Email = entity.Email,
            PasswordHash = entity.Password_hash,
            Token = entity.Token,
            DateBirth = entity.DateBirth
        };
    }

    /// <summary>
    /// Converte UserModel para UserEntity
    /// </summary>
    public static UserEntity ToEntity(this UserModel model)
    {
        return new UserEntity(
            model.Id,
            model.Role,
            model.FirstName,
            model.LastName,
            model.Cpf,
            model.Email,
            model.PasswordHash,
            model.Token,
            model.DateBirth
        );
    }

    /// <summary>
    /// Converte UserEntity para UserRecordedResponseDTO
    /// </summary>
    public static UserRecordedResponseDTO ToRecordedResponse(this UserEntity entity)
    {
        return new UserRecordedResponseDTO(
            entity.Id,
            $"{entity.FirstName} {entity.LastName}",
            entity.Email.Value,
            entity.DateBirth
        );
    }

    /// <summary>
    /// Converte UserModel para UserRecordedResponseDTO
    /// </summary>
    public static UserRecordedResponseDTO ToRecordedResponse(this UserModel model)
    {
        return new UserRecordedResponseDTO(
            model.Id,
            $"{model.FirstName} {model.LastName}",
            model.Email.Value,
            model.DateBirth
        );
    }
}
