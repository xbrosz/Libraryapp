namespace Infrastructure.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetByID(object id);

        void Insert(TEntity entity);

        void Delete(object id);

        void Delete(TEntity entityToDelete);

        void Update(TEntity entityToUpdate);

        public IEnumerable<TEntity> GetAll();
    }
}
