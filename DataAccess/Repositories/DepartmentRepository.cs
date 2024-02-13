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
    public class DepartmentRepository : IGenericCRUD<Department>
    {
        private readonly KitchenServerDbContext _context;

        public DepartmentRepository(KitchenServerDbContext context)
        {
            _context = context;
        }

        public List<Department> GetAll()
        {
            return _context.Department.ToList();
        }

        public Department? GetById(int id)
        {
            return _context.Department.Find(id);
        }


        public Department Save(Department entity)
        {
            entity = _context.Add(entity).Entity;
            return entity;
        }


        public bool Delete(Department entity)
        {
            _context.Remove(entity);
            int deleted = _context.SaveChanges();
            return deleted > 0;
        }


        public Department Update(Department entity)
        {
            entity = _context.Update(entity).Entity;
            return entity;
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _context.Department.ToListAsync();
        }


        public async Task<Department> GetByIdAsync(int id)
        {
            return await _context.Department.FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
