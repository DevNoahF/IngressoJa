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
            DateBirth = entity.DateBirth,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
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
            model.PasswordHash,
            model.PhotoProfile ?? new PhotoProfileVO(string.Empty),
            model.DateBirth,
            model.CreatedAt,
            model.UpdatedAt
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
    public UserEntity UserAuthRequestUserToEntity(UserAuthRequestDTO dto, Guid id)
    {
        return new UserEntity(
            id,
            RoleEnum.User,
            string.Empty,
            string.Empty,
            new CpfVO(string.Empty),
            dto.Email,
            PasswordVO.CreatePassword(dto.Password.Value),
            new PhotoProfileVO(string.Empty),
            DateOnly.FromDateTime(DateTime.Now)
        );
    }

    // UserAuthOrganizer para UserEntity
    public UserEntity UserAuthRequestOrganizerToEntity(UserAuthRequestDTO dto, Guid id)
    {
        return new UserEntity(
            id,
            RoleEnum.Organizer,
            string.Empty,
            string.Empty,
            new CpfVO(string.Empty),
            dto.Email,
            PasswordVO.CreatePassword(dto.Password.Value),
            new PhotoProfileVO(string.Empty),
            DateOnly.FromDateTime(DateTime.Now)
        );
    }

    // UserRegisterRequestDTO para UserEntity
    public UserEntity RegisterUserToEntity(UserRegisterRequestDTO dto, Guid id)
    {
        return new UserEntity(
            id,
            RoleEnum.User,
            dto.FirstName,
            dto.LastName,
            dto.Cpf,
            dto.Email,
            PasswordVO.CreatePassword(dto.Password.Value),
            dto.PhotoProfile,
            dto.DateBirth
        );
    }

    // UserRegisterOrganizer para UserEntity
    public UserEntity RegisterOrganizerToEntity(UserRegisterRequestDTO dto, Guid id)
    {
        return new UserEntity(
            id,
            RoleEnum.Organizer,
            dto.FirstName,
            dto.LastName,
            dto.Cpf,
            dto.Email,
            PasswordVO.CreatePassword(dto.Password.Value),
            dto.PhotoProfile,
            dto.DateBirth
        );
    }

    // UserEntity e token para UserAuthResponseDTO
    public UserAuthResponseDTO AuthResponse(UserEntity user, string token)
    {
        return new UserAuthResponseDTO(
            user.Id,
            user.Role,
            token,
            user.FirstName,
            user.PhotoProfile
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
            currentUser.DateBirth,
            currentUser.CreatedAt
        );
    }
    
}
