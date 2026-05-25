using System;

namespace IngressoJa.Contexts.Vendas.Domain.Entities
{
    public class TicketEntity
    {
        public Guid Codigo { get; private set; }

        public Guid UserId { get; private set; }

        public TicketEntity(Guid codigo, Guid userId)
        {
            Codigo = codigo;
            UserId = userId;
        }
    }
}