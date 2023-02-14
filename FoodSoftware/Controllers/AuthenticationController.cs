using Common;
using Dapper;
using FoodSoftware.Common;
using FoodSoftware.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FoodSoftware.Controllers
{

    public class AuthenticationController : BaseHomeController
    {
        ISqlUtility sqlUtil;
        public IConfiguration Configuration { get; }
        public AuthenticationController(ISqlUtility sqlUtil, IConfiguration configuration)
        {
            this.sqlUtil = sqlUtil;
            Configuration = configuration;

        }


        /// <summary>
        /// دریافت رفرش توکن جدید یا آپدیت رفرش توکن
        /// </summary>
        /// <param name="model"></param>
        /// <param name="callFromAuthenticate"></param>
        /// <returns></returns>
        [HttpGet]
        private async Task<AuthenticateModel> GetTokenWithRefreshToken(LoginModel model, bool callFromAuthenticate = true)
        {
            using (IDbConnection dapper = sqlUtil.GetNewConnection())
            {
                var sp = "[sp_Users_GetUser]";
                var paramet = new DynamicParameters();
                paramet.Add("UserName", model.UserName);
                var queryResult = await dapper.QuerySingleOrDefaultAsync<UserModel>(sp, paramet, commandType: CommandType.StoredProcedure);


                #region generate token
                var secretKey = Configuration.GetValue<string>("TokenKey");
                var tokenTimeOut = Configuration.GetValue<int>("TokenTimeOut");

                //authentication successful so generate jwt token

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(secretKey);


                //X509Certificate2 cert = new X509Certificate2("MySelfSignedCertificate.pfx", "password");
                //SecurityKey key2 = new X509SecurityKey(cert); //well, seems to be that simple



                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, model.UserName),
                    new Claim("FullName", model.UserName),
                    new Claim("Role", queryResult.RoleID.ToString()),
                }),
                    Expires = DateTime.UtcNow.AddMinutes(tokenTimeOut),
                    SigningCredentials = new SigningCredentials(/*key2*/new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

                };

                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                var refreshToken = Guid.NewGuid();


                if (callFromAuthenticate)
                {
                    //insert new refreshToken

                    var sp2 = "[sp_UserRefreshTokens_Insert]";
                    var para = new DynamicParameters();
                    para.Add("UserId", queryResult.Id);
                    para.Add("RefreshToken", refreshToken);
                    para.Add("IsValid", true);
                    var sp2result = await dapper.ExecuteAsync(sp2, para, commandType: CommandType.StoredProcedure);
                }
                else
                {

                    //update refreshToken
                    var sp3 = "[sp_UserRefreshTokens_Update]";
                    var para = new DynamicParameters();
                    para.Add("UserId", queryResult.Id);
                    para.Add("RefreshToken", refreshToken);
                    var sp2result = await dapper.ExecuteAsync(sp3, para, commandType: CommandType.StoredProcedure);
                }




                var result = new AuthenticateModel
                {
                    RefreshToken = refreshToken.ToString(),
                    Token = token,
                    FullName = model.UserName,
                    RoleId = queryResult.RoleID
                };
                return result;
                #endregion
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            using (IDbConnection dapper = sqlUtil.GetNewConnection())
            {
                var sp = "[sp_Users_GetUser]";
                var p = new DynamicParameters();
                p.Add("UserName", model.UserName);
                var queryResult = await dapper.QuerySingleOrDefaultAsync<UserModel>(sp, p, commandType: CommandType.StoredProcedure);

                if (queryResult == null)
                {
                    return BadRequest("نام کاربری اشتباه است");
             
                }
                else
                {
                    var hashPassordUser = EncryptionUtility.HashPasswordWithSalt(model.Password, queryResult.PasswordSalt.ToString());

                    if (hashPassordUser != queryResult.Password)
                    {  
                        return BadRequest("رمز عبور صحیح نمی باشد");
                    }
                    else if (!queryResult.IsActive)
                    {
                        return BadRequest("حساب کاربری، فعال نمی باشد");
                    }
                    else
                    {
                        //var result = await GetTokenWithRefreshToken(model);
                        //return Ok(result);

                        #region generate token
                        var secretKey = Configuration.GetValue<string>("TokenKey");
                        var tokenTimeOut = Configuration.GetValue<int>("TokenTimeOut");

                        //authentication successful so generate jwt token

                        var tokenHandler = new JwtSecurityTokenHandler();
                        var key = Encoding.UTF8.GetBytes(secretKey);


                        //X509Certificate2 cert = new X509Certificate2("MySelfSignedCertificate.pfx", "password");
                        //SecurityKey key2 = new X509SecurityKey(cert); //well, seems to be that simple

                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new Claim[]
                            {
                                  new Claim(ClaimTypes.Name, model.UserName),
                                  //new Claim("FullName", model.UserName),
                                  new Claim("Role", queryResult.RoleID.ToString()),


                            }),
                            Expires = DateTime.UtcNow.AddMinutes(tokenTimeOut),
                            SigningCredentials = new SigningCredentials(/*key2*/new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

                        };

                        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                        var token = tokenHandler.WriteToken(securityToken);
                
                        #endregion
                        #region refreshtoken
                        //------------------------------------------------------------------------
                        var refreshToken = Guid.NewGuid();

                        //check if there is any refreshtoken for userid

                        var sp3 = "sp_UserRefreshTokens_GetByUserId";
                        var para3 = new DynamicParameters();
                        para3.Add("UserId", queryResult.Id);
                        var q3 = await dapper.QuerySingleOrDefaultAsync<UserRefreshTokenModel>(sp3, para3, commandType: CommandType.StoredProcedure);

                        if (q3 != null)
                        {
                            if (!q3.IsValid)
                                return BadRequest("حساب کاربری غیرفعال می باشد");
                            else
                            {
                                var sp4 = "sp_UserRefreshTokens_Update";
                                var para4 = new DynamicParameters();
                                para4.Add("UserId", queryResult.Id);
                                para4.Add("RefreshToken", refreshToken);
                                await dapper.ExecuteAsync(sp4, para4, commandType: CommandType.StoredProcedure);

                            }

                        }
                        //insert refreshToken + userId in db
                        else if (q3 == null)
                        {
                            var sp2 = "[sp_UserRefreshTokens_Insert]";
                            var para = new DynamicParameters();
                            para.Add("UserId", queryResult.Id);
                            para.Add("RefreshToken", refreshToken);
                            para.Add("IsValid", true);
                            await dapper.ExecuteAsync(sp2, para, commandType: CommandType.StoredProcedure);
                        }


                        var result = new AuthenticateModel
                        {
                            RefreshToken = refreshToken.ToString(),
                            Token = token,
                            //FullName = model.UserName,
                            RoleId = queryResult.RoleID,
                            UserId = queryResult.Id
                        };
                        return Ok(result);
                        #endregion

                    }
                }
            }
        }




        /// <summary>
        /// رفرش توکن
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// 


        [HttpPost("Refresh")]
        //public async Task<IActionResult> GetNewToken([FromBody]Guid refreshToken, [FromBody] Guid userId)
        public async Task<IActionResult> GetNewToken(RefreshTokenModel model)

        {
            //result.UserId = model.UserId;
            using (IDbConnection dapper = sqlUtil.GetNewConnection())
            {
                //claims from token userid
                var sp = "[sp_UserRefreshTokens_Get]";
                var parameters = new DynamicParameters();
                parameters.Add("UserId", model.UserId);
                parameters.Add("RefreshToken", model.RefreshToken);
                var queryResult = await dapper.QuerySingleOrDefaultAsync<UserRefreshTokenModel>(sp, parameters, commandType: CommandType.StoredProcedure);

                if (queryResult == null)
                {
                    return BadRequest("حساب کاربری معتبر نمی باشد");
                    //return BadRequest("400-1");
                }
                else
                {
                    if (!queryResult.IsValid)
                    {
                        return BadRequest("حساب کاربری غیرفعال است");
                        //return BadRequest("400-2");


                    }
                    else
                    {
                        var refreshTokenTimeOut = Configuration.GetValue<int>("RefreshTokenTimeOut");
                        if (DateTime.Now.CompareTo(queryResult.CreateDate.AddMinutes(refreshTokenTimeOut)) != -1)
                            return BadRequest("لطفا مجددا وارد شوید");

                        //create new token
                        //update refreshtoken or update refreshtoken createdate

                        var secretKey = Configuration.GetValue<string>("TokenKey");
                        var tokenTimeOut = Configuration.GetValue<int>("TokenTimeOut");


                        var tokenHandler = new JwtSecurityTokenHandler();
                        var key = Encoding.UTF8.GetBytes(secretKey);


                        //X509Certificate2 cert = new X509Certificate2("MySelfSignedCertificate.pfx", "password");
                        //SecurityKey key2 = new X509SecurityKey(cert); //well, seems to be that simple

                        var sp5 = "sp_Users_UserRefreshTokens_Get";
                        var param5 = new DynamicParameters();
                        param5.Add("UserId", model.UserId);
                        var queryResult5 = await dapper.QuerySingleOrDefaultAsync<UserModel>(sp5, param5, commandType: CommandType.StoredProcedure);

                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new Claim[]
                            {
                                    new Claim(ClaimTypes.Name, queryResult5.UserName),
                                    new Claim("FullName", queryResult5.UserName),
                                    new Claim("Role", queryResult5.RoleID.ToString()),
                            }),
                            Expires = DateTime.UtcNow.AddMinutes(tokenTimeOut),
                            SigningCredentials = new SigningCredentials(/*key2*/new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

                        };

                        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                        var token = tokenHandler.WriteToken(securityToken);
                        var newRefreshToken = Guid.NewGuid();

                        var sp3 = "[sp_UserRefreshTokens_Update]";
                        var para = new DynamicParameters();
                        para.Add("UserId", model.UserId);
                        para.Add("RefreshToken", newRefreshToken);
                        await dapper.ExecuteAsync(sp3, para, commandType: CommandType.StoredProcedure);


                        var result = new AuthenticateModel
                        {
                            RefreshToken = newRefreshToken.ToString(),
                            Token = token,
                            //FullName = queryResult5.UserName,
                            RoleId = queryResult5.RoleID,
                            UserId = model.UserId
                        };
                        return Ok(result);

                    }
                }
            }

        }


    }
}
