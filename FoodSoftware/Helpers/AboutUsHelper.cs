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
        public interface IAboutUsHelper : IHelper<AboutUs>
        {
        }

        public class AboutUsHelper : IAboutUsHelper
        {
            IGenericRepository<AboutUs> repository;
            public AboutUsHelper(IGenericRepository<AboutUs> repo)
            {
                this.repository = repo;
            }

            public async Task<List<AboutUs>> GetAllAsync()
            {
                var query = "sp_AboutUs_GetAll";
                var result = await repository.GetAllAsync(query);
                return result;
            }

            public async Task<List<AboutUs>> GetAllForDrpDn()
            {
                var query = "sp_AboutUs_GetAll";
                var result = await repository.GetAllAsync(query);
                return result.ToList();
            }

            public async Task<PaginationResultModel<AboutUs>> GetAllWithPaginationAsync(int page, int limit)
            {
                var query = "sp_AboutUs_GetAllWithPagination";
                var result = await repository.GetAllWithPagination(query, page, limit);
                return result;
            }

            public async Task<AboutUs> GetByIdAsync(object id)
            {
                var query = "sp_AboutUs_GetById";
                var model = await repository.GetByIdAsync(id, query, "Id");
                return model;
            }

            public async Task InsertAsync(AboutUs model)
            {
                try
                {
                    var query = "[sp_AboutUs_Insert]";
                    var dictionary = new Dictionary<string, object>();
                    dictionary.Add("Text", model.Text);
                    dictionary.Add("Item", model.Item);
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

            public async Task UpdateAsync(AboutUs model)
            {
                try
                {
                    var query = "sp_AboutUs_Update";
                    var dictionary = new Dictionary<string, object>();
                    dictionary.Add("Text", model.Text);
                    dictionary.Add("Item", model.Item);

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

            public Task<int> UpdateWithIdAsync(AboutUs t)
            {
                throw new NotImplementedException();
            }

            public Task<int> InserWithIdAsync(AboutUs t)
            {
                throw new NotImplementedException();
            }

            public async Task DeleteAsync(int id)
            {
                try
                {
                    var query = "sp_AboutUs_Delete";
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
