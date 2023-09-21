using System.ComponentModel.DataAnnotations;

namespace DAL.DAO
{
    public class State
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public virtual ICollection<Monitor> Monitors { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}