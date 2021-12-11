using System;

namespace backend.Models {
    public class Task {

        public int Id {get; set;}
        public string Name {get; set;}
        public DateTime Deadline {get; set;}

        public User User {get; set;}
    }
}