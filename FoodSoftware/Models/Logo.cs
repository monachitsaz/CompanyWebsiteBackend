using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodSoftware.Models
{
    public class Logo
    {
        public int Id { get; set; }
        public string Alt { get; set; }
        public string Link { get; set; }
        public int Image { get; set; }
        public byte[] ThumbnailByte { get; set; }
    }
}
