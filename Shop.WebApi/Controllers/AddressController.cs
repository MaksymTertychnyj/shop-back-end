using MediatR;
using Microsoft.AspNetCore.Mvc;
using NovaPochta.Infrastructure.MediatR.Addresses;

namespace Shop.WebApi.Controllers
{
    [ApiController]
    [Route("address/")]
    public class AddressController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AddressController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("getAreas")]
        public async Task<IActionResult> GetAreas()
        {
            var areas = await _mediator.Send(new GetAreasRequest());
            return Ok(areas);
        }

        [HttpGet("getCities/{areaRef}")]
        public async Task<IActionResult> GetCities([FromRoute] string areaRef)
        {
            var cities = await _mediator.Send(new GetCitiesRequest(areaRef));
            return Ok(cities);
        }

        [HttpGet("getWarehouses/{cityRef}")]
        public async Task<IActionResult> GetWarehouses(string cityRef)
        {
            var warehouses = await _mediator.Send(new GetWarehousesRequest(cityRef));
            return Ok(warehouses);
        }
    }
}
