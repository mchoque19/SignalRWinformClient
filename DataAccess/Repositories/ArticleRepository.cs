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
    public class ArticleRepository : IGenericCRUD<Article>
    {
        private readonly KitchenServerDbContext _context;

        public ArticleRepository(KitchenServerDbContext context)
        {
            _context = context;
        }

        public List<Article> GetAll()
        {
            return _context.Article.ToList();
        }

        public Article? GetById(int id)
        {
            return _context.Article.Find(id);
        }


        public Article Save(Article entity)
        {
            entity = _context.Add(entity).Entity;
            return entity;
        }


        public bool Delete(Article entity)
        {
            _context.Remove(entity);
            int deleted = _context.SaveChanges();
            return deleted > 0;
        }


        public Article Update(Article entity)
        {
            entity = _context.Update(entity).Entity;
            return entity;
        }

        public async Task<IEnumerable<Article>> GetAllAsync()
        {
            return await _context.Article.ToListAsync();
        }


        public async Task<Article> GetByIdAsync(int id)
        {
            return await _context.Article.FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
