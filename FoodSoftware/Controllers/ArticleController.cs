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

    public class ArticleController : BaseHomeController
    {
        IArticleHelper helper;
        public ArticleController(IArticleHelper helper)
        {
            this.helper = helper;
        }


        /// <summary>
        /// List of articles without pagination
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await helper.GetAllAsync();
            return Ok(result);
        }


        /// <summary>
        /// Create a new article
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(Articles model)
        {    
            await helper.InsertAsync(model);           
            return Ok("عملیات با موفقیت انجام شد");
        }
        /// <summary>
        /// Get an article by id
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
        /// Get an article by title
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>

        [HttpGet("Blog/{title}")]
        public async Task<IActionResult> GetByTitle(string title)
        {
            var model = await helper.GetByTitle(title);
            return Ok(model);
        }

        /// <summary>
        /// Update an article 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(Articles model)
        {
            await helper.UpdateAsync(model);
            return Ok("ویرایش با موفقیت انجام شد");
        }

        /// <summary>
        /// Delete an article 
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
