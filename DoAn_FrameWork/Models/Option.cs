using System;
using System.Collections.Generic;

namespace DoAn_FrameWork.Models
{
    public partial class Option
    {
        public Option()
        {
            ProductDetails = new HashSet<ProductDetail>();
            ProductImages = new HashSet<ProductImage>();
        }

        public int OptionId { get; set; }
        public string? OptionName { get; set; }
        public int? ProductId { get; set; }
        public int? OptionPrice { get; set; }
        public string? OptionImage { get; set; }
        public int? OptionSaleQuantity { get; set; }
        public int? OptionStockQuantity { get; set; }

        public virtual Product? Product { get; set; }
        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}
