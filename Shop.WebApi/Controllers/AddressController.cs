using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Dto.NovaPochta;
using Shop.Domain.Dto.NovaPochta.Enums;
using Shop.Domain.Services.Interfaces.NovaPochta;
using System.Text.Json;

namespace Shop.WebApi.Controllers
{
    [ApiController]
    [Route("address/")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet("getAreas")]
        public async Task<IActionResult> GetAreas()
        {
            var request = new RequestDto
            {
                modelName = "Address",
                calledMethod = "getAreas",
                methodProperties = null
            };
            var response = await _addressService.FetchDataAsync(request);
            
            return Ok(JsonSerializer.Deserialize<AreaResponseDto<AreaDto>>(response)!.data);
        }
    }
}
