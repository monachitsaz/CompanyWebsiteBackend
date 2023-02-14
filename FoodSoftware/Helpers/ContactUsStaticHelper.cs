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
        public interface IContactUsStaticHelper : IHelper<Articles>
        {
        }

        public class ContactUsStaticHelper : IContactUsStaticHelper
    {
            IGenericRepository<Articles> repository;
            public ContactUsStaticHelper(IGenericRepository<Articles> repo)
            {
                this.repository = repo;
            }

            public async Task<List<Articles>> GetAllAsync()
            {
                var query = "sp_ContactUsStatic_GetAll";
                var result = await repository.GetAllAsync(query);
                return result;
            }

            public async Task<List<Articles>> GetAllForDrpDn()
            {
                var query = "sp_ContactUsStatic_GetAll";
                var result = await repository.GetAllAsync(query);
                return result.ToList();
            }

            public async Task<PaginationResultModel<Articles>> GetAllWithPaginationAsync(int page, int limit)
            {
                var query = "sp_ContactUsStatic_GetAllWithPagination";
                var result = await repository.GetAllWithPagination(query, page, limit);
                return result;
            }

            public async Task<Articles> GetByIdAsync(object id)
            {
                var query = "sp_ContactUsStatic_GetById";
                var model = await repository.GetByIdAsync(id, query, "Id");
                return model;
            }

            public async Task InsertAsync(Articles model)
            {
                try
                {
                    var query = "[sp_ContactUsStatic_Insert]";
                    var dictionary = new Dictionary<string, object>();
                  
                    dictionary.Add("Text", model.Text);
 
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

            public async Task UpdateAsync(Articles model)
            {
                try
                {
                    var query = "sp_ContactUsStatic_Update";
                    var dictionary = new Dictionary<string, object>();               
                    dictionary.Add("Text", model.Text);
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

            public Task<int> UpdateWithIdAsync(Articles t)
            {
                throw new NotImplementedException();
            }

            public Task<int> InserWithIdAsync(Articles t)
            {
                throw new NotImplementedException();
            }

            public async Task DeleteAsync(int id)
            {
                try
                {
                    var query = "sp_ContactUsStatic_Delete";
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
