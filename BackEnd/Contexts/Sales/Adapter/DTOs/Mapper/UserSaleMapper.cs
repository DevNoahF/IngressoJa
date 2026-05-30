using IngressoJa.Contexts.Eventos.Application.DTOs.Request.Event;
using IngressoJa.Contexts.Eventos.Domain.Entities;
using IngressoJa.Contexts.Eventos.Domain.Entities.Enums;
using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Request.UserSale;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Response.UserSale;
using IngressoJa.Contexts.Sales.Domain.Entities;
using IngressoJa.Data.Model;

namespace IngressoJa.Contexts.Sales.Adapter.DTOs.Mapper;

public static class UserSaleMapper
{
    public static CreateUserSaleResponse ToCreateUserSaleResponse(this UserSaleEntity entity)
    {
        return new CreateUserSaleResponse(
            entity.Id,
            entity.FirstName,
            entity.LastName,
            entity.Cpf,
            entity.Email
        );
    }

    public static UserSaleEntity ToEntity(this CreateUserSaleRequestDTO dto)
    {
        return new UserSaleEntity(
            dto.FirstName,
            dto.LastName,
            dto.Cpf.Value,
            dto.Email.Value
            );
    }

    public static GetUserSaleResponseDTO ToGetUserSaleResponseDTO(this UserSaleEntity entity)
    {
        return new GetUserSaleResponseDTO(
            entity.Id,
            entity.FirstName,
            entity.LastName,
            entity.Cpf,
            entity.Email
        );
    }

    public static UpdateUserSaleResponseDTO ToUpdateUserSaleResponseDTO(this UserSaleEntity entity)
    {
        return new UpdateUserSaleResponseDTO(
            entity.Id,
            entity.FirstName,
            entity.LastName,
            entity.Cpf,
            entity.Email
            );
    }

    public static UserSaleEntity ToEntity(this UpdateUserSaleRequestDTO dto)
    {
        return new UserSaleEntity(
            dto.FirstName,
            dto.LastName,
            dto.Cpf.Value,
            dto.Email.Value
        );
    }
    

    public static UserSaleEntity ModelToEntity(this UserModel model)
    {
        return new UserSaleEntity(
            model.FirstName,
            model.LastName,
            model.Cpf.Value,
            model.Email.Value
            );
    }
}