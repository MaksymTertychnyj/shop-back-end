using Shop.Domain.Dto.NovaPochta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Services.Interfaces.NovaPochta
{
    public interface IAddressService
    {
        public Task<string> FetchDataAsync(RequestDto requestData);
    }
}
