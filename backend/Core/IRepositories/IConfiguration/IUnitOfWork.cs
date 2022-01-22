using System.Threading.Tasks;
using backend.Core.IRepositories;
namespace backend.Core.IConfiguration 
{
    public interface IUnitOfWork
    {
        IFrontManRepository FrontMen {get;}
        ITodoRepository Todos {get;}
        IVIPRepository VIPS {get;}
        IPlayerRepository Players {get;}
        IGuardRepository Guards {get;}

        Task CompleteAsync();

    }
}