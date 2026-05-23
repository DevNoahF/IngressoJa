using System;
using System.Threading.Tasks;

using IngressoJa.Contexts.Vendas.Adapter.Interfaces;
using IngressoJa.Contexts.Vendas.Domain.Entities;
using IngressoJa.Contexts.Vendas.Domain.IRepositories;

namespace IngressoJa.Contexts.Vendas.Domain.UseCases.CreateTicket
{
    public class CreateTicketUseCase : ICreateTicketUseCase
    {
        private readonly ITicketRepository _repository;

        public CreateTicketUseCase(ITicketRepository repository)
        {
            _repository = repository;
        }

        public async Task<TicketEntity> ExecuteAsync(Guid userId)
        {
            var ticket = new TicketEntity(Guid.NewGuid(), userId);

            await _repository.CreateAsync(ticket);

            return ticket;
        }
    }
}