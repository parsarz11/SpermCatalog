using SpermCatalog.API.models.DTOs.csvDTOs;
using SpermCatalog.API.models.DTOs.ResponseDTOs;

namespace SpermCatalog.API.Contracts
{
    public interface IDairyServices
    {
        void AddDairySperms(List<DairySpermCsvDTO> spermDTO);
        List<DairyResponseDTO> DairySpermListResponse();
    }
}
