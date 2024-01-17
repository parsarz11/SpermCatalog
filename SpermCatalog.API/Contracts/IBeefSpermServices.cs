using SpermCatalog.API.models.DTOs.csvDTOs;
using SpermCatalog.API.models.DTOs.Filters;
using SpermCatalog.API.models.DTOs.ResponseDTOs;
using SpermCatalog.DataAccess.Entities;

namespace SpermCatalog.API.Contracts
{
    public interface IBeefSpermServices
    {
        void AddBeefSperms(List<BeefSpermCsvDTO> spermDTO);
        List<BeefSperm> FilterBeefSperms(BeefFilterDTO beefFilterDTO);
        BeefSperm FindSperm(int id);
        void UpdateBeefSperm(BeefSperm beefSperm);
        void DeleteSperm(int id);
        void DeleteAllSperms();
    }
}
