using DoAn_FrameWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAn_FrameWork.Controllers
{
    public class AccountController : Controller
    {
        TechnoShop_DBContext _context = new TechnoShop_DBContext();
        public IActionResult Index()
        {
            //get order list theo customer
            var customerName = HttpContext.Session.GetString("Username");
            var customerid = _context.Customers.FirstOrDefault(x => x.Username == customerName).CustomerId;
            //var orderList = _context.Orders.Include(x => x.Customer).Where(x => x.CustomerId == customerid).Include(x => x.OrderDetails).ThenInclude(x => x.Product).ToList();
            var orderList = _context.Orders.Where(x => x.CustomerId == customerid).ToList();
            //view bag
            ViewBag.OrderList = orderList;
            return View();
            
        }
        [HttpPost]
        public IActionResult Index(Customer user)
        {
            var existingUser = _context.Customers.FirstOrDefault(x => x.CustomerEmail.Equals(user.CustomerEmail));
            if (existingUser != null)
            {
                string newPassword = user.RePassword;

                existingUser.Password = newPassword;
                _context.Update(existingUser);
                _context.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("Password", "Mật khẩu hiện tại chưa đúng!");
            }
            if (user.Password != user.RePassword)
            {
                ModelState.AddModelError("RePassword", "Mật khẩu nhập lại không khớp!");
            }
            //await Task.Delay(1500);
            return View();
        }
    }
}
