using SpermCatalog.DataAccess.Contracts;
using SpermCatalog.DataAccess.Repositories;

namespace SpermCatalog.API.Extenssions
{
    public static class Dependencies
    {
        public static IServiceCollection AddDependencies(this IServiceCollection Services)
        {
            Services.AddScoped<IDairyRepository , DairySpermRepository>();
            Services.AddScoped<IBeefRepository , BeefSpermRepository>();

            return Services;
        }
    }
}
