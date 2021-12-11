using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Guard {

        [Key]
        [ForeignKey("User")]
        public Guid UserId {get; set;}
        public string Type {get; set;}

    }
}