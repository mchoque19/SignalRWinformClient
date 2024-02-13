using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Department
    {
        private int id;
        private string name;
        

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
     

        public virtual Family Family { get; set; }
        public virtual ICollection<Monitor> Monitors { get; set; }
        public virtual ICollection<Article> Articles { get; set; }

    }
}
