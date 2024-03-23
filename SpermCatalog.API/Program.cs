using Microsoft.EntityFrameworkCore;
using SpermCatalog.DataAccess.DatabaseContext;
using SpermCatalog.API.Extenssions;
using SpermCatalog.API.MiddleWares;
using SpermCatalog.API.Extenssions.configs;
using Serilog;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddDbContext<SpermCatalogDbContext>(config =>
    {
        config.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
    }
);

builder.Services.AddDependencies();

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandlerMiddleWare();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
