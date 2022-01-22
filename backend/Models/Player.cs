
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.Models 
{
    public class Player {

        [Key]
        public Guid PlayerId {get; set;}

        [Required]
        [StringLength(30)]
        public string FirstName {get; set;}
        
        [Required]
        [StringLength(30)]
        public string LastName {get; set;}
        
        [Required]
        public string Gender {get; set;}

        public string ApplicationUserForeignKey {get; set;}

        public ApplicationUser ApplicationUser {get; set;}
    }
}