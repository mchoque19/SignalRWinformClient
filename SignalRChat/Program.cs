using Microsoft.AspNet.SignalR;
using SignalRChat.Hubs;
using DAL;
using Microsoft.EntityFrameworkCore;
using DAL.Services;
using DAL.DAO;
using DAL.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddDbContext<KitchenServerDbContext>(opts=> opts.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") 
    //b => b.MigrationsAssembly("SignalRWinformClient.DAL")
    ));
builder.Services.AddScoped<KitchenOrderService>();
builder.Services.AddScoped<OrderItemService>();

 
builder.Services.AddScoped<IService<PrintOrderGroup>, PrintOrderGroupService>(); 
 

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<KitchenServerDbContext>();
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
app.MapHub<ListasHub>("/listashub");

app.Run();
