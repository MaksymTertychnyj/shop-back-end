using AutoMapper;
using Nancy.Json;
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
        private readonly IMapper _mapper;

        public AddressService(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
        }

        public async Task<string> FetchDataAsync(RequestDto requestData)
        {
            var request = _mapper.Map<Request>(requestData);
            request.apiKey = _httpClient.GetApiKey();

            var serializerOptions = new JsonSerializerOptions()
            {
                IncludeFields = true,
            };

            try
            {
                 var response = await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress, request, serializerOptions);
                 return await response.Content.ReadAsStringAsync();
            }
            catch (Exception)
            {
                return null!;    
            }
        }
    }
}
