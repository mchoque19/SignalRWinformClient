﻿using DAL.Interfaces;
using DAL.Models;
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
            try
            {
                _context.Remove(family);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al borrar familia");
            }             
            return Save();
        }

        public async Task<IEnumerable<Family>> GetAll()
        {
            return await _context.Family.ToListAsync(); 
        }

        //public List<Family> GetAllFamily()
        //{
        //    return  _context.Family.ToList();

        //}

        public async Task<Family> GetByIdAsync(int id)
        {
            return await _context.Family.FirstAsync(i => i.Id == id);
             
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
    }
}
