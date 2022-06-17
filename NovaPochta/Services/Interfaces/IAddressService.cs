using NovaPochta.Dto;

namespace NovaPochta.Services.Interfaces
{
    public interface IAddressService
    {
        public Task<string> FetchDataAsync(RequestDto requestData);
    }
}
