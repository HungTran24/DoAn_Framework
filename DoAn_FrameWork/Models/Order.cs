using System;
using System.Collections.Generic;

namespace DoAn_FrameWork.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public int? ShippingId { get; set; }
        public int? PaymentId { get; set; }
        public int? OrderTotal { get; set; }
        public int? OrderStatus { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Shipping? Shipping { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
