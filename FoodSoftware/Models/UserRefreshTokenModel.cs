using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodSoftware.Models
{
    public class UserRefreshTokenModel
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RefreshToken { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsValid { get; set; }
    }
}
