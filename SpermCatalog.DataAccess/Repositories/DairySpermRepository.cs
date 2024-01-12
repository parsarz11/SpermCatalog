using Microsoft.EntityFrameworkCore;
using SpermCatalog.DataAccess.Contracts;
using SpermCatalog.DataAccess.DatabaseContext;
using SpermCatalog.DataAccess.Entities;

namespace SpermCatalog.DataAccess.Repositories
{
    public class DairySpermRepository : IDairySperm
    {
        private readonly SpermCatalogDbContext _DbContext;

        public DairySpermRepository(SpermCatalogDbContext dbContext)
        {
            _DbContext = dbContext;
        }


        public async Task AddDairySpermsListAsync(List<DairySperm> dairySperms)
        {
            await _DbContext.DairySperms.AddRangeAsync(dairySperms);
            await _DbContext.SaveChangesAsync();
        }

        public async Task DeleteAllDairySpermsAsync()
        {
            await _DbContext.DairySperms.ExecuteDeleteAsync();
        }

        public async Task DeleteDairySperm(int id)
        {
            var beefSperm = await FindDairySpermAsync(id);

            _DbContext.DairySperms.Remove(beefSperm);
            await _DbContext.SaveChangesAsync();

        }

        public async Task<DairySperm> FindDairySpermAsync(int id)
        {
            var result = await _DbContext.DairySperms.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<List<DairySperm>> GetDairySpermsAsync()
        {
            var result = await _DbContext.DairySperms.ToListAsync();
            return result;
        }

        public async Task UpdateDairySpermsAsync(int id)
        {
            var selectedSperm = await FindDairySpermAsync(id);

            _DbContext.DairySperms.Update(selectedSperm);
            await _DbContext.SaveChangesAsync();
        }
    }
}
