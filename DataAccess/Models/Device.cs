using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
	[Table("Device")]
	public partial class Device
    {
		[Key]
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public string Mac { get; set; } = null!;
		public string Ip { get; set; } = null!;
		public string Version { get; set; } = null!;
		public DateTime Date { get; set; } = DateTime.Now;
		public bool Active { get; set; } = false;
	}
}
