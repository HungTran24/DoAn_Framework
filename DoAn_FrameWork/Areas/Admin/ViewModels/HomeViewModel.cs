using DoAn_FrameWork.Areas.Admin.Models;

namespace DoAn_FrameWork.Areas.Admin.ViewModels
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            Customers = new List<CustomerHomeVM>();
            Products = new List<ProductHomeVM>();
        }
        public List<CustomerHomeVM>? Customers { get; set; }
        public List<ProductHomeVM>? Products { get; set; }
        public int NumberOfCustomers { get; set; }
        public int NumberOfOrders { get; set; }
        public int NumberOfOrdersShipped { get; set; }
        public int NumberOfProducts { get; set; }
        public int NumberOfGroupProducts { get; set; }
        public int NumberOfUsers { get; set; }
        public int NumberOfOrdersDone { get; set; }
        public int NumberOfProductSaled { get; set; }
    }
}
