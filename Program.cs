using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using App.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using App.ExtendMethods;
using App.Models;
using System.Configuration;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

 
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppMvcConnectionString")));

builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    //{0} ten Action
    //{1} ten Controller
    //{2} Ten Area
    options.ViewLocationFormats.Add("/MyView/{1}/{0}.cshtml");
});
// builder.Services.AddSingleton<ProductService>();
// builder.Services.AddSingleton<ProductService, ProductService>();
// builder.Services.AddSingleton(typeof(ProductService));
builder.Services.AddSingleton(typeof(ProductService), typeof(ProductService));
builder.Services.AddSingleton<PlanetService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.AddStatusCodePage(); // tra respone loi tu 400-599

app.UseRouting();

app.UseAuthorization();


    
app.MapAreaControllerRoute(
    name: "productManage",
    areaName: "productManage",
    pattern: "productManage/{controller=Product}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapGet("/sayhi", async (context) =>
{
    await context.Response.WriteAsync($"Hello ASP.NET {DateTime.Now}");
});



app.MapRazorPages();
app.Run();
