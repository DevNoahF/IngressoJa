<<<<<<< HEAD
    using IngressoJa.Contexts.Sales.Adapter.Interfaces;
    using IngressoJa.Contexts.Sales.Domain.Entities;
    using IngressoJa.Contexts.Sales.Domain.Entities.Enums;
    using IngressoJa.Contexts.Sales.Domain.IRepositories;
=======
using IngressoJa.Contexts.Sales.Adapter.Interfaces;
using IngressoJa.Contexts.Sales.Domain.Entities;
using IngressoJa.Contexts.Sales.Domain.IRepositories;
>>>>>>> 6bc7467a4162024c06fe2d5e40f8599c1d3a7d60

    namespace IngressoJa.Contexts.Sales.Application.UseCases.Ticket
    {
<<<<<<< HEAD
        public class CreateTicketUseCase : ICreateTicketUseCase
        {
            private readonly ITicketRepository _ticketRepository;
            private readonly ISaleRepository _saleRepository;

            public CreateTicketUseCase(ITicketRepository ticketRepository, ISaleRepository saleRepository)
            {
                _ticketRepository = ticketRepository;
                _saleRepository = saleRepository;
            }

            public async Task<TicketEntity> ExecuteAsync(int saleId, Guid userId, CancellationToken cancellationToken = default)
            {
                // Buscar a venda
                var sale = await _saleRepository.GetByIdAsync(saleId, cancellationToken);

                if (sale == null)
                    throw new InvalidOperationException($"Sale with ID {saleId} not found.");

                // Verificar se a venda foi aprovada
                if (sale.SaleStatus != SaleStatusEnum.Approved)
                    throw new InvalidOperationException($"Ticket can only be created for approved sales. Current status: {sale.SaleStatus}");

                // Validar se o usuário é o proprietário da venda
                if (sale.UserId != userId)
                    throw new UnauthorizedAccessException("User is not authorized to create tickets for this sale.");

                // Criar o ingresso
                var ticket = new TicketEntity(Guid.NewGuid(), userId);
=======
        private readonly ITicketRepository _repository;

        public CreateTicketUseCase(ITicketRepository repository)
        {
            _repository = repository;
        }

        public async Task<TicketEntity> ExecuteAsync(Guid userId)
        {
            var ticket = new TicketEntity(Guid.NewGuid(), userId);

            await _repository.CreateAsync(ticket);
>>>>>>> 6bc7467a4162024c06fe2d5e40f8599c1d3a7d60

                await _ticketRepository.CreateAsync(ticket);

                return ticket;
            }
        }
    }
