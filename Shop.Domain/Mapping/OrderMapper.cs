using AutoMapper;
using Shop.Data.Entities.Orders;
using Shop.Domain.Dto.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Mapping
{
    public class OrderMapper : Profile
    {
        public OrderMapper()
        {
            CreateMap<Order, OrderDto>()
                .ReverseMap();

            CreateMap<OrderAddressDto, OrderAddress>()
                .ReverseMap();

            CreateMap<OrderProductDto, OrderProduct>()
                .ReverseMap();
        }
    }
}
