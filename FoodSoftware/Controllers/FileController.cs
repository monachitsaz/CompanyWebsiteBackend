using FoodSoftware.Helpers;
using FoodSoftware.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSoftware.Controllers
{

    public class FileController : BaseHomeController
    {
        IWebHostEnvironment _env;
        IFileHelper helper;
        public FileController(IWebHostEnvironment env, IFileHelper helper)
        {
            this._env = env;
            this.helper = helper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await helper.GetAllAsync();
            return Ok(result);
            //foreach (var item in result)
            //{
            //    var filePath = Path.Combine(_env.WebRootPath, "Files", item.Title);
            //    if (!System.IO.File.Exists(filePath))
            //        return NotFound();


            //    using (var stream = new FileStream(filePath, FileMode.Open))
            //    {
            //        await stream.CopyToAsync(memory);
            //    }
             
            //}
          
            //return File(memory, "image/jpg", name);

            //return Ok(memory);

            //return PhysicalFile(file, "image/png");
        }

        //برای نشان دادن بیس64 در ویو باید به شکل زیر بنویسیم
        //اکستنشن آن هم مهم نیست 
        //<img src="data:image/png;base64,@item.ThumbnailBase64"  />
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] Files? formData)
        {

            //مسیر فولدر
            string folderPath = Path.Combine(_env.WebRootPath, "Files");
            var RandomValue = new Random().Next(100000);
            //var name = Guid.NewGuid()+ formData.Title;
            string fileName = $"{formData.Title}_{RandomValue}{Path.GetExtension(formData.Thumbnail.FileName)}";
            formData.Title = fileName;
            string filePath = Path.Combine(_env.WebRootPath, "Files", fileName);

            formData.Url = $"/Files/{fileName}";

            //  فایل را به صورت آرایه در دیتابیس نگه داریم

            using (MemoryStream stream = new MemoryStream())
            {
                formData.Thumbnail.CopyTo(stream);
                formData.ThumbnailByte = stream.ToArray();
            }
           
            formData.ThumbnailFileExtenstion = formData.Thumbnail.ContentType;

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {

                await formData.Thumbnail.CopyToAsync(fileStream);
            
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "خطا");
                }
                else
                {
                    await helper.InsertAsync(formData);
                }
            }
            return Ok("عملیات با موفقیت انجام شد");
        }





        //این اکشن باید در تگ دکمه دانلود قرار بگیرد
        //دانلود فایل بیس 64
        //با این روش اگر فایل در فولدر هم باشد، دسترسی مستقیم به فولدر نمی دهیم و از لحاظ امنیتی بهتر است
        //public async Task<IActionResult> Download(int id)
        //{
        //    var model = new Files();
        //    //فایل زیر باید ارایه ای از بایت باشد تا دانلود شود
        //    return File(model.ThumbnailByte,model.ThumbnailfileExtenstion,"name");
        //}


        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] IFormFile thumbnail, string url, string alt)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        ModelState.AddModelError("", "خطا");
        //    }
        //    else
        //    {
        //        await helper.InsertAsyncformFile(thumbnail, url,alt);
        //    }
        //    return Ok("عملیات با موفقیت انجام شد");

        //}

        /// <summary>
        /// دریافت یک رکورد از سکتورها 
        /// </summary>
        /// <param name="id">آیدی</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var model = await helper.GetByIdAsync(id);
            return Ok(model);
        }

        /// <summary>
        /// ویرایش سکتور 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(Files model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "خطا");
            }
            else
            {
                await helper.UpdateAsync(model);
            }
            return Ok("ویرایش با موفقیت انجام شد");
        }


        /// <summary>
        /// حذف فایل 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var mess = "";
           
            mess= await helper.DeleteAsync2(id);
           
            return Ok(mess);
        }


    }
}
