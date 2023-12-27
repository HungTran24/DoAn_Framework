using System;
using System.Collections.Generic;

namespace DoAn_FrameWork.Areas.Models
{
    public partial class GroupProduct
    {
        public GroupProduct()
        {
            Products = new HashSet<Product>();
        }

        public int GroupProductId { get; set; }
        public string? GroupProductName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
