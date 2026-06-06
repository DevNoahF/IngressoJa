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
            entity.UserId,
            entity.FirstName,
            entity.LastName,
            entity.Cpf,
            entity.Email
        );
    }

    // Cria UserSaleEntity a partir de um UserModel já existente
    public UserSaleEntity ToEntity(CreateUserSaleRequestDTO dto, UserModel user)
    {
        return new UserSaleEntity(
            user.FirstName,
            user.LastName,
            user.Cpf.Value,
            user.Email.Value,
            user.Id
        );
    }

    public GetUserSaleResponseDTO ToGetUserSaleResponseDTO(UserSaleEntity entity)
    {
        return new GetUserSaleResponseDTO(
            entity.UserId,
            entity.FirstName,
            entity.LastName,
            entity.Cpf,
            entity.Email
        );
    }

    public UpdateUserSaleResponseDTO ToUpdateUserSaleResponseDTO(UserSaleEntity entity)
    {
        return new UpdateUserSaleResponseDTO(
            entity.UserId,
            entity.FirstName,
            entity.LastName,
            entity.Cpf,
            entity.Email
        );
    }

    // Update também parte do usuário existente
    public UserSaleEntity ToEntity(UpdateUserSaleRequestDTO dto, UserModel user)
    {
        return new UserSaleEntity(
            user.FirstName,
            user.LastName,
            user.Cpf.Value,
            user.Email.Value,
            user.Id
        );
    }

    public UserSaleEntity ModelToEntity(UserModel model)
    {
        return new UserSaleEntity(
            model.FirstName,
            model.LastName,
            model.Cpf.Value,
            model.Email.Value,
            model.Id
        );
    }

    public UserModel ToModel(UserSaleEntity entity)
    {
        return new UserModel
        {
            Id = entity.UserId,
            Role = RoleEnum.User,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Cpf = entity.Cpf,
            Email = entity.Email,
            PasswordHash = new PasswordVO(),
            PhotoProfile = null,
            DateBirth = DateOnly.MinValue,
            CreatedAt = DateTime.UtcNow
        };
    }
}