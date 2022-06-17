using MediatR;
using NovaPochta.Dto;
using NovaPochta.Dto.Enums;
using NovaPochta.Services.Interfaces;
using System.Text.Json;

namespace NovaPochta.Infrastructure.MediatR.Addresses
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
