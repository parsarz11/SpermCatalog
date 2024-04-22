using MongoDB.Driver;
using SpermCatalog.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpermCatalog.DataAccess.Contracts
{
    public interface ISpermCatalogDbContext
    {
        IMongoCollection<DairySperm> DairySperms { get; set; }
        IMongoCollection<BeefSperm> BeefSperms { get; set; }
        IMongoCollection<RangeFilter> RangeFilter { get; set; }
    }
}
