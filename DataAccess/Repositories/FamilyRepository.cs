using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class FamilyRepository : IGenericCRUD<Family>
    {
        private readonly KitchenServerDbContext _context;

        public FamilyRepository(KitchenServerDbContext context)
        {
            _context = context;
        }

        public List<Family> GetAll()
        {
            return _context.Family.ToList();
        }

        public Family? GetById(int id)
        {
            return _context.Family.Find(id);
        }


        public Family Save(Family entity)
        {
            entity = _context.Add(entity).Entity;
            return entity;
        }


        public bool Delete(Family entity)
        {
            _context.Remove(entity);
            int deleted = _context.SaveChanges();
            return deleted > 0;
        }


        public Family Update(Family entity)
        {
            entity = _context.Update(entity).Entity;
            return entity;
        }

        public async Task<IEnumerable<Family>> GetAllAsync()
        {
            return await _context.Family.ToListAsync();
        }


        public async Task<Family> GetByIdAsync(int id)
        {
            return await _context.Family.FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
