using System;

namespace IngressoJa.Contexts.Vendas.Adapter.DTOs.Response.Ingresso
{
    public class CreateIngressoResponseDTO
    {
        public Guid Id { get; set; }

        public Guid EventoId { get; set; }

        public String Tipo { get; set; } = String.Empty;

        public Decimal Preco { get; set; }

        public Int32 QuantidadeDisponivel { get; set; }
    }
}