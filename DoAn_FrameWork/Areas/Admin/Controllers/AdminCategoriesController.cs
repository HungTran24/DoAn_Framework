using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspNetCoreHero.ToastNotification.Abstractions;
using DoAn_FrameWork.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;

namespace DoAn_FrameWork.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminCategoriesController : Controller
    {
        private readonly AdminDBContext _context;
        public INotyfService _notifyService { get; }
        public AdminCategoriesController(AdminDBContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;

        }

        // GET: Admin/AdminCategory
        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 8, string searchTerm = "")
        {
            // Lọc dữ liệu theo tên nếu có giá trị tìm kiếm
            var query = _context.Categories.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(c => c.CategoryName.Contains(searchTerm));
            }

            // Thực hiện phân trang
            var categories = await query
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

            return View(categories);
        }

        // GET: Admin/AdminCategory/Create
        [Authorize(Roles = "Admin, Employee")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                _notifyService.Success("Thêm mới thành công");
                return RedirectToAction(nameof(Index));
            }
            _notifyService.Error("Thêm mới thất bại");
            return View(category);
        }

        // GET: Admin/AdminCategory/Edit/5
        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Admin/AdminCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                    _notifyService.Success("Cập nhật thành công");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
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
            _notifyService.Error("Cập nhật thất bại");
            return View(category);
        }

        // GET: Admin/AdminCategory/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                if (category == null)
                {
                    return NotFound();
                }

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Record deleted successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while deleting the record." });
            }
        }


        // POST: Admin/AdminCategory/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Categories == null)
        //    {
        //        return Problem("Entity set 'TechnoShop_DBContext.Categories'  is null.");
        //    }
        //    var category = await _context.Categories.FindAsync(id);
        //    if (category != null)
        //    {
        //        _context.Categories.Remove(category);
        //    }

        //    await _context.SaveChangesAsync();
        //    _notifyService.Success("Xóa thành công");
        //    return RedirectToAction(nameof(Index));
        //}

        private bool CategoryExists(int id)
        {
            return (_context.Categories?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
    }
}
