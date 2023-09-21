using System;
using System.Collections.Generic;

namespace BackOfficeMonitorCocina.Models;

public partial class Family
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    public virtual ICollection<Monitor> Monitors { get; set; } = new List<Monitor>();
}
