using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.Entities.Orders
{
    public class OrderProduct
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string JsonParameters { get; set; } = string.Empty;
        public int OrderId { get; set; }
        public Order? Order { get; set; }
    }
}
