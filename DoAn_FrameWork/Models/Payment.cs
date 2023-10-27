using System;
using System.Collections.Generic;

namespace DoAn_FrameWork.Models
{
    public partial class Payment
    {
        public int PaymentId { get; set; }
        public string? PaymentMethod { get; set; }
        public string? PaymentStatus { get; set; }
    }
}
