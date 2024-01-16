using SpermCatalog.API.models.DTOs.Filters;
using SpermCatalog.API.models.DTOs.ResponseDTOs;

namespace SpermCatalog.API.Contracts
{
    public interface IFilterServices
    {
        List<DairyResponseDTO> FilterDairySperms(DairyFilterDTO dairyFilterDTO);
        List<BeefResponseDTO> FilterBeefSperms(BeefFilterDTO beefFilterDTO);
    }
}
