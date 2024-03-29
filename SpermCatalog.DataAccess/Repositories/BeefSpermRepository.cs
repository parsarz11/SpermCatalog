﻿using Microsoft.EntityFrameworkCore;
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
            if (beefSperms == null)
            {
                throw new Exception("beef sperm is null| BeefSpermRepository/AddBeefSpermsListAsync");
            }
            await _DbContext.BeefSperms.AddRangeAsync(beefSperms);
            _DbContext.SaveChanges();
        }

        public async Task DeleteAllBeefSpermsAsync()
        {
            await _DbContext.BeefSperms.ExecuteDeleteAsync();
        }

        public void DeleteBeefSperm(int id)
        {
            BeefSperm beefSperm = FindBeefSpermAsync(id).Result;
            _DbContext.BeefSperms.Remove(beefSperm);
            _DbContext.SaveChanges();

        }

        public async Task<BeefSperm> FindBeefSpermAsync(int id)
        {
            var result = await _DbContext.BeefSperms.FirstOrDefaultAsync(x => x.Id == id);

            if (result != null)
            {
                return result;
            }

            throw new Exception("beef Sperm not found | BeefSpermRepository/FindBeefSpermAsync");

        }

        public async Task<List<BeefSperm>> GetBeefSpermsAsync()
        {
            var result = await _DbContext.BeefSperms.ToListAsync();

            if (result != null)
            {
                return result;
            }

            throw new Exception("beef sperm is empty | BeefSpermRepository/GetBeefSpermsAsync");
        }

        public void UpdateBeefSperm(BeefSperm beefSperm)
        {

            _DbContext.BeefSperms.Update(beefSperm);
            _DbContext.SaveChanges();
            
        }
    }
}
