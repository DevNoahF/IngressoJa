using System;

namespace IngressoJa.Contexts.Vendas.Adapter.DTOs.Request.Ingresso
{
    public class CreateIngressoRequestDTO
    {
        public Guid EventoId { get; set; }

        public String Tipo { get; set; } = String.Empty;

        public Decimal Preco { get; set; }

        public Int32 QuantidadeDisponivel { get; set; }
    }
}