using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitorCocinaApi.Models;
using MonitorCocinaApi.Models.response;
using MonitorCocinaApi.DTO;
using Microsoft.AspNetCore.Authorization;

namespace MonitorCocinaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MonitorController : ControllerBase
    {
        private readonly KitchenServerDbContext _context;

        public MonitorController(KitchenServerDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<MonitorDto>>> GetTodoItems()
        {
            var listMonitor = _context.Monitor.Select(m => new MonitorDto()
            {
                Id = m.Id,
                Name = m.Name,
                States = (List<StateDto>)m.States.Select(s => new StateDto()
               {
                   Id = s.Id 
               })

            }).ToList();
		 
            return listMonitor.Count() > 0 ? Ok(listMonitor) : NotFound("No se encontraron monitores");         
		}

	}
}
