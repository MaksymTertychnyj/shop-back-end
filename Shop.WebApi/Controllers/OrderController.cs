using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop.Data.Entities.Orders;
using Shop.Domain.Dto.Order;
using Shop.Domain.Helpers;
using Shop.Domain.Services.Interfaces;

namespace Shop.WebApi.Controllers
{
    [ApiController]
    [Route("order/")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)          
        {
            this.orderService = orderService;
        }

        [Authorize]
        [HttpGet("getAll")]
        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await orderService.GetAllOrdersAsync();
        }

        [Authorize]
        [HttpGet("getById/{orderId}")]
        public async Task<IActionResult> GetOrderById([FromRoute]int orderId)
        {
            var order = await orderService.GetOrderByIdAsync(orderId);

            return order != null ? Ok(order) : NotFound();
        }

        [Authorize]
        [HttpGet("getByCustomer/{customerLogin}")]
        public async Task<IActionResult> GetOrdersByCustomer([FromRoute]string customerLogin)
        {
            var orders = await orderService.GetOrdersByCustomerAsync(customerLogin);

            return orders != null ? Ok(orders) : NotFound();
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> ConfirmOrder([FromBody] OrderDto order)
        {
            var result = await orderService.ConfirmOrderAsync(order);

            return result != null ? Ok(result) : BadRequest(new { message = "confirming the order has been failed" });
        }

        [Authorize]
        [HttpDelete("delete/{orderId}")]
        public async Task<IActionResult> DeleteOrder([FromRoute]int orderId)
        {
            var result = await orderService.DeleteOrderAsync(orderId);
         
            return result ? Ok() : BadRequest(new { message = "deleting the order has been failed" });
        }

        //[Authorize]
        //[HttpPut("update")]
        //public async Task<IActionResult> UpdateOrderAsync([FromBody] Order order)
        //{
        //    var result = await orderService.UpdateEntityAsync(order, order.Id);
        //    if (result != null)
        //    {
        //        return Ok(result);
        //    }

        //    return BadRequest(new { message = "updating the order has been failed" });
        //}
    }
}
