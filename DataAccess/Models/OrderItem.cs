using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

[PrimaryKey("OrderId", "OrderLineNo")]
[Table("OrderItem")]
[Index("ArticleId", Name = "IX_OrderItem_ArticleId")]
[Index("PrintOrderGroupId", Name = "IX_OrderItem_PrintOrderGroupId")]
[Index("StateId", Name = "IX_OrderItem_StateId")]
public partial class OrderItem
{
    [Key]
    public int OrderId { get; set; }

    [Key]
    public int OrderLineNo { get; set; }

    public int? PrintOrderGroupId { get; set; }

    public int ArticleId { get; set; }

    public int Units { get; set; }

    public string ModifList { get; set; } = null!;

    public int? MenuOrderLineNo { get; set; }

    public int StateId { get; set; }

    [ForeignKey("ArticleId")]
    [InverseProperty("OrderItems")]
    public virtual Article Article { get; set; } = null!;

    [ForeignKey("PrintOrderGroupId")]
    [InverseProperty("OrderItems")]
    public virtual PrintOrder? PrintOrderGroup { get; set; }

    [ForeignKey("StateId")]
    [InverseProperty("OrderItems")]
    public virtual State State { get; set; } = null!;
}
