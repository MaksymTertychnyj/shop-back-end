using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shop.Data.Context;
using Shop.Data.Entities;
using Shop.Data.Entities.Orders;
using Shop.Data.Infrastructure;
using Shop.Domain.Dto;
using Shop.Domain.Mapping;
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
            Services.AddScoped<IRepository<Image>, Repository<Image>>();
            Services.AddScoped<IRepository<JsonModel>, Repository<JsonModel>>();
            Services.AddScoped<IRepository<Customer>, Repository<Customer>>();
            Services.AddScoped<IRepository<Order>, Repository<Order>>();
            Services.AddScoped<IRepository<OrderProduct>, Repository<OrderProduct>>();
            Services.AddScoped<IRepository<OrderAddress>, Repository<OrderAddress>>();
            Services.AddScoped<IEntityService<Category>, EntityService<Category>>();
            Services.AddScoped<IEntityService<Department>, EntityService<Department>>();
            Services.AddScoped<IEntityService<Product>, EntityService<Product>>();
            Services.AddScoped<IEntityService<JsonModel>, EntityService<JsonModel>>();
            Services.AddScoped<IEntityService<Order>, EntityService<Order>>();
            Services.AddScoped<IEntityService<OrderAddress>, EntityService<OrderAddress>>();
            Services.AddScoped<IEntityService<OrderProduct>, EntityService<OrderProduct>>();
            Services.AddScoped<IOrderService, OrderService>();
            Services.AddScoped<ILoginService<User, UserAuthenticateResponse>, LoginUserService>();
            Services.AddScoped<ILoginService<Customer, Domain.Dto.User.CustomerAuthenticateResponse>, LoginCustomerService>();
            Services.AddScoped<IEmployeeService, EmployeeService>();
            Services.AddScoped<IImageService, ImageService>();
        }
    }
}
