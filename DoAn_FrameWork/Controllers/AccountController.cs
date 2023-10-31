using Microsoft.AspNetCore.Mvc;

namespace DoAn_FrameWork.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
