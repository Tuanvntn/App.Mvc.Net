using System.Numerics;
using App.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace App.Controllers
{
    public class PlanetController : Controller
    {
        private readonly PlanetService _planetService;
        private readonly ILogger<PlanetController> _logger;

        public PlanetController(PlanetService planetService, ILogger<PlanetController> logger)
        {
            _planetService = planetService;
            _logger = logger;
        }
        // [Route("Danh-sach-cac-hanh-tinh.html")]
        public ActionResult Index()
        {
            return View();
        }
        [BindProperty(SupportsGet = true, Name = "action")]
        public string Name { get; set; }

        public IActionResult Mercury()
        {
            var planet = _planetService.FirstOrDefault(p => p.Name == Name);
            return View("Detail", planet);
        }
        public IActionResult Earth()
        {
            var planet = _planetService.FirstOrDefault(p => p.Name == Name);
            return View("Detail", planet);
        }
        public IActionResult Jupiter()
        {
            var planet = _planetService.FirstOrDefault(p => p.Name == Name);
            return View("Detail", planet);

        }
        public IActionResult Mars()
        {
            var planet = _planetService.FirstOrDefault(p => p.Name == Name);
            return View("Detail", planet);
        }
        public IActionResult Neptune()
        {
            var planet = _planetService.FirstOrDefault(p => p.Name == Name);
            return View("Detail", planet);
        }
        public IActionResult Saturn()
        {
            var planet = _planetService.FirstOrDefault(p => p.Name == Name);
            return View("Detail", planet);
        }
        public IActionResult Venus()
        {
            var planet = _planetService.FirstOrDefault(p => p.Name == Name);
            return View("Detail", planet);
        }
        public IActionResult Uranus()
        {
            var planet = _planetService.FirstOrDefault(p => p.Name == Name);
            return View("Detail", planet);
        }
        [Route("hanhtinh/{id:int}")]
        public IActionResult PlanetInfo(int id)
        {
            var planet = _planetService.FirstOrDefault(p => p.Id == id);
            return View("Detail", planet);
        }

    }
}
