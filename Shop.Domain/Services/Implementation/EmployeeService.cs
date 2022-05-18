using Shop.Data.Entities;
using Shop.Data.Infrastructure;
using Shop.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Services.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<User> repository;

        public EmployeeService(IRepository<User> repository)
        {
            this.repository = repository;
        }

        public async Task DeleteEmployeeAsync(User employee)
        {
            User? emp = await repository.GetByIdAsync(employee.Login);

            if (emp != null)
            {
                repository.Delete(employee);
                await repository.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<User>> GetAllEmployeesAsync()
        {
            return await repository.GetAllAsync();
        }

        public async Task<User> GetEmployeeByLoginAsync(string login)
        {
            User? user = await repository.GetByIdAsync(login);

            if (user != null)
            {
                return user;
            }

            return null!;
        }

        public Task UpdateEmployeeAsync(User employee)
        {
            throw new NotImplementedException();
        }
    }
}
