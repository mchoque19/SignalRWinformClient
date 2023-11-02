using DAL;
using DAL.DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MonitorCocinaApi.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Security.Claims;

namespace MonitorCocinaApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly KitchenServerDbContext _context;
		private readonly IConfiguration _configuration;
	 
		public AuthController(KitchenServerDbContext context, IConfiguration configuration)
		{
			_context = context;
			_configuration = configuration;
		}

		[HttpPost("login")]
		public async Task<ActionResult<DeviceDto>> Login(DeviceDto request)
		{
	 		var todoItem = _context.Device.SingleOrDefault(d => d.Mac == request.Mac);
			if (todoItem ==  null)  
			{
				Console.WriteLine("Guardar dispositivo");
				var device = new Device()
				{
					Name = request.Name,
					Mac = request.Mac,
					Ip = request.Ip,
					Version = request.Version,
					Active = true,
					Date = DateTime.Now
				};
				_context.Device.Add(device);
				_context.SaveChanges();
				return NotFound("Dispositivo no encontrado");
			}

			string token = CreateToken(request);
			if (todoItem.Active)
			{
				return Ok(token);
			}
			return BadRequest("Dispositivo no Activado");		 
		}
		private string CreateToken(DeviceDto request)
		{
			List<Claim> claims = new()
			{
				new Claim(ClaimTypes.Name, request.Mac)
			};

			var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
				_configuration.GetSection("AppSettings:Token").Value!));

			var cread = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

			var token = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.Now.AddDays(1),
				signingCredentials: cread);

			var jwt = new JwtSecurityTokenHandler().WriteToken(token);
			return jwt;
		}
	}
}
