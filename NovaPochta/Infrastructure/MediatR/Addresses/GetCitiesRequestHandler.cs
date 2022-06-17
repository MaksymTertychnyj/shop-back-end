using MediatR;
using NovaPochta.Dto;
using NovaPochta.Dto.Enums;
using NovaPochta.Services.Interfaces;
using System.Text.Json;

namespace NovaPochta.Infrastructure.MediatR.Addresses
{
    public class GetCitiesRequestHandler : IRequestHandler<GetCitiesRequest, List<CityDto>>
    {
        private readonly IAddressService _addressService;

        public GetCitiesRequestHandler(IAddressService addressService)
        {
            _addressService = addressService;
        }

        public async Task<List<CityDto>> Handle(GetCitiesRequest request, CancellationToken cancellationToken)
        {
            var req = new RequestDto
            {
                ModelName = ModelName.Address.ToString(),
                CalledMethod = CalledMethod.getCities.ToString(),
                MethodProperties = new { AreaRef = request.AreaRef, Warehouse = "1" }
            };

            var response = await _addressService.FetchDataAsync(req);
            return JsonSerializer.Deserialize<ResponseDto<CityDto>>(response)!.data;
        }
    }
}
