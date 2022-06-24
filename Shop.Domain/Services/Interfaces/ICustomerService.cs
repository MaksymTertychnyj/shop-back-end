using Shop.Data.Entities;
using Shop.Domain.Dto.CustomerDto;

namespace Shop.Domain.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByLoginAsync(string customerLogin);
        Task<Customer> UpdateCustomerAsync(string customerLogin, CustomerUpdateDto data);
        Task<bool> DeleteCustomerAsync(string customerLogin);
    }
}
