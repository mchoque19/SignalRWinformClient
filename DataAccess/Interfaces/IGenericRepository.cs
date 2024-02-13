using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IGenericRepository<T>
    {        
        public Task<IEnumerable<T>> GetAll();
        public Task<T> GetByIdAsync(int id);
        public bool Insert(T obj);
        public bool Update(T obj);
        public bool Delete(T obj);
        public bool Save();

      
    }
}
