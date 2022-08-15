using GB.DataAccess.DataContext;
using GB.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GB.Repository.Repo
{
    public class GenericRepository<TEntity> :
        IGenericRepository<TEntity> where TEntity : class
    {
        ApplicationDbContext _context;
        DbSet<TEntity> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        public virtual IEnumerable<TEntity> Get()
        {
            try
            {
                var data = _dbSet.ToList();
                return data;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public virtual TEntity GetById(int Id)
        {
            try
            {
                return _dbSet.Find(Id);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public virtual void Add(TEntity entity)
        {
            try
            {
                _dbSet.Add(entity);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }
        public virtual void Delete(int id)
        {
            try
            {
                var deletdata = _dbSet.Find(id);
                _dbSet.Remove(deletdata);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public virtual void Edit(TEntity entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    
    }
}
