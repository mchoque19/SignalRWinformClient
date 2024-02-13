using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class OrderRepository : IGenericCRUD<Order>
    {
        private readonly KitchenServerDbContext _context;

        public OrderRepository(KitchenServerDbContext context)
        {
            _context = context;
        }

        public bool Delete(Order entity)
        {
            _context.Remove(entity);
            int deleted = _context.SaveChanges();
            return deleted > 0;
        }

        public List<Order> GetAll()
        {
            return _context.Order.ToList();
        }

        public Order? GetById(int id)
        {
            return _context.Order.Find(id);
        }


        public Order Save(Order entity)
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

        public Order Update(Order entity)
        {
            entity = _context.Update(entity).Entity;
            return entity;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Order.ToListAsync();
        }


        public async Task<Order> GetByIdAsync(int id)
        {
            return await _context.Order.FirstOrDefaultAsync(i => i.Id == id);
        }

        public Order? Find(string softwareVers, int madiCustNo, int compNo, int storeNo, int termNo, long transNo)
        {
            return _context.Order.Where(o => o.SoftwareVers == softwareVers 
                && o.MadiCustNo == madiCustNo
                && o.CompNo == compNo
                && o.StoreNo == storeNo 
                && o.TermNo == termNo 
                && o.TransNo == transNo
                ).FirstOrDefault();
        }
    }
}
