using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using DoAn_FrameWork.Areas.Admin.Models;

namespace DoAn_FrameWork.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminGroupProductsController : Controller
    {
        private readonly AdminDBContext _context;

        public AdminGroupProductsController(AdminDBContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminGroupProducts
        public async Task<IActionResult> Index(int page = 1, int pageSize = 8, string searchTerm = "")
        {
            var query = _context.GroupProducts.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(c => c.GroupProductName.Contains(searchTerm));
            }

            // Thực hiện phân trang
            var groupProducts = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Tính toán thông tin phân trang
            int totalItems = await query.CountAsync();
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // Truyền giá trị tìm kiếm để hiển thị lại trong ô tìm kiếm
            ViewBag.SearchTerm = searchTerm;

            return View(groupProducts);

            //return _context.GroupProducts != null ? 
            //            View(await _context.GroupProducts.ToListAsync()) :
            //            Problem("Entity set 'TechnoShop_DBContext.GroupProducts'  is null.");
        }

        // GET: Admin/AdminGroupProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GroupProducts == null)
            {
                return NotFound();
            }

            var groupProduct = await _context.GroupProducts
                .FirstOrDefaultAsync(m => m.GroupProductId == id);
            if (groupProduct == null)
            {
                return NotFound();
            }

            return View(groupProduct);
        }

        // GET: Admin/AdminGroupProducts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminGroupProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GroupProductId,GroupProductName")] GroupProduct groupProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(groupProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(groupProduct);
        }

        // GET: Admin/AdminGroupProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GroupProducts == null)
            {
                return NotFound();
            }

            var groupProduct = await _context.GroupProducts.FindAsync(id);
            if (groupProduct == null)
            {
                return NotFound();
            }
            return View(groupProduct);
        }

        // POST: Admin/AdminGroupProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GroupProductId,GroupProductName")] GroupProduct groupProduct)
        {
            if (id != groupProduct.GroupProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groupProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupProductExists(groupProduct.GroupProductId))
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
            return View(groupProduct);
        }

        // GET: Admin/AdminGroupProducts/Delete/5
        // POST: Admin/AdminGroupProducts/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                var groupProduct = await _context.GroupProducts.FindAsync(id);
                if (groupProduct == null)
                {
                    return NotFound();
                }

                _context.GroupProducts.Remove(groupProduct);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Record deleted successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while deleting the record." });
            }
        }


        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.GroupProducts == null)
        //    {
        //        return Problem("Entity set 'TechnoShop_DBContext.GroupProducts'  is null.");
        //    }
        //    var groupProduct = await _context.GroupProducts.FindAsync(id);
        //    if (groupProduct != null)
        //    {
        //        _context.GroupProducts.Remove(groupProduct);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool GroupProductExists(int id)
        {
            return (_context.GroupProducts?.Any(e => e.GroupProductId == id)).GetValueOrDefault();
        }
    }
}
