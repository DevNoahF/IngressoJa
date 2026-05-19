using IngressoJa.Contexts.Vendas.Domain.Entities.Enums;

namespace IngressoJa.Contexts.Vendas.Adapter.DTOs.Response.Payment;

public record CreatePaymentResponseDTO(
        Guid Id,
        Guid VendaId,
        double Value,
        MethodEnum MethodEnum,
        PaymentStatusEnum Status,
        DateTime CreatedAt,
        DateTime AprovadoAt
    );