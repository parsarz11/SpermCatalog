using MapsterMapper;
using SpermCatalog.API.Contracts;
using SpermCatalog.API.Services.BeefSpermServices;
using SpermCatalog.API.Services.DiarySpermServices;
using SpermCatalog.API.Services.FileServices;
using SpermCatalog.DataAccess.Contracts;
using SpermCatalog.DataAccess.DatabaseContext;
using SpermCatalog.DataAccess.Repositories;

namespace SpermCatalog.API.Extenssions
{
    public static class Dependencies
    {
        public static IServiceCollection AddDependencies(this IServiceCollection Services)
        {
            Services.AddSingleton<ISpermCatalogDbContext, SpermCatalogDbContext>();

            Services.AddScoped<IDairyRepository , DairySpermRepository>();
            Services.AddScoped<IBeefRepository , BeefSpermRepository>();

            Services.AddScoped<IDairyServices, DairySpermServices>();
            Services.AddScoped<IFileServices, FileServices>();
            Services.AddScoped<IBeefSpermServices, BeefSpermServices>();
            Services.AddScoped<IMapper, Mapper>();
            return Services;
        }
    }
}
