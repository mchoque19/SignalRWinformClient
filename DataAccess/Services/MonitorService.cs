using DAL.DAO;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class MonitorService : IGenericRepository<DAO.Monitor>
    {
        private readonly KitchenServerDbContext _context;

        public MonitorService(KitchenServerDbContext context)
        {
            _context = context;
        }
       
        public bool Delete(DAO.Monitor monitor)
        {
            _context.Remove(monitor);
            return Save();
        }

        public async Task<IEnumerable<DAO.Monitor>> GetAll()
        {
            return await _context.Monitor.ToListAsync();
        }

        public async Task<DAO.Monitor> GetByIdAsync(int id)
        {
            return await _context.Monitor.FirstAsync(i => i.Id == id);
        }

        public bool Insert(DAO.Monitor monitor)
        {
            _context.Add(monitor);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(DAO.Monitor monitor)
        {    
           _context.Update(monitor);
            return Save();
        }

        public bool InsertArticleInMonitor(DAO.Monitor monitor, int articleId)
        {
            Article article = _context.Article
                         .Where(p => p.Id == articleId)
                         .First();
            monitor.Articles.Add(article);
            return Save();
        }
        public bool DeleteArticleOfMonitor(DAO.Monitor monitor, int articleId)
        {
            Article article = _context.Article
                         .Where(p => p.Id == articleId)
                         .First();

            monitor.Articles.Remove(article);
            return Save();
        }     

    }
}
