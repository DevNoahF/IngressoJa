using System;

namespace IngressoJa.Contexts.Vendas.Domain.Entities
{
    public class IngressoEntity
    {
        public Guid Id { get; private set; }

        public Guid EventoId { get; private set; }

        public String Tipo { get; set; }

        public Decimal Preco { get; set; }

        public Int32 QuantidadeDisponivel { get; set; }

        public IngressoEntity(Guid id, Guid eventoId, String tipo, Decimal preco, Int32 quantidadeDisponivel)
        {
            Id = id;
            EventoId = eventoId;
            Tipo = tipo;
            Preco = preco;
            QuantidadeDisponivel = quantidadeDisponivel;
        }
    }
}