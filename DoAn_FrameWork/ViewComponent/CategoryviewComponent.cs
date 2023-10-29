using DoAn_FrameWork.Models;
using DoAn_FrameWork.Repository;
using Microsoft.AspNetCore.Mvc;
namespace DoAn_FrameWork.ViewComponents
{
    public class CategoryviewComponent : ViewComponent
    {
        private readonly ICategoryRepository _dmsp;
        public CategoryviewComponent(ICategoryRepository CategoryRepository) {
            _dmsp = CategoryRepository;
         }
        public IViewComponentResult Invoke() {
            var dmsp = _dmsp.GetAllCategories().OrderBy(x=>x.CategoryId);
            return View(dmsp);
        }
    }
}
