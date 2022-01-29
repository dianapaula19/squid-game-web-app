using backend.Configuration;

namespace backend.Models.DTOS.Requests
{
    public class LoginResponse : AuthResult
    {
        public string Status {get; set;}
    }
}