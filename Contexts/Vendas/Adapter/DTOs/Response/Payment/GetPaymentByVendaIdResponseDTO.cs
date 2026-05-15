using IngressoJa.Contexts.Vendas.Domain.Entities.Enums;

namespace IngressoJa.Contexts.Vendas.Adapter.DTOs.Response.Payment;

public record GetPaymentByVendaIdResponseDTO(
    Guid Id,
    Guid VendaId,
    Double Value,
    MethodEnum Method,
    PaymentStatusEnum Status,
    DateTime CreatedAt,
    DateTime AprovadoAt
    );