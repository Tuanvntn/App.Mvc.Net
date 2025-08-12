using App.Data;
using App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace App.Areas.Database.Controllers
{

    [Area("Database")]
    [Route("/database-manage/[action]")]
    public class DbManageController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbManageController(AppDbContext dbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult DeleteDb()
        {
            return View();
        }
        [TempData]
        public string StatusMessage { set; get; }


        [HttpPost]
        public async Task<IActionResult> DeleteDbAsync()
        {
            var success = await _dbContext.Database.EnsureDeletedAsync();
            StatusMessage = success ? "Xoa Database thanh cong" : "Khong xoa duoc";
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Migrate()
        {
            await _dbContext.Database.MigrateAsync();
            StatusMessage = "Cap nhat Database thanh cong";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> SeedDataAsync()
        {
            var rolenames = typeof(RoleName).GetFields().ToList();
            foreach (var r in rolenames)
            {
                var rolename = r.GetRawConstantValue();
                var rfound = await _roleManager.FindByNameAsync(rolename.ToString());
                if (rfound == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole(rolename.ToString()));
                }
            }
            var useradmin = await _userManager.FindByNameAsync("admin");
            if (useradmin == null)
            {
                useradmin = new AppUser()
                {
                    UserName = "admin",
                    Email = "admin@example.com",
                    EmailConfirmed = true,
                };
                await _userManager.CreateAsync(useradmin, "admin123");
                await _userManager.AddToRoleAsync(useradmin, RoleName.Administrator);
            }
            StatusMessage = "vua seed Database";
            return RedirectToAction("Index");

        }
    }
}
