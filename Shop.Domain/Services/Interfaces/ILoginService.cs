using Microsoft.AspNetCore.Authorization;
using Shop.Data.Entities;
using Shop.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Services.Interfaces
{
    public interface ILoginService
    {
        Task<UserAuthenticateResponse> Authenticate(UserAuthenticateRequest request);

        Task<User> Register(User user);
    }
}
