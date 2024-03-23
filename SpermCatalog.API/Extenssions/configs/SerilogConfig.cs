using Serilog;

namespace SpermCatalog.API.Extenssions.configs
{
    public static class SerilogConfig
    {
        public static IServiceCollection AddSerilog(this IServiceCollection services,IConfiguration configuration,ILoggingBuilder loggingBuilder)
        {
           var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.Console()
                .CreateLogger();

            loggingBuilder.ClearProviders();
            loggingBuilder.AddConsole();
            loggingBuilder.AddSerilog(logger);

            return services;
        }
    }
}
