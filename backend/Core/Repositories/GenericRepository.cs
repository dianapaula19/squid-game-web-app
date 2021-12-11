using Microsoft.EntityFrameworkCore;
using backend.Core.IRepositories;
using backend.DAL;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace backend.Core.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected DatabaseContext _context;
        protected ILogger _logger;
        protected DbSet<T> _dbSet;

        public GenericRepository(
            DatabaseContext context,
            ILogger logger
        )
        {
            _context = context;
            _logger = logger;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> All()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T> GetById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<bool> Add(T entity) 
        {
            await _dbSet.AddAsync(entity);
            return true;
        }

        public virtual Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
        
    }
}