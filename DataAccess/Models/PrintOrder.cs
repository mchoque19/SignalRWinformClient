using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

[Table("PrintOrder")]
public partial class PrintOrder
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    [InverseProperty("PrintOrderGroup")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
