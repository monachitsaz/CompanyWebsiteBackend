using FoodSoftware.Models;
using Microsoft.Data.SqlClient;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSoftware.Helpers
{
        public interface ICustomerContactHelper : IHelper<CustomerContact>
        {
        }

        public class CustomerContactHelper : ICustomerContactHelper
        {
            IGenericRepository<CustomerContact> repository;
            public CustomerContactHelper(IGenericRepository<CustomerContact> repo)
            {
                this.repository = repo;
            }

            public async Task<List<CustomerContact>> GetAllAsync()
            {
                var query = "sp_CustomerContact_GetAll";
                var result = await repository.GetAllAsync(query);
                return result;
            }

            public async Task<List<CustomerContact>> GetAllForDrpDn()
            {
                var query = "sp_CustomerContact_GetAll";
                var result = await repository.GetAllAsync(query);
                return result.ToList();
            }

            public async Task<PaginationResultModel<CustomerContact>> GetAllWithPaginationAsync(int page, int limit)
            {
                var query = "sp_CustomerContact_GetAllWithPagination";
                var result = await repository.GetAllWithPagination(query, page, limit);
                return result;
            }

            public async Task<CustomerContact> GetByIdAsync(object id)
            {
                var query = "sp_CustomerContact_GetById";
                var model = await repository.GetByIdAsync(id, query, "Id");
                return model;
            }

            public async Task InsertAsync(CustomerContact model)
            {
                try
                {
                    var query = "[sp_CustomerContact_Insert]";
                    var dictionary = new Dictionary<string, object>();
                    dictionary.Add("CompanyName", model.CompanyName);
                    dictionary.Add("FullName", model.FullName);
                    dictionary.Add("Email", model.Email);
                    dictionary.Add("Tel", model.Tel);
                    dictionary.Add("Address", model.Address);
                    dictionary.Add("CreationDate", DateTime.Now);
                    dictionary.Add("Sector", model.Sector);
                    dictionary.Add("IndActivity", model.IndActivity);
                    dictionary.Add("Desc", model.Desc);
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

            public async Task UpdateAsync(CustomerContact model)
            {
                try
                {
                    var query = "sp_CustomerContact_Update";
                    var dictionary = new Dictionary<string, object>();
                    dictionary.Add("CompanyName", model.CompanyName);
                    dictionary.Add("FullName", model.FullName);
                    dictionary.Add("Email", model.Email);
                    dictionary.Add("Tel", model.Tel);
                    dictionary.Add("Address", model.Address);
                    dictionary.Add("CreationDate",DateTime.Now);
                    dictionary.Add("Sector", model.Sector);
                    dictionary.Add("IndActivity", model.IndActivity);
                    dictionary.Add("Desc", model.Desc);

                    dictionary.Add("Id", model.Id);
                    await repository.UpdateAsync(query, dictionary);
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

            public Task<int> UpdateWithIdAsync(CustomerContact t)
            {
                throw new NotImplementedException();
            }

            public Task<int> InserWithIdAsync(CustomerContact t)
            {
                throw new NotImplementedException();
            }

            public async Task DeleteAsync(int id)
            {
                try
                {
                    var query = "sp_CustomerContact_Delete";
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
