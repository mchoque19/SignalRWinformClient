//using BackOfficeMonitorCocina.Data;
using DAL.Interfaces;
using DAL.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web; 
using DAL.DAO;
using DAL;
using Microsoft.EntityFrameworkCore;
using Monitor = DAL.DAO.Monitor;
//using DAL.Util;
using BackOfficeMonitorCocina.Extensiones;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using DAL.Util;
using BackOfficeMonitorCocina.Data;
//using BackOfficeMonitorCocina.Interfaces;
//using BackOfficeMonitorCocina.Util;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
//conexion importante 
builder.Services.AddSignalR();
builder.Services.AddDbContext<KitchenServerDbContext>(opts => opts.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
   //b => b.MigrationsAssembly("SignalRWinformClient.DAL")
   ));
//Se agrego los servicios 
builder.Services.AddScoped<IGenericRepository<Family>, FamilyService>();
builder.Services.AddScoped<IGenericRepository<Department>, DepartmentService>();
builder.Services.AddScoped<IGenericRepository<Article>, ArticleService>();
builder.Services.AddScoped<IGenericRepository<Monitor>, MonitorService>();
builder.Services.AddScoped<IGenericRepository<State>, StateService>();
builder.Services.AddScoped<IGenericRepository<User>, UserService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<MonitorService>();
builder.Services.AddScoped<StateService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<DepartmentService>();
builder.Services.AddScoped<FamilyService>();
//autenticacion
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<AuthenticationStateProvider, AutenticacionExtension>();
builder.Services.AddAuthenticationCore();


 

builder.Services.AddMudServices();


var app = builder.Build();

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
