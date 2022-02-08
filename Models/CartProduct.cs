

using System.ComponentModel.DataAnnotations;

namespace Final.Models
{
    public class CartProduct
    {
        public int id { get; set; }
        public Cart cart { get; set; }

        public int cartId { get; set; }

        [Display(Name = "Producto")]

        public Product product { get; set; }

        public int productId { get; set; }

        [Display(Name = "Cantidad")]

        public int ammount { get; set; }

        public CartProduct() { }
    }
}
