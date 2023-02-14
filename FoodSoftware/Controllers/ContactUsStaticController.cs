using FoodSoftware.Helpers;
using FoodSoftware.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodSoftware.Controllers
{
   
    public class ContactUsStaticController : BaseHomeController
    {
        IContactUsStaticHelper helper;
        public ContactUsStaticController(IContactUsStaticHelper helper)
        {
            this.helper = helper;
        }
        ///// <summary>
        ///// لیست مقالات
        ///// </summary>
        ///// <param name="page"></param>
        ///// <param name="limit"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<IActionResult> Index(int page = 1, int limit = 3)
        //{
        //    var result = await helper.GetAllWithPaginationAsync(page, limit);
        //    return Ok(result);
        //}


        /// <summary>
        /// لیست تماس با ما بدون پیجینگ
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await helper.GetAllAsync();
            return Ok(result);
        }


        /// <summary>
        /// افزودن  متن
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(Articles model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "خطا");
            }
            else
            {
                await helper.InsertAsync(model);
            }
            return Ok("ثبت با موفقیت انجام شد");
        }
        /// <summary>
        /// دریافت   یک تماس  
        /// </summary>
        /// <param name="id">آیدی</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var model = await helper.GetByIdAsync(id);
            return Ok(model);
        }

        /// <summary>
        /// ویرایش تماس 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(Articles model)
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
        /// حذف متن 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "خطا");
            }
            else
            {
                await helper.DeleteAsync(id);
            }
            return Ok("حذف با موفقیت انجام شد");
        }
    }
}
