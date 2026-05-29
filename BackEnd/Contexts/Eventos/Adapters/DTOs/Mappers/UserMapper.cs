using IngressoJa.Contexts.Eventos.Application.DTOs.Response.User;
using IngressoJa.Contexts.Eventos.Domain.Entities;
using IngressoJa.Data.Model;
using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;
using IngressoJa.Contexts.Eventos.Application.DTOs.Request;
using IngressoJa.Contexts.Eventos.Domain.Entities.Enums;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response;
using BackEnd.Contexts.Eventos.Adapters.DTOs.Request.User;
using IngressoJa.Contexts.Eventos.Adapters.Interfaces.User;

namespace IngressoJa.Contexts.Eventos.Application.DTOs.Mappers;

public class UserMapper : IUserMapper
{
    
    //Converte UserEntity para UserModel
    public UserModel EntityToUserModel(UserEntity entity)
    {
        return new UserModel
        {
            Id = entity.Id,
            Role = entity.Role,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Cpf = entity.Cpf,
            Email = entity.Email,
            PhotoProfile = entity.PhotoProfile,
            PasswordHash = entity.PasswordHash,
            Token = entity.Token,
            DateBirth = entity.DateBirth
        };
    }

    
    //Converte UserEntity para UserModel  
    public UserEntity ModelToEntity(UserModel model)
    {
        return new UserEntity(
            model.Id,
            model.Role,
            model.FirstName,
            model.LastName,
            model.Cpf,
            model.Email,
            PasswordVO.FromHash(model.PasswordHash.Value),
            model.PhotoProfile ?? new PhotoProfileVO(string.Empty),
            model.Token ?? string.Empty,
            model.DateBirth
        );
    }

    //Converte UserRecordedResponseDTO para UserEntity  
    public UserRecordedResponseDTO EntityToRecordedResponse(UserEntity entity)
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
    public UserAuthRequestDTO EntityToAuthRequestDTO(UserEntity entity)
    {
        return new UserAuthRequestDTO(
            entity.Email,
            entity.PasswordHash
        );
    }

    // UserAuthUser para UserEntity
    public UserEntity UserAuthRequestUserToEntity(UserAuthRequestDTO dto)
    {
        return new UserEntity(
            Guid.NewGuid(),
            RoleEnum.User,
            string.Empty,
            string.Empty,
            new CpfVO(string.Empty),
            dto.Email,
            PasswordVO.CreatePassword(dto.Password.Value),
            new PhotoProfileVO(string.Empty),
            string.Empty,
            DateOnly.FromDateTime(DateTime.Now)
        );
    }

    // UserAuthOrganizer para UserEntity
    public UserEntity UserAuthRequestOrganizerToEntity(UserAuthRequestDTO dto)
    {
        return new UserEntity(
            Guid.NewGuid(),
            RoleEnum.Organizer,
            string.Empty,
            string.Empty,
            new CpfVO(string.Empty),
            dto.Email,
            PasswordVO.CreatePassword(dto.Password.Value),
            new PhotoProfileVO(string.Empty),
            string.Empty,
            DateOnly.FromDateTime(DateTime.Now)
        );
    }

    // UserRegisterRequestDTO para UserEntity
    public UserEntity RegisterUserToEntity(UserRegisterRequestDTO dto)
    {
        return new UserEntity(
            Guid.NewGuid(),
            RoleEnum.User,
            dto.FirstName,
            dto.LastName,
            dto.Cpf,
            dto.Email,
            PasswordVO.CreatePassword(dto.Password.Value),
            dto.PhotoProfile,
            string.Empty,
            dto.DateBirth
        );
    }

    // UserRegisterOrganizer para UserEntity
    public UserEntity RegisterOrganizerToEntity(UserRegisterRequestDTO dto)
    {
        return new UserEntity(
            Guid.NewGuid(),
            RoleEnum.Organizer,
            dto.FirstName,
            dto.LastName,
            dto.Cpf,
            dto.Email,
            PasswordVO.CreatePassword(dto.Password.Value),
            dto.PhotoProfile,
            string.Empty,
            dto.DateBirth
        );
    }

    // responseAuth to UserEntity
    public UserAuthResponseDTO AuthResponse(UserEntity entity)
    {
        return new UserAuthResponseDTO(entity.Token);
    }

    // UserAuthRequestDTO para UserAuthResponseDTO
    public UserAuthRequestDTO UserAuthRequestToAuthResponse(UserAuthRequestDTO dto)
    {
        return new UserAuthRequestDTO(
            dto.Email,
            dto.Password
        );
    }

    public  UserEntity UpdateUserToEntity(UserEntity currentUser, UserUpdateRequestDTO dto)
    {
        return new UserEntity(
            currentUser.Id,
            currentUser.Role,
            dto.FirstName,
            dto.LastName,
            currentUser.Cpf,
            dto.Email,
            PasswordVO.CreatePassword(dto.Password.Value),
            dto.PhotoProfile,
            currentUser.Token,
            currentUser.DateBirth
        );
    }
    
}
