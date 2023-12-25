using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoAn_FrameWork.Interfaces;
using DoAn_FrameWork.Areas.Admin.ViewModels;
using ClosedXML.Excel;
using System.Data;
using DoAn_FrameWork.Areas.Admin.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;

namespace DoAn_FrameWork.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminProductsController : Controller
    {
        private readonly AdminDBContext _context;
        private readonly IPhotoService _photoService;
        public INotyfService _notifyService { get; }

        public AdminProductsController(AdminDBContext context, IPhotoService photoService, INotyfService notyfService)
        {
            _context = context;
            _photoService = photoService;
            _notifyService = notyfService;
        }

        // GET: Admin/AdminProducts
        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 8, string searchTerm = "")
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(c => c.ProductName.Contains(searchTerm));
            }

            // Thực hiện phân trang
            var products = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize).Include(p => p.Category).Include(p => p.GroupProduct)
                .ToListAsync();

            // Tính toán thông tin phân trang
            int totalItems = await query.CountAsync();
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // Truyền giá trị tìm kiếm để hiển thị lại trong ô tìm kiếm
            ViewBag.SearchTerm = searchTerm;

            return View(products);

            //var technoShop_DBContext = _context.Products.Include(p => p.Category).Include(p => p.GroupProduct);
            //return View(await technoShop_DBContext.ToListAsync());
        }

        // GET: Admin/AdminProducts/Details/5
        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.GroupProduct)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/AdminProducts/Create
        [Authorize(Roles = "Admin, Employee")]
        public IActionResult Create()
        {
            ViewData["Categories"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewData["GroupProducts"] = new SelectList(_context.GroupProducts, "GroupProductId", "GroupProductName");
            //ViewBag.Categories = _context.Categories.ToList();
            //ViewBag.GroupProducts = _context.GroupProducts.ToList();

            return View();
        }

        // POST: Admin/AdminProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel productVM, List<IFormFile> ProductImagesVM)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(product);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                var existingGroupProduct = _context.GroupProducts.FirstOrDefault(g => g.GroupProductName == productVM.GroupProduct.GroupProductName);
                if (existingGroupProduct == null && productVM.GroupProduct.GroupProductName != null)
                {
                    _context.Add(productVM.GroupProduct);
                    await _context.SaveChangesAsync();
                }

                var result = await _photoService.AddPhotoAsync(productVM.ProductImage);
                var product = new Product
                {
                    ProductName = productVM.ProductName,
                    ProductPrice = productVM.ProductPrice,
                    ProductImage = result.Url.ToString(),
                    ProductDesc = productVM.ProductDesc,
                    DiscountPercentage = productVM.DiscountPercentage,
                    CategoryId = productVM.CategoryId,
                    SaleQuantity = productVM.SaleQuantity,
                    StockQuantity = productVM.StockQuantity,
                    WarrantyTime = productVM.WarrantyTime,
                    GroupProductId = productVM.GroupProduct.GroupProductId,
                };
                if (product.GroupProductId == 0) product.GroupProductId = null;
                _context.Add(product);
                await _context.SaveChangesAsync();

                //upload images
                foreach (IFormFile file in ProductImagesVM)
                {
                    var resUpload = await _photoService.AddPhotoAsync(file);
                    var productImages = new ProductImage
                    {
                        Image = resUpload.Url.ToString(),
                        ProductId = product.ProductId,
                    };
                    _context.Add(productImages);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["Categories"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewData["GroupProducts"] = new SelectList(_context.GroupProducts, "GroupProductId", "GroupProductName");
            return View(productVM);
        }

        // GET: Admin/AdminProducts/Edit/5
        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            ViewData["Categories"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewData["GroupProducts"] = new SelectList(_context.GroupProducts, "GroupProductId", "GroupProductName");
            product.ProductImages = _context.ProductImages.Where(x => x.ProductId == product.ProductId).ToList();
            return View(product);
        }

        // POST: Admin/AdminProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([Bind("ProductId,ProductName,ProductPrice,ProductImage,ProductDesc,DiscountPercentage,CategoryId,SaleQuantity,StockQuantity,WarrantyTime,GroupProductId")] Product product, IFormFile? ProductImage, List<IFormFile>? ProductImages)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (ProductImage != null)
                    {
                        var result = await _photoService.AddPhotoAsync(ProductImage);
                        product.ProductImage = result.Url.ToString();
                    }

                    if (ProductImages.Count != 0)
                    {
                        foreach (var image in ProductImages)
                        {
                            var resUpload = await _photoService.AddPhotoAsync(image);
                            var productImages = new ProductImage
                            {
                                Image = resUpload.Url.ToString(),
                                ProductId = product.ProductId,
                            };
                            _context.Add(productImages);
                            await _context.SaveChangesAsync();
                        }
                    }

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.CategoryId);
            ViewData["GroupProductId"] = new SelectList(_context.GroupProducts, "GroupProductId", "GroupProductId", product.GroupProductId);
            return View(product);
        }

        // GET: Admin/AdminProducts/Delete/5
        [HttpGet]
        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                _context.ProductImages.RemoveRange(_context.ProductImages.Where(pi => pi.ProductId == id));
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Record deleted successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while deleting the record." });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Employee")]
        public async Task<FileResult> ExportProductsInExcel()
        {
            var products = await _context.Products.Include(p => p.Category).Include(p => p.GroupProduct).ToListAsync();
            var fileName = "products.xlsx";
            return GenerateExcel(fileName, products);
        }

        private FileResult GenerateExcel(string fileName, IEnumerable<Product> products)
        {
            DataTable dataTable = new DataTable("Products");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                 new DataColumn("ID"),
                 new DataColumn("Product Name"),
                 new DataColumn("Price"),
                 new DataColumn("Image"),
                 new DataColumn("Description"),
                 new DataColumn("Discount percent"),
                 new DataColumn("Category"),
                 new DataColumn("Group product"),
                 new DataColumn("Sale quantity"),
                 new DataColumn("Stock quantity"),
                 new DataColumn("Warranty time"),
            });

            foreach (var product in products)
            {
                dataTable.Rows.Add(
                    product.ProductId,
                    product.ProductName,
                    product.ProductPrice,
                    product.ProductImage,
                    product.ProductDesc,
                    product.DiscountPercentage,
                    product.Category?.CategoryName,
                    product.GroupProduct?.GroupProductName,
                    product.SaleQuantity,
                    product.StockQuantity,
                    product.WarrantyTime
                    );
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                };
            };

        }

        // POST: Admin/AdminProducts/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Products == null)
        //    {
        //        return Problem("Entity set 'TechnoShop_DBContext.Products'  is null.");
        //    }
        //    var product = await _context.Products.FindAsync(id);
        //    if (product != null)
        //    {
        //        _context.Products.Remove(product);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
