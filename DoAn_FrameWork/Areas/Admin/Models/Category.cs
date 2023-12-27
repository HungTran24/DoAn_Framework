using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DoAn_FrameWork.Areas.Admin.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Cần điền tên danh mục")]
        public string? CategoryName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
