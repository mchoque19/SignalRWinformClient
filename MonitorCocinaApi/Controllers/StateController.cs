using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitorCocinaApi.DTO;

namespace MonitorCocinaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly KitchenServerDbContext _context;

        public StateController(KitchenServerDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatesDto>>> GetStates()
        {
            var todoItem = _context.State.Select(m => new StatesDto()
            {
                Id = m.Id,
                Name = m.Name,
                Color = m.Color,
                Order = m.Order

            }).ToListAsync();
            if (todoItem == null)
            {
                return NotFound();
            }

            return await todoItem;
        }
    }
}
