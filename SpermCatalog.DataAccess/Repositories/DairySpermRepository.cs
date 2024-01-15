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
            await _DbContext.DairySperms.AddRangeAsync(dairySperms);
             _DbContext.SaveChanges();
            Console.WriteLine("-----------------------------");
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
            return result;
        }

        public async Task<List<DairySperm>> GetDairySpermsAsync()
        {
            var result = await _DbContext.DairySperms.ToListAsync();
            return result;
        }

        public void UpdateDairySperms(DairySperm dairySperm)
        {

            _DbContext.DairySperms.Update(dairySperm);

            
            _DbContext.SaveChanges();
        }
    }
}
