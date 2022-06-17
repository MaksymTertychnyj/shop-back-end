using MediatR;
using NovaPochta.Dto;

namespace NovaPochta.Infrastructure.MediatR.Addresses
{
    public class GetWarehousesRequest : IRequest<List<WarehouseDto>>
    {
        public string CityRef { get; set; } = string.Empty;

        public GetWarehousesRequest(string cityRef)
        {
            CityRef = cityRef;
        }
    }
}
