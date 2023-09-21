
namespace DAL.DAO
{
    public class PrintOrderGroup
    {
        private int id;
        private string name;
        public virtual ICollection<OrderItem> Items { get; set; }
        //private ICollection<ArticleOrderPrintOrderGroup> orderGroupList;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
    }
}
