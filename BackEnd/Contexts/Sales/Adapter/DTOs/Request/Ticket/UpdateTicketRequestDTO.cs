namespace IngressoJa.Contexts.Sales.Adapter.DTOs.Request.Ticket;

public record UpdateTicketRequestDTO(
    Guid Code,
    Guid UserId
);
