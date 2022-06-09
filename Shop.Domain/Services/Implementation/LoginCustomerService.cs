using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Shop.Data.Entities;
using Shop.Data.Infrastructure;
using Shop.Domain.Dto;
using Shop.Domain.Dto.User;
using Shop.Domain.Helpers;
using Shop.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Services.Implementation
{
    public class LoginCustomerService : ILoginService<Customer, CustomerAuthenticateResponse>
    {
        private readonly IRepository<Customer> repository;
        private readonly IConfiguration configuration;

        public LoginCustomerService(IRepository<Customer> repository, IConfiguration configuration)
        {
            this.repository = repository;
            this.configuration = configuration;
        }

        public async Task<CustomerAuthenticateResponse> Authenticate(AuthenticateRequest request)
        {
            Customer? customer = await repository.GetByIdAsync(request.Login);

            if (customer != null)
            {
                string decryptedPassword = ToHash(request.Password);

                if (customer.Password == decryptedPassword)
                {
                    var token = configuration.GenerateJwtToken(customer.Login);
                    return new CustomerAuthenticateResponse(customer, token);
                }
            }

            return null!;
        }

        public async Task<Customer> Register(Customer customer)
        {
            Customer? checkedUser = await repository.GetByIdAsync(customer.Login);

            if (checkedUser != null)
                return null!;

            string customerPassword = customer.Password;

            customer.Password = ToHash(customerPassword);

            await repository.AddAsync(customer);
            await repository.SaveChangesAsync();

            customer.Password = "";

            return customer;
        }

        private string ToHash(string key)
        {
            string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: key,
                salt: Encoding.ASCII.GetBytes(configuration["Jwt:Key"]),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100,
                numBytesRequested: 256 / 8));

            return hash;
        }
    }
}
