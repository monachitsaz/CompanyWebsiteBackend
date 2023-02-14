using Common;
using FoodSoftware.Models;
using Microsoft.Data.SqlClient;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public interface IUserHelper : IHelper<UserInfoModel>
    {
    }
    public class UserHelper : IUserHelper
    {
        IGenericRepository<UserInfoModel> repository;
        public UserHelper(IGenericRepository<UserInfoModel> repo)
        {
            this.repository = repo;
        }
        public async Task<List<UserInfoModel>> GetAllAsync()
        {
            var query = "sp_Users_GetAll";
            var result = await repository.GetAllAsync(query);
            return result;
        }

        public async Task<List<UserInfoModel>> GetAllForDrpDn()
        {
            var query = "sp_Users_GetAll";
            var result = await repository.GetAllAsync(query);
            return result.ToList();
        }

        public async Task<PaginationResultModel<UserInfoModel>> GetAllWithPaginationAsync(int page, int limit)
        {
            var query = "[sp_Users_GetAllWithPagination]";
            var result = await repository.GetAllWithPagination(query, page, limit);
            return result;
        }

        public async Task<UserInfoModel> GetByIdAsync(object id)
        {
            try
            {
                var query = "sp_Users_GetById";
                var model = await repository.GetByIdAsync(id, query, "Id");
                return model;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }


        public async Task InsertAsync(UserInfoModel model)
        {
            try
            {
                var saltpassword = Guid.NewGuid().ToString();
                var hashPassord = EncryptionUtility.HashPasswordWithSalt(model.Password, saltpassword.ToString());
                var query = "[sp_Users_Insert]";
                var dictionary = new Dictionary<string, object>();
                dictionary.Add("UserName", model.UserName);
                dictionary.Add("Password", hashPassord);
                dictionary.Add("PasswordSalt", saltpassword);
                dictionary.Add("IsActive", model.IsActive);
                dictionary.Add("RoleID", model.RoleID);
                dictionary.Add("CreatorId", model.CreatorId);
                dictionary.Add("FullName", model.FullName);
                dictionary.Add("Email", model.Email);
                dictionary.Add("Phone", model.Phone);
                dictionary.Add("PDate", model.PDate);
                await repository.InsertAsync(query, dictionary);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //public async Task<string> UpdateAsync(RoleModel model)

        public async Task UpdateAsync(UserInfoModel model)
        {
            try
            {
                var query = "sp_Users_Update";
                var saltpassword = Guid.NewGuid().ToString();
                var hashPassord = EncryptionUtility.HashPasswordWithSalt(model.Password, saltpassword.ToString());
                var dictionary = new Dictionary<string, object>();
                dictionary.Add("UserName", model.UserName);
                dictionary.Add("Password", hashPassord);
                dictionary.Add("PasswordSalt", saltpassword);
                dictionary.Add("IsActive", model.IsActive);
                dictionary.Add("RoleID", model.RoleID);
                dictionary.Add("CreatorId", model.CreatorId);
                dictionary.Add("FullName", model.FullName);
                dictionary.Add("Email", model.Email);
                dictionary.Add("Phone", model.Phone);
                dictionary.Add("PDate", model.PDate);
                dictionary.Add("Id", model.Id);
                await repository.UpdateAsync(query, dictionary);
                //return "true";
            }
            catch (SqlException ex)
            {
                //return  ex.Message;
                throw ex;
            }
            catch (Exception ex)
            {
                //return ex.Message;
                throw ex;
            }
        }

        public Task<int> UpdateWithIdAsync(UserInfoModel t)
        {
            throw new NotImplementedException();
        }

        public Task<int> InserWithIdAsync(UserInfoModel t)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var query = "sp_Users_Delete";
                await repository.DeleteAsync(query, id, "Id");
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
    }
}
