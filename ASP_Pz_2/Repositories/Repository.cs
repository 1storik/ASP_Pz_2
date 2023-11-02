using ASP_Pz_2.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ASP_Pz_2.Repositories
{
    public class Repository<T> where T : class
    {
        private readonly GymBroDbContext _db;
        internal DbSet<T> dbSet;
        public Repository()
        {
            _db = new GymBroDbContext();
            dbSet = _db.Set<T>();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, string includeProperties = null,
                                      bool isTracking = true)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            if (!isTracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>,
                                               IOrderedQueryable<T>> orderBy = null, string includeProperties = null,
                                               bool isTracking = true)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            if (orderBy != null)
                query = orderBy(query);

            if (!isTracking)
                query = query.AsNoTracking();

            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task RemoveAsync(T entity)
        {
            dbSet.Remove(entity);
            await SaveAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
            await SaveAsync();
        }

        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await SaveAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            dbSet.Update(entity);
            await SaveAsync();
            return entity;
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}

