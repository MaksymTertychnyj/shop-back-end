using Shop.Domain.Helpers;
using Shop.Domain.Services.Implementation.NovaPochta;
using Shop.Domain.Services.Interfaces.NovaPochta;

namespace Shop.WebApi.ServiceExtention
{
    public static class NovaPochtaClient
    {
        public static void AddNovaPochtaClient(this IServiceCollection Services, IConfiguration Configuration)
        {
            Services.AddHttpClient<IAddressService, AddressService>(client =>
            {
                client.BaseAddress = new Uri(Configuration["NovaPochta:BaseAddress"]);
                client.SetApiKey(Configuration["NovaPochta:ApiKey"]);
            });
        }
    }
}
