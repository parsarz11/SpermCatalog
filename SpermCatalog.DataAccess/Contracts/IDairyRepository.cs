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
        Task AddRangeDairySpermsAsync(List<DairySperm> dairySperms);
        Task AddDairySpermAsync(DairySperm dairySperm);

        Task<DairySperm> FindDairySpermAsync(string id);
        Task UpdateDairySpermAsync(DairySperm dairySperm);
        Task DeleteAllDairySpermsAsync();
        Task DeleteDairySpermAsync(string id);
    }
}
