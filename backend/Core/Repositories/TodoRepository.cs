using System;
using System.Collections.Generic;
using backend.Core.IRepositories;
using backend.DAL;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Linq;

namespace backend.Core.Repositories
{
    public class TodoRepository : GenericRepository<Todo>, ITodoRepository
    {
        public TodoRepository(
            DatabaseContext context,
            ILogger logger

        ) : base(context, logger)
        {}

        public override async Task<IEnumerable<Todo>> All() 
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All method error", typeof(TodoRepository));
                return new List<Todo>();
            }

        }

        public override async Task<bool> Update(Todo todo)
        {
            try
            {
                var result = await _dbSet.Where(x => x.Id == todo.Id).FirstOrDefaultAsync();
                
                if(result == null) 
                {  
                    return false;
                }

                _context.Entry(result).CurrentValues.SetValues(todo);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update method error", typeof(TodoRepository));
                return false;
            }
        }

        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var exist = await _dbSet.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (exist != null) 
                {
                    _dbSet.Remove(exist);
                    return true;
                }

                return false;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete method error", typeof(TodoRepository));
                return false;
            }
        }
        
    }
}