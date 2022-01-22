using System;

namespace backend.Models
{
    public class ApplicationUserVIP
    {
        public string ApplicationUserId {get; set;}

        public ApplicationUser ApplicationUser {get; set;}

        public Guid VIPId {get; set;}

        public VIP VIP {get; set;}

    }
}