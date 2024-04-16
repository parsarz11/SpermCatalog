using MongoDB.Bson;
using MongoDB.Driver;
using SpermCatalog.DataAccess.Contracts;
using SpermCatalog.DataAccess.Entities;

namespace SpermCatalog.DataAccess.Repositories
{
    public class BeefSpermRepository : IBeefRepository
    {
        private readonly ISpermCatalogDbContext _DbContext;

        public BeefSpermRepository(ISpermCatalogDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task AddRangeBeefSpermsAsync(List<BeefSperm> beefSperms)
        {
            await _DbContext.BeefSperms.InsertManyAsync(beefSperms);
        }

        public async Task AddBeefSpermAsync(BeefSperm beefSperm)
        {
            await _DbContext.BeefSperms.InsertOneAsync(beefSperm);
        }

        public async Task DeleteAllBeefSpermsAsync()
        {
            await _DbContext.BeefSperms.DeleteManyAsync(new BsonDocument());
        }

        public async Task DeleteBeefSpermAsync(string id)
        {
            var filter = Builders<BeefSperm>.Filter.Eq(p => p.Id, id);
            await _DbContext.BeefSperms.DeleteOneAsync(filter);

        }

        public async Task<BeefSperm> FindBeefSpermAsync(string id)
        {
            var filter = Builders<BeefSperm>.Filter.Eq("Id", id);
            var result = await _DbContext.BeefSperms.Find(filter).FirstOrDefaultAsync();
            return result;
        }

        public async Task<List<BeefSperm>> GetBeefSpermsAsync()
        {
            var result = await _DbContext.BeefSperms.FindSync(_ => true).ToListAsync();
            return result;
        }

        public async Task UpdateBeefSpermAsync(BeefSperm beefSperm)
        {
            await _DbContext.BeefSperms.ReplaceOneAsync(p => p.Id == beefSperm.Id, beefSperm);
        }
    }
}
