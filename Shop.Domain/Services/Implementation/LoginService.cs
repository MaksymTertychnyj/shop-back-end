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
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

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

        public async Task<UserAuthenticateResponse> Authenticate(UserAuthenticateRequest request)
        {
            User? user = await repository.GetByIdAsync(request.Login);

            if (user != null)
            {
                string decryptedPassword = ToHash(request.Password);

                if (user.Password == decryptedPassword)
                {
                    var token = configuration.GenerateJwtToken(user);
                    return new UserAuthenticateResponse(user, token);
                }
            }

            return null!;
        }


        public async Task<User> Register(User user)
        {
            User? checkedUser = await repository.GetByIdAsync(user.Login);

            if (checkedUser != null)
                return null!;

            string userPassword = user.Password;
            
            user.Password = ToHash(userPassword);

            await repository.AddAsync(user);
            await repository.SaveChangesAsync();

            user.Password = "";
            
            return user;
        }

        private string ToHash(string key)
        {
            string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: key,
                salt: Encoding.ASCII.GetBytes(configuration["Jwt:Key"]),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100,
                numBytesRequested: 256 / 8));

            return hash;
        }
    }
}
