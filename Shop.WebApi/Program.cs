using Shop.Domain.Helpers;
using Shop.WebApi.ServiceExtention;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddNewtonsoftJson(options => 
        options.SerializerSettings
        .ReferenceLoopHandling = Newtonsoft
        .Json.ReferenceLoopHandling.Ignore
     );
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthorization();
builder.Services.AddCors();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMemoryCache();
builder.Services.AddCacheInstaller();

builder.Services.AddDbInstaller(builder.Configuration);
builder.Services.AddSwaggerInstaller();

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
app.UseEndpoints(endpoints => endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller}/action/{key?}"
    ));

app.Run();
