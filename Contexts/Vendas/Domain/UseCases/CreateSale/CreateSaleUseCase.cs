using System;
using System.Threading.Tasks;

using IngressoJa.Contexts.Vendas.Domain.Entities;
using IngressoJa.Contexts.Vendas.Domain.Entities.Enums;
using IngressoJa.Contexts.Vendas.Domain.IRepositories;
using IngressoJa.Contexts.Vendas.Interfaces;

namespace IngressoJa.Contexts.Vendas.Domain.UseCases.CreateSale
{
    public class CreateSaleUseCase : ICreateSaleUseCase
    {
        private readonly ISaleRepository _repository;

        public CreateSaleUseCase(ISaleRepository repository)
        {
            _repository = repository;
        }

        public async Task<SaleEntity> ExecuteAsync(
            int userId,
            int eventId,
            int selectedTicketsUser,
            double totalPrice
        )
        {
            var sale = new SaleEntity(
                0,
                userId,
                eventId,
                selectedTicketsUser,
                totalPrice,
                DateTime.UtcNow,
                SaleStatusEnum.Pending
            );

            await _repository.CreateAsync(sale);

            return sale;
        }
    }
}