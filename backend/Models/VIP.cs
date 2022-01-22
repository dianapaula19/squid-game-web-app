using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
namespace backend.Models 
{
    public class VIP 
    {   
        [Key]
        public Guid Id {get; set;}
        
        [EmailAddress]
        public string Email {get; set;}
        
        public string Country {get; set;}

        public string Mask {get; set;}

        public ICollection<ApplicationUserVIP> ApplicationUserVIPs {get; set;}
    }
}