﻿using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitorCocinaApi.Models;
using MonitorCocinaApi.Models.response;
using MonitorCocinaApi.DTO;

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
        public async Task<ActionResult<IEnumerable<MonitorDto>>> GetTodoItems()
        {
            var todoItem = _context.Monitor.Select(m => new MonitorDto()
            {
                Id = m.Id,
                Name = m.Name,
                states = (List<StateDto>)m.States.Select(s => new StateDto()
               {
                   Id = s.Id 
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
