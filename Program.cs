using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Para escuchar en todas las interfaces de red
builder.WebHost.UseUrls("http://0.0.0.0:5115");

// Agregar servicios de controladores
builder.Services.AddControllers();

// Opcional: agregar OpenAPI / Swagger para documentación
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Mapear rutas para controladores
app.MapControllers();

app.Run();
