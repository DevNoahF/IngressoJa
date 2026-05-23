using IngressoJa.Contexts.Vendas.Domain.Entities;
using IngressoJa.Contexts.Vendas.Domain.IRepositories;

namespace IngressoJa.Contexts.Vendas.Application.UseCases;

public sealed class CreateSaleUseCase
{
    private readonly ISaleRepository _vendaRepository;

    public CreateSaleUseCase(ISaleRepository vendaRepository)
    {
        _vendaRepository = vendaRepository;
    }

    public async Task<SaleEntity> ExecuteAsync(
        int userId,
        int eventId,
        int selectedTicketsUser,
        double totalPrice,
        int availableTickets,
        CancellationToken cancellationToken = default)
    {
        if (selectedTicketsUser > availableTickets)
            throw new InvalidOperationException("There are not enough tickets available.");

        var venda = new SaleEntity(
            userId,
            eventId,
            selectedTicketsUser,
            totalPrice);

        await _vendaRepository.AdicionarAsync(venda, cancellationToken);

        return venda;
    }
}
