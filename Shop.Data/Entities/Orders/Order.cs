using Shop.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.Entities.Orders
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime DateRegister { get; set; } = DateTime.Now;
        public OrderStatus Status { get; set; } = OrderStatus.negotiation;
        public int OrderAddressId { get; set; }
        public OrderAddress? OrderAddress { get; set; }
        public string CustomerLogin { get; set; } = string.Empty;
        public Customer? Customer { get; set; }
        public List<OrderProduct> Products { get; set; } = new List<OrderProduct>();
    }
}
