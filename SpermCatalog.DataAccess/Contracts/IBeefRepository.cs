using SpermCatalog.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpermCatalog.DataAccess.Contracts
{
    public interface IBeefRepository
    {
        Task<List<BeefSperm>> GetBeefSpermsAsync();
        Task AddRangeBeefSpermsAsync(List<BeefSperm> beefSperms);
        Task AddBeefSpermAsync(BeefSperm beefSperm);
        Task<BeefSperm> FindBeefSpermAsync(string id);
        Task UpdateBeefSpermAsync(BeefSperm beefSperm);
        Task DeleteAllBeefSpermsAsync();
        Task DeleteBeefSpermAsync(string id);
        Task AddRangeFilterAsync(RangeFilter rangeFilter);
        Task<List<RangeFilter>> GetRangeFiltersAsync();
    }
}
