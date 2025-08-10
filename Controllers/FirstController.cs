using Microsoft.AspNetCore.Mvc;
using App.Services;
using App.Models;

namespace App.Controllers
{
   
    public class FirstController : Controller
    {
        private readonly ILogger<FirstController> _logger;
        private readonly IWebHostEnvironment _env;
        private readonly ProductService _productService;

        public FirstController(ILogger<FirstController> logger, IWebHostEnvironment env, ProductService productService)
        {
            _logger = logger;
            _env = env; // Lấy đường dẫn lưu file
            _productService = productService;
        }

        public string Index()
        {
            _logger.LogWarning("thong bao");
            _logger.LogInformation("Action");
            return "Toi la Index cua First";
        }

        public IActionResult Bird()
        {
            // Sử dụng Path.Combine() để tạo đường dẫn tuyệt đối đến tệp tin trong wwwroot
            // Su dung ContentRootPath de lay duong dan vao thu muc nam ngoai root
            string filePath = Path.Combine(_env.WebRootPath, "images", "Birds.jpg");

            // Kiểm tra xem tệp tin có tồn tại không để tránh lỗi
            if (!System.IO.File.Exists(filePath))
            {
                // Trả về lỗi 404 nếu tệp không tồn tại
                return NotFound();
            }

            // Đọc tệp tin thành mảng bytes
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            // Trả về tệp tin, chỉ định loại MIME và tên tệp
            return File(fileBytes, "image/jpg");
        }

        public IActionResult IphonePrice()
        {
            return Json(
                new
                {
                    productName = "IphoneX",
                    Price = 1000,
                }
            );
        }

        public IActionResult Privacy()
        {
            var url = Url.Action("Privacy", "Home");
            _logger.LogInformation("Chuyen huong den" + url);
            // Sử dụng '!' để khẳng định url không null
            return LocalRedirect(url!);
        }

        public IActionResult Google()
        {
            var url = "https://google.com";
            _logger.LogInformation("Chuyen huong den" + url);
            // Url là chuỗi hằng nên không thể null
            return Redirect(url);
        }

        public IActionResult HelloView(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                username = "khach";
            }
            return View("xinchao2", username);
        }

        [TempData]
        public string? StatusMessage { set; get; }

        [Route("khach-hang/danh-sach/{ten_danh_sach}")]
        public IActionResult Danh_sach(string? ten_danh_sach)
        {
            return Content($"Danh sach khach hang: {ten_danh_sach}");
        }
        [AcceptVerbs("POST", "GET")]
        public IActionResult ViewProduct(int? id)
        {
            var product = _productService.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                StatusMessage = "San pham ko co";
                // Url.Action() luôn trả về chuỗi, nên an toàn
                return Redirect(Url.Action("Index", "Home")!);
            }

            // ViewBag.product = product;
            // return View("ViewProduct3"); // c3 su dung cho du lieu nho

            // ViewData["product"] = product;
            // return View("ViewProduct2"); c2 tuong tu c3
            
            return View("ViewProduct", product); // c1 toi uu nhat
        }
    }
}