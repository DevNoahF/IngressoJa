using System.Threading.Tasks;

using IngressoJa.Contexts.Vendas.Adapter.DTOs.Request.Ingresso;
using IngressoJa.Contexts.Vendas.Adapter.DTOs.Response.Ingresso;

namespace IngressoJa.Contexts.Vendas.Domain.UseCases.CreateIngresso
{
    public interface ICreateIngressoUseCase
    {
        Task<CreateIngressoResponseDTO> ExecuteAsync(CreateIngressoRequestDTO request);
    }
}