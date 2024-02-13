using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class State
    {
        private int id;
        private string name;
        private int order;
        private string color;
        private bool marchando;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int Order { get => order; set => order = value; }
        public string Color { get => color; set => color = value; }
        public bool Marchando { get => marchando; set => marchando = value; }


        public virtual ICollection<Monitor> Monitors { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }

        //public int Id { get; set; }
        //public string Name { get; set; }
        //public int Order { get; set; }
        //public string Color { get; set; }
        //public bool Marchando { get; set; }
    }
}