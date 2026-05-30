namespace IngressoJa.Contexts.Sales.Adapter.DTOs.Request.Ticket;

public record CreateTicketRequestDTO(
    Guid UserId,
    Guid EventId,
    int SaleId
    );

    
