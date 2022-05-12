using AutoMapper;
using Microsoft.Extensions.Configuration;
using Shop.Data.Entities;
using Shop.Data.Infrastructure;
using Shop.Domain.Dto;
using Shop.Domain.Services.Interfaces;
using Shop.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Services.Implementation
{
    public class LoginService : ILoginService
    {
        private readonly IRepository<User> repository;
        private readonly IConfiguration configuration;

        public LoginService(IRepository<User> repository, IConfiguration configuration)
        {
            this.repository = repository;
            this.configuration = configuration;
        }

        public UserAuthenticateResponse Authenticate(UserAuthenticateRequest request)
        {
            var user = repository.GetByIdAsync(request.Login).Result;

            if (user != null && user.Password == request.Password)
            {
                var token = configuration.GenerateJwtToken(user);

                return new UserAuthenticateResponse(user, token);
            }

            return null!;
        }


        public async Task<UserAuthenticateResponse> Register(User user)
        {
            var addedUser = await repository.AddAsync(user);

            await repository.SaveChangesAsync();

            var response = Authenticate(new UserAuthenticateRequest
            {
                Login = user.Login,
                Password = user.Password
            });

            return response;
        }
    }
}
