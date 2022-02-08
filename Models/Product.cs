using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Final.Models
{
    public class Product 
    {
        [Display(Name = "ID Producto")]
        public int productId { get; set; }
        [Display(Name = "Producto")]
        public String name { get; set; }
        [Display(Name = "Precio")]
        public double price { get; set; }
        [Display(Name = "Descripcion")]
        public String description { get; set; }
        [Display(Name = "Stock")]
        public int ammount { get; set; }
        [Display(Name = "Categoria")]
        public Category category { get; set; }
        public List<Cart> Carts { get; }
        public List<Purchase> Purchases { get; }
        public List<CartPurchase> CartPurchases { get; set; }
        public List<CartProduct> CartProducts { get; set; }

        public Product( String name, double price, String description, int ammount, Category category)
        {            
            this.name = name;
            this.price = price;
            this.description = description;
            this.ammount = ammount;
            this.category = category;
        }

        public Product() { }               
        

    }
}