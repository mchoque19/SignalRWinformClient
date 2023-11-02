using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SignalRChat.Models;

[Table("Article")]
[Index("DepartmentId", Name = "IX_Article_DepartmentId")]
public partial class Article
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int DepartmentId { get; set; }

    [ForeignKey("DepartmentId")]
    [InverseProperty("Articles")]
    public virtual Department Department { get; set; } = null!;

    [InverseProperty("Article")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [ForeignKey("ArticlesId")]
    [InverseProperty("Articles")]
    public virtual ICollection<Monitor> Monitors { get; set; } = new List<Monitor>();
}
