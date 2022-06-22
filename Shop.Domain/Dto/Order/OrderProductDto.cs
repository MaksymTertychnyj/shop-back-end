﻿namespace Shop.Domain.Dto.Order
{
    public class OrderProductDto
    {
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string JsonParameters { get; set; } = string.Empty;
    }
}
