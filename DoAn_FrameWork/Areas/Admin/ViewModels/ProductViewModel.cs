using DoAn_FrameWork.Areas.Admin.Models;
using System.ComponentModel.DataAnnotations;

namespace DoAn_FrameWork.Areas.Admin.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            OrderDetails = new HashSet<OrderDetail>();
            ProductImages = new HashSet<ProductImage>();
        }
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Cần điền tên sản phẩm")]

        public string? ProductName { get; set; }
        [Required(ErrorMessage = "Cần điền giá sản phẩm")]


        public int? ProductPrice { get; set; }
        [Required(ErrorMessage = "Cần thêm ảnh sản phẩm")]


        public IFormFile? ProductImage { get; set; }
        [Required(ErrorMessage = "Cần ghi mô tả sản phẩm")]


        public string? ProductDesc { get; set; }
        [Required(ErrorMessage = "Cần thêm mức giảm giá")]
        [Range(0, double.MaxValue, ErrorMessage = "Mức giảm giá phải lớn hơn hoặc bằng 0")]
        public double? DiscountPercentage { get; set; }
        [Required(ErrorMessage = "Cần chọn danh mục sản phẩm")]

        public int? CategoryId { get; set; }
        [Required(ErrorMessage = "Cần thêm số lượng bán")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng bán phải lớn hơn hoặc bằng 0")]

        public int? SaleQuantity { get; set; }

        [Required(ErrorMessage = "Cần thêm số lượng kho")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng kho phải lớn hơn hoặc bằng 0")]
        public int? StockQuantity { get; set; }

        [Required(ErrorMessage = "Cần thêm thời gian bảo hành")]
        [Range(0, int.MaxValue, ErrorMessage = "Thời gian bảo hành phải lớn hơn hoặc bằng 0")]
        public int? WarrantyTime { get; set; }

        public int? GroupProductId { get; set; }
        public string? Color { get; set; }
        public string? Options { get; set; }

        public virtual Category? Category { get; set; }
        public virtual GroupProduct? GroupProduct { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}
