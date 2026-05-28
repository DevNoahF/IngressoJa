using IngressoJa.Contexts.Sales.Adapter.Interfaces;
using IngressoJa.Contexts.Sales.Domain.Entities;
using IngressoJa.Contexts.Sales.Domain.IRepositories;

namespace IngressoJa.Contexts.Sales.Application.UseCases.Ticket
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
