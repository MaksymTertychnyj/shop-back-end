using AutoMapper;
using Shop.Data.Entities;
using Shop.Data.Entities.Orders;
using Shop.Data.Infrastructure;
using Shop.Domain.Dto.Order;
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
        private readonly IEntityService<Order> orderService;
        private readonly IEntityService<OrderAddress> orderAddressService;
        private readonly IEntityService<OrderProduct> orderProductService;
        private readonly IEntityService<Product> productService;
        private readonly IMapper _mapper;

        public OrderService(
                            IEntityService<Order> orderService,
                            IEntityService<OrderAddress> orderAddressService,
                            IEntityService<OrderProduct> orderProductService,
                            IEntityService<Product> productService,
                            IMapper mapper
                           )
        {
            this.orderService = orderService;
            this.orderAddressService = orderAddressService;
            this.orderProductService = orderProductService;
            this.productService = productService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            await orderAddressService.GetAllEntitiesAsync();
            await orderProductService.GetAllEntitiesAsync();

            return await orderService.GetAllEntitiesAsync();
        }

        public async Task<OrderDto> GetOrderByIdAsync(int orderId)
        {
            var order = await Task.Run(() => orderService.GetWithIncludedEntities(
                                o => o.Id == orderId,
                                o => o.OrderAddress!,
                                o => o.Products
                               ).Result.FirstOrDefault());

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByCustomerAsync(string customerLogin)
        {
            var orders = await orderService.GetWithIncludedEntities(
                o => o.CustomerLogin == customerLogin,
                o => o.OrderAddress!,
                o => o.Products
               );

            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto> ConfirmOrderAsync(OrderDto order)
        {
            try
            {
                foreach (var product in order.Products)
                {
                    var productEntity = await productService.GetEntityByKeyAsync(product.Id);
                    if (productEntity.Quantity >= product.Quantity)
                    {
                        productEntity.Quantity -= product.Quantity;
                    }
                    else
                    {
                        return null!;
                    }

                    await productService.UpdateEntityAsync(productEntity, productEntity.Id);
                }
                
                var result = await orderService.AddEntityAsync(_mapper.Map<Order>(order));
                return _mapper.Map<OrderDto>(result);
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            var order = await orderService.GetEntityByKeyAsync(orderId);
            if (order != null)
            {
                await Task.Run(() => orderService.DeleteEntityAsync(order));
                return true;
            }

            return false;
        }

    }
}
