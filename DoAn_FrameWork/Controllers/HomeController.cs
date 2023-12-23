using DoAn_FrameWork.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DoAn_FrameWork.Models.Authentication;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

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

        public IActionResult Index(int? page, String SearchString = "")
        {
            //if (SearchString != "")
            //{
            //    var sanPham = _context.Products.Include(s => s.Category)
            //        .Where(x => x.ProductName.ToUpper().Contains(SearchString.ToUpper()));
            //    return View(sanPham.ToList());
            //}
            try
            {
                var pageNumber = page == null || page <= 0 ? 1 : page.Value;
                var pageSize = 8;
                var lsProducts = _context.Products
                    .AsNoTracking()
                    .OrderByDescending(x => x.SaleQuantity);
                if (SearchString != "")
                {
                    var sanPham = _context.Products.Include(s => s.Category)
                    .Where(x => x.ProductName.ToUpper().Contains(SearchString.ToUpper()));
                    PagedList<Product> searchProduct = new PagedList<Product>(sanPham.ToList(), pageNumber, pageSize);
                    return View(searchProduct);
                }
                PagedList<Product> models = new PagedList<Product>(lsProducts, pageNumber, pageSize);
                ViewBag.CurrentPage = page;
                return View(models);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
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