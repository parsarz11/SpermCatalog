using SpermCatalog.API.models;
using SpermCatalog.API.models.DTOs.csvDTOs;
using SpermCatalog.API.models.DTOs.Filters;
using SpermCatalog.API.models.DTOs.ResponseDTOs;
using SpermCatalog.DataAccess.Entities;
using static SpermCatalog.API.models.TimeSelectionModel;

namespace SpermCatalog.API.Contracts
{
    public interface IBeefSpermServices
    {
        Task AddRangeBeefSpermsAsync(List<BeefSperm> spermList);
        Task AddBeefSpermAsync(BeefSperm beefSperm);
        Task<List<BeefSperm>> FilterBeefSpermsAsync(BeefFilterDTO beefFilterDTO);
        Task<BeefSperm> FindSpermAsync(string id);
        Task UpdateBeefSpermAsync(BeefSperm beefSperm);
        Task DeleteSpermAsync(string id);
        Task DeleteAllSpermsAsync();
        Task<List<RangeFilter>> GetRangeFiltersAsync(string? category);
        Task<List<RangeFilterCountModel>> CalculateRangeFilterSearchCountAsync(TimeSelectionEnum timeSelection, string category);
        Task<List<AvgRangeFilterModel>> CalculateRangeFilterAvgAsync(TimeSelectionEnum timeSelection, string category);
        
    }
}
