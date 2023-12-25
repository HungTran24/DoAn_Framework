using DoAn_FrameWork.Models;

namespace DoAn_FrameWork.ModelViews
{
    public class ProductDetailViewModel
    {
        public Product Product { get; set; }
        public List<Product> RelativeProducts { get; set; }
        public List<Product> VariantProducts { get; set; }

        public List<ProductImage> ProductImages { get; set; }
    }
}
