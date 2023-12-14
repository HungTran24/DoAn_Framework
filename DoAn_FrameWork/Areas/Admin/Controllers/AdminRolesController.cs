using AspNetCoreHero.ToastNotification.Abstractions;
using DoAn_FrameWork.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Abstractions;

namespace DoAn_FrameWork.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminRolesController : Controller
    {
        private readonly AdminDBContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        public INotyfService _notifyService { get; }
        public AdminRolesController(AdminDBContext context, INotyfService notifyService, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _notifyService = notifyService;
            _roleManager = roleManager;
        }

        // GET: Admin/AdminRole
        public async Task<IActionResult> Index(int page = 1, int pageSize = 8, string searchTerm = "")
        {
            // Lọc dữ liệu theo tên nếu có giá trị tìm kiếm
            var query = _context.Roles.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(c => c.Name.Contains(searchTerm));
            }

            // Thực hiện phân trang
            var roles = await query
                .OrderBy(c => c.Name)
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

            return View(roles);
        }

        // GET: Admin/AdminRole/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminRole/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                var roleExist = await _roleManager.RoleExistsAsync(role.Name);

                if (!roleExist)
                {
                    var result = await _roleManager.CreateAsync(role);

                    if (result.Succeeded)
                    {
                        _notifyService.Success("Thêm mới thành công");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Vai trò đã tồn tại");
                }
            }
            return View(role);
        }

        //public async Task<IActionResult> Create([Bind("Name")] IdentityRole Role)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        //_context.Add(Role);
        //        //await _context.SaveChangesAsync();
        //        //_notifyService.Success("Thêm mới thành công");
        //        //return RedirectToAction(nameof(Index));
        //    }
        //    return View(Role);
        //}
        public async Task<IActionResult> Edit(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }
        // GET: Admin/AdminRole/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Roles == null)
        //    {
        //        return NotFound();
        //    }

        //    var role = await _context.Roles.FindAsync(id);
        //    if (role == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(role);
        //}

        // POST: Admin/AdminRole/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name")] IdentityRole role)
        {
            if (id != role.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(role);
                    await _context.SaveChangesAsync();
                    _notifyService.Success("Cập nhật thành công");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleExists(role.Id))
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
            return View(role);
        }

        // GET: Admin/AdminRole/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role == null)
                {
                    return NotFound();
                }

                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return Json(new { success = true, message = "Record deleted successfully." });
                }
                else
                {
                    return Json(new { success = false, message = "An error occurred while deleting the record." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while deleting the record." });
            }
        }



        // POST: Admin/AdminRole/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Roles == null)
        //    {
        //        return Problem("Entity set 'TechnoShop_DBContext.Roles'  is null.");
        //    }
        //    var Role = await _context.Roles.FindAsync(id);
        //    if (Role != null)
        //    {
        //        _context.Roles.Remove(Role);
        //    }

        //    await _context.SaveChangesAsync();
        //    _notifyService.Success("Xóa thành công");
        //    return RedirectToAction(nameof(Index));
        //}

        private bool RoleExists(string id)
        {
            return (_context.Roles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
