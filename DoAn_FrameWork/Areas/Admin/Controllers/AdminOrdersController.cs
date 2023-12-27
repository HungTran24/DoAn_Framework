using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClosedXML.Excel;
using System.Data;
using DocumentFormat.OpenXml.Office2010.Excel;
using Rotativa;
using Rotativa.AspNetCore;
using DoAn_FrameWork.Areas.Admin.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;

namespace DoAn_FrameWork.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminOrdersController : Controller
    {
        private readonly AdminDBContext _context;
        public INotyfService _notifyService { get; }

        public AdminOrdersController(AdminDBContext context, INotyfService notyfService)
        {
            _context = context;
            _notifyService = notyfService;
        }

        // GET: Admin/AdminOrders
        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 8, string searchTerm = "")
        {
            var query = _context.Orders.Include(o => o.Customer).AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(c => c.Customer.CustomerName.Contains(searchTerm));
            }

            // Thực hiện phân trang
            var orders = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize).Include(o => o.Customer).Include(o => o.Payment).Include(o => o.Shipping)
                .ToListAsync();

            // Tính toán thông tin phân trang
            int totalItems = await query.CountAsync();
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // Truyền giá trị tìm kiếm để hiển thị lại trong ô tìm kiếm
            ViewBag.SearchTerm = searchTerm;

            return View(orders);

            //var technoShop_DBContext = _context.Orders.Include(o => o.Customer).Include(o => o.Payment).Include(o => o.Shipping);
            //return View(await technoShop_DBContext.ToListAsync());
        }

        // GET: Admin/AdminOrders/Details/5
        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Payment)
                .Include(o => o.Shipping)
                .Include(o => o.OrderDetails).ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                _notifyService.Error("Cập nhật trạng thái thất bại");
                return NotFound();
            }
            order.OrderStatus = status;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            _notifyService.Success("Cập nhật trạng thái thành công");
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        [Authorize(Roles = "Admin, Employee")]
        public async Task<FileResult> ExportordersInExcel()
        {
            var orders = await _context.Orders.Include(o => o.Customer).Include(o => o.Payment).Include(o => o.Shipping).ToListAsync();
            var fileName = "orders.xlsx";
            return GenerateExcel(fileName, orders);
        }

        [Authorize(Roles = "Admin, Employee")]
        private FileResult GenerateExcel(string fileName, IEnumerable<Order> orders)
        {
            DataTable dataTable = new DataTable("orders");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                 new DataColumn("ID"),
                 new DataColumn("Customer Name"),
                 new DataColumn("Shipping"),
                 new DataColumn("Payment"),
                 new DataColumn("Description"),
                 new DataColumn("Total"),
                 new DataColumn("Status"),
                 new DataColumn("Created At"),
            });

            foreach (var order in orders)
            {
                dataTable.Rows.Add(
                    order.OrderId,
                    order.Customer?.CustomerName,
                    order.Shipping?.ShippingName,
                    order.Payment?.PaymentMethod,
                    order.OrderTotal,
                    order.OrderStatus,
                    order.CreatedAt
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

        [Authorize(Roles = "Admin, Employee")]
        public async Task<ActionResult> GeneratePDF(int id)
        {
            var model = await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Payment)
            .Include(o => o.Shipping)
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .FirstOrDefaultAsync(m => m.OrderId == id);
            var pdf = new ViewAsPdf("Invoice", model) // Replace "Your_View_Name" with the name of your view
            {
                FileName = "Invoice.pdf" // Name of the exported PDF file
            };

            // Return the PDF as ActionResult
            return pdf;
        }

        private bool OrderExists(int id)
        {
            return (_context.Orders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
