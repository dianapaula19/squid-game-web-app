using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Guard {

        [Key]
        [ForeignKey("User")]
        public int UserId {get; set;}
        public string type {get; set;}

    }
}