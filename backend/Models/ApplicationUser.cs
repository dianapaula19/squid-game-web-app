using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System;

namespace backend.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Country {get; set;}

        public string Status {get; set;}

        public Player PlayerInfo {get; set;}

        public Guard GuardInfo {get; set;}

        public ICollection<ApplicationUserVIP> ApplicationUserVIPs {get; set;}

        public ICollection<Todo> Todos {get; set;}


    }
}