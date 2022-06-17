using MediatR;
using NovaPochta.Dto;
using NovaPochta.Dto.Enums;
using NovaPochta.Services.Interfaces;
using System.Text.Json;

namespace NovaPochta.Infrastructure.MediatR.Addresses
{
    public class GetAreasRequestHandler : IRequestHandler<GetAreasRequest, List<AreaDto>>
    {
        private readonly IAddressService _addressService;

        public GetAreasRequestHandler(IAddressService addressService)
        {
            _addressService = addressService;
        }

        public async Task<List<AreaDto>> Handle(GetAreasRequest request, CancellationToken cancellationToken)
        {
            var req = new RequestDto
            {
                ModelName = ModelName.Address.ToString(),
                CalledMethod = CalledMethod.getAreas.ToString(),
                MethodProperties = null
            };

            var response = await _addressService.FetchDataAsync(req);
            return JsonSerializer.Deserialize<ResponseDto<AreaDto>>(response)!.data;
        }
    }
}
