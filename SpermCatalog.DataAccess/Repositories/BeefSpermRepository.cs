using Microsoft.EntityFrameworkCore;
using SpermCatalog.DataAccess.Contracts;
using SpermCatalog.DataAccess.DatabaseContext;
using SpermCatalog.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpermCatalog.DataAccess.Repositories
{
    public class BeefSpermRepository : IBeefRepository
    {
        private readonly SpermCatalogDbContext _DbContext;

        public BeefSpermRepository(SpermCatalogDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task AddBeefSpermsListAsync(List<BeefSperm> beefSperms)
        {
            await _DbContext.BeefSperms.AddRangeAsync(beefSperms);
            await _DbContext.SaveChangesAsync();
        }

        public async Task DeleteAllBeefSpermsAsync()
        {
            await _DbContext.BeefSperms.ExecuteDeleteAsync();
        }

        public async Task DeleteBeefSperm(int id)
        {
            BeefSperm beefSperm = await FindBeefSpermAsync(id);

            _DbContext.BeefSperms.Remove(beefSperm);
            await _DbContext.SaveChangesAsync();
        }

        public async Task<BeefSperm> FindBeefSpermAsync(int id)
        {
            var result = await _DbContext.BeefSperms.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<List<BeefSperm>> GetBeefSpermsAsync()
        {
            var result = await _DbContext.BeefSperms.ToListAsync();
            return result;
        }

        public async Task UpdateBeefSpermsAsync(int id)
        {
            var selectedSperm = await FindBeefSpermAsync(id);

            _DbContext.BeefSperms.Update(selectedSperm);
            await _DbContext.SaveChangesAsync();
            
        }
    }
}
