using MediatR;
using Shop.Domain.Dto.NovaPochta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Infrastructure.MediatR.NovaPochta
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
