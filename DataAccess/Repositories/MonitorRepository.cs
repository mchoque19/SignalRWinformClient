using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class MonitorRepository : IGenericCRUD<Models.Monitor>
    {
        private readonly KitchenServerDbContext _context;

        public MonitorRepository(KitchenServerDbContext context)
        {
            _context = context;
        }

        public List<Models.Monitor> GetAll()
        {
            return _context.Monitor.ToList();
        }

        public Models.Monitor? GetById(int id)
        {
            return _context.Monitor.Find(id);
        }

        public bool Delete(Models.Monitor entity)
        {
            _context.Remove(entity);
            int deleted = _context.SaveChanges();
            return deleted > 0;
        }

        public Models.Monitor Save(Models.Monitor entity)
        {
            entity = _context.Add(entity).Entity;
            return entity;
        }

        public Models.Monitor Update(Models.Monitor entity)
        {
            entity = _context.Update(entity).Entity;
            return entity;
        }

        public async Task<IEnumerable<Models.Monitor>> GetAllAsync()
        {
            return await _context.Monitor.ToListAsync();
        }

        public async Task<Models.Monitor> GetByIdAsync(int id)
        {
            return await _context.Monitor.FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
