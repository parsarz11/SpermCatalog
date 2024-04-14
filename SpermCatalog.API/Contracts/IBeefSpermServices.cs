using SpermCatalog.API.models.DTOs.csvDTOs;
using SpermCatalog.API.models.DTOs.Filters;
using SpermCatalog.API.models.DTOs.ResponseDTOs;
using SpermCatalog.DataAccess.Entities;

namespace SpermCatalog.API.Contracts
{
    public interface IBeefSpermServices
    {
        void AddRangeBeefSperms(List<BeefSperm> spermList);
        void AddBeefSperm(BeefSperm beefSperm);
        List<BeefSperm> FilterBeefSperms(BeefFilterDTO beefFilterDTO);
        BeefSperm FindSperm(string id);
        void UpdateBeefSperm(BeefSperm beefSperm);
        void DeleteSperm(string id);
        void DeleteAllSperms();
    }
}
