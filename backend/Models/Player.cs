using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace backend.Models 
{
    public class Player {
        
        [Key]
        [ForeignKey("User")]
        public int UserId {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}

        public string Gender {get; set;}
    }
}