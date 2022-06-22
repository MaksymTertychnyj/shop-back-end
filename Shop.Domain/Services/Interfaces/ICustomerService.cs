using Shop.Data.Entities;
using Shop.Domain.Dto.CustomerDto;

namespace Shop.Domain.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<Customer> UpdateCustomer(string customerLogin, CustomerUpdateDto data);
    }
}
