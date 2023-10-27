using System;
using System.Collections.Generic;

namespace DoAn_FrameWork.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int? ProductPrice { get; set; }
        public string? ProductImage { get; set; }
        public string? ProductDesc { get; set; }
        public int? CategoryId { get; set; }
    }
}
