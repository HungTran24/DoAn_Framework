using System;
using System.Collections.Generic;

namespace DoAn_FrameWork.Models
{
    public partial class Customer
    {
        public int CustomerId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? RePassword { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? Phone { get; set; }
    }
}
