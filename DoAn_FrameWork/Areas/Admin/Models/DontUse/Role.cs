﻿using System;
using System.Collections.Generic;

namespace DoAn_FrameWork.Areas.Admin.Models.DontUse
{
    public partial class Role
    {
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? NormalizedName { get; set; }
        public string? ConcurrencyStamp { get; set; }
    }
}
