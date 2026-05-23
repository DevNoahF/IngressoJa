using IngressoJa.Contexts.Eventos.Application.DTOs.Response.User;
using IngressoJa.Contexts.Eventos.Domain.Entities;
using IngressoJa.Data.Model;
using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;
using IngressoJa.Contexts.Eventos.Application.DTOs.Request;
using IngressoJa.Contexts.Eventos.Domain.Entities.Enums;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response;

namespace IngressoJa.Contexts.Eventos.Application.DTOs.Mappers;

public static class UserMapper
{
    
    //Converte UserEntity para UserModel
    public static UserModel  EntityToUserModel(this UserEntity entity)
    {
        return new UserModel
        {
            Id = entity.Id,
            Role = entity.Role,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Cpf = entity.Cpf,
            Email = entity.Email,
            PasswordHash = entity.PasswordHash,
            Token = entity.Token,
            DateBirth = entity.DateBirth
        };
    }

    
    //Converte UserEntity para UserModel  
    public static UserEntity ModelToEntity(this UserModel model)
    {
        return new UserEntity(
            model.Id,
            model.Role,
            model.FirstName,
            model.LastName,
            model.Cpf,
            model.Email,
            model.PasswordHash,
            model.PhotoProfile,
            model.Token,
            model.DateBirth
        );
    }

    //Converte UserRecordedResponseDTO para UserEntity  
    public static UserRecordedResponseDTO EntityToRecordedResponse(this UserEntity entity)
    {
        return new UserRecordedResponseDTO(
            entity.Id,
            entity.FirstName,
            entity.LastName,
            entity.Cpf,
            entity.Email,
            entity.PhotoProfile,
            entity.DateBirth
        );
    }

    // AuthUserRequestDTO para UserEntity
    public static UserAuthRequestDTO EntityToAuthRequestDTO(this UserEntity entity)
    {
        return new UserAuthRequestDTO(
            entity.Email,
            entity.PasswordHash
        );
    }

    // UserAuthUser para UserEntity
    public static UserEntity UserAuthRequestUserToEntity(this UserAuthRequestDTO dto)
    {
        return new UserEntity(
            Guid.NewGuid(),
            RoleEnum.User,
            string.Empty,
            string.Empty,
            new CpfVO(string.Empty),
            dto.Email,
            dto.Password,
            new PhotoProfileVO(string.Empty),
            string.Empty,
            DateTime.MinValue
        );
    }

    // UserAuthOrganizer para UserEntity
    public static UserEntity UserAuthRequestOrganizerToEntity(this UserAuthRequestDTO dto)
    {
        return new UserEntity(
            Guid.NewGuid(),
            RoleEnum.Organizer,
            string.Empty,
            string.Empty,
            new CpfVO(string.Empty),
            dto.Email,
            dto.Password,
            new PhotoProfileVO(string.Empty),
            string.Empty,
            DateTime.MinValue
        );
    }

    // UserRegisterRequestDTO para UserEntity
    public static UserEntity RegisterUserToEntity(this UserRegisterRequestDTO dto)
    {
        return new UserEntity(
            Guid.NewGuid(),
            RoleEnum.User,
            dto.FirstName,
            dto.LastName,
            dto.Cpf,
            dto.Email,
            dto.Password,
            dto.PhotoProfile,
            string.Empty,
            dto.DateBirth
        );
    }

    // UserRegisterOrganizer para UserEntity
    public static UserEntity RegisterOrganizerToEntity(this UserRegisterRequestDTO dto)
    {
        return new UserEntity(
            Guid.NewGuid(),
            RoleEnum.Organizer,
            dto.FirstName,
            dto.LastName,
            dto.Cpf,
            dto.Email,
            dto.Password,
            dto.PhotoProfile,
            string.Empty,
            dto.DateBirth
        );
    }

    // responseAuth to UserEntity
    public static UserAuthResponseDTO EntityToAuthResponseDTO(this UserEntity entity, string token)
    {
        return new UserAuthResponseDTO(token);
    }
    
}
