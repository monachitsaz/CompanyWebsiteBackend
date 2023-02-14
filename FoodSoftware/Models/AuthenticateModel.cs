using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodSoftware.Models
{
    public class AuthenticateModel
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string FullName { get; set; }
        public int RoleId { get; set; }
        public Guid UserId { get; set; }
    }
}
