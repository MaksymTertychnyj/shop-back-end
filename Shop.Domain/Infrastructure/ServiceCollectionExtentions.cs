using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Domain.Helpers;
using Shop.Domain.Services.Implementation.NovaPochta;
using Shop.Domain.Services.Interfaces.NovaPochta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Infrastructure
{
    public static class ServiceCollectionExtentions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(typeof(ServiceCollectionExtentions));
            services.AddHttpClient<IAddressService, AddressService>(client =>
            {
                client.BaseAddress = new Uri(configuration["NovaPochta:BaseAddress"]);
                client.SetApiKey(configuration["NovaPochta:ApiKey"]);
            });

            return services;
        }
    }
}
