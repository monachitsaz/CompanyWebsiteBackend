using Common;
using Dapper;
using FoodSoftware.Common;
using FoodSoftware.Models;
using Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FoodSoftware.Controllers
{
    public class AccountController : BaseController
    {
        ISqlUtility sqlUtil;
        IUserHelper helper;
        public IConfiguration Configuration { get; }


        public AccountController(ISqlUtility sqlUtil, IConfiguration configuration, IUserHelper helper)
        {
            this.helper = helper;
            this.sqlUtil = sqlUtil;
            Configuration = configuration;

        }

        /// <summary>
        /// ثبت نام کاربر
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public async Task<IActionResult> Register(RegisterAccountModel model)
        {
            //check username already exists
            //check pasword difficulty
            //check password and confirmPassword
            var saltpassword = Guid.NewGuid().ToString();
            //var hashPassord = EncryptionUtility.Sha256(model.Password);
            var hashPassord = EncryptionUtility.HashPasswordWithSalt(model.Password, saltpassword.ToString());
            //var userId = Guid.NewGuid().ToString();

            using (IDbConnection dapper = sqlUtil.GetNewConnection())
            {
                var query = "sp_Users_Insert";
                var parameters = new DynamicParameters();
                //parameters.Add("Id", userId);
                parameters.Add("UserName", model.Username);
                parameters.Add("Password", hashPassord);
                parameters.Add("PasswordSalt", saltpassword);
                parameters.Add("IsActive", true);
                parameters.Add("RoleID", model.RoleId);
                parameters.Add("CreatorId", model.CreatorId);
                parameters.Add("FullName", model.FullName);
                parameters.Add("PDate", model.PDate);
                parameters.Add("Hint", model.Hint);
                await dapper.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
            }
            return Ok("ثبت نام با موفقیت انجام شد");
        }



        /// <summary>
        /// تعویض نام کاربری و رمز عبور
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// 
        [HttpPatch]
        public async Task<IActionResult> ChangeCredential(UserModel model)
        {              
            try
            {
                if(model.Password.Trim()!="" && model.UserName.Trim() != "" && model.Email.Trim() != "")
                {
                    var saltpassword = Guid.NewGuid().ToString();
                    var hashPassord = EncryptionUtility.HashPasswordWithSalt(model.Password, saltpassword.ToString());
                    var result = await helper.GetByIdAsync(model.Id);



                    using (IDbConnection dapper = sqlUtil.GetNewConnection())
                    {
                        var query = "sp_Users_Update";
                        var parameters = new DynamicParameters();

                        parameters.Add("UserName", model.UserName);
                        parameters.Add("Password", hashPassord);
                        parameters.Add("PasswordSalt", saltpassword);
                        parameters.Add("Hint", model.Password);
                        parameters.Add("Email", model.Email);

                        parameters.Add("Id", model.Id);

                        await dapper.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
                    }
                }
                else
                {
                    return Ok("1");
                }
            }             
            catch (Exception ex)
            {
                var message = ex.Message;
                return Ok(message);
            }
            return Ok("2");
        }
    }
}
