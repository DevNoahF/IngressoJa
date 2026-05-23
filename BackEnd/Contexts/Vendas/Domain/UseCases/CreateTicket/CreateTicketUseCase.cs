using System;
using System.Threading.Tasks;

using IngressoJa.Contexts.Vendas.Adapter.DTOs.Request.Ingresso;
using IngressoJa.Contexts.Vendas.Adapter.DTOs.Response.Ingresso;

using IngressoJa.Contexts.Vendas.Domain.Entities;
using IngressoJa.Contexts.Vendas.Domain.IRepositories;

namespace IngressoJa.Contexts.Vendas.Domain.UseCases.CreateIngresso
{
    public class CreateIngressoUseCase : ICreateIngressoUseCase
    {
        private readonly IIngressoRepository _repository;

        public CreateIngressoUseCase(IIngressoRepository repository)
        {
            _repository = repository;
        }

        public async Task<CreateIngressoResponseDTO> ExecuteAsync(CreateIngressoRequestDTO dto)
        {
            var ingresso = new IngressoEntity(
                Guid.NewGuid(),
                dto.EventoId,
                dto.Tipo,
                dto.Preco,
                dto.QuantidadeDisponivel
            );

            await _repository.CreateAsync(ingresso);

            return new CreateIngressoResponseDTO
            {
                Id = ingresso.Id,
                EventoId = ingresso.EventoId,
                Tipo = ingresso.Tipo,
                Preco = ingresso.Preco,
                QuantidadeDisponivel = ingresso.QuantidadeDisponivel
            };
        }
    }
}