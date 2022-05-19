using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration configuration;

        public EmployeeService(IRepository<User> repository, IConfiguration configuration)
        {
            this.repository = repository;
            this.configuration = configuration;
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

        public async Task UpdateEmployeeAsync(User employee)
        {
            User? user = await repository.GetByIdAsync(employee.Login);

            if (user != null)
            {
                if (employee.Password == "")
                {
                    employee.Password = user.Password;
                }
                else
                {
                    var password = ToHash(employee.Password);
                    employee.Password = password;
                }

                repository.Delete(user);
                await repository.UpdateAsync(employee);
                await repository.SaveChangesAsync();
            }
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
