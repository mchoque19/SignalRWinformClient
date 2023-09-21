using DAL.DAO;
using DAL.DTO;
using DAL.Interfaces;
using DAL.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class UserService : IGenericRepository<User>
    {
        private readonly KitchenServerDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(KitchenServerDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher; 
        }

        public bool Delete(User user)
        {
            _context.Remove(user);
            return Save();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.User.FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Insert(User user)
        {
            var passwordHash = _passwordHasher.Hash(user.Password);

            User newUser = new User();
            newUser.Username = user.Username;
            newUser.Password = passwordHash;
            _context.Add(newUser);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(User user)
        {
            _context.Update(user);
            return Save();
        }
        
        public async Task<SessionDto> Login(User user)
        {
            User searchUser = _context.User
                        .Where(p => p.Username == user.Username)
                        .First();
            var result = _passwordHasher.Verify(searchUser.Password, user.Password);

            if (!result)
            {
                throw new Exception("Username or password is not correct.");
            }
            //Cambiar lo que devuelve
            SessionDto sessionDto = new SessionDto();
            sessionDto.username = searchUser.Username;
            return sessionDto;
        }
    
    }
}
