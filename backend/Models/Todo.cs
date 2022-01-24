using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Models.DTOS.Requests;

namespace backend.Models {
    public class Todo {

        [Key]
        public Guid Id {get; set;}
        
        [Required]
        public string Name {get; set;}
        
        [Required]
        public DateTime Deadline {get; set;}

        public ApplicationUser ApplicationUser {get; set;}

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserForeignKey {get; set;}
        
    }
}