namespace DAL.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        IEnumerable<T> Find(Func<T, Boolean> predicate, int pageNumber = 0, int pageSize = 0);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
