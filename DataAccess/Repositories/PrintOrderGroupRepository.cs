using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PrintOrderGroupRepository : IGenericCRUD<PrintOrderGroup>
    {
        private readonly KitchenServerDbContext _context;

        public PrintOrderGroupRepository(KitchenServerDbContext context)
        {
            _context = context;
        }

        public bool Delete(PrintOrderGroup entity)
        {
            _context.Remove(entity);
            int deleted = _context.SaveChanges();
            return deleted > 0;
        }

        public List<PrintOrderGroup> GetAll()
        {
            return _context.PrintOrder.ToList();
        }

        public async Task<IEnumerable<PrintOrderGroup>> GetAllAsync()
        {
            return await _context.PrintOrder.ToListAsync();
        }

        public PrintOrderGroup? GetById(int id)
        {
            return _context.PrintOrder.Find(id);
        }

        public async Task<PrintOrderGroup> GetByIdAsync(int id)
        {
            return await _context.PrintOrder.FirstOrDefaultAsync(i => i.Id == id);
        }

        public PrintOrderGroup Save(PrintOrderGroup entity)
        {
            entity = _context.Add(entity).Entity;
            return entity;
        }

        public PrintOrderGroup Update(PrintOrderGroup entity)
        {
            entity = _context.Update(entity).Entity;
            return entity;
        }
    }
}
