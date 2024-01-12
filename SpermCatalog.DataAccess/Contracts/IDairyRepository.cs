using SpermCatalog.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpermCatalog.DataAccess.Contracts
{
    public interface IDairyRepository
    {
        Task<List<DairySperm>> GetDairySpermsAsync();
        Task AddDairySpermsListAsync(List<DairySperm> dairySperms);
        Task<DairySperm> FindDairySpermAsync(int id);
        void UpdateDairySperms(DairySperm dairySperm);
        Task DeleteAllDairySpermsAsync();
        void DeleteDairySperm(int id);
    }
}
