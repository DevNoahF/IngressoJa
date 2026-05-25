using System;
using IngressoJa.Contexts.Vendas.Domain.Entities.Enums;

namespace IngressoJa.Contexts.Vendas.Domain.Entities
{
    public class SaleEntity
    {
        public int Id { get; private set; }

        public int UserId { get; private set; }

        public int EventId { get; private set; }

        public int SelectedTicketsUser { get; private set; }

        public double TotalPrice { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public SaleStatusEnum SaleStatus { get; private set; }

        public SaleEntity(
            int id,
            int userId,
            int eventId,
            int selectedTicketsUser,
            double totalPrice,
            DateTime createdAt,
            SaleStatusEnum saleStatus
        )
        {
            Id = id;
            UserId = userId;
            EventId = eventId;
            SelectedTicketsUser = selectedTicketsUser;
            TotalPrice = totalPrice;
            CreatedAt = createdAt;
            SaleStatus = saleStatus;
        }

        public void ApproveSale()
        {
            SaleStatus = SaleStatusEnum.Approved;
        }
    }
}