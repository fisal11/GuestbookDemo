using GB.DataAccess.Entities;
using GB.Repository.Interfaces;

namespace GB.Services.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Message> MessageRepo{ get; }
        void Save();
    }
}
