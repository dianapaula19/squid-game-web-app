using System.Collections.Generic;
namespace backend.Models 
{
    public class VIP 
    {
        public int Id {get; set;}
        public string Email {get; set;}
        public string Country {get; set;}

        public string Mask {get; set;}

        public ICollection<FrontMan> FrontMen {get; set;}
    }
}