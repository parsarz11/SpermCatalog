using SpermCatalog.API.models.DTOs.csvDTOs;
using SpermCatalog.API.models.DTOs.Filters;
using SpermCatalog.API.models.DTOs.ResponseDTOs;
using SpermCatalog.DataAccess.Entities;

namespace SpermCatalog.API.Contracts
{
    public interface IDairyServices
    {
        void AddRangeDairySperms(List<DairySperm> spermList);
        void AddDairySperm(DairySperm dairySperm);
        List<DairySperm> FilterDairySperms(DairyFilterDTO dairyFilterDTO);
        DairySperm FindSperm(string id);
        void UpdateDairySperm(DairySperm dairySperm);
        void DeleteSperm(string id);
        void DeleteAllSperms();

    }
}
