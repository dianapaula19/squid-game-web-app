using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace backend.Models
{   
    public class ApplicationUserVIP
    {   
        [Key]
        public Guid Id {get; set;}
        
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId {get; set;}

        public ApplicationUser ApplicationUser {get; set;}

        [ForeignKey("VIP")]
        public Guid VIPId {get; set;}

        public VIP VIP {get; set;}

    }
}