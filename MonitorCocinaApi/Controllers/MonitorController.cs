using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitorCocinaApi.Models;
using MonitorCocinaApi.Models.response;


namespace MonitorCocinaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitorController : ControllerBase
    {
        private readonly KitchenServerDbContext _context;

        public MonitorController(KitchenServerDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<DAL.DTO.MonitorDto>>> GetTodoItems()
        {
            var todoItem = _context.Monitor.Select(m => new DAL.DTO.MonitorDto()
            {
                Id = m.Id,
                Name = m.Name,
                states = (List<DAL.DTO.StateDto>)m.States.Select(s => new DAL.DTO.StateDto()
               {
                   Id = s.Id,
                   Name = s.Name
               })

            }).ToListAsync();

            if (todoItem == null)
            {
                return NotFound();
            }

            return await todoItem;
        }

                
    }
}
