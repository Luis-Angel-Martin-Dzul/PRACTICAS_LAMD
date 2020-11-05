using AnimalSpawn.Domain.Entities;
using AnimalSpawn.Domain.Interfaces;
using AnimalSpwan.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AnimalSpwan.Infraestructure.Repositories
{
    public class SQLRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AnimalSpawnContext _context;
        private readonly DbSet<T> _entity;

        public SQLRepository(AnimalSpawnContext context)
        {
            this._context = context;
            this._entity = context.Set<T>();
        }
        public async Task Add(T entity)
        {
            if (entity == null) throw new ArgumentException("Entity");
            _entity.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            if (id <= 0) throw new ArgumentException("Entity");

            var entity = await GetById(id);
            _entity.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return await _entity.Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _entity.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _entity.SingleOrDefaultAsync(entity => entity.Id == id);
        }

        public async Task Update(T entity)
        {
            if (entity == null) throw new ArgumentException("Entity");
            if (entity.Id <= 0) throw new ArgumentException("Entity");
            _entity.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
