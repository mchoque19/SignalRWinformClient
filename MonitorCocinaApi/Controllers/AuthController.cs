using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MonitorCocinaApi.DTO;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
			ResponseLoginDto<string> response = new();
 
			if (ValidateDevice(request))
			{
				var todoItem = _context.Device.SingleOrDefault(d => d.Mac == request.Mac);
				if (todoItem == null)
				{
					//Guardar dispositivo
					var device = new Device()
					{
						Name = request.Name,
						Mac = request.Mac,
						Ip = request.Ip,
						Version = request.Version,
						Date = DateTime.Now
					};
					_context.Device.Add(device);
					_context.SaveChanges();				 
					response.Error = errorMessage(400, "Dispositivo no encontrado");
					response.Result = -1;
					return StatusCode(401, response);			
				} 
				 
				if (todoItem.Active)
				{
					string token = CreateToken(request);
					response.Content = token;
					return Ok(response);
				}
				response.Error = errorMessage(401, "Dispositivo no activado");
				response.Result = -1;
				return StatusCode(401, response);
				 		 			 
			}		 
			response.Error = errorMessage(400, "Credenciales incorrectas");
			response.Result = -1;			
			return StatusCode(400, response);
		}

		private ErrorDto errorMessage (int code, string message)
		{
			ErrorDto error = new ErrorDto();
			error.Error_type = code;
			error.Error_desc = message;
			return error;
		}

		private bool ValidateDevice(DeviceDto? device)
		{
			if (device is null)
				return false;
			if (string.IsNullOrEmpty(device.Name))
				return false;
			if (string.IsNullOrEmpty(device.Mac))
				return false;
			if (string.IsNullOrEmpty(device.Ip))
				return false;
			if (string.IsNullOrEmpty(device.Version))
				return false;
			return true;
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
