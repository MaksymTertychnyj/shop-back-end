using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using NovaPochta.Mapping;
using NovaPochta.Services.Interfaces;
using NovaPochta.Services.Implementation;
using NovaPochta.Helpers;

namespace NovaPochta.Infrastructure
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
