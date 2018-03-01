using LucidiaIT.Interfaces;
using LucidiaIT.Models;
using LucidiaIT.Models.PartnerModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LucidiaIT.Services
{
    public class PartnerService : IDataService<Partner>
    {
        private readonly PartnerContext _dbcontext;

        public PartnerService(PartnerContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<int> CreateAsync(Partner Partner)
        {
            _dbcontext.Add(Partner);
            return await _dbcontext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Partner Partner)
        {
            _dbcontext.Partner.Remove(Partner);
            return await _dbcontext.SaveChangesAsync();
        }

        public async Task<int> EditAsync(Partner Partner)
        {
            _dbcontext.Update(Partner);
            return await _dbcontext.SaveChangesAsync();
        }

        public async Task<Partner> GetDataObjectAsync(int? id)
        {
            return await _dbcontext.Partner.SingleOrDefaultAsync(m => m.ID == id);
        }

        public async Task<List<Partner>> GetListAsync()
        {
            return await _dbcontext.Partner.ToListAsync();
        }

        public bool DataObjectExists(int id)
        {
            return _dbcontext.Partner.Any(e => e.ID == id);
        }
    }
}
