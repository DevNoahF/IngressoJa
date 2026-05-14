namespace IngressoJa.Contexts.Vendas.Adapter.DTOs.Request;

public sealed record RealizarVendaRequestDTO(
    Guid UserId,
    Guid EventoId,
    Guid IngressoId,
    int Quantidade,
    int IngressosDisponiveis);
