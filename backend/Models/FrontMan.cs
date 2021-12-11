using System;
using System.Collections.Generic;

namespace backend.Models 
{   
    public class FrontMan 
    {
        public int Id {get; set;}
        public string Email {get; set;}
        public string Password {get; set;}

        public string Country {get; set;}

        public ICollection<User> Users {get; set;}

        public ICollection<VIP> VIPs {get; set;}
    }

}