using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class StateRepository : IGenericCRUD<State>
    {
        private readonly KitchenServerDbContext _context;

        public StateRepository(KitchenServerDbContext context)
        {
            _context = context;
        }

        public bool Delete(State entity)
        {
            _context.Remove(entity);
            int deleted = _context.SaveChanges();
            return deleted > 0;
        }

        public List<State> GetAll()
        {
            return _context.State.ToList();
        }

        public async Task<IEnumerable<State>> GetAllAsync()
        {
            return await _context.State.ToListAsync();
        }

        public State? GetById(int id)
        {
            return _context.State.Find(id);
        }

        public async Task<State> GetByIdAsync(int id)
        {
            return await _context.State.FirstOrDefaultAsync(i => i.Id == id);
        }

        public State Save(State entity)
        {
            entity = _context.Add(entity).Entity;
            return entity;
        }

        public State Update(State entity)
        {
            entity = _context.Update(entity).Entity;
            return entity;
        }
    }
}
