using DoAn_FrameWork.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DoAn_FrameWork.Models.Authentication;
using Microsoft.EntityFrameworkCore;

namespace DoAn_FrameWork.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        TechnoShop_DBContext _context = new TechnoShop_DBContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        //[Authentication]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}