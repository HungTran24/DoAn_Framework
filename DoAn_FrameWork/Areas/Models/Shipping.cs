using System;
using System.Collections.Generic;

namespace DoAn_FrameWork.Areas.Models
{
    public partial class Shipping
    {
        public Shipping()
        {
            Orders = new HashSet<Order>();
        }

        public int ShippingId { get; set; }
        public string? ShippingName { get; set; }
        public string? ShippingAddress { get; set; }
        public string? ShippingPhone { get; set; }
        public string? ShippingEmail { get; set; }
        public string? ShippingNote { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
