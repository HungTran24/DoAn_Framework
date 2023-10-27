using System;
using System.Collections.Generic;

namespace DoAn_FrameWork.Models
{
    public partial class Setting
    {
        public int SettingId { get; set; }
        public string? ConfigKey { get; set; }
        public string? ConfigValue { get; set; }
    }
}
