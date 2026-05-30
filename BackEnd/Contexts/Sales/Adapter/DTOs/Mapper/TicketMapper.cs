using IngressoJa.Contexts.Sales.Adapter.DTOs.Request.Ticket;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Response.Ticket;
using IngressoJa.Contexts.Sales.Domain.Entities;
using IngressoJa.Contexts.Shared.Data.Model;

namespace IngressoJa.Contexts.Sales.Adapter.DTOs.Mapper;

public static class TicketMapper
{
    public static TicketEntity ToEntity(this CreateTicketRequestDTO dto)
    {
        return new TicketEntity(
            dto.Code,
            dto.UserId
        );
    }

    public static CreateTicketResponseDTO ToCreateTicketResponseDTO(this TicketEntity entity)
    {
        return new CreateTicketResponseDTO(
            Guid.NewGuid(),
            entity.UserId
            );
    }

    public static TicketEntity ToEntity(this CreateTicketResponseDTO dto)
    {
        return new TicketEntity(
            Guid.NewGuid(),
            dto.UserId
        );
    }

    public static GetTicketResponseDTO ToGetTicketResponseDTO(this TicketEntity entity)
    {
        return new GetTicketResponseDTO(
            entity.Code,
            entity.UserId
            );
    }
    public static TicketModel ToModel(this TicketEntity entity)
    {
        return entity.EntityToModel();
    }

    public static TicketEntity ToEntity(this TicketModel model)
    {
        return model.ModelToEntity();
    }

    public static TicketModel EntityToModel(this TicketEntity entity)
    {
        return new TicketModel
        {
            Code = entity.Code,
            UserId = entity.UserId
        };
    }

    public static TicketEntity ModelToEntity(this TicketModel model)
    {
        return new TicketEntity(model.Code, model.UserId);
    }
}
