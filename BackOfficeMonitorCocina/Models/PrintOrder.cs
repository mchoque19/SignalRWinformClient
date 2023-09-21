using System;
using System.Collections.Generic;

namespace BackOfficeMonitorCocina.Models;

public partial class PrintOrder
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
