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
  
        public IActionResult Index(int? page, String SearchString = "", String sort = "")
        {
            try
            {
                //sort = Request.Form["sort-select"];
                var pageNumber = page == null || page <= 0 ? 1 : page.Value;
                var pageSize = 8;
                var lsProducts = _context.Products
                    .AsNoTracking()
                    .OrderByDescending(x => x.ProductId);
                if (SearchString != "") {
                    var sanPham = _context.Products.Include(s => s.Category)
                    .Where(x => x.ProductName.ToUpper().Contains(SearchString.ToUpper()));
                    PagedList<Product> searchProduct = new PagedList<Product>(sanPham.ToList(), pageNumber, pageSize);
                    return View(searchProduct);
                }
                if (sort == "2")
                {
                    var sanPham = _context.Products.Include(s => s.Category)
                    .OrderByDescending(x => x.ProductPrice);
                    PagedList<Product> searchProduct = new PagedList<Product>(sanPham.ToList(), pageNumber, pageSize);
                    return View(searchProduct);
                }
                else if (sort == "3") {
                    var sanPham = _context.Products.Include(s => s.Category)
                   .OrderBy(x => x.ProductPrice);
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
        public IActionResult Product_Detail(int id)
        {
            try 
            {
 
                var product = _context.Products.SingleOrDefault(x => x.ProductId == id);
                return View(product);
            }
            catch 
            {
                return RedirectToAction("Index", "Home");
            }
         
        }
    }
}
