﻿using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitorCocinaApi.DTO;
using System.Drawing;

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
			ResponseLoginDto<object> response = new ResponseLoginDto<object>();
			var listState = _context.State.Select(m => new StatesDto()
            {
                Id = m.Id,
                Name = m.Name,
                Color = "#" + Color.FromArgb(Convert.ToInt32(m.Color)).Name,
                Order = m.Order

            }).ToList();

			if (listState.Count() > 0)
			{
				response.Content = listState;
				return Ok(response);
			}

			response.Error = errorMessage(400, "No se encontraron estados");
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
