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
        /// Get menuItem list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await helper.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Get submenuItem list
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetMenuItemById/{id}")]
        public async Task<IActionResult> GetMenuItemById(int id)
        {
            var result = await helper.GetAllByParentIdAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Get MenuItems For dropDown
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetMenuItemForDrpDn")]
        public async Task<IActionResult> GetMenuItemForDrpDn()
        {
            var result = await helper.GetAllForDrpDn();
            return Ok(result);
        }

        /// <summary>
        /// Get menuItem list for dashboard
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
        /// Create a menuItem
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(MenuItems model)
        {        
            await helper.InsertAsync(model);
            return Ok("عملیات با موفقیت انجام شد");
        }
        /// <summary>
        /// Get a record of menuItem
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {

            var model = await helper.GetByIdAsync(id);
            return Ok(model);
        }

        /// <summary>
        /// Update a menu item 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(MenuItems model)
        {    
            await helper.UpdateAsync(model);         
            return Ok("ویرایش با موفقیت انجام شد");
        }

        /// <summary>
        /// Delete menuItem 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {         
            await helper.DeleteAsync(id);
            return Ok("حذف با موفقیت انجام شد");
        }
    }
}
