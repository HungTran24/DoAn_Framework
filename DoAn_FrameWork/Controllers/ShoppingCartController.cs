using DoAn_FrameWork.Data;
using DoAn_FrameWork.Models;
using DoAn_FrameWork.ModelViews;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Helpers;

namespace DoAn_FrameWork.Controllers
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, System.Text.Json.JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : System.Text.Json.JsonSerializer.Deserialize<T>(value);
        }
    }

    public class ShoppingCartController : Controller
    {
        private readonly TechnoShop_DBContext _context;
        private readonly INotyfService _notifyService;

        public ShoppingCartController(TechnoShop_DBContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }

        public List<CartItem> GioHang
        {
            get
            {
                var gh = HttpContext.Session.Get<List<CartItem>>("GioHang");
                if (gh == default(List<CartItem>))
                {
                    gh = new List<CartItem>();
                }
                return gh;
            }
        }

        [HttpPost]
        [Route("api/cart/add")]
        public IActionResult AddToCart(int productId, int? amount)
        {
            Console.WriteLine("productId:" + productId);
            Console.WriteLine("amount:" + amount);

            List<CartItem> gioHang = GioHang;
            try
            {
                Product hh = _context.Products.SingleOrDefault(p => p.ProductId == productId);
                CartItem item = GioHang.SingleOrDefault(p => p.product.ProductId == productId);
                int temp_amount = 0;
                if (item != null)
                {
                    if (amount.HasValue)
                    {
                        temp_amount = amount.Value;
                    }
                    else
                    {
                        temp_amount = item.amount + 1;
                    }
                    if (temp_amount > hh.StockQuantity)
                    {
                        _notifyService.Error("Không đủ sản phẩm");
                        return Json(new { success = false });
                    }
                    else
                    {
                        item.amount = temp_amount;
                        _notifyService.Success("Thêm vào giỏ hàng thành công");
                    }
                }
                else
                {
                    temp_amount = amount.HasValue ? amount.Value : 1;
                    if (temp_amount > hh.StockQuantity)
                    {
                        _notifyService.Error("Không đủ sản phẩm");
                        return Json(new { success = false });
                    }
                    else
                    {
                        item = new CartItem
                        {
                            amount = temp_amount,
                            product = hh
                        };
                        gioHang.Add(item);
                        _notifyService.Success("Thêm vào giỏ hàng thành công");
                    }
                }
                HttpContext.Session.Set<List<CartItem>>("GioHang", gioHang);
                return Json(new { success = true });
            }
            catch (Exception e)
            {
                _notifyService.Error("Lỗi:" + e.Message);
                return Json(new { error = true });
            }
        }

        [HttpPost]
        [Route("api/cart/remove")]
        public ActionResult Remove(int productId)
        {
            try
            {
                List<CartItem> gioHang = GioHang;
                Console.WriteLine("productId:" + productId);
                CartItem item = gioHang.SingleOrDefault(p => p.product.ProductId == productId);
                if (item != null)
                {
                    gioHang.Remove(item);
                    HttpContext.Session.Set<List<CartItem>>("GioHang", gioHang);
                    _notifyService.Success("Xóa sản phẩm thành công");
                    return Json(new { success = true });
                }
                
                return Json(new { success = false });
            }
            catch
            {
                _notifyService.Error("Lỗi");
                return Json(new { success = false });
            }
        }
        [HttpPost]
        [Route("api/cart/update")]
        public ActionResult Update(int productId, int amount)
        {
            try
            {
                List<CartItem> gioHang = GioHang;
                Console.WriteLine("productId:" + productId);
                CartItem item = gioHang.SingleOrDefault(p => p.product.ProductId == productId);
                if (item != null)
                {
                    if (amount > item.product.StockQuantity)
                    {
                        _notifyService.Error("Không đủ sản phẩm");
                        return Json(new { success = false });
                    }
                    else
                    {
                        item.amount = amount;
                        HttpContext.Session.Set<List<CartItem>>("GioHang", gioHang);
                        _notifyService.Success("Cập nhật giỏ hàng thành công");
                        return Json(new { success = true });
                    }
                }
                return Json(new { success = false });
            }
            catch
            {
                _notifyService.Error("Lỗi");
                return Json(new { success = false });
            }
        }   

        public IActionResult Index()
        {
            return View(GioHang);
        }
    }
}
