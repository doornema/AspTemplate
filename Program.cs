using template.Data;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using template.Middleware;
using System.Globalization;
using template.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IConfiguration configuration = builder.Configuration;
builder.Services.AddControllers();
Console.WriteLine(configuration.GetConnectionString("Default"));
builder.Services.AddDbContext<AppDbContext>(o => o.UseNpgsql(configuration.GetConnectionString("Default")));

builder.Services.Configure<JsonOptions>(x => x.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddExceptionHandler<DefaultExceptionHandler>();
var app = builder.Build();

// Configure the HTTP request pipeline.
//app.Run(async context =>
//{
//    Console.WriteLine("call middleware");
//    await context.Response.WriteAsync("Hellow worls");
//});

app.UseAuthJwtMiddleware();
app.UseAuthorization();

app.MapControllers();

app.Run();
