using System;
using System.Collections.Generic;

namespace BackOfficeMonitorCocina.Models;

public partial class OrderItem
{
    public int OrderId { get; set; }

    public int OrderLineNo { get; set; }

    public int? PrintOrderGroupId { get; set; }

    public int ArticleId { get; set; }

    public int Units { get; set; }

    public string ModifList { get; set; } = null!;

    public int? MenuOrderLineNo { get; set; }

    public int StateId { get; set; }

    public virtual Article Article { get; set; } = null!;

    public virtual PrintOrder? PrintOrderGroup { get; set; }

    public virtual State State { get; set; } = null!;
}
