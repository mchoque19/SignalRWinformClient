using DAL;
using DAL.DAO;
using DAL.Interfaces;
using DAL.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
 
builder.Services.AddDbContext<KitchenServerDbContext>(opts => opts.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
   //b => b.MigrationsAssembly("SignalRWinformClient.DAL")
   ));

builder.Services.AddScoped<IGenericRepository<Family>, FamilyService>();
builder.Services.AddScoped<IGenericRepository<Department>, DepartmentService>();

var app = builder.Build();

// Add services to the container.
//builder.Services.AddControllersWithViews();


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<KitchenServerDbContext>();
    context.Database.Migrate();
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
