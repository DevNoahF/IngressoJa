using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IngressoJa.Contexts.Sales.Domain.Entities.Enums;

namespace IngressoJa.Data.Model
{
    public class SaleModel
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
        public Guid? TicketId { get; set; }
        public int SelectedTicketsUser { get; set; }
        public double TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public SaleStatusEnum SaleStatus { get; set; } = SaleStatusEnum.Pending;
        
    }
}