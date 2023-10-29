using DoAn_FrameWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
//using PagedList.Core;
using X.PagedList;

namespace DoAn_FrameWork.Controllers
{
    public class ProductController : Controller
    {
        private readonly TechnoShop_DBContext _context;
        public ProductController(TechnoShop_DBContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? page)
        {
            try
            {
                var pageNumber = page == null || page <= 0 ? 1 : page.Value;
                var pageSize = 8;
                var lsProducts = _context.Products
                    .AsNoTracking()
                    .OrderByDescending(x => x.ProductId);
                PagedList<Product> models = new PagedList<Product>(lsProducts, pageNumber, pageSize);
                ViewBag.CurrentPage = page;
                return View(models);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult ProductbyCategory(int id, int? page){
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 8;
            var lsProducts = _context.Products
                .AsNoTracking()
                .Where(x => x.CategoryId == id)
                .OrderByDescending(x => x.ProductId);
            PagedList<Product> models = new PagedList<Product>(lsProducts, pageNumber, pageSize);
            ViewBag.CurrentPage = page;
            return View(models);
        }
        //[Route("/{Alias}--{id}", Name = "ListProduct")]
        public IActionResult List(int id, int page=1)
        {
            try
            {
                var pageSize = 10;
                var cat = _context.Categories.Find(id);
                var lsProducts = _context.Products
                    .AsNoTracking()
                    .Where(x => x.CategoryId == id)
                    .OrderByDescending(x => x.ProductId);
                PagedList<Product> models = new PagedList<Product>(lsProducts, page, pageSize);
                ViewBag.CurrentPage = page;
                ViewBag.CurrentCat = page;
                return View(models);
            }
            catch 
            {
                return RedirectToAction("Index", "Home");
            }
            
        }
        //[Route("/{Alias}--{id}.html", Name = "Product_Detail")]
        public IActionResult Product_Detail(int id)
        {
            try 
            {
                //var product = _context.Products.Include(x => x.Category).FirstOrDefault(x => x.ProductId == id);
                var product = _context.Products.SingleOrDefault(x => x.ProductId == id);
                //if (product == null)
                //{
                //    return RedirectToAction("Index");
                //}
                return View();
            }
            catch 
            {
                return RedirectToAction("Index", "Home");
            }
         
        }
    }
}
