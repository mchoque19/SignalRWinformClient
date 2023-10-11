using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MonitorCocinaApi.Models;

[Table("Department")]
[Index("FamilyId", Name = "IX_Department_FamilyId")]
public partial class Department
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int FamilyId { get; set; }

    [InverseProperty("Department")]
    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    [ForeignKey("FamilyId")]
    [InverseProperty("Departments")]
    public virtual Family Family { get; set; } = null!;

    [ForeignKey("DepartmentsId")]
    [InverseProperty("Departments")]
    public virtual ICollection<Monitor> Monitors { get; set; } = new List<Monitor>();
}
