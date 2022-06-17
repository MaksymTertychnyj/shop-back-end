using AutoMapper;
using NovaPochta.Dto;

namespace NovaPochta.Mapping
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
