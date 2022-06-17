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
