using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Family
    {
        private int id;
        private string name;


        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }


        public virtual ICollection<Monitor> Monitors { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
    }
}