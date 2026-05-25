using System.Threading.Tasks;

namespace IngressoJa.Contexts.Vendas.Domain.UseCases.ApproveSale
{
    public interface IApproveSaleUseCase
    {
        Task ExecuteAsync(int saleId);
    }
}