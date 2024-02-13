using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Monitor
    {
        private int id;
        private string name;


        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }


        public virtual ICollection<Family> Families { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<State> States { get; set; }

    }
}
