using SpermCatalog.API.models.DTOs;

namespace SpermCatalog.API.Contracts
{
    public interface IDairyServices
    {
        void AddDairySperms(List<DairySpermCsvDTO> spermDTO);
    }
}
