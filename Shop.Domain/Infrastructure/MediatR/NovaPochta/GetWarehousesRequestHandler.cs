using MediatR;
using Shop.Domain.Dto.NovaPochta;
using Shop.Domain.Dto.NovaPochta.Enums;
using Shop.Domain.Services.Interfaces.NovaPochta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Shop.Domain.Infrastructure.MediatR.NovaPochta
{
    public class GetWarehousesRequestHandler : IRequestHandler<GetWarehousesRequest, List<WarehouseDto>>
    {
        private readonly IAddressService _addressService;

        public GetWarehousesRequestHandler(IAddressService addressService)
        {
            _addressService = addressService;
        }

        public async Task<List<WarehouseDto>> Handle(GetWarehousesRequest request, CancellationToken cancellationToken)
        {
            var req = new RequestDto
            {
                ModelName = ModelName.Address.ToString(),
                CalledMethod = CalledMethod.getWarehouses.ToString(),
                MethodProperties = new { CityRef = request.CityRef }
            };

            var response = await _addressService.FetchDataAsync(req);
            return JsonSerializer.Deserialize<ResponseDto<WarehouseDto>>(response)!.data;
        }
    }
}
