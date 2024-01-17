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
        Task AddBeefSpermsListAsync(List<BeefSperm> beefSperms);
        Task<BeefSperm> FindBeefSpermAsync(int id);
        public void UpdateBeefSperm(BeefSperm beefSperm);
        Task DeleteAllBeefSpermsAsync();
        void DeleteBeefSperm(int id);
    }
}
