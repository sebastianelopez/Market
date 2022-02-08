using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Final.Models
{
    public class Purchase 
    {
        [Display(Name = "ID Compra")]
        public int purchaseId { get; set; }
        [Display(Name = "Total")]
        public double total { get; set; }
        [Display(Name = "Productos")]
        public ICollection<Product> products { get; } = new List<Product>();
        [Display(Name = "Cupones")]
        public ICollection<Coupon> coupons { get; } = new List<Coupon>();
        [Display(Name = "Comprador")]
        public User buyer { get; set; }
        public List<CartPurchase> cartPurchases { get; set; }

        public List<PurchaseCoupon> purchaseCoupon { get; set; }

        public Purchase( double total, User buyer)
        {            
            this.total = total;
            this.buyer = buyer;
        }
        public Purchase()
        {
            
        }

    }
}