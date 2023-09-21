using DAL.DAO;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class ArticleMonitorService 
    {
        private readonly KitchenServerDbContext _context;

        public ArticleMonitorService(KitchenServerDbContext context)
        {
            _context = context;
        }

        
    }
}
