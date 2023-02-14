using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodSoftware.Models
{
    public class RefreshTokenModel
    {
        public Guid UserId { get; set; }
        public Guid RefreshToken { get; set; }
    }
}
