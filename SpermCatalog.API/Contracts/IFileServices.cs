using SpermCatalog.API.models.DTOs;

namespace SpermCatalog.API.Contracts
{
    public interface IFileServices
    {
        void DairyCsvReader(IFormFile file);
        void BeefCsvReader(IFormFile file);
    }
}
