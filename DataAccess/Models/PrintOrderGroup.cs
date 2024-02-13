namespace DAL.Models
{
    public class PrintOrderGroup
    {
        private int id;
        private string name;


        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }


        public virtual ICollection<OrderItem> Items { get; set; }
    }
}
