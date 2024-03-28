using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UserCard.API.Data;
using UserCard.API.Repositories;
using UserCard.API.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddDbContext<CardsContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("UsercardDb")));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(
    c => {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "UserCard.API", Version = "v1" });
    }
);

builder.Services.AddScoped<ICardsRepository, CardsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(
        c => {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserCard.API v1");
        }
    );
}

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    //endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
    //{
    //    Predicate = _ => true,
    //    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    //});
});

app.Run();