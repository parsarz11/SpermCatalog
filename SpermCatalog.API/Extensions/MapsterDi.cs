using Mapster;
using MapsterMapper;
using System.Reflection;

namespace SpermCatalog.API.Extenssions
{
    public static class MapsterDi
    {
        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            services.AddSingleton(config.Scan(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}
