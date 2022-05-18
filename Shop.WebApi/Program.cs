using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shop.Data.Context;
using Shop.Data.Entities;
using Shop.Data.Infrastructure;
using Shop.Domain.Helpers;
using Shop.Domain.Services.Implementation;
using Shop.Domain.Services.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthorization();

builder.Services.AddCors();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ShopContext>(options => 
            options.UseSqlServer(builder.Configuration
            .GetConnectionString("DefaultConnection"))
            );

builder.Services.AddScoped<IRepository<User>, Repository<User>>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseMiddleware<JwtMiddleware>();
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
