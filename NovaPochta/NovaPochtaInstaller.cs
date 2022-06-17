using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NovaPochta.Infrastructure;

namespace NovaPochta
{
    public static class NovaPochtaInstaller
    {
        public static void AddNovaPochtaServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructureServices(configuration);
        }
    }
}
