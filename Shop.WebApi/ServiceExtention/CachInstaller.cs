using Shop.Data.Entities;
using Shop.Domain.Services.Implementation;
using Shop.Domain.Services.Interfaces;

namespace Shop.WebApi.ServiceExtention
{
    public static class CachInstaller
    {
        public static void AddCacheInstaller(this IServiceCollection Services)
        {
            Services.AddScoped<ICacheService<Department>, CacheService<Department>>();
            Services.AddScoped<ICacheService<Category>, CacheService<Category>>();
            Services.AddScoped<ICacheService<Product>, CacheService<Product>>();
        }
    }
}
