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
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(
            DatabaseContext context,
            ILogger logger

        ) : base(context, logger)
        {}

        public override async Task<IEnumerable<User>> All() 
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All method error", typeof(UserRepository));
                return new List<User>();
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
                _logger.LogError(ex, "{Repo} Delete method error", typeof(UserRepository));
                return false;
            }
        }
        
    }
}