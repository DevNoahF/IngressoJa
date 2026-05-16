using IngressoJa.Contexts.Vendas.Domain.Entities.Enums;

namespace IngressoJa.Contexts.Vendas.Adapter.DTOs.Request.Payment;

public record UpdatePaymentRequestDTO(
        MethodEnum Method,
        PaymentStatusEnum Status,
        DateTime AprovadoAt
    );