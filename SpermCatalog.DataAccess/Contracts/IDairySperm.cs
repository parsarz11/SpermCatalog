using SpermCatalog.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpermCatalog.DataAccess.Contracts
{
    public interface IDairySperm
    {
        Task<List<DairySperm>> GetDairySpermsAsync();
        Task AddDairySpermsListAsync(List<DairySperm> dairySperms);
        Task<DairySperm> FindDairySpermAsync(int id);
        Task UpdateDairySpermsAsync(int id);
        Task DeleteAllDairySpermsAsync();
        Task DeleteDairySperm(int id);
    }
}
