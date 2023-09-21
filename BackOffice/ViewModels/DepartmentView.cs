using DAL.DAO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BackOffice.ViewModels
{
    public class DepartmentView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Family Family { get; set; }
 
    }
}
