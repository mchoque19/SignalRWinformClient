namespace DAL.DAO
{
    public class Article
    {
        private int id;
        private string name;
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public virtual ICollection<Monitor> Monitors{ get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        
    }
}
