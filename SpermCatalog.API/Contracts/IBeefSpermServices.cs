using SpermCatalog.API.models.DTOs.csvDTOs;
using SpermCatalog.API.models.DTOs.ResponseDTOs;

namespace SpermCatalog.API.Contracts
{
    public interface IBeefSpermServices
    {
        void AddBeefSperms(List<BeefSpermCsvDTO> spermDTO);
        List<BeefResponseDTO> BeefSpermListResponse();
    }
}
