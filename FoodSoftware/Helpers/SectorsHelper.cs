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
        public interface ISectorsHelper : IHelper<Sectors>
        {
           Task<Sectors> GetByTitle(object id);
        }

        public class SectorsHelper : ISectorsHelper
        {
            IGenericRepository<Sectors> repository;
            public SectorsHelper(IGenericRepository<Sectors> repo)
            {
                this.repository = repo;
            }

            public async Task<List<Sectors>> GetAllAsync()
            {
                var query = "sp_Sectors_GetAll";
                var result = await repository.GetAllAsync(query);
                return result;
            }

            public async Task<List<Sectors>> GetAllForDrpDn()
            {
                var query = "sp_Sectors_GetAll";
                var result = await repository.GetAllAsync(query);
                return result.ToList();
            }

            public async Task<PaginationResultModel<Sectors>> GetAllWithPaginationAsync(int page, int limit)
            {
                var query = "sp_Sectors_GetAllWithPagination";
                var result = await repository.GetAllWithPagination(query, page, limit);
                return result;
            }

            public async Task<Sectors> GetByIdAsync(object id)
            {
                var query = "sp_Sectors_GetById";
                var model = await repository.GetByIdAsync(id, query, "Id");
                return model;
            }

            public async Task InsertAsync(Sectors model)
            {
                try
                {
                    var query = "[sp_Sectors_Insert]";
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

            public async Task UpdateAsync(Sectors model)
            {
                try
                {
                    var query = "sp_Sectors_Update";
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

            public Task<int> UpdateWithIdAsync(Sectors t)
            {
                throw new NotImplementedException();
            }

            public Task<int> InserWithIdAsync(Sectors t)
            {
                throw new NotImplementedException();
            }

            public async Task DeleteAsync(int id)
            {
                try
                {
                    var query = "sp_Sectors_Delete";
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


        public async Task<Sectors> GetByTitle(object title)
        {
            try
            {
                var query = "sp_Sectors_GetByTitle";
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

    }
}
