using System;
using System.Collections.Generic;

namespace BackOfficeMonitorCocina.Models;

public partial class Monitor
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    public virtual ICollection<Family> Families { get; set; } = new List<Family>();

    public virtual ICollection<State> States { get; set; } = new List<State>();
}
