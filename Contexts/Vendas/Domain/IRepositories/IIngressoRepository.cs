using System;
using System.Threading.Tasks;

using IngressoJa.Contexts.Vendas.Domain.Entities;

namespace IngressoJa.Contexts.Vendas.Domain.IRepositories
{
    public interface IIngressoRepository
    {
        Task CreateAsync(IngressoEntity ingresso);

        Task<IngressoEntity?> GetByIdAsync(Guid id);
    }
}