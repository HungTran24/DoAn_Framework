using AspNetCoreHero.ToastNotification.Abstractions;
using DoAn_FrameWork.Areas.Admin.Models;
using DoAn_FrameWork.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DoAn_FrameWork.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminAccountController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AdminDBContext _context;
        public INotyfService _notifyService { get; }
        public AdminAccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, AdminDBContext context, INotyfService notyfService, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _notifyService = notyfService;
            _roleManager = roleManager;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 8, string searchTerm = "")
        {
            // Lọc dữ liệu theo tên nếu có giá trị tìm kiếm
            var query = _userManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(c => c.UserName.Contains(searchTerm));
            }

            // Thực hiện phân trang
            var users = await query
                .OrderBy(c => c.UserName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            List<RegisterViewModel> userVMs = new List<RegisterViewModel>();

            foreach (var user in users)
            {
                IList<string> Roles = await _userManager.GetRolesAsync(user);

                string role = Roles.Any() ? Roles[0] : "No role"; // Sử dụng Roles.Any() để kiểm tra danh sách rỗng

                RegisterViewModel userVM = new RegisterViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Role = role
                };
                userVMs.Add(userVM);
            }

            int totalItems = await query.CountAsync();
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // Truyền giá trị tìm kiếm để hiển thị lại trong ô tìm kiếm
            ViewBag.SearchTerm = searchTerm;

            return View(userVMs);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["Roles"] = new SelectList(_roleManager.Roles, "Name");
            return View();
        }

        // POST: Admin/AdminRole/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterViewModel userVM)
        {
            if (ModelState.IsValid)
            {
                var userExist = await _userManager.FindByNameAsync(userVM.UserName);

                if (userExist == null)
                {
                    var user = new AppUser { UserName = userVM.UserName, Email = userVM.Email };
                    var result = await _userManager.CreateAsync(user, userVM.PassWord);
                    //Cấp quyền cho người dùng
                    await _userManager.AddToRoleAsync(user, userVM.Role);
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
                    ModelState.AddModelError("", "User đã tồn tại");
                }
            }
            ViewData["Roles"] = new SelectList(_roleManager.Roles, "Name");
            return View(userVM);
        }






        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }
            var user = await _userManager.FindByEmailAsync(loginVM.EmailAdress);
            if (user != null)
            {
                var passwordChecked = await _userManager.CheckPasswordAsync(user, loginVM.PassWord);
                if (passwordChecked)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.PassWord, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }


            TempData["Error"] = "Wrong credentials, Please try again";
            return View(loginVM);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "AdminAccount");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var userRole = await _userManager.GetRolesAsync(user);
            var userVM = new RegisterViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                PassWord = "",
                Role = userRole[0]
            };
            ViewData["Roles"] = new SelectList(_roleManager.Roles, "Name");
            return View(userVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, RegisterViewModel userVM)
        {


            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    //Cap nhat mat khau
                    var newPassword = userVM.PassWord;
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var resetPasswordResult = await _userManager.ResetPasswordAsync(user, token, newPassword);
                    //Cap nhat role
                    var roles = await _userManager.GetRolesAsync(user);
                    await _userManager.RemoveFromRolesAsync(user, roles.ToArray()); // Xóa tất cả các vai trò hiện có
                    var rolesToAdd = new List<string> { userVM.Role }; // Thay thế bằng danh sách vai trò cần cập nhật
                    var addToRolesResult = await _userManager.AddToRolesAsync(user, rolesToAdd);
                    //Cap nhat thon tin user
                    user.UserName = userVM.UserName;
                    user.Email = userVM.Email;

                    var updateResult = await _userManager.UpdateAsync(user);
                    if (resetPasswordResult.Succeeded && addToRolesResult.Succeeded && updateResult.Succeeded)
                    {
                        _notifyService.Success("Cập nhật thành công");
                        return RedirectToAction(nameof(Index));

                    }

                    _notifyService.Error("Cập nhật thất bại");
                    return View(userVM);
                }
                else
                    return NotFound();
            }
            return View(userVM);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                var result = await _userManager.DeleteAsync(user);
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

        private bool RoleExists(string id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
