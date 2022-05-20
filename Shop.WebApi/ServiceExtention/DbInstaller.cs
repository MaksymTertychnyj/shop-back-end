using Microsoft.EntityFrameworkCore;
using Shop.Data.Context;
using Shop.Data.Entities;
using Shop.Data.Infrastructure;
using Shop.Domain.Services.Implementation;
using Shop.Domain.Services.Interfaces;

namespace Shop.WebApi.ServiceExtention
{
    public static class DbInstaller
    {
        public static void AddDbInstaller(this IServiceCollection Services, IConfiguration Configuration)
        {
            Services.AddDbContext<ShopContext>(options =>
            options.UseSqlServer(Configuration
            .GetConnectionString("DefaultConnection"))
            );

            Services.AddScoped<IRepository<User>, Repository<User>>();
            Services.AddScoped<IRepository<Category>, Repository<Category>>();
            Services.AddScoped<IRepository<Department>, Repository<Department>>();
            Services.AddScoped<IRepository<Product>, Repository<Product>>();
            Services.AddScoped<IEntityService<Category>, EntityService<Category>>();
            Services.AddScoped<IEntityService<Department>, EntityService<Department>>();
            Services.AddScoped<IEntityService<Product>, EntityService<Product>>();
            Services.AddScoped<ILoginService, LoginService>();
            Services.AddScoped<IEmployeeService, EmployeeService>();
        }
    }
}
