using System;
using System.ComponentModel.DataAnnotations;

namespace Final.Models
{
    public class User
    {        
        [Display(Name="ID Usuario")]
        public int userId { get; set; }
        [Display(Name = "Documento")]
        public Int64 dni { get; set; }
        [Display(Name = "Nombre de Usuario")]
        public String name { get; set; }
        [Display(Name = "Apellido")]
        public String lastName { get; set; }
        [Display(Name = "Email")]
        public String email { get; set; }
        [Display(Name = "CUIT/CUIL")]
        public Int64 CUITCUIL { get; set; }
        public String password { get; set; }
        [Display(Name = "Tipo de Usuario")]
        public String userType { get; set; }
        public Cart cart { get; set; }

        public int attemps { get; set; }

        public bool locked { get; set; }

        public User(Int64 dni, String name, String lastName, String email, Int64 CUITCUIL, String password, String userType, Cart cart, int attemps, bool locked)
        {            
            this.dni = dni;
            this.name = name;
            this.lastName = lastName;
            this.email = email;
            this.CUITCUIL = CUITCUIL;
            this.password = password;
            this.userType = userType;
            this.cart = cart;
            this.attemps = attemps;
            this.locked = locked;
        }

        public User( String name,  String email, Int64 CUITCUIL, String password, String userType, Cart cart, int attemps, bool locked)
        {
            
            this.name = name;
            this.email = email;
            this.CUITCUIL = CUITCUIL;
            this.password = password;
            this.userType = userType;
            this.cart = cart;
            this.attemps = attemps;
            this.locked = locked;
        }

        public User( String name, String email, Int64 CUITCUIL, String password, String userType, int attemps, bool locked)
        {            
            this.name = name;
            this.email = email;
            this.CUITCUIL = CUITCUIL;
            this.password = password;
            this.userType = userType;
            this.attemps = attemps;
            this.locked = locked;
        }

        public User(){
            
        }

    }

}