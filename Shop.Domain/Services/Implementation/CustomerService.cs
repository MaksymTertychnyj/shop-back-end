using Shop.Data.Entities;
using Shop.Data.Infrastructure;
using Shop.Domain.Dto.CustomerDto;
using Shop.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Services.Implementation
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _repository;

        public CustomerService(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Customer> GetCustomerByLoginAsync(string customerLogin)
        {
            var customer = await _repository.GetByIdAsync(customerLogin);
            return customer != null ? customer : null!;
        }

        public async Task<Customer> UpdateCustomerAsync(string customerLogin, CustomerUpdateDto data)
        {
            var customer = await _repository.GetByIdAsync(customerLogin);

            if (customer != null)
            {
                customer.FirstName = data.FirstName;
                customer.LastName = data.LastName;
                customer.Email = data.Email;
                customer.PhoneNumber = data.PhoneNumber;
                customer.Address = data.Address;

                try
                {
                    var result = await _repository.UpdateAsync(customer);
                    await _repository.SaveChangesAsync();

                    return result;
                }
                catch (Exception)
                {
                    return null!;
                }
            }

            return null!;
        }

        public async Task<bool> DeleteCustomerAsync(string customerLogin)
        {
            var customer = await _repository.GetByIdAsync(customerLogin);

            if (customer != null)
            {
                _repository.Delete(customer);
                await _repository.SaveChangesAsync();

                return true;
            }

            return false;
        }

    }
}
