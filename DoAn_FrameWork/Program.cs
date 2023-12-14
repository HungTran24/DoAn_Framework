//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllersWithViews();

//var app = builder.Build();

//builder.Services.AddRazorPages()
//    .AddRazorRuntimeCompilation();
//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//      name: "areas",
//      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
//    );
//});

//app.Run();

using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using DoAn_FrameWork.Areas.Admin.Models;
using DoAn_FrameWork.Data;
using DoAn_FrameWork.Helpers;
using DoAn_FrameWork.Interfaces;
using DoAn_FrameWork.Migrations;
using DoAn_FrameWork.Models;
using DoAn_FrameWork.Repository;
using DoAn_FrameWork.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rotativa.AspNetCore;
using System.Text.Encodings.Web;
using System.Text.Unicode;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
var connectionString = builder.Configuration.GetConnectionString("TechnoShop_DB");
builder.Services.AddDbContext<TechnoShop_DBContext>(options =>
{
    options.UseSqlServer(connectionString);
});
builder.Services.AddDbContext<DoAn_FrameWork.Areas.Admin.Models.AdminDBContext>(options =>
{
    options.UseSqlServer(connectionString);
});
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddSession();
builder.Services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.All }));
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddNotyf(config => { config.DurationInSeconds = 4; config.IsDismissable = true; config.Position = NotyfPosition.BottomRight; });
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<DoAn_FrameWork.Areas.Admin.Models.AdminDBContext>()
    .AddDefaultTokenProviders();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();


var app = builder.Build();
IConfiguration configuration = app.Configuration;




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

//config cho rotativa
app.UseRotativa();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseNotyf();

app.UseAuthorization();
app.UseSession();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();