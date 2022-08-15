using GB.DataAccess.DataContext;
using GB.DataAccess.Entities;
using GB.Repository.Interfaces;
using GB.Repository.Repo;
using GB.Services.Interfaces;

namespace GB.Services.Repo
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context) => _context = context;

        private GenericRepository<Message> messageRepo;
        public IGenericRepository<Message> MessageRepo
        {
            get
            {
                if (this.messageRepo == null)
                {
                    this.messageRepo = new GenericRepository<Message>(_context);
                }
                return messageRepo;
            }
        }


        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;

    }
}
