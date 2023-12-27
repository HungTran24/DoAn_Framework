using System;
using System.Collections.Generic;

namespace DoAn_FrameWork.Areas.Models
{
    public partial class OrderDetail
    {
        public int OrderDetailsId { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? ProductSalesQuantity { get; set; }

        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
    }
}
