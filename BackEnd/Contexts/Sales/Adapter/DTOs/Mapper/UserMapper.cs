using IngressoJa.Contexts.Sales.Adapter.DTOs.Request;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Response;
using IngressoJa.Contexts.Sales.Domain.Entities;

namespace IngressoJa.Contexts.Sales.Adapter.DTOs.Mapper;

public static class UserMapper
{
    public static UserSaleEntity ToEntity(this CreateUserRequestDTO dto)
    {
        return new UserSaleEntity(
            Guid.NewGuid(),
            dto.FirstName,
            dto.LastName,
            dto.Cpf,
            dto.Email);
    }

    public static UserResponseDTO ToResponse(this UserSaleEntity entity)
    {
        return new UserResponseDTO(
            entity.Id,
            entity.FirstName,
            entity.LastName,
            entity.Cpf,
            entity.Email);
    }

    public static IEnumerable<UserResponseDTO> ToResponse(this IEnumerable<UserSaleEntity> entities)
    {
        return entities.Select(entity => entity.ToResponse());
    }
}
