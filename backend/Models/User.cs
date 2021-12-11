using System.Collections.Generic;

namespace backend.Models
{
    public class User
    {   

        public int Id {get; set;}
        
        public string SocialIdentificationNo {get; set;}

        public string Password {get; set;}

        public string Country {get; set;}

        public string Status {get; set;}

        public string Role {get; set;}

        public Player PlayerData {get; set;}

        public Guard GuardData {get; set;}

        public ICollection<Task> Tasks {get; set;}

    }
}