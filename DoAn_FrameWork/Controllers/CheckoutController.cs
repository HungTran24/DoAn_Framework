using AspNetCoreHero.ToastNotification.Abstractions;
using DoAn_FrameWork.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DoAn_FrameWork.ModelViews;
using DoAn_FrameWork.Services;

namespace DoAn_FrameWork.Controllers
{
    
    public class CheckoutController : Controller
    {

        private readonly TechnoShop_DBContext _context;
        public INotyfService _notifyService { get; }

        private readonly IVnPayService _vnpayService;

        public CheckoutController(TechnoShop_DBContext context, INotyfService notifyService, IVnPayService vnPayService)
        {
            _context = context;
            _notifyService = notifyService;
            _vnpayService = vnPayService;
        }

        // GET: CheckoutController
        public ActionResult Index()
        {
            try
            {
                var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
                var username = HttpContext.Session.GetString("Username");
                var customer = _context.Customers.FirstOrDefault(x => x.Username == username);
                if(customer == null)
                {
                    _notifyService.Warning("Bạn cần đăng nhập để thanh toán");
                    return RedirectToAction("Login", "Login");
                   
                }
                CheckOut checkOuts = new CheckOut();
                if (cart == null)
                {
                    _notifyService.Warning("Giỏ hàng trống");
                    return RedirectToAction();
                }
                else
                {
                   checkOuts.cart = cart;
                    checkOuts.customer = customer;
                }
                return View(checkOuts);
            }
            catch(Exception ex)
            {
                _notifyService.Error("Lỗi" + ex.Message);
                return RedirectToAction("Index", "Home");
            }
            
        }

        // GET: CheckoutController/Details/5
        
        //IsValidEmail
        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }
       

