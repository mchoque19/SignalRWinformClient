using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CancellationRepository : IGenericCRUD<Cancellation>
    {
        private readonly KitchenServerDbContext _context;

        public CancellationRepository(KitchenServerDbContext context)
        {
            _context = context;
        }

        public bool Delete(Cancellation entity)
        {
            _context.Remove(entity);
            int deleted = _context.SaveChanges();
            return deleted > 0;
        }

        public List<Cancellation> GetAll()
        {
            return _context.Cancellation.ToList();
        }

        public async Task<IEnumerable<Cancellation>> GetAllAsync()
        {
            return await _context.Cancellation.ToListAsync();
        }

        public Cancellation? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Cancellation? GetByIdAndOrderLine(int orderId, int orderLine)
        {
            return _context.Cancellation.Where(c => c.OrderId == orderId && c.OrderLineNo == orderLine).FirstOrDefault();
        }
        public async Task<Cancellation> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Cancellation Save(Cancellation entity)
        {
            entity = _context.Add(entity).Entity;
            var saved = _context.SaveChanges();
            if (saved > 0)
            {
                return entity;
            }
            else
            {
                return entity;
            }
        }

        public Cancellation Update(Cancellation entity)
        {
            entity = _context.Update(entity).Entity;
            return entity;
        }
    }
}
