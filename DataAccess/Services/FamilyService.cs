using DAL.DAO;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Services
{
    public class FamilyService : IGenericRepository<Family>
    {
        private readonly KitchenServerDbContext _context;

        public FamilyService(KitchenServerDbContext context)
        {
            _context = context;
        }

        public bool Delete(Family family)
        {
            _context.Remove(family);
            return Save();
        }

        public async Task<IEnumerable<Family>> GetAll()
        {
            return await _context.Family.ToListAsync(); 
        }

        public List<Family> GetAllFamily()
        {
            return  _context.Family.ToList();

        }

        public async Task<Family> GetByIdAsync(int id)
        {
            return await _context.Family.FirstOrDefaultAsync(i => i.Id == id);
             
        }

        public bool Insert(Family family)
        {
            _context.Add(family);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Family family)
        {
            _context.Update(family);
            return Save();
        }

        public bool addFamilyInMonitor(DAO.Monitor monitor, int familyId)
        {
            try
            {
                Family family = _context.Family
                         .Where(p => p.Id == familyId)
                         .First();

                monitor.Families.Add(family);
                foreach(var item in family.Departments)
                {
                    monitor.Departments.Add(item);
                }                                

            }
            catch (Exception e)
            {
                Console.WriteLine("Error al guardar familia en monitor");
            }
            return Save();
        }
    }
}
