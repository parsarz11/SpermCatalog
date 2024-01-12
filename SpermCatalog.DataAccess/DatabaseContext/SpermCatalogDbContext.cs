using Microsoft.EntityFrameworkCore;
using SpermCatalog.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpermCatalog.DataAccess.DatabaseContext
{
    public class SpermCatalogDbContext : DbContext
    {
        public SpermCatalogDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BeefSperm> BeefSperms { get; set; }
        public DbSet<DairySperm> DairySperms { get; set; }
    }
}
