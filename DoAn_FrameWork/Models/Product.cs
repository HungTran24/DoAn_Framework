using System;
using System.Collections.Generic;

namespace DoAn_FrameWork.Models
{
    public partial class Product
    {
        public Product()
        {
            ProductImages = new HashSet<ProductImage>();
            ProductTags = new HashSet<ProductTag>();
        }

        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int? ProductPrice { get; set; }
        public string? ProductImage { get; set; }
        public string? ProductDesc { get; set; }
        public int? CategoryId { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<ProductTag> ProductTags { get; set; }
    }
}