        // POST: CheckoutController/Create
        [HttpPost]
        [Route("api/checkout/check")]
        public ActionResult Check([FromBody] ShippingInfo info)
        {
            try
            {
                string name = info.Name;
                string phone = info.Phone;
                string address = info.Address;
                string email = info.Email;
                string note = info.Note;
                
                //check null
                if (name == "" || phone == "" || address == "" || email == "")
                {
                    _notifyService.Warning("Vui lòng nhập đầy đủ thông tin");

                     return Json(new { success = false }); 
                }
                //check phone
                if (phone.Length != 10 || !phone.StartsWith("0"))
                {
                    _notifyService.Warning("Số điện thoại không đúng định dạng");
                    return Json(new { success = false });
                }
                //check email
                if (!IsValidEmail(email))
                {
                    _notifyService.Warning("Email không đúng định dạng");
                    return Json(new { success = false });
                }
                //put data in ses
                HttpContext.Session.Set<ShippingInfo>("ShippingInfo",info);
                return Json(new {success= true });
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        //[Route("api/checkout/checkout")]
        public ActionResult Checkout()
        {
            string name = Request.Form["name"];
            string phone = Request.Form["phone"];
            string address = Request.Form["address"];
            string email = Request.Form["email"];
            string note = Request.Form["note"];
            int method = int.Parse(Request.Form["method"]);
            
            //validate
            //check null
            if (name == "" || phone == "" || address == "" || email == "")
            {
                _notifyService.Warning("Vui lòng nhập đầy đủ thông tin");
                // stay at checkout page
                return RedirectToAction();
 
            }
            //check phone
            if (phone.Length != 10 || !phone.StartsWith("0"))
            {
                _notifyService.Warning("Số điện thoại không đúng định dạng");
                return RedirectToAction("Index","Checkout");
                // trở lại trnag checkout


            }
            //check email
            if (!IsValidEmail(email))
            {
                _notifyService.Warning("Email không đúng định dạng");
                return RedirectToAction("Index", "Checkout");
            }
            //put data in session
            ShippingInfo info = new ShippingInfo
            {
                Name = name, Phone = phone, Address = address, Email = email, Note = note 
            };
            HttpContext.Session.Set<ShippingInfo>("ShippingInfo", info);

            if (method == 0)
            {
                return RedirectToAction("CheckoutCOD");
            }
            else
            {
                return RedirectToAction("CheckoutVnPay");
            }
            
           
        }
        public Boolean StoreOrder(int method)
        {
            string PaymentMethod;
            switch (method)
            {
                case 0:
                    PaymentMethod = "COD";
                    break;
                case 1:
                    PaymentMethod = "VnPay";
                    break;
                default:
                    PaymentMethod = "COD";
                    break;
            }
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
            var info = HttpContext.Session.Get<ShippingInfo>("ShippingInfo");
            var username = HttpContext.Session.GetString("Username");
            var customer = _context.Customers.FirstOrDefault(x => x.Username == username);
            if (customer == null)
            {
                _notifyService.Warning("Bạn cần đăng nhập để thanh toán");
                return false;

            }
            if (cart == null)
            {
                
                
                return false;
            }
            if (info == null)
            {
                
                
                return false;
            }
            //check stock
            foreach (var item in cart)
            {
                var product = _context.Products.Find(item.product.ProductId);
                if (product.StockQuantity < item.amount)
                {
                    
                   
                    return false;
                }
            }
            //add shipping 
            Shipping shipping = new Shipping();
            shipping.ShippingName = customer.CustomerName;
            shipping.ShippingAddress = info.Address;
            shipping.ShippingEmail = info.Email;
            shipping.ShippingPhone = info.Phone;
            shipping.ShippingNote = info.Note;
            _context.Shippings.Add(shipping);
            _context.SaveChanges();

            Payment payment = new Payment();
            payment.PaymentMethod = PaymentMethod;

            payment.PaymentStatus = (method == 0) ? "pending" : "success";
            _context.Payments.Add(payment);
            _context.SaveChanges();


            //add order
            Order order = new Order
            {
                CustomerId = customer.CustomerId,
                CreatedAt = DateTime.Now,
                OrderStatus = "Pending",
                PaymentId = payment.PaymentId,
                ShippingId = shipping.ShippingId,
                OrderTotal = cart.Sum(x => x.product.ProductPrice * x.amount)
            };
            _context.Order.Add(order);
            _context.SaveChanges();

            //add order detail
            foreach (var item in cart)
            {
                OrderDetail orderDetail = new OrderDetail();
                orderDetail.OrderId = order.OrderId;
                orderDetail.ProductId = item.product.ProductId;
                orderDetail.ProductSalesQuantity = item.amount;
                _context.OrderDetails.Add(orderDetail);
                _context.SaveChanges();
            }
            //update stock
            foreach (var item in cart)
            {
                var product = _context.Products.Find(item.product.ProductId);
                if(product==null)
                {
                    _notifyService.Warning("Sản phẩm không tồn tại");
                    Console.WriteLine("Sản phẩm không tồn tại");
                    return false;
                }
                product.StockQuantity -= item.amount;
                _context.Products.Update(product);
                _context.SaveChanges();
            }
            
           
            return true;
        }
       
        public ActionResult PaymentCallback()
        {
            var response = _vnpayService.PaymentExecute(Request.Query);
            if (response != null && response.VnPayResponseCode == "00")
            {
                if (StoreOrder(1))
                {
                    HttpContext.Session.Remove("GioHang");
                    HttpContext.Session.Remove("ShippingInfo");
                    _notifyService.Success("Đặt hàng thành công");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    _notifyService.Error("Đặt hàng thất bại");
                    return RedirectToAction("Index", "Checkout");
                }
            }
            else
            {
                _notifyService.Error("Thanh toán thất bại");
                return RedirectToAction("Index", "Checkout");
            }
        }   
        public ActionResult CheckoutVnPay()
        {
            try
            {
                var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
                var model = HttpContext.Session.Get<ShippingInfo>("ShippingInfo");
                var vnPayModel = new VnPaymentRequestModel
                {
                    Amount = cart.Sum(p => p.Total),
                    CreateDate = DateTime.Now,
                    OrderDescription = $"{model.Name} {model.Phone}",
                    Name = model.Name,
                    OrderId = new Random().Next(1000, 100000)
                };
                return Redirect(_vnpayService.CreatePaymentUrl(HttpContext, vnPayModel));
               
            }
            catch(Exception ex)
            {
                _notifyService.Error("Lỗi" + ex.Message);
                return View();
            }
           
        }
        public ActionResult CheckoutCOD()
        {
            try
            {
                var result = StoreOrder(0);
                Console.WriteLine("Ket qua: "+result);
                if (result)
                {
                    HttpContext.Session.Remove("GioHang");
                    HttpContext.Session.Remove("ShippingInfo");

                    _notifyService.Success("Đặt hàng thành công");
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    _notifyService.Error("Đặt hàng thất bại");
                    return RedirectToAction("Index", "Checkout");
                }
                //clear cart

            }
            catch (Exception ex)
            {
                _notifyService.Error("Lỗi" + ex.Message);
                return RedirectToAction();
            }

        }
    }
}
