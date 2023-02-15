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
   
    public class CustomerContactController : BaseHomeController
    {
        ICustomerContactHelper helper;
        public CustomerContactController(ICustomerContactHelper helper)
        {
            this.helper = helper;
        }
     

        /// <summary>
        /// List of Customer contacts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var result = await helper.GetAllAsync();
            return Ok(result);
        }


        /// <summary>
        /// Create new Customer contact
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CustomerContact model)
        {
            await helper.InsertAsync(model);
            return Ok("عملیات با موفقیت انجام شد");
        }

        /// <summary>
        /// Get a Customer contact
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
        ///Update a Customer contact
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(CustomerContact model)
        {   
            await helper.UpdateAsync(model);       
            return Ok("ویرایش با موفقیت انجام شد");
        }

        /// <summary>
        /// Delete a Customer contact
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
