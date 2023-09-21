using Microsoft.AspNetCore.Mvc;
using DAL.DAO;
using DAL.Interfaces;
using BackOffice.ViewModels;

namespace BackOffice.Controllers
{
    public class FamilyController : Controller
    {
        private readonly IGenericRepository<Family> _GenericRespository;

        public FamilyController(IGenericRepository<Family> genericRepository)
        {
            _GenericRespository = genericRepository;
        }


        public async Task<IActionResult> Index()
        {
            IEnumerable<Family> family = await _GenericRespository.GetAll();
            return View(family);
        }


        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FamilyView familyView)
        {
            //var errors = ModelState.Values.SelectMany(v => v.Errors);
            //Console.WriteLine("****************");
            //foreach (var i in errors)
            //{
            //    Console.WriteLine(i.ErrorMessage);
            //}

            if (!ModelState.IsValid)
            {
                Console.WriteLine("Error");
                return View(familyView);
            }

            var family = new Family
            { Name = familyView.Name, };

            _GenericRespository.Insert(family);
            return RedirectToAction("Index");
         
        }

        public async Task<IActionResult> Edit(int id)
        {
            Family family = await _GenericRespository.GetByIdAsync(id);
            if (family == null) return View("Error");
            var familyVM = new FamilyView
            {
                Id = family.Id,
                Name = family.Name,               

            };
            return View(familyVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, FamilyView familyView)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failded to editado family");
                return View("Edit", familyView);
            }
            var family = new Family
            {
                Id = familyView.Id,
                Name = familyView.Name,               
            };
            _GenericRespository.Update(family);
            return RedirectToAction("Index");
        }
    }
}
