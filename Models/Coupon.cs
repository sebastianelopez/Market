using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Final.Models
{

    public class Coupon
    {
        [Display(Name = "ID Cupon")]
        public int id { get; set; }
        [Display(Name = "Codigo")]
        public String code { get; set; }
        [Display(Name = "Porcentaje de descuento")]
        public int percentage { get; set; }
        public List<PurchaseCoupon> purchaseCoupon { get; set; }
        public ICollection<Purchase> purchases { get; } = new List<Purchase>();
        


        public Coupon(String code, int percentage)
        {
            this.code = code;
            this.percentage = percentage;
        }
    }
}
