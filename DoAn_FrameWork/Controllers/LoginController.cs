using Microsoft.AspNetCore.Mvc;
using DoAn_FrameWork.Models;
using Microsoft.EntityFrameworkCore;

namespace DoAn_FrameWork.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using System.Runtime.InteropServices;

public class LoginController : Controller
{
    TechnoShop_DBContext _context = new TechnoShop_DBContext();
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Login()
    {
        if (HttpContext.Session.GetString("Username") == null)
        {
            return View();
        }
        else
        {
            return RedirectToAction("Index", "Home");
        }
    }
    [HttpPost]
    public IActionResult Login(Customer user)
    {
        if (HttpContext.Session.GetString("Username") == null)
        {
            var u = _context.Customers
                .Where(x => x.Username.Equals(user.Username)
                && x.Password.Equals(user.Password)).FirstOrDefault();
            if (u != null)
            {
                var lsDonhang = _context.Orders
                    .AsNoTracking()
                    .Where(x => x.CustomerId == u.CustomerId)
                    .OrderByDescending(x => x.PaymentId).ToList();
                //ViewBag.Donhang = lsDonhang;
                HttpContext.Session.SetString("Username", u.Username.ToString());
                HttpContext.Session.SetString("CustomerName", u.CustomerName.ToString());
                HttpContext.Session.SetString("CustomerEmail", u.CustomerEmail.ToString());
                return RedirectToAction("Index", "Account");
            }
            else {
                ViewBag.LoginFail = "Sai thông tin tài khoản!";
            }
        }
        return View();
    }
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(Customer user)
    {
        if (ModelState.IsValid)
        {
            var existingUser = _context.Customers.FirstOrDefault(x => x.Username.Equals(user.Username));
            if (existingUser == null)
            {
                if (user.Phone.Length == 10 && user.Phone.StartsWith("0") && user.Password == user.RePassword && IsValidEmail(user.CustomerEmail))
                {
                    _context.Customers.Add(user);
                    _context.SaveChanges();
                    //TempData["RegisterSuccess"] = "Đăng ký thành công!";
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    if (user.Phone.Length != 10 || !user.Phone.StartsWith("0"))
                    {
                        ModelState.AddModelError("Phone", "Số điện thoại không đúng định dạng!");
                    }
                    if (user.Password != user.RePassword)
                    {
                        ModelState.AddModelError("RePassword", "Mật khẩu không khớp!");
                    }
                    if (!IsValidEmail(user.CustomerEmail))
                    {
                        ModelState.AddModelError("CustomerEmail", "Email không đúng định dạng!");
                    }
                }
            }
            else
            {
                ViewBag.RegisterFail = "Tài khoản đã tồn tại!";
            }
        }
        return View();
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }



    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        HttpContext.Session.Remove("Username");
        HttpContext.Session.Remove("CustomerName");
        HttpContext.Session.Remove("CustomerEmail");
        return RedirectToAction("Login", "Login");
    }
}