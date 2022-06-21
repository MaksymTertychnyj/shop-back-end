namespace Shop.Domain.Dto.Order
{
    public class OrderAddressDto
    {
        public string Country { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Place { get; set; } = string.Empty;
    }
}
