using System.Collections.Generic;
namespace backend.Configuration
{
    public class AuthResult
    {
        public string Token {get; set;}

        public string Email {get; set;}

        public string Role {get; set;} 

        public bool Success {get; set;}

        public List<string> Errors {get; set;}
    }
}