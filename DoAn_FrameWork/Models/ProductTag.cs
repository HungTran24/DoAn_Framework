﻿using System;
using System.Collections.Generic;

namespace DoAn_FrameWork.Models
{
    public partial class ProductTag
    {
        public int ProductTagId { get; set; }
        public int? ProductId { get; set; }
        public int? TagId { get; set; }

        public virtual Product? Product { get; set; }
        public virtual Tag? Tag { get; set; }
    }
}
