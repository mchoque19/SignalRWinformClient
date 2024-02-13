using DAL.Models;

namespace DAL.Repositories
{
    public interface IGenericCRUD<T>
    {
        public List<T> GetAll();
        public T? GetById(int id);
        public T Save(T entity);
        public bool Delete(T entity);
        public T Update(T entity);

        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int id);
    }
}
