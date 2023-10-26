using Microsoft.AspNetCore.Mvc;

namespace DoAn_FrameWork.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ditme : Controller
    {
        public IActionResult Index()
        {
            return View("test");
        }
    }
}
