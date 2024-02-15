using Azure;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonitorCocinaApi.DTO;
using System.Drawing;

namespace MonitorCocinaApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class ProfitCenter : ControllerBase
	{
		private readonly KitchenServerDbContext _context;

		public ProfitCenter(KitchenServerDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ProfitCenterDto>>> GetTodoItems()
		{
			ResponseLoginDto<object> response = new ResponseLoginDto<object>();

			var listProfitCenter = _context.CentralProduction.Select(c => new ProfitCenterDto()
			{
				Id = c.Id,
				Name = c.Name
			}).ToList();

			if (listProfitCenter.Count() > 0)
			{
				response.Content = listProfitCenter;
				return Ok(response);
			}

			response.Error = errorMessage(400, "No se encontraron centros de produccion");
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
