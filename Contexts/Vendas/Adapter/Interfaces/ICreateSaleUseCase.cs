using System.Threading.Tasks;

using IngressoJa.Contexts.Vendas.Domain.Entities;

namespace IngressoJa.Contexts.Vendas.Domain.UseCases.CreateSale
{
    public interface ICreateSaleUseCase
    {
        Task<SaleEntity> ExecuteAsync(
            int userId,
            int eventId,
            int selectedTicketsUser,
            double totalPrice
        );
    }
}