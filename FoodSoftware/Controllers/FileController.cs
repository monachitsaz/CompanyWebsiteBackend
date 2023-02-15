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
        }

  
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] Files? formData)
        {
            //folder path
            string folderPath = Path.Combine(_env.WebRootPath, "Files");
            var RandomValue = new Random().Next(100000);
            string fileName = $"{formData.Title}_{RandomValue}{Path.GetExtension(formData.Thumbnail.FileName)}";
            formData.Title = fileName;
            string filePath = Path.Combine(_env.WebRootPath, "Files", fileName);

            formData.Url = $"/Files/{fileName}";

            //  file as array

            using (MemoryStream stream = new MemoryStream())
            {
                formData.Thumbnail.CopyTo(stream);
                formData.ThumbnailByte = stream.ToArray();
            }
           
            formData.ThumbnailFileExtenstion = formData.Thumbnail.ContentType;

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await formData.Thumbnail.CopyToAsync(fileStream);           
                await helper.InsertAsync(formData);  
            }
            return Ok("عملیات با موفقیت انجام شد");
        }
 

        /// <summary>
        ///Get one record
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var model = await helper.GetByIdAsync(id);
            return Ok(model);
        }

        /// <summary>
        /// Update sector
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(Files model)
        {        
            await helper.UpdateAsync(model);
            return Ok("ویرایش با موفقیت انجام شد");
        }


        /// <summary>
        /// Delete file
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
