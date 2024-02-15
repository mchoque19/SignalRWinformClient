using DAL.Models;
using Microsoft.EntityFrameworkCore;


namespace DAL.Repositories
{
    public class OrderItemRepository : IGenericCRUD<OrderItem>
    {
        private readonly KitchenServerDbContext _context;

        public OrderItemRepository(KitchenServerDbContext context)
        {
            _context = context;
        }

        public bool Delete(OrderItem entity)
        {
            _context.Remove(entity);
            int deleted = _context.SaveChanges();
            return deleted > 0;
        }

        public List<OrderItem> GetAll()
        {
            return _context.OrderItem.ToList();
        }

        public async Task<IEnumerable<OrderItem>> GetAllAsync()
        {
            return await _context.OrderItem.ToListAsync(); 
        }

        public OrderItem? GetById(int id)
        {
            return _context.OrderItem.Find(id);
        }

        public async Task<OrderItem> GetByIdAsync(int id)
        {
            return await _context.OrderItem.FirstOrDefaultAsync(i => i.OrderId == id);
        }

        public OrderItem Save(OrderItem entity)
        {
            entity = _context.Add(entity).Entity;
            var saved = _context.SaveChanges();
            if (saved > 0)
            {
                return entity;
            }
            else
            {
                return entity;
            }
        }

        public OrderItem Update(OrderItem entity)
        {
            entity = _context.Update(entity).Entity;
            var saved = _context.SaveChanges();
            if (saved > 0)
            {
                return entity;
            }
            return entity;
        }

        public OrderItem? GetByIdAndLine(int id,int orderLineNo)
        {
            return _context.OrderItem.Find(id, orderLineNo);
        }

        public OrderItem? GetByArticleDescription(int orderId, int articleId, List<string> ModifList, int? printOrderId)
        {
            return _context.OrderItem.Where(item => item.OrderId == orderId
                && item.Article.Id == articleId
                && item.ModifList == String.Join(',',ModifList)
                && item.PrintOrderGroup.Id == printOrderId).FirstOrDefault();
        }
    }
}
