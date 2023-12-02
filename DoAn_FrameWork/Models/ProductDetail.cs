using System;
using System.Collections.Generic;

namespace DoAn_FrameWork.Models
{
    public partial class ProductDetail
    {
        public int ProductDetailId { get; set; }
        public string? ProductDetailName { get; set; }
        public string? ProductDetailDesc { get; set; }
        public int? ProductId { get; set; }

        public virtual Product? Product { get; set; }
    }
}
