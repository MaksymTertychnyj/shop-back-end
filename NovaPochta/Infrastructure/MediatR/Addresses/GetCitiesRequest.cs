using MediatR;
using NovaPochta.Dto;

namespace NovaPochta.Infrastructure.MediatR.Addresses
{
    public class GetCitiesRequest : IRequest<List<CityDto>>
    {
        public string AreaRef { get; set; } = string.Empty;

        public GetCitiesRequest(string areaRef)
        {
            AreaRef = areaRef;
        }
    }
}
