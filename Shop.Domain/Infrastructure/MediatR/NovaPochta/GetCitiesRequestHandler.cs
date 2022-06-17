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
