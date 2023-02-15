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
        public interface IArticleHelper : IHelper<Articles>
        {
          Task<Articles> GetByTitle(object id);
        }

        public class ArticleHelper : IArticleHelper
        {
            IGenericRepository<Articles> repository;
            public ArticleHelper(IGenericRepository<Articles> repo)
            {
                this.repository = repo;
            }

            public async Task<List<Articles>> GetAllAsync()
            {
                var query = "sp_Articles_GetAll";
                var result = await repository.GetAllAsync(query);
                return result;
            }

            public async Task<List<Articles>> GetAllForDrpDn()
            {
                var query = "sp_Articles_GetAll";
                var result = await repository.GetAllAsync(query);
                return result.ToList();
            }

            public async Task<PaginationResultModel<Articles>> GetAllWithPaginationAsync(int page, int limit)
            {
                var query = "sp_Articles_GetAllWithPagination";
                var result = await repository.GetAllWithPagination(query, page, limit);
                return result;
            }

            public async Task<Articles> GetByIdAsync(object id)
            {
                var query = "sp_Articles_GetById";
                var model = await repository.GetByIdAsync(id, query, "Id");
                return model;
            }

            public async Task InsertAsync(Articles model)
            {
                try
                {
                    var query = "[sp_Articles_Insert]";
                    var dictionary = new Dictionary<string, object>();
                    dictionary.Add("Title", model.Title);
                    dictionary.Add("Text", model.Text);
                    dictionary.Add("Image", model.Image);          

                dictionary.Add("CreationDate", DateTime.Now);
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
                    var query = "sp_Articles_Update";
                    var dictionary = new Dictionary<string, object>();
                    dictionary.Add("Title", model.Title);
                    dictionary.Add("Text", model.Text);
                    dictionary.Add("Image", model.Image);


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
                    var query = "sp_Articles_Delete";
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
         public async Task<Articles> GetByTitle(object title)
        {
            try
            {
                var query = "sp_Articles_GetByTitle";
                var model = await repository.GetOneField(title, query, "Title");
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


    }
}
