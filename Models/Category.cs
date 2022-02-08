using System;
using System.ComponentModel.DataAnnotations;

namespace Final.Models
{
    public class Category 
    {
        [Display(Name = "ID Categoria")]
        public int categoryId { get; set; }
        [Display(Name = "Categoria")]
        public String name { get; set; }

        public Category( String name)
        {            
            this.name = name;
        }

        public Category()
        {
            
        }
        public int CompareTo(Category other)
        {
            return name.CompareTo(other.name);
        }

        
    }
}