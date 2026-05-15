using IngressoJa.Contexts.Vendas.Domain.Entities;

namespace IngressoJa.Contexts.Vendas.Adapter.DTOs.Response;

public sealed record VendaResponseDTO(
    Guid Id,
    Guid UserId,
    Guid EventoId,
    Guid IngressoId,
    int Quantidade,
    string StatusCompra,
    DateTime DataVenda)
{
    public static VendaResponseDTO FromEntity(VendasEntidy venda)
    {
        return new VendaResponseDTO(
            venda.Id,
            venda.UserId,
            venda.EventoId,
            venda.IngressoId,
            venda.Quantidade,
            venda.StatusCompra,
            venda.DataVenda);
    }
}
