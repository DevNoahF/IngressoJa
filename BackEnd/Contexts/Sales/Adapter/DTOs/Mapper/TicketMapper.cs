using IngressoJa.Contexts.Sales.Domain.Entities;
using IngressoJa.Contexts.Shared.Data.Model;

namespace IngressoJa.Contexts.Sales.Adapter.DTOs.Mapper;

public static class TicketMapper
{
    public static TicketModel ToModel(this TicketEntity entity)
    {
        return new TicketModel
        {
            Code = entity.Code,
            UserId = entity.UserId
        };
    }

    public static TicketEntity ToEntity(this TicketModel model)
    {
        return new TicketEntity(model.Code, model.UserId);
    }
}
