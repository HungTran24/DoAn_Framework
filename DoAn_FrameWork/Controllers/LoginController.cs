using Microsoft.AspNetCore.Mvc;
using DoAn_FrameWork.Models;
using Microsoft.EntityFrameworkCore;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DoAn_FrameWork.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using System.Runtime.InteropServices;
using System.Text;

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

    public IActionResult ResetPassword()
    {
        return View();
    }
    [HttpPost]
    public IActionResult ResetPassword(Customer user)
    {
        var existingUser = _context.Customers.FirstOrDefault(x => x.CustomerEmail.Equals(user.CustomerEmail));
        if (existingUser != null)
        {
            string newPassword = GenerateRandomPassword();

            // Gửi mật khẩu mới đến email đã nhập
            SendNewPasswordByEmail(existingUser.CustomerEmail, newPassword);

            // Cập nhật mật khẩu mới vào cơ sở dữ liệu
            existingUser.Password = newPassword;
            _context.Update(existingUser);
            _context.SaveChanges();

            return RedirectToAction("Login", "Login");
        }
        else
        {
            return View("ResetPasswordError");
        }
    }

    private string GenerateRandomPassword()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";
        Random random = new Random();
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < 7; i++)
        {
            int index = random.Next(chars.Length);
            sb.Append(chars[index]);
        }

        return sb.ToString();
    }

    private void SendNewPasswordByEmail(string email, string newPassword)
    {
        // Viết mã để gửi email chứa mật khẩu mới sử dụng thư viện MailKit
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("TechnoShop", "minhtamtele1@gmail.com"));
        message.To.Add(new MailboxAddress("", email));
        message.Subject = "ĐỔI MẬT KHẨU - TECHNOSHOP";

        message.Body = new TextPart("plain")
        {
            Text = "Mật khẩu mới của bạn là: " + newPassword
        };

        using (var client = new SmtpClient())
        {
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("minhtamtele1@gmail.com", "zjvh eawi rwtk bkqp");
            client.Send(message);
            client.Disconnect(true);
        }
    }

    private string HashPassword(string password)
    {
        // Viết mã để mã hóa mật khẩu trước khi lưu vào cơ sở dữ liệu
        // Trả về mật khẩu đã mã hóa
        return "123";
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