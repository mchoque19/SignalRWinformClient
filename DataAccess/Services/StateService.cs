using DAL.DAO;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class StateService : IGenericRepository<State>
    {
        private readonly KitchenServerDbContext _context;

        public StateService(KitchenServerDbContext context)
        {
            _context = context;
        }
        public bool Delete(State state)
        {
            _context.Remove(state);
            return Save();
        }

        public async Task<IEnumerable<State>> GetAll()
        {
            return await _context.State.ToListAsync();
        }

        public async Task<State> GetByIdAsync(int id)
        {
            return await _context.State.FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Insert(State state)
        {
            _context.Add(state);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(State state)
        {
            _context.Update(state);
            return Save();

        }

        public bool addStatInMonitorn(DAO.Monitor monitor, int stateId)
        {
            try
            {
                State state = _context.State
                         .Where(p => p.Id == stateId)
                         .First();


                monitor.States.Add(state);
            }
            catch (Exception e)
            {
                Console.WriteLine("erro al guarrdar");
            }
       
            return Save();
        }

        public bool deleteMonitorStatus(DAO.Monitor monitor, int stateId)
        {
            State state = _context.State
                               .Where(p => p.Id == stateId)
                               .First();

            monitor.States.Remove(state);
            return Save();
        }

        //obtener estados del monitor 
        //public async Task<IEnumerable<State>> GetStatesNotIncludedInMonitor(DAO.Monitor monitor)
        //{
                                   
        //    return await _context.State.ToListAsync();
        //}
    }
}
