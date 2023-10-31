using DoAn_FrameWork.Models;

namespace DoAn_FrameWork.ModelViews
{
    public class CartItem
    {
        public Product product { get; set; }
        public int amount { get; set; }
        public double Total => amount * product.ProductPrice.Value;
    }
}
