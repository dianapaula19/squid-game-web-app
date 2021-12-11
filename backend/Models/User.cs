using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class User
    {   
        public Guid Id {get; set;}
        
        [Required]
        [StringLength(13)]
        public string SocialIdentificationNo {get; set;}

        [Required]
        public string Password {get; set;}

        [Required]
        public string Country {get; set;}

        [Required]
        public string Status {get; set;}

        [Required]
        public string Role {get; set;}

        [Required]
        [ForeignKey("FrontMan")]
        public Guid FrontManId {get; set;}

        public virtual Player PlayerData {get; set;}

        public virtual Guard GuardData {get; set;}

        public ICollection<Todo> Todos {get; set;}

    }
}