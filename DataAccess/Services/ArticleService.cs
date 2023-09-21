using DAL.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Services
{
    public class ArticleService : IGenericRepository<Article>
    {
        private readonly KitchenServerDbContext _context;

        public ArticleService(KitchenServerDbContext context)
        {
            _context = context;
        }

        public Article? GetById(int id)
        {
            Article? article = _context.Find<Article>(id);

            return article;

        }
        public bool Delete(Article article)
        {
            _context.Remove(article);
            return Save();         
        }

        public async Task<IEnumerable<Article>> GetAll()
        {
            return await _context.Article.ToListAsync();
        }

        public async Task<Article> GetByIdAsync(int id)
        {
            return await _context.Article.FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Insert(Article article)
        {
            _context.Add(article);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Article article)
        {
            _context.Update(article);
            return Save();
        }
    }
}
