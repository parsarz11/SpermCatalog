using Microsoft.EntityFrameworkCore;
using SpermCatalog.DataAccess.Contracts;
using SpermCatalog.DataAccess.DatabaseContext;
using SpermCatalog.DataAccess.Entities;

namespace SpermCatalog.DataAccess.Repositories
{
    public class DairySpermRepository : IDairyRepository
    {
        private readonly SpermCatalogDbContext _DbContext;

        public DairySpermRepository(SpermCatalogDbContext dbContext)
        {
            _DbContext = dbContext;
        }


        public async Task AddDairySpermsListAsync(List<DairySperm> dairySperms)
        {
            if (dairySperms == null)
            {
                throw new Exception("beef sperm is null| DairySpermRepository/AddDairySpermsListAsync");
            }

            await _DbContext.DairySperms.AddRangeAsync(dairySperms);
             _DbContext.SaveChanges();
        }

        public async Task DeleteAllDairySpermsAsync()
        {
            await _DbContext.DairySperms.ExecuteDeleteAsync();
        }

        public void DeleteDairySperm(int id)
        {
            var beefSperm =  FindDairySpermAsync(id).Result;

            _DbContext.DairySperms.Remove(beefSperm);
            _DbContext.SaveChanges();

        }

        public async Task<DairySperm> FindDairySpermAsync(int id)
        {
            var result = await _DbContext.DairySperms.FirstOrDefaultAsync(x => x.Id == id);
            
            if (result != null)
            {
                return result;
            }

            throw new Exception("dairy sperm not found | DairySpermsRepository/FindDairySpermsAsync");

        }

        public async Task<List<DairySperm>> GetDairySpermsAsync()
        {
            var result = await _DbContext.DairySperms.ToListAsync();
            
            if (result != null)
            {
                return result;
            }

            throw new Exception("dairy sperm is empty | DairySpermsRepository/DairySpermsSpermsAsync");
        }

        public void UpdateDairySperm(DairySperm dairySperm)
        {

            _DbContext.DairySperms.Update(dairySperm);

            
            _DbContext.SaveChanges();
        }
    }
}
