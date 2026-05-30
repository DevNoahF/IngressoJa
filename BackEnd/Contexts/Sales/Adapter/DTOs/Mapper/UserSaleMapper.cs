using IngressoJa.Contexts.Eventos.Domain.Entities.Enums;
using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Request.UserSale;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Response.UserSale;
using IngressoJa.Contexts.Sales.Adapter.Interfaces;
using IngressoJa.Contexts.Sales.Domain.Entities;
using IngressoJa.Data.Model;

namespace IngressoJa.Contexts.Sales.Adapter.DTOs.Mapper;

public class UserSaleMapper : IUserSaleMapper
{
    public CreateUserSaleResponse ToCreateUserSaleResponse(UserSaleEntity entity)
    {
        return new CreateUserSaleResponse(
            entity.Id,
            entity.FirstName,
            entity.LastName,
            entity.Cpf,
            entity.Email
        );
    }

    public UserSaleEntity ToEntity(CreateUserSaleRequestDTO dto)
    {
        return new UserSaleEntity(
            dto.FirstName,
            dto.LastName,
            dto.Cpf.Value,
            dto.Email.Value
            );
    }

    public GetUserSaleResponseDTO ToGetUserSaleResponseDTO(UserSaleEntity entity)
    {
        return new GetUserSaleResponseDTO(
            entity.Id,
            entity.FirstName,
            entity.LastName,
            entity.Cpf,
            entity.Email
        );
    }

    public UpdateUserSaleResponseDTO ToUpdateUserSaleResponseDTO(UserSaleEntity entity)
    {
        return new UpdateUserSaleResponseDTO(
            entity.Id,
            entity.FirstName,
            entity.LastName,
            entity.Cpf,
            entity.Email
            );
    }

    public UserSaleEntity ToEntity(UpdateUserSaleRequestDTO dto)
    {
        return new UserSaleEntity(
            dto.FirstName,
            dto.LastName,
            dto.Cpf.Value,
            dto.Email.Value
        );
    }

    public UserSaleEntity ToEntity(UpdateUserSaleRequestDTO dto, Guid id)
    {
        return new UserSaleEntity(
            dto.FirstName,
            dto.LastName,
            dto.Cpf.Value,
            dto.Email.Value
        );
    }
    

    public UserSaleEntity ModelToEntity(UserModel model)
    {
        return new UserSaleEntity(
            model.FirstName,
            model.LastName,
            model.Cpf.Value,
            model.Email.Value
            );
    }

    public UserModel ToModel(UserSaleEntity entity)
    {
        return new UserModel
        {
            Id = entity.Id,
            Role = RoleEnum.User,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Cpf = entity.Cpf,
            Email = entity.Email,
            PasswordHash = new PasswordVO(),
            PhotoProfile = null,
            Token = string.Empty,
            DateBirth = DateOnly.MinValue
        };
    }
}