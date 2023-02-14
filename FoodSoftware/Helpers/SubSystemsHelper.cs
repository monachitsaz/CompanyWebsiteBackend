using FoodSoftware.Models;
using Microsoft.Data.SqlClient;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FoodSoftware.Helpers
{
        public interface ISubSystemsHelper : IHelper<SubSystems>
        {
           Task<SubSystems> GetByTitle(object id);
        }

        public class SubSystemsHelper : ISubSystemsHelper
        {
            IGenericRepository<SubSystems> repository;
            public SubSystemsHelper(IGenericRepository<SubSystems> repo)
            {
                this.repository = repo;
            }

            public async Task<List<SubSystems>> GetAllAsync()
            {
                var query = "sp_SubSystems_GetAll";
                var result = await repository.GetAllAsync(query);
                return result;
            }

            public async Task<List<SubSystems>> GetAllForDrpDn()
            {
                var query = "sp_SubSystems_GetAll";
                var result = await repository.GetAllAsync(query);
                return result.ToList();
            }

            public async Task<PaginationResultModel<SubSystems>> GetAllWithPaginationAsync(int page, int limit)
            {
                var query = "sp_SubSystems_GetAllWithPagination";
                var result = await repository.GetAllWithPagination(query, page, limit);
                return result;
            }

            public async Task<SubSystems> GetByIdAsync(object id)
            {
                try
                {
                    var query = "sp_SubSystems_GetById";
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

            //private async Task<string> GetByTitle(string title)
            //{
            //    var query = "sp_SubSystems_GetByTitle";
            //    title = "N" + title;
            //    var dictionary = new Dictionary<string, object>();
            //    dictionary.Add("Title", title);
            //    var Name = await repository.GetOneField(title, query, "Title");
            //    return Name.ToString();
            //}

            public async Task<SubSystems> GetByTitle(object title)
            {
                try
                {
                    var query = "sp_SubSystems_GetByTitle";
                    var model = await repository.GetOneField(title, query, "Title");
                //var deptObj = JsonSerializer.Deserialize<SubSystems>(model);
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

             public async Task InsertAsync(SubSystems model)
             {
                try
                {
                    var query = "[sp_SubSystems_Insert]";
                    var dictionary = new Dictionary<string, object>();
                    dictionary.Add("Title", model.Title);
                    dictionary.Add("Text", model.Text);
                    dictionary.Add("Image", model.Image);
                    dictionary.Add("Link", model.Link);         

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
   
             public async Task UpdateAsync(SubSystems model)
             {
                try
                {
                    var query = "sp_SubSystems_Update";
                    var dictionary = new Dictionary<string, object>();
                    dictionary.Add("Title", model.Title);
                    dictionary.Add("Text", model.Text);
                    dictionary.Add("Image", model.Image);
                    dictionary.Add("Link", model.Link);


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

            public Task<int> UpdateWithIdAsync(SubSystems t)
            {
                throw new NotImplementedException();
            }

            public Task<int> InserWithIdAsync(SubSystems t)
            {
                throw new NotImplementedException();
            }

            public async Task DeleteAsync(int id)
            {
                try
                {
                    var query = "sp_SubSystems_Delete";
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
