using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class PrintOrderGroupService : IService<PrintOrderGroup>
    {
        private readonly KitchenServerDbContext _context;

        public PrintOrderGroupService(KitchenServerDbContext context)
        {
            _context = context;
        }

        public PrintOrderGroup? GetById(int id)
        {
            return _context.Find<PrintOrderGroup>(id);
        }
    }
}
