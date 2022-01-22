using System;
using System.Threading.Tasks;
using backend.Core.IConfiguration;
using backend.Core.IRepositories;
using backend.Core.Repositories;
using backend.Models;
using Microsoft.Extensions.Logging;

namespace backend.DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DatabaseContext _context;
        private readonly ILogger _logger;

        public IFrontManRepository FrontMen {get; set;}
        public ITodoRepository Todos {get; set;}
        public IVIPRepository VIPS {get; set;}
        public IPlayerRepository Players {get; set;}
        public IGuardRepository Guards {get; set;}

        public UnitOfWork(
            DatabaseContext context,
            ILoggerFactory loggerFactory
        )
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");
            FrontMen = new FrontManRepository(_context, _logger);
            Todos = new TodoRepository(_context, _logger);
            VIPS = new VIPRepository(_context, _logger);
            Players = new PlayerRepository(_context, _logger);
            Guards = new GuardRepository(_context, _logger);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose() 
        {
            _context.Dispose();
        }

    }
}