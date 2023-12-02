using System;
using System.Collections.Generic;

namespace DoAn_FrameWork.Models
{
    public partial class Product
    {
        public Product()
        {
            Options = new HashSet<Option>();
            OrderDetails = new HashSet<OrderDetail>();
            ProductDetails = new HashSet<ProductDetail>();
            ProductImages = new HashSet<ProductImage>();
        }

        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int? ProductPrice { get; set; }
        public string? ProductImage { get; set; }
        public string? ProductDesc { get; set; }
        public int? DiscountPercentage { get; set; }
        public int? CategoryId { get; set; }
        public int? SaleQuantity { get; set; }
        public int? StockQuantity { get; set; }
        public int? WarrantyTime { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ICollection<Option> Options { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}
