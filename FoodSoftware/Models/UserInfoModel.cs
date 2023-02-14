using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodSoftware.Models
{
    public class UserInfoModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public bool IsActive { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CreatorId { get; set; }
        public string CreatorName { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public string PDate { get; set; }
        public string Hint { get; set; }
    }
}
