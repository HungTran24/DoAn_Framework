using DoAn_FrameWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;

namespace DoAn_FrameWork.Controllers
{
    public class ProductController : Controller
    {
        //private readonly TechnoShop_DBContext _context;
        //public ProductController(TechnoShop_DBContext context) { 
        //    _context = context;
        //}
        public IActionResult Index()
        {
            return View();
        }
        //public IActionResult Product_Detail(int id)
        //{
        //    var product = _context.Products.Include(x => x.CategoryId).FirstOrDefault(x => x.ProductId == id);
        //    if (product == null)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}
    }
}
