using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IngressoJa.Contexts.Vendas.Domain.Entities.Enums;

namespace IngressoJa.Data.Model
{
    public class SalesModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public int SelectedTicketsUser { get; set; }
        public double TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public SaleStatusEnum SaleStatus { get; set; } = SaleStatusEnum.Pending;
        
    }
}