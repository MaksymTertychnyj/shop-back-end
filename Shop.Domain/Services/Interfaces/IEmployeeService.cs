using Shop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<User>> GetAllEmployeesAsync();
        Task<User> GetEmployeeByLoginAsync(string login);
        Task UpdateEmployeeAsync(User employee);
        Task DeleteEmployeeAsync(User employee);
    }
}
