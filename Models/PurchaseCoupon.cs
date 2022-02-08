using System;
using System.Collections.Generic;
using System.Text;

namespace Final.Models
{
    public class PurchaseCoupon
    {
        public int id { get; set; }
        public Purchase purchase { get; set; }

        public int purchaseId { get; set; }

        public Coupon coupon { get; set; }

        public int couponId { get; set; }        

        public PurchaseCoupon() { }
    }
}
