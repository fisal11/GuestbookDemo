namespace GB.Repository.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {

        IEnumerable<TEntity> Get();
        TEntity GetById(int Id);
        void Add(TEntity entity);
        void Edit(TEntity entity);
        void Delete(int id);

    }
}
