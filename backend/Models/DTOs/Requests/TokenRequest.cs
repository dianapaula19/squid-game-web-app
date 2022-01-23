using System.ComponentModel.DataAnnotations;

namespace backend.Models.DTOS.Requests
{
    public class TokenRequest
    {
        [Required]
        public string Token {get; set;}

        [Required]
        public string RefreshToken {get; set;}
    }
}