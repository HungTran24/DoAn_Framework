using DoAn_FrameWork.Models;

namespace DoAn_FrameWork.Areas.Admin.ViewModels
{
    public class ProductImagesViewModel
    {
        public int ProductImageId { get; set; }
        public IFormFile? Image { get; set; }
        public int? ProductId { get; set; }

        public virtual Product? Product { get; set; }
    }
}
