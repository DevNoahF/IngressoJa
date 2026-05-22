using System;

namespace IngressoJa.Contexts.Vendas.Infrastructure.Persistence.Entities
{
    public class IngressoPersistenceEntity
    {
        public Guid Id { get; set; }

        public Guid EventoId { get; set; }

        public String Tipo { get; set; } = String.Empty;

        public Decimal Preco { get; set; }

        public Int32 QuantidadeDisponivel { get; set; }
    }
}