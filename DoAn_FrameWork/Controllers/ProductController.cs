using AspNetCoreHero.ToastNotification.Abstractions;
using DoAn_FrameWork.Models;
using DoAn_FrameWork.ModelViews;
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
        public INotyfService _notifyService { get; }
        public ProductController(TechnoShop_DBContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }

        public IActionResult Index(int? page, String SearchString = "", String selectedValue = "")
        {
            try
            {
                var pageNumber = page == null || page <= 0 ? 1 : page.Value;
                var pageSize = 16;
                var lsProducts = _context.Products
                    .AsNoTracking()
                    .OrderByDescending(x => x.ProductId);
                if (selectedValue == "2")
                {
                    lsProducts = _context.Products
                   .AsNoTracking()
                   .OrderByDescending(x => x.ProductPrice);
                }
                else if(selectedValue == "3")
                {
                    lsProducts = _context.Products
                  .AsNoTracking()
                  .OrderBy(x => x.ProductPrice);
                }
                if (SearchString != "")
                {
                    var sanPham = _context.Products.Include(s => s.Category)
                    .Where(x => x.ProductName.ToUpper().Contains(SearchString.ToUpper()));
                    PagedList<Product> searchProduct = new PagedList<Product>(sanPham.ToList(), pageNumber, pageSize);
                    return View(searchProduct);
                }
                //if (selectedValue == "2")
                //{
                //    var sanPham = _context.Products.Include(s => s.Category)
                //    .OrderByDescending(x => x.ProductPrice);
                //    PagedList<Product> sortProduct = new PagedList<Product>(sanPham.ToList(), pageNumber, pageSize);
                //    return View(sortProduct);
                //}
                //else if (selectedValue == "3")
                //{
                //    var sanPham = _context.Products.Include(s => s.Category)
                //   .OrderBy(x => x.ProductPrice);
                //    PagedList<Product> sortProduct = new PagedList<Product>(sanPham.ToList(), pageNumber, pageSize);
                //    return View(sortProduct);
                //}
                PagedList<Product> models = new PagedList<Product>(lsProducts, pageNumber, pageSize);
                ViewBag.CurrentPage = page;
                return View(models);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Lỗi xảy ra: " + ex.Message);
                // Nếu bạn muốn in ra cả stack trace của lỗi
                Console.WriteLine("Stack trace: " + ex.StackTrace);
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult ProductbyCategory(int id, int? page)
        {
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
        public IActionResult List(int id, int page = 1)
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
            //try
            //{

            //    var product = _context.Products.SingleOrDefault(x => x.ProductId == id);
            //    int categoryId = (int)product.CategoryId;
            //    var relative_products = _context.Products
            //        .AsNoTracking()
            //        .Where(x => x.CategoryId == categoryId);
            //    return View(product);
            //}
            //catch
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            try
            {
                var product = _context.Products.SingleOrDefault(x => x.ProductId == id);
       

                if (product == null)
                {
                    _notifyService.Error("This is an product == null");
                    return RedirectToAction("Index", "Home");
                }

                var categoryId = product.CategoryId;
                var relative_products = _context.Products
                    .AsNoTracking()
                    .Where(x => x.CategoryId == categoryId && x.ProductId != id)
                    .ToList();
                var groupProductId = product.GroupProductId;
                var variant_products = _context.Products
                    .AsNoTracking()
                    .Where(x => x.GroupProductId == groupProductId && x.ProductId != id)
                    .ToList();
                var productImages = _context.ProductImages
                    .AsNoTracking()
                    .Where(x => x.ProductId == id)
                    .ToList();
                //thêm ảnh mặc định vào đầu list
                

                var model = new ProductDetailViewModel
                {
                    Product = product,
                    RelativeProducts = relative_products,
                    VariantProducts = variant_products,
                    ProductImages = productImages
                };

                return View(model);
            }
            catch (Exception ex)
            {
                // Log the exception
                _notifyService.Error("This is an Error Notification"+ex);
                TempData["Error"] = "An error occurred: " + ex.Message;
                return RedirectToAction("Index", "Home");
            }

        }
    }
}
