namespace DAL.Models
{
    public class Article
    {
        private int id;
        private string name;
        private int preparationTime;


        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int PreparationTime { get => preparationTime; set => preparationTime = value; }


        public virtual ICollection<Monitor> Monitors { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
