using Catalog.Data;
using Catalog.Repositories;
using Catalog.Repositories.Interfaces;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<CatalogContext>(options =>
     options.UseNpgsql(builder.Configuration.GetConnectionString("CatalogDb")));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(
    c => {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog.API", Version = "v1" });
    }
);

builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(
        c => {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog.API v1");
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
