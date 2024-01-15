using SpermCatalog.API.models.DTOs;

namespace SpermCatalog.API.Contracts
{
    public interface IBeefSpermServices
    {
        void AddBeefSperms(List<BeefSpermCsvDTO> spermDTO);
    }
}
