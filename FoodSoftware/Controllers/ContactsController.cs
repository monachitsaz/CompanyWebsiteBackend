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

    public class ContactsController : BaseHomeController
    {
        IContactsHelper helper;
        public ContactsController(IContactsHelper helper)
        {
            this.helper = helper;
        }
 


        /// <summary>
        /// لیست تماس ها بدون پیجینگ
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await helper.GetAllAsync();
            return Ok(result);
        }


        /// <summary>
        /// افزودن  تماس
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(Contacts model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "خطا");
            }
            else
            {
                await helper.InsertAsync(model);
            }
            return Ok("عملیات با موفقیت انجام شد");
        }
        /// <summary>
        /// دریافت یک تماس  
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
        public async Task<IActionResult> Update(Contacts model)
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
        /// حذف تماس 
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
