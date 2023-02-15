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
   
    public class SectorsController : BaseHomeController
    {
        ISectorsHelper helper;
        public SectorsController(ISectorsHelper helper)
        {
            this.helper = helper;
        }
       
        /// <summary>
        /// Get sectors list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await helper.GetAllAsync();
            return Ok(result);
        }


        /// <summary>
        /// Create sector
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(Sectors model)
        {     
            await helper.InsertAsync(model);
            return Ok("عملیات با موفقیت انجام شد");
        }

        /// <summary>
        /// Get a sector record
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
        /// Get sector by title
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [HttpGet("SectorArticle/{title}")]
        public async Task<IActionResult> GetByTitle(string title)
        {
            var model = await helper.GetByTitle(title);
            return Ok(model);
        }

        /// <summary>
        /// Update sector 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(Sectors model)
        {       
            await helper.UpdateAsync(model);
            return Ok("ویرایش با موفقیت انجام شد");
        }

        /// <summary>
        /// Delete a sector 
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
