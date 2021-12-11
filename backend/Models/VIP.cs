using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
namespace backend.Models 
{
    public class VIP 
    {
        public Guid Id {get; set;}
        
        [EmailAddress]
        public string Email {get; set;}
        
        public string Country {get; set;}

        public string Mask {get; set;}

        public ICollection<FrontMan> FrontMen {get; set;}
    }
}