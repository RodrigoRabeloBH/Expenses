using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace Data
{
    public abstract class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly DataContext _context;

        protected Repository(DataContext context)
        {
            _context = context;
        }
        public async Task Create(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            _context.Set<T>().Remove(await GetById(id));
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }
        public async Task Update(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}