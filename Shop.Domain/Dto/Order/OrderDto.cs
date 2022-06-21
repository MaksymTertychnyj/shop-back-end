using Shop.Data.Enums;

namespace Shop.Domain.Dto.Order
{
    public class OrderDto
    {
        public OrderStatus Status { get; set; }
        public OrderAddressDto? OrderAddress { get; set; }
        public string CustomerLogin { get; set; } = string.Empty;
        public List<OrderProductDto> Products { get; set; } = new List<OrderProductDto>();
    }
}
