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
        public interface ILogoHelper : IHelper<Logo>
        {
        }

        public class LogoHelper : ILogoHelper
       {
            IGenericRepository<Logo> repository;
            public LogoHelper(IGenericRepository<Logo> repo)
            {
                this.repository = repo;
            }

            public async Task<List<Logo>> GetAllAsync()
            {
                var query = "sp_Logo_GetAll";
                var result = await repository.GetAllAsync(query);
                return result;
            }

            public async Task<List<Logo>> GetAllForDrpDn()
            {
                var query = "sp_Logo_GetAll";
                var result = await repository.GetAllAsync(query);
                return result.ToList();
            }

            public async Task<PaginationResultModel<Logo>> GetAllWithPaginationAsync(int page, int limit)
            {
                var query = "sp_Logo_GetAllWithPagination";
                var result = await repository.GetAllWithPagination(query, page, limit);
                return result;
            }

            public async Task<Logo> GetByIdAsync(object id)
            {
                var query = "sp_Logo_GetById";
                var model = await repository.GetByIdAsync(id, query, "Id");
                return model;
            }

            public async Task InsertAsync(Logo model)
            {
                try
                {
                    var query = "[sp_Logo_Insert]";
                    var dictionary = new Dictionary<string, object>();
                    dictionary.Add("Link", model.Link);
                    dictionary.Add("Image", model.Image);

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

            public async Task UpdateAsync(Logo model)
            {
                try
                {
                    var query = "sp_Logo_Update";
                    var dictionary = new Dictionary<string, object>();
                    dictionary.Add("Link", model.Link);
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

            public Task<int> UpdateWithIdAsync(Logo t)
            {
                throw new NotImplementedException();
            }

            public Task<int> InserWithIdAsync(Logo t)
            {
                throw new NotImplementedException();
            }

            public async Task DeleteAsync(int id)
            {
                try
                {
                    var query = "sp_Logo_Delete";
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
