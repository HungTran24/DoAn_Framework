using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DoAn_FrameWork.Areas.Admin.Models
{
    public partial class GroupProduct
    {
        public GroupProduct()
        {
            Products = new HashSet<Product>();
        }

        public int GroupProductId { get; set; }
        [Required(ErrorMessage = "Cần điền tên nhóm sản phẩm")]

        public string? GroupProductName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
