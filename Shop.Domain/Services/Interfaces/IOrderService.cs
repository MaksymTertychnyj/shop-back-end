using Shop.Data.Entities.Orders;
using Shop.Data.Enums;
using Shop.Domain.Dto.Order;
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
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<OrderDto> GetOrderByIdAsync(int orderId);
        Task<IEnumerable<OrderDto>> GetOrdersByCustomerAsync(string customerLogin);
        Task<OrderDto> ConfirmOrderAsync(OrderDto order);
        Task<OrderDto> UpdateOrderAsync(OrderDto order);
        Task<OrderDto> UpdateStatusAsync(OrderDto order, OrderStatus status);
        Task<bool> DeleteOrderAsync(int orderId);
    }
}
