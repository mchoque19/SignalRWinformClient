using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

[Table("State")]
public partial class State
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Order { get; set; }

    public string Color { get; set; }

    public bool Marchando { get; set; }

    [InverseProperty("State")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [ForeignKey("StatesId")]
    [InverseProperty("States")]
    public virtual ICollection<Monitor> Monitors { get; set; } = new List<Monitor>();
}
