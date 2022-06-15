using Shop.Domain.Dto.NovaPochta;
using Shop.Domain.Helpers;
using Shop.Domain.Services.Interfaces.NovaPochta;
using System.Net.Http.Json;
using System.Text.Json;

namespace Shop.Domain.Services.Implementation.NovaPochta
{
    public class AddressService : IAddressService
    {
        private readonly HttpClient _httpClient;

        public AddressService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> FetchDataAsync(RequestDto requestData)
        {
            var request = new {
                apiKey = _httpClient.GetApiKey(),
                modelName = requestData.modelName,
                calledMethod = requestData.calledMethod,
                methodProperties = requestData.methodProperties
            };

            try
            {
                var response = await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress, request);
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception)
            {
                return null!;    
            }
        }
    }
}
