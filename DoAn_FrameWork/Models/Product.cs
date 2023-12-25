using System;
using System.Collections.Generic;

namespace DoAn_FrameWork.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
            ProductImages = new HashSet<ProductImage>();
        }

        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int? ProductPrice { get; set; }
        public string? ProductImage { get; set; }
        public string? ProductDesc { get; set; }
        public double? DiscountPercentage { get; set; }
        public int? CategoryId { get; set; }
        public int? SaleQuantity { get; set; }
        public int? StockQuantity { get; set; }
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
