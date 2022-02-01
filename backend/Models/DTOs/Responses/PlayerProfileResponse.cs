namespace backend.Models.DTOS.Responses
{
    public class PlayerProfileResponse 
    {
        public ApplicationUserProfileResponse ApplicationUserProfileResponse {get; set;}
        public string FirstName {get; set;}

        public string LastName {get; set;}

        public string Gender {get; set;}
    }
}