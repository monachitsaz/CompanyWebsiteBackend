using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodSoftware.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public int RoleID { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public string PasswordSalt { get; set; }
        public string Email { get; set; }
    }
}
