using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitorCocinaApi.Models;
 
using MonitorCocinaApi.DTO;
using Microsoft.AspNetCore.Authorization;
using Azure.Core;
using System.Collections.Generic;

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
			 
			ResponseLoginDto<object> response = new ResponseLoginDto<object>();

			var listMonitor = _context.Monitor.Select(m => new MonitorDto()
            {
                Id = m.Id,
                Name = m.Name,
                States = (List<StateDto>)m.States.Select(s => new StateDto()
               {
                   Id = s.Id 
               })

            }).ToList();

            if(listMonitor.Count() > 0)
            {			 
				response.Content = listMonitor;
				return Ok(response);
			}

			response.Error = errorMessage(400, "No se encontraron monitores");
			response.Result = -1;
			return StatusCode(400, response);			     
		}

		private ErrorDto errorMessage(int code, string message)
		{
			ErrorDto error = new ErrorDto();
			error.Error_type = code;
			error.Error_desc = message;
			return error;
		}


	}
}
