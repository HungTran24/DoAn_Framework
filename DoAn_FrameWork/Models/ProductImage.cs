using System;
using System.Collections.Generic;

namespace DoAn_FrameWork.Models
{
    public partial class ProductImage
    {
        public int ProductImageId { get; set; }
        public string? Image { get; set; }
        public int? ProductId { get; set; }

        public virtual Product? Product { get; set; }
    }
}
