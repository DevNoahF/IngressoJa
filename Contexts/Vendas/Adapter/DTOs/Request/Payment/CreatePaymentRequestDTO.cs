using IngressoJa.Contexts.Vendas.Domain.Entities.Enums;

namespace IngressoJa.Contexts.Vendas.Adapter.DTOs.Request.Payment;

public record CreatePaymentRequestDTO(
    Double Value,
    MethodEnum Method
    );