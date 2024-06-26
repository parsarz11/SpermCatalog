﻿using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SpermCatalog.DataAccess.Contracts;
using SpermCatalog.DataAccess.Entities;

namespace SpermCatalog.DataAccess.DatabaseContext
{
    public class SpermCatalogDbContext : ISpermCatalogDbContext 
    {
        private readonly MongoDbConfigurations _mongoDbConfigs;
        private IMongoDatabase _database;
        public SpermCatalogDbContext(IOptions<MongoDbConfigurations> configs)
        {
            _mongoDbConfigs = configs.Value;

            MongoClient mongoClient = new MongoClient(_mongoDbConfigs.ConnectionString);
            _database = mongoClient.GetDatabase(_mongoDbConfigs.DatabaseName);

            DairySperms = _database.GetCollection<DairySperm>("Dairy");
            BeefSperms = _database.GetCollection<BeefSperm>("Beef");
            DairyRangeFilters = _database.GetCollection<RangeFilter>("DairyRangeFilter");
            BeefRangeFilters = _database.GetCollection<RangeFilter>("BeefRangeFilter");
        }

        public IMongoCollection<DairySperm> DairySperms { get; set; }
        public IMongoCollection<BeefSperm> BeefSperms { get; set; }
        public IMongoCollection<RangeFilter> DairyRangeFilters { get; set; }
        public IMongoCollection<RangeFilter> BeefRangeFilters { get; set; }
    }
}
