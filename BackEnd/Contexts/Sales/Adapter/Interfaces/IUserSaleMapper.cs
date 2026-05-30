using IngressoJa.Contexts.Sales.Adapter.DTOs.Request.UserSale;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Response.UserSale;
using IngressoJa.Contexts.Sales.Domain.Entities;
using IngressoJa.Data.Model;

namespace IngressoJa.Contexts.Sales.Adapter.Interfaces;

public interface IUserSaleMapper
{
    CreateUserSaleResponse ToCreateUserSaleResponse(UserSaleEntity entity);
    UserSaleEntity ToEntity(CreateUserSaleRequestDTO dto);
    GetUserSaleResponseDTO ToGetUserSaleResponseDTO(UserSaleEntity entity);
    UpdateUserSaleResponseDTO ToUpdateUserSaleResponseDTO(UserSaleEntity entity);
    UserSaleEntity ToEntity(UpdateUserSaleRequestDTO dto);
    UserSaleEntity ToEntity(UpdateUserSaleRequestDTO dto, Guid id);
    UserSaleEntity ModelToEntity(UserModel model);
    UserModel ToModel(UserSaleEntity entity);
}