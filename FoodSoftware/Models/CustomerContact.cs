using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodSoftware.Models
{
    public class CustomerContact
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string Sector { get; set; }
        public string IndActivity { get; set; }
        public string Address { get; set; }
        public string Desc { get; set; }
        public DateTime CreationDate { get; set; }
    }
  
}
