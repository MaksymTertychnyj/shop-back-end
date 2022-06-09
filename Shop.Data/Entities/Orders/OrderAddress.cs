using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.Entities.Orders
{
    public class OrderAddress
    {
        public int Id { get; set; }
        public string Country { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Place { get; set; } = string.Empty;
        public Order? Order { get; set; }
    }
}
