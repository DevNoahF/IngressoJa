using System;

namespace IngressoJa.Contexts.Eventos.Adapter.DTOs.Response.Ingresso
{
    public class CreateIngressoResponseDTO
    {
        public Guid Id { get; set; }

        public Guid EventoId { get; set; }

        public String Tipo { get; set; }

        public Decimal Preco { get; set; }

        public int QuantidadeDisponivel { get; set; }
    }
}