using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodSoftware.Models
{
    public class Articles
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int Image { get; set; }
        public string Alt { get; set; }
        public byte[] ThumbnailByte { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
