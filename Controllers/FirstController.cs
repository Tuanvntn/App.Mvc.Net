using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
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
            _env = env; // lay duong dan luu file
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

            //Kiểm tra xem tệp tin có tồn tại không để tránh lỗi
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
            return LocalRedirect(url);
        }
        public IActionResult Google()
        {
            var url = "https://google.com";
            _logger.LogInformation("Chuyen huong den" + url);
            return Redirect(url);
        }

        public IActionResult HelloView(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                username = "khach";
            }

            // return View("/MyView/xinchao.cshtml", username); // duong dan tuyet doi
            // return View("xinchao2", username); // lay duong dan trong thu muc Views/First/xinchao2
            // return View(); // lay file mac dinh nhu ten phuong thuc trong thu muc Views/First
            // return View((object)username); // them thuoc tinh
            return View("xinchao2", username);
        }
        [TempData]
        public string StatusMessage{ set; get; }
        public IActionResult ViewProduct(int? id)
        {
            var product = _productService.FirstOrDefault(p => p.Id == id);;
            if (product == null)
            {
                // TempData["StatusMessage"] = "San pham ko co";
                StatusMessage = "San pham ko co";
                return Redirect(Url.Action("Index", "Home"));
            }
            // /View/First/ViewProduct.cshtml
            // /MyView/First/ViewProduct.cshtml

            //Model
            // return View(product);

            //View Data
            // this.ViewData["product"] = product;
            // ViewData["Title"] = product.Name;
            // return View("ViewProduct2");
            // ViewBag.product = product;
            return View("ViewProduct3");

        }

    }
}