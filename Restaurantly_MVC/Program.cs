using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurantly_MVC.DAL;
using Restaurantly_MVC.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(opt=>opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddIdentity<AppUser, IdentityRole>(opt => {

    opt.Password.RequireNonAlphanumeric = false;

    opt.User.RequireUniqueEmail = true;


}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(cfg =>
{
    cfg.LoginPath = $"/Admin/Account/Login/{cfg.ReturnUrlParameter}";
});

var app = builder.Build();



app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();



app.MapControllerRoute(

    "default",
    "{area:exists}/{controller=Home}/{action=Index}/{id?}"

    );

app.MapControllerRoute(
    
    "default",
    "{controller=Home}/{action=Index}/{id?}"
    
    );

app.Run();
