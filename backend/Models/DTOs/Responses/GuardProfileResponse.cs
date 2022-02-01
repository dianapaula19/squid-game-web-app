namespace backend.Models.DTOS.Responses
{   
    public class GuardProfileResponse
    {
        public ApplicationUserProfileResponse ApplicationUserProfileResponse {get; set;}
        public string Type {get; set;}
    }
}