using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using App.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using App.ExtendMethods;
using App.Models;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


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
 // Truy cập IdentityOptions
builder.Services.Configure<IdentityOptions> (options => {
    // Thiết lập về Password
    options.Password.RequireDigit = false; // Không bắt phải có số
    options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
    options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
    options.Password.RequireUppercase = false; // Không bắt buộc chữ in
    options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
    options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

    // Cấu hình Lockout - khóa user
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes (5); // Khóa 5 phút
    options.Lockout.MaxFailedAccessAttempts = 3; // Thất bại 3 lầ thì khóa
    options.Lockout.AllowedForNewUsers = true;

    // Cấu hình về User.
    options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;  // Email là duy nhất


    // Cấu hình đăng nhập.
    options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
    options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại
    options.SignIn.RequireConfirmedAccount = true; 
    
});      

builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = "/login/";
    options.LogoutPath = "/logout/";
    options.AccessDeniedPath = "/khongduoctruycap.html";
});  

builder.Services.AddAuthentication()
        .AddGoogle(options => {
            // Sử dụng builder.Configuration thay vì Configuration
            var gconfig = builder.Configuration.GetSection("Authentication:Google");
            options.ClientId = gconfig["ClientId"];
            options.ClientSecret = gconfig["ClientSecret"];
            options.CallbackPath =  "/dang-nhap-tu-google";
        })
        .AddFacebook(options =>
        {
            // Sử dụng builder.Configuration thay vì Configuration
            var fconfig = builder.Configuration.GetSection("Authentication:Facebook");
            options.AppId = fconfig["AppId"];
            options.AppSecret = fconfig["AppSecret"];
            options.CallbackPath = "/dang-nhap-tu-facebook";
        });
                   
      
builder.Services.AddIdentity<AppUser, IdentityRole>()
                    .AddEntityFrameworkStores<AppDbContext>()
                    .AddDefaultTokenProviders();
                    
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

app.UseAuthentication();
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
