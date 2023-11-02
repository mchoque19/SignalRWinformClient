using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitorCocinaApi.DTO;

namespace MonitorCocinaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
            var listState = _context.State.Select(m => new StatesDto()
            {
                Id = m.Id,
                Name = m.Name,
                Color = m.Color,
                Order = m.Order

            }).ToList();

			return listState.Count() > 0 ? Ok(listState) : NotFound("No se encontraron estados");
		}
	}
}
