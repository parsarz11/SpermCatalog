using SpermCatalog.API.models.DTOs.csvDTOs;
using SpermCatalog.API.models.DTOs.Filters;
using SpermCatalog.API.models.DTOs.ResponseDTOs;
using SpermCatalog.DataAccess.Entities;

namespace SpermCatalog.API.Contracts
{
    public interface IDairyServices
    {
        void AddDairySperms(List<DairySpermCsvDTO> spermDTO);
        List<DairySperm> FilterDairySperms(DairyFilterDTO dairyFilterDTO);
        DairySperm FindSperm(int id);
        void UpdateDairySperm(DairySperm dairySperm);
        void DeleteSperm(int id);
        void DeleteAllSperms();

    }
}
