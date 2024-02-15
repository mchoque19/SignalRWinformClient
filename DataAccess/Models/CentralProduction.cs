using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
	[Table("CentralProduction")]
	public partial class CentralProduction
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; } = null!;
	 
	}
}
