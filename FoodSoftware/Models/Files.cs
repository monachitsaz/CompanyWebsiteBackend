using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodSoftware.Models
{
    public class Files
    {
        public int Id { get; set; }
        public IFormFile Thumbnail { get; set; } //from view to action
        public string Url { get; set; }
        public string Alt { get; set; }
        public string Title { get; set; }

        //زمانیکه می خواهیم فایل را به صورت آرایه در دیتابیس نگه داریم
        public string ThumbnailBase64 { get; set; } //from action to view
        public byte[] ThumbnailByte { get; set; }
        public string ThumbnailFileExtenstion { get; set; }

    }
}
