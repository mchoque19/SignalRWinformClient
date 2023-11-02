using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SignalRChat.Models;

[Table("Family")]
public partial class Family
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    [InverseProperty("Family")]
    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    [ForeignKey("FamiliesId")]
    [InverseProperty("Families")]
    public virtual ICollection<Monitor> Monitors { get; set; } = new List<Monitor>();
}
