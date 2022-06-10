using Microsoft.AspNetCore.Mvc;
using Shop.Data.Entities.Orders;
using Shop.Domain.Dto;
using Shop.Domain.Helpers;
using Shop.Domain.Services.Interfaces;

namespace Shop.WebApi.Controllers
{
    [ApiController]
    [Route("order/")]
    public class OrderController : ControllerBase
    {
        private readonly IEntityService<Order> orderService;
        private readonly IEntityService<OrderAddress> addressService;
        private readonly IEntityService<OrderProduct> productService;

        public OrderController(
            IEntityService<Order> orderService,
            IEntityService<OrderAddress> addressService, 
            IEntityService<OrderProduct> productService
            )          
        {
            this.orderService = orderService;
            this.addressService = addressService;
            this.productService = productService;
        }

        [Authorize]
        [HttpGet("getAll")]
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            await addressService.GetAllEntitiesAsync();
            await productService.GetAllEntitiesAsync();

            return await orderService.GetAllEntitiesAsync();
        }

        [Authorize]
        [HttpGet("getById/{orderId}")]
        public async Task<IActionResult> GetOrderByIdAsync([FromRoute] int orderId)
        {
            var result = await orderService.GetWithIncludedEntities(
                o => o.Id == orderId,
                o => o.OrderAddress!,
                o => o.Products
               );
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [Authorize]
        [HttpGet("getByCustomer/{customerLogin}")]
        public async Task<IActionResult> GetOrdersByCustomer([FromRoute] string customerLogin)
        {
            var result = await orderService.GetWithIncludedEntities(
                o => o.CustomerLogin == customerLogin, 
                o => o.OrderAddress!, 
                o => o.Products
               );
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> ConfirmOrderAsync([FromBody] Order order)
        {
            var orderObj = await orderService.AddEntityAsync(order);

            if (orderObj == null)
            {
                return BadRequest(new { message = "confirming the order has been failed" });
            }

            return Ok();
        }

        [Authorize]
        [HttpDelete("delete/{orderId}")]
        public async Task<IActionResult> DeleteOrderAsync([FromRoute] int orderId)
        {
            var order = await orderService.GetEntityByKeyAsync(orderId);
            if (order != null)
            {
                await Task.Run(() => orderService.DeleteEntityAsync(order));
                return Ok();
            }

            return BadRequest(new { message = "deleting the order has been failed" });
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateOrderAsync([FromBody] Order order)
        {
            var result = await orderService.UpdateEntityAsync(order, order.Id);
            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest(new { message = "updating the order has been failed" });
        }
    }
}
