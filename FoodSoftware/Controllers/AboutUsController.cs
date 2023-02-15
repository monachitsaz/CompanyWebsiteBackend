﻿using FoodSoftware.Helpers;
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
   
    public class AboutUsController : BaseHomeController
    {
        IAboutUsHelper helper;
        public AboutUsController(IAboutUsHelper helper)
        {
            this.helper = helper;
        }
     
        /// <summary>
        /// List of "about us" texts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await helper.GetAllAsync();
            return Ok(result);
        }


        /// <summary>
        /// Create new "about us" text
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(AboutUs model)
        {            
            await helper.InsertAsync(model);
            return Ok("عملیات با موفقیت انجام شد");
        }

        /// <summary>
        /// Get one records of "about us" texts 
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
        /// Update selected "about us" text
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(AboutUs model)
        {
            await helper.UpdateAsync(model);
            return Ok("ویرایش با موفقیت انجام شد");
        }

        /// <summary>
        /// Delete a "about us" text 
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
