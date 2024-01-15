using SpermCatalog.API.models.DTOs;

namespace SpermCatalog.API.Contracts
{
    public interface IFileServices
    {
        List<DairySpermCsvDTO> DairyCsvReader(IFormFile file);
    }
}
