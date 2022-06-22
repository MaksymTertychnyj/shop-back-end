using AutoMapper;
using Shop.Data.Entities;
using Shop.Domain.Dto.CustomerDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Mapping
{
    public class CustomerMapper : Profile
    {
        public CustomerMapper()
        {
            CreateMap<Customer, CustomerUpdateDto>()
                .ReverseMap();
        }
    }
}
