using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backend.Models 
{   
    public class FrontMan 
    {
        public Guid Id {get; set;}
        
        [EmailAddress]
        public string Email {get; set;}
        
        public string Password {get; set;}

        public string Country {get; set;}

        public ICollection<User> Users {get; set;}

        public ICollection<VIP> VIPs {get; set;}
    }

}