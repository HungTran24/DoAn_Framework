using DoAn_FrameWork.Areas.Admin.Models;
using DoAn_FrameWork.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAn_FrameWork.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly AdminDBContext _context;
        public HomeController(AdminDBContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin, Employee")]
        public IActionResult Index()
        {
            HomeViewModel home = new HomeViewModel();
            //Lấy các thông tin số
            home.NumberOfOrders = _context.Orders.Count();
            home.NumberOfOrdersShipped = _context.Orders.Count(o => o.OrderStatus == "Shipped");
            home.NumberOfProducts = _context.Products.Count();
            home.NumberOfCustomers = _context.Customers.Count();
            home.NumberOfGroupProducts = _context.GroupProducts.Count();
            home.NumberOfUsers = _context.Users.Count();
            home.NumberOfOrdersDone = _context.Orders.Count(o => o.OrderStatus == "Done");
            home.NumberOfProductSaled = (int)_context.OrderDetails
            .Where(od => _context.Orders.Any(o => o.OrderId == od.OrderId && o.OrderStatus == "Done"))
            .Sum(od => od.ProductSalesQuantity);

            //Lay list top san pham va khach hang
            home.Customers = _context.CustomerHomeVMs.FromSqlRaw("EXEC GetTopFiveCustomersWithMostOrders").ToList();
            home.Products = _context.ProductHomeVMs.FromSqlRaw("EXEC GetTopFiveBestSellingProducts").ToList();

            return View(home);
        }


        [HttpGet]
        [Authorize(Roles = "Admin, Employee")]
        public IActionResult GetSaleChartData()
        {
            // Lọc dữ liệu từ cơ sở dữ liệu và sau đó thực hiện các phép tính trong bộ nhớ
            var orders = _context.Orders
                .ToList() // Lấy tất cả dữ liệu từ cơ sở dữ liệu vào bộ nhớ
                .Where(o => Convert.ToDateTime(o.CreatedAt).Year == DateTime.Now.Year);

            var orderData = orders
                .GroupBy(o => new { Convert.ToDateTime(o.CreatedAt).Month })
                .Select(g => new
                {
                    MonthValue = g.Key.Month,
                    NumOfOrder = g.Count(),
                    MoneyEarn = g.Sum(o => o.OrderTotal)
                })
                .OrderBy(x => x.MonthValue)
                .ToList();


            return Json(orderData);
        }




        [HttpGet]
        [Authorize(Roles = "Admin, Employee")]
        public IActionResult GetCusChartData()
        {
            var orders = _context.Orders
                .ToList()
                .Where(o => Convert.ToDateTime(o.CreatedAt).Year == DateTime.Now.Year);

            var customerData = orders
                .GroupBy(o => new { Convert.ToDateTime(o.CreatedAt).Month })
                .Select(g => new
                {
                    MonthValue = g.Key.Month,
                    NumberOfCustomer = g.Select(o => o.CustomerId).Distinct().Count()
                })
                .OrderBy(x => x.MonthValue)
                .ToList();

            return Json(customerData);

        }

    }
}
