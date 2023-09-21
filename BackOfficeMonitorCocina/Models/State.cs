using System;
using System.Collections.Generic;

namespace BackOfficeMonitorCocina.Models;

public partial class State
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Order { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<Monitor> Monitors { get; set; } = new List<Monitor>();
}
