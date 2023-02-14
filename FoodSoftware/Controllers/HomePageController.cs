using FoodSoftware.Helpers;
using FoodSoftware.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodSoftware.Controllers
{
   
    public class HomePageController : BaseHomeController
    {
        IHomePageHelper helper;
        public HomePageController(IHomePageHelper helper)
        {
            this.helper = helper;
        }
        /// <summary>
        /// تمام عناصر صفحه اول
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await helper.GetAllForDrpDn();
            return Ok(result);
        }

        /// <summary>
        /// افزودن  آیتم جدید
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(HomePage model)
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
        /// دریافت یک رکورد از عناصر صفحه اول 
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
        /// ویرایش عناصر صفحه اول 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(HomePage model)
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
        /// حذف آیتم 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
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
