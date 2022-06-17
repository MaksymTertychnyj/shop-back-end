using AutoMapper;
using Shop.Domain.Dto.NovaPochta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Mapping.NovaPochta
{
    public class MapRequestProfile : Profile
    {
        public MapRequestProfile()
        {
            CreateMap<Request, RequestDto>()
                .ReverseMap();
        }
    }
}
