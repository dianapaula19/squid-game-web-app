using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Guard {

        [Key]
        public Guid GuardId {get; set;}
        public string Type {get; set;}

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserForeignKey {get; set;}

        public ApplicationUser ApplicationUser {get; set;}

    }
}