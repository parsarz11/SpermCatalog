using SpermCatalog.API.models;
using SpermCatalog.API.models.DTOs.csvDTOs;
using SpermCatalog.API.models.DTOs.Filters;
using SpermCatalog.API.models.DTOs.ResponseDTOs;
using SpermCatalog.DataAccess.Entities;
using static SpermCatalog.API.models.TimeSelectionModel;

namespace SpermCatalog.API.Contracts
{
    public interface IDairyServices
    {
        Task AddRangeDairySpermsAsync(List<DairySperm> spermList);
        Task AddDairySpermAsync(DairySperm dairySperm);
        Task<List<DairySperm>> FilterDairySpermsAsync(DairyFilterDTO dairyFilterDTO);
        Task<DairySperm> FindSpermAsync(string id);
        Task UpdateDairySpermAsync(DairySperm dairySperm);
        Task DeleteSpermAsync(string id);
        Task DeleteAllSpermsAsync();
        Task<List<RangeFilter>> GetRangeFiltersAsync(string? category);
        Task<List<RangeFilterCountModel>> CalculateRangeFilterSearchCountAsync(TimeSelectionEnum timeSelection);
        Task<List<RangeFilterCountModel>> CalculateRangeFilterSearchCountAsync(TimeSelectionEnum timeSelection, string category);
        Task<List<AvgRangeFilterModel>> CalculateRangeFilterAvgAsync(TimeSelectionEnum timeSelection, string category);
    }
}
