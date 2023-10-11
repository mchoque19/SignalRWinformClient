using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

[Table("Monitor")]
public partial class Monitor
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    [ForeignKey("MonitorsId")]
    [InverseProperty("Monitors")]
    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    [ForeignKey("MonitorsId")]
    [InverseProperty("Monitors")]
    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    [ForeignKey("MonitorsId")]
    [InverseProperty("Monitors")]
    public virtual ICollection<Family> Families { get; set; } = new List<Family>();

    [ForeignKey("MonitorsId")]
    [InverseProperty("Monitors")]
    public virtual ICollection<State> States { get; set; } = new List<State>();
}
