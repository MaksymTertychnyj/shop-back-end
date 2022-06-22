using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shop.Data.Entities;
using Shop.Data.Enums;
using Shop.Data.Infrastructure;
using Shop.Domain.Dto;
using Shop.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace Shop.Domain.Helpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IConfiguration configuration;

        public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            this.next = next;
            this.configuration = configuration;
        }

        public async Task Invoke(HttpContext context, IRepository<User> userRepository, IRepository<Customer> customerRepository)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var visitor = context.Request.Headers["Visitor"];

            if (token != null)
            {
                if (visitor == "employee")
                {
                    AttachUserToContext(context, userRepository, token);
                }
                if (visitor == "customer")
                {
                    AttachUserToContext(context, customerRepository, token);
                }
            }

            await next(context);
        }

        public void AttachUserToContext<TEntity>(HttpContext context, IRepository<TEntity> repository, string token)
            where TEntity : class
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userLogin = jwtToken.Claims.First(u => u.Type == "Login").Value;
#pragma warning disable CA2012
                var user = repository.GetByIdAsync(userLogin).Result;
#pragma warning restore CA2012
                if (user != null)
                {
                    //user.Password = String.Empty;
                    context.Items["User"] = user;
                    repository.Detach(user);
                }
            }
            catch 
            {
            }
        }
    }
}
