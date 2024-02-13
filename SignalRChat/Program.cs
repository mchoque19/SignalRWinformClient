using Microsoft.AspNet.SignalR;
using SignalRChat.Hubs;
using DAL;
using Microsoft.EntityFrameworkCore;
using DAL.Repositories;
using DAL.Interfaces;
using Microsoft.AspNetCore.Cors.Infrastructure;
//using Microsoft.Owin.Security.OAuth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SignalRChat.Services;
using DAL.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddDbContext<KitchenServerDbContext>(opts=> opts.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") 
    //b => b.MigrationsAssembly("SignalRWinformClient.DAL")
    ));
//builder.Services.AddScoped<KitchenOrderService>();
//builder.Services.AddScoped<ArticleService>();
//builder.Services.AddScoped<DAL.Services.OrderItemService>();
//builder.Services.AddSingleton<OrderService>();


builder.Services.AddScoped<IGenericCRUD<Article>, ArticleRepository>();
builder.Services.AddScoped<IGenericCRUD<Department>, DepartmentRepository>();
builder.Services.AddScoped<IGenericCRUD<Family>, FamilyRepository>();
builder.Services.AddScoped<IGenericCRUD<DAL.Models.Monitor>, MonitorRepository>();
builder.Services.AddScoped<IGenericCRUD<OrderItem>, OrderItemRepository>();
builder.Services.AddScoped<IGenericCRUD<Order>, OrderRepository>();
builder.Services.AddScoped<IGenericCRUD<PrintOrderGroup>, PrintOrderGroupRepository>();
builder.Services.AddScoped<IGenericCRUD<State>, StateRepository>();
builder.Services.AddScoped<OrderItemRepository>();
builder.Services.AddScoped<OrderRepository>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<OrderItemService>();

//inicio prueba
builder.Services.AddAuthentication(options =>
{
	// Identity made Cookie authentication the default.
	// However, we want JWT Bearer Auth to be the default.
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
	// Configure the Authority to the expected value for
	// the authentication provider. This ensures the token
	// is appropriately validated.
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuerSigningKey = true,
		ValidateAudience = false,
		ValidateIssuer = false,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
		builder.Configuration.GetSection("AppSettings:Token").Value!))
	};

	// We have to hook the OnMessageReceived event in order to
	// allow the JWT authentication handler to read the access
	// token from the query string when a WebSocket or 
	// Server-Sent Events request comes in.

	// Sending the access token in the query string is required when using WebSockets or ServerSentEvents
	// due to a limitation in Browser APIs. We restrict it to only calls to the
	// SignalR hub in this code.
	// See https://docs.microsoft.com/aspnet/core/signalr/security#access-token-logging
	// for more information about security considerations when using
	// the query string to transmit the access token.
	options.Events = new JwtBearerEvents
	{
		OnMessageReceived = context =>
		{
			var accessToken = context.Request.Query["access_token"];
			Console.WriteLine("##########");
			Console.WriteLine(accessToken);
			// If the request is for our hub...
			var path = context.HttpContext.Request.Path;
			if (!string.IsNullOrEmpty(accessToken) &&
				(path.StartsWithSegments("/chathub")))
			{
				// Read the token out of the query string
				context.Token = accessToken;
			}
			return Task.CompletedTask;
		}
	};
});
//fin prueba

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<KitchenServerDbContext>();
	//context.Database.EnsureCreated();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//app.MapRazorPages();
app.MapHub<ChatHub>("/chathub");

 

app.Run();
