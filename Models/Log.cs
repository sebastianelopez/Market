using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Final.Models
{
    public class Log
    {
        
        public int logId { get; set; }

        [Display(Name = "Tipo de evento")]
        public int eventType { get; set; }

        [Display(Name = "Usuario")]
        public User user { get; set; }
        [Display(Name = "Fecha de creación")]
        public DateTime createdAt { set; get ; }

        public Log(User user, int eventType)
        {
            this.user = user;
            this.eventType = eventType;
            this.createdAt = DateTime.Now;
        }

        public Log()
        {
            
        }
    }
}
