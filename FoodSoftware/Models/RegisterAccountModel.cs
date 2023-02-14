using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodSoftware.Models
{
    public class RegisterAccountModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int RoleId { get; set; }
        public int CreatorId { get; set; }
        public string FullName { get; set; }
        public string PDate { get; set; }
        public string Hint { get; set; }
    }
}
