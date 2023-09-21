using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Family Family { get; set; }
        public virtual ICollection<Monitor> Monitors { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
    }
}
