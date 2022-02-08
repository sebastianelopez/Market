using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Final.Models
{
    public class Cart
    {
        [Display(Name = "ID Carrito")]
        public int cartId { get; set; }
        [Display(Name = "Usuario")]
        public User user { get; set; }
        [Display(Name = "ID Usuario")]
        public int userId { get; set; }
        [Display(Name = "Productos")]
        public ICollection<Product> products { get; } = new List<Product>();

        public List<CartProduct> CartProducts { get; set; }
        
        public Cart()
        {            
        }


        public override string ToString()
        {
            return "\nCart: " +
                "\nId: " + cartId + " - " +
                "Products: " + products;
        }

        public static string parseCartToString(Cart cart)
        {
            return cart.cartId.ToString();
        }
    }
}