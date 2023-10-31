using DoAn_FrameWork.Data;
using DoAn_FrameWork.Models;
using DoAn_FrameWork.ModelViews;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
namespace DoAn_FrameWork.Controllers;

public class ShoppingCartController : Controller
{
    TechnoShop_DBContext _context = new TechnoShop_DBContext();
    //public List<CartItem> GioHang
    //{
    //    get
    //    {
    //        var gh = HttpContext.Session.Get<List<CartItem>>("GioHang");
    //        if (gh == default(List<CartItem>))
    //        {
    //            gh = new List<CartItem>();
    //        }
    //        return gh;
    //    }
    //}
    //[HttpPost]
    //[Route("api/cart/add")]
    //public IActionResult AddToCart(int productId, int? amount)
    //{
    //    List<CartItem> gioHang = GioHang;
    //    try
    //    {
    //        CartItem item = GioHang.SingleOrDefault(p => p.product.ProductId == productId);
    //        if (item != null)
    //        {
    //            if (amount.HasValue)
    //            {
    //                item.amount = amount.Value;
    //            }
    //            else
    //            {
    //                item.amount++;
    //            }
    //        }
    //        else
    //        {
    //            Product hh = _context.Products.SingleOrDefault(p => p.ProductId == productId);
    //            item = new CartItem
    //            {
    //                amount = amount.HasValue ? amount.Value : 1,
    //                product = hh
    //            };
    //            gioHang.Add(item);
    //        }
    //        HttpContext.Session.Set<List<CartItem>>("GioHang", gioHang);
    //        return Json(new { success = true });
    //    }
    //    catch
    //    {
    //        return Json(new { success = false });
    //    }
    //}

    //[HttpPost]
    //[Route("api/cart/remove")]
    //public ActionResult Remove(int productId)
    //{
    //    try
    //    {
    //        List<CartItem> gioHang = GioHang;
    //        CartItem item = GioHang.SingleOrDefault(p => p.product.ProductId == productId);
    //        if (item != null)
    //        {
    //            gioHang.Remove(item);
    //        }
    //        HttpContext.Session.Set<List<CartItem>>("GioHang", gioHang);
    //        return Json(new { success = true });
    //    }
    //    catch
    //    {
    //        return Json(new { success = false });
    //    }

    //}

    public IActionResult Index()
    {
        return View();
    }
}
