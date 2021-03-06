using System.ComponentModel.DataAnnotations;

namespace backend.Models.DTOS.Requests
{
    public class UserRegistrationDto
    {   
        [Required]
        public string Username {get; set;}
        
        [Required]
        [EmailAddress]
        public string Email {get; set;}

        [Required]
        public string Password {get; set;}

        [Required]
        public string Role {get; set;}

        public string Country {get; set;}

        public PlayerInfo PlayerInfo {get; set;}


    }
}