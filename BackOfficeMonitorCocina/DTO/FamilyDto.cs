namespace BackOfficeMonitorCocina.DTO
{
	public class FamilyDto
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public bool ShowDetails { get; set; } = false;
	    //public List<DAL.DAO.Monitor>? Monitors { get; set; }

		public ICollection<DAL.DAO.Department>? Departments { get; set; }
	}
} 