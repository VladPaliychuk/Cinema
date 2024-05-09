using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using User.BLL.Configurations;
using User.BLL.Services;
using User.BLL.Services.Interfaces;
using User.DAL.Data;
using User.DAL.Repositories;
using User.DAL.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<UserContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("UserDb")));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(
    c => {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "User.API", Version = "v1" });
    }
);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMyOrigin",
        builder => builder.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader());
});



var app = builder.Build();

app.UseCors("AllowMyOrigin");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
        c => {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "User.API v1");
        }
    );
}
app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();
