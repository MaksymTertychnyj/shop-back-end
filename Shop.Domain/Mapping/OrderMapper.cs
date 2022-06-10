using AutoMapper;
using Shop.Data.Entities.Orders;
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
            //CreateMap<OrderDTO, Order>()
            //    .ForMember(
            //        dest => dest.Status,
            //        opt => opt.MapFrom(src => src.Status)
            //     )
            //    .ForMember(
            //        dest => dest.OrderAddress,
            //        opt => opt.MapFrom(src => src.OrderAddress)
            //    )
            //    .ForMember(
            //        dest => dest.CustomerLogin,
            //        opt => opt.MapFrom(src => src.CustomerLogin)
            //    )
            //    .ForMember(
            //        dest => dest.Products,
            //        opt => opt.MapFrom(src => src.Products)
            //    );
        }
    }
}
