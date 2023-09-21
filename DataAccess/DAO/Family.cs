namespace DAL.DAO
{
    public class Family
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Monitor> Monitors { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
    }
}