using AutoMapper;
using Shop.Data.Entities.Orders;
using Shop.Data.Infrastructure;
using Shop.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> orderRepository;
        private readonly IRepository<OrderAddress> orderAddressRepository;
        private readonly IRepository<OrderProduct> orderProductRepository;
        private readonly IMapper mapper;

        public OrderService(
            IRepository<Order> _orderRepository,
            IRepository<OrderAddress> _orderAddressRepository,
            IRepository<OrderProduct> _orderProductRepository,
            IMapper _mapper
            )
        {
            orderRepository = _orderRepository;
            orderAddressRepository = _orderAddressRepository;
            orderProductRepository = _orderProductRepository;
            mapper = _mapper;
        }

        public async Task<Order> AddOrderAsync(Order order)
        {
            //var orderObj = mapper.Map<Order>(order); 

            await orderRepository.AddAsync(order);
            try
            {
                await orderRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                return null!;
            }

            return order;
        }

        public Task DeleteOrderAsync(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderByPropertyAsync(Expression<Func<Order, bool>> property, string customerLogin)
        {
            throw new NotImplementedException();
        }

        public Task<Order[]> GetOrdersByCustomerAsync(string customerLogin)
        {
            throw new NotImplementedException();
        }

        public Task<Order> UpdateOrderAddressAsync(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<Order> UpdateOrderAsync(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<Order> UpdateOrderProductsAsync(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
