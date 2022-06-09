using Shop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Dto.User
{
    public class CustomerAuthenticateResponse
    {
        public Customer Customer { get; set; }
        public string Token { get; set; } = string.Empty;

        public CustomerAuthenticateResponse(Customer customer, string token)
        {
            Customer = customer;
            Customer.Password = "";
            Token = token;
        }
    }
}
