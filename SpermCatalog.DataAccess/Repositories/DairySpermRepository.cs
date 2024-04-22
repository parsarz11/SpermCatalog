using MongoDB.Bson;
using MongoDB.Driver;
using SpermCatalog.DataAccess.Contracts;
using SpermCatalog.DataAccess.Entities;

namespace SpermCatalog.DataAccess.Repositories
{
    public class DairySpermRepository : IDairyRepository
    {
        private readonly ISpermCatalogDbContext _DbContext;

        public DairySpermRepository(ISpermCatalogDbContext dbContext)
        {
            _DbContext = dbContext;
        }


        public async Task AddRangeDairySpermsAsync(List<DairySperm> dairySperms)
        {
            await _DbContext.DairySperms.InsertManyAsync(dairySperms);
        }

        public async Task AddDairySpermAsync(DairySperm dairySperm)
        {
            await _DbContext.DairySperms.InsertOneAsync(dairySperm);
        }

        public async Task DeleteAllDairySpermsAsync()
        {
            await _DbContext.DairySperms.DeleteManyAsync(new BsonDocument());
        }

        public async Task DeleteDairySpermAsync(string id)
        {
            var filter = Builders<DairySperm>.Filter.Eq(p => p.Id, id);
            await _DbContext.DairySperms.DeleteOneAsync(filter);
        }

        public async Task<DairySperm> FindDairySpermAsync(string id)
        {
            var filter = Builders<DairySperm>.Filter.Eq("Id", id);
            var result =await _DbContext.DairySperms.Find(filter).FirstOrDefaultAsync();

            return result;
        }

        public async Task<List<DairySperm>> GetDairySpermsAsync()
        {
            var result = await _DbContext.DairySperms.FindSync(_ => true).ToListAsync();
            
            return result;
        }

        public async Task UpdateDairySpermAsync(DairySperm dairySperm)
        {
            await _DbContext.DairySperms.ReplaceOneAsync(p => p.Id == dairySperm.Id, dairySperm);
        }

        public async Task AddRangeFilterAsync(RangeFilter rangeFilter)
        {
            await _DbContext.RangeFilter.InsertOneAsync(rangeFilter);
        }
    }
}
