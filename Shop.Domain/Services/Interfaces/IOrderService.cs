using Shop.Data.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order[]> GetOrdersByCustomerAsync(string customerLogin);
        Task<Order> GetOrderByPropertyAsync(Expression<Func<Order, bool>> property, string customerLogin);
        Task<Order> AddOrderAsync(Order order);
        Task DeleteOrderAsync(int orderId);
        Task<Order> UpdateOrderAsync(Order order);
        Task<Order> UpdateOrderAddressAsync(Order order);
        Task<Order> UpdateOrderProductsAsync(Order order);
    }
}
