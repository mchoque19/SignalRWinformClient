using System;
using System.Collections.Generic;

namespace BackOfficeMonitorCocina.Models;

public partial class Department
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int FamilyId { get; set; }

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    public virtual Family Family { get; set; } = null!;

    public virtual ICollection<Monitor> Monitors { get; set; } = new List<Monitor>();
}
