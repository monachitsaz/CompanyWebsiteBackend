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
   
    public class MenuItemController : BaseHomeController
    {
        IMenuItemHelper helper;
        public MenuItemController(IMenuItemHelper helper)
        {
            this.helper = helper;
        }


        /// <summary>
        /// لیست   منو آیتم ها
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await helper.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// لیست منو آیتم های فرزند
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetMenuItemById/{id}")]
        public async Task<IActionResult> GetMenuItemById(int id)
        {
            var result = await helper.GetAllByParentIdAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// لیست منو آیتم ها برای دراپ دان
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetMenuItemForDrpDn")]
        public async Task<IActionResult> GetMenuItemForDrpDn()
        {
            var result = await helper.GetAllForDrpDn();
            return Ok(result);
        }

        /// <summary>
        /// لیست منو آیتم ها برای داشبورد 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllForDashboard")]
        [Authorize]
        public async Task<IActionResult> GetAllForDashboard()
        {
            var result = await helper.GetAllForDashboardAsync();
            return Ok(result);
        }

        /// <summary>
        /// افزودن  منو آیتم
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(MenuItems model)
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
        /// دریافت یک رکورد از منو آیتم ها 
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
        /// ویرایش منو آیتم 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(MenuItems model)
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
        /// حذف مقاله 
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
