using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models {
    public class Todo {

        [Key]
        public Guid Id {get; set;}
        
        [Required]
        public string Name {get; set;}
        
        [Required]
        public DateTime Deadline {get; set;}
        
    }
}