using LucidiaIT.Interfaces;
using LucidiaIT.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LucidiaIT.Services
{
    public class DataService<TEntity> : IDataService<TEntity> where TEntity : Identifiable
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _entity;

        public DataService(DbContext context)
        {
            _context = context;
            _entity = _context.Set<TEntity>();
        }

        public async Task<int> CreateAsync(TEntity t)
        {
            _entity.Add(t);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(TEntity t)
        {
            _entity.Remove(t);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> EditAsync(TEntity t)
        {
            _entity.Update(t);
            return await _context.SaveChangesAsync();
        }

        public async Task<TEntity> GetDataObjectAsync(int? id)
        {
            return await _entity.SingleOrDefaultAsync(m => m.ID == id);
        }

        public async Task<List<TEntity>> GetListAsync()
        {
            return await _entity.ToListAsync();
        }

        public bool DataObjectExists(int id)
        {
            return _entity.Any(m => m.ID == id);
        }        

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
