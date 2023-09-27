using DAL.DAO;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class DepartmentService : IGenericRepository<Department>
    {
        private readonly KitchenServerDbContext _context;

        public DepartmentService(KitchenServerDbContext context)
        {
            _context = context;
        }

        public bool Delete(Department department)
        {
            _context.Remove(department);
            return Save();
        }

        public async Task<IEnumerable<Department>> GetAll()
        {
            return await _context.Department.ToListAsync();
        }    

        public async Task<Department> GetByIdAsync(int id)
        {
            return await _context.Department.FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Insert(Department department)
        {
            _context.Add(department);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false; throw new NotImplementedException();
        }

        public bool Update(Department department)
        {
            _context.Update(department);
            return Save(); throw new NotImplementedException();
        }
        ////obtener los departamentos con la familia
        //public async Task<IEnumerable<Department>> GetAllDepartmentAndFamily()
        //{
        //    return await _context.Department.Include(d => d.Family).ToListAsync();

        //}



    }
}
