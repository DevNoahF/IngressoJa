using System;
using System.Threading.Tasks;

using IngressoJa.Contexts.Vendas.Domain.Entities;
using IngressoJa.Contexts.Vendas.Domain.IRepositories;
using IngressoJa.Contexts.Vendas.Interfaces;

namespace IngressoJa.Contexts.Vendas.Domain.UseCases.ApproveSale
{
    public class ApproveSaleUseCase : IApproveSaleUseCase
    {
        private readonly ISaleRepository _saleRepository;

        private readonly ITicketRepository _ticketRepository;

        public ApproveSaleUseCase(
            ISaleRepository saleRepository,
            ITicketRepository ticketRepository
        )
        {
            _saleRepository = saleRepository;
            _ticketRepository = ticketRepository;
        }

        public async Task ExecuteAsync(int saleId)
        {
            var sale = await _saleRepository.GetByIdAsync(saleId);

            if (sale == null)
                throw new Exception("Venda não encontrada");

            sale.ApproveSale();

            await _saleRepository.UpdateAsync(sale);

            for (int i = 0; i < sale.SelectedTicketsUser; i++)
            {
                var ticket = new TicketEntity(
                    Guid.NewGuid(),
                    Guid.NewGuid()
                );

                await _ticketRepository.CreateAsync(ticket);
            }
        }
    }
}