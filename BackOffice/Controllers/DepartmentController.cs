using BackOffice.ViewModels;
using DAL.DAO;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackOffice.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IGenericRepository<Department> _GenericRespository;
        private readonly IGenericRepository<Family> _familyRespository;

        public DepartmentController(IGenericRepository<Department> genericRepository, 
            IGenericRepository<Family> familyRespository)
        {
            _GenericRespository = genericRepository;
            _familyRespository = familyRespository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Department> departments = await _GenericRespository.GetAll();
            return View(departments); 
        }


        public  IActionResult Create()
        {
             
 
            return View( ); 
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentView departmentView)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            Console.WriteLine("****************");
            foreach (var i in errors)
            {
                Console.WriteLine(i.ErrorMessage);
            }

            if (!ModelState.IsValid)
            {
                Console.WriteLine("Error");
                return View(departmentView);
            }

            ////var department = new Department
            ////{ Name = departmentView.Name, };

            ////_GenericRespository.Insert(department);
            return RedirectToAction("Index");

        }
    }
}
