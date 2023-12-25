using DoAn_FrameWork.Models;
using DoAn_FrameWork.ModelViews;


namespace DoAn_FrameWork.ModelViews
{
    public class CheckOut
    {
        public Customer customer { get; set; }
        public  List<CartItem> cart { get; set; }
        public Shipping shipping { get; set; }

    }
}
