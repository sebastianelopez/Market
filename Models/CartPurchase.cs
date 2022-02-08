
using System.Collections.Generic;

namespace Final.Models
{
    public class CartPurchase
    {
        public int id { get; set; }
        public Purchase purchase { get; set; }

        public int purchaseId { get; set; }

        public Product product { get; set; }

        public int productId { get; set; }

        public List<Coupon> couponsAplied { get; set; }

        public CartPurchase() { }
    }
}
