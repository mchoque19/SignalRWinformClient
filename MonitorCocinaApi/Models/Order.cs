using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MonitorCocinaApi.Models;

[Table("Order")]
public partial class Order
{
    [Key]
    public int Id { get; set; }

    public string SoftwareVers { get; set; } = null!;

    public int MadiCustNo { get; set; }

    public int CompNo { get; set; }

    public int StoreNo { get; set; }

    public string OrderId { get; set; } = null!;

    public int? TermNo { get; set; }

    public int? OperNo { get; set; }

    public string? OperName { get; set; }

    public string? TbNum { get; set; }

    public int Pax { get; set; }

    public string TableType { get; set; } = null!;

    public DateTime StartTime { get; set; }
}
