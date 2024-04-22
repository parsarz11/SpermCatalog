using SpermCatalog.DataAccess.DatabaseContext;
using SpermCatalog.API.Extenssions;
using SpermCatalog.API.MiddleWares;
using SpermCatalog.API.Extenssions.configs;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.Configure<MongoDbConfigurations>(builder.Configuration.GetSection("MongoDbConfigurations"));

builder.Services.AddDependencies();
builder.Services.AddMapping();
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
