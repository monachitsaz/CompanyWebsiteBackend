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
        public interface ILinkHelper : IHelper<Link>
        {
        }

        public class LinkHelper : ILinkHelper
        {
            IGenericRepository<Link> repository;
            public LinkHelper(IGenericRepository<Link> repo)
            {
                this.repository = repo;
            }

            public async Task<List<Link>> GetAllAsync()
            {
                var query = "sp_Links_GetAll";
                var result = await repository.GetAllAsync(query);
                return result;
            }

            public async Task<List<Link>> GetAllForDrpDn()
            {
                var query = "sp_Links_GetAll";
                var result = await repository.GetAllAsync(query);
                return result.ToList();
            }

            public async Task<PaginationResultModel<Link>> GetAllWithPaginationAsync(int page, int limit)
            {
                var query = "sp_Links_GetAllWithPagination";
                var result = await repository.GetAllWithPagination(query, page, limit);
                return result;
            }

            public async Task<Link> GetByIdAsync(object id)
            {
                var query = "sp_Links_GetById";
                var model = await repository.GetByIdAsync(id, query, "Id");
                return model;
            }

            public async Task InsertAsync(Link model)
            {
                try
                {
                    var query = "[sp_Links_Insert]";
                    var dictionary = new Dictionary<string, object>();
                    dictionary.Add("LinkTitle", model.LinkTitle);
                    dictionary.Add("LinkAddress", model.LinkAddress);
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

            public async Task UpdateAsync(Link model)
            {
                try
                {
                    var query = "sp_Links_Update";
                    var dictionary = new Dictionary<string, object>();
                    dictionary.Add("LinkTitle", model.LinkTitle);
                    dictionary.Add("LinkAddress", model.LinkAddress);

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

            public Task<int> UpdateWithIdAsync(Link t)
            {
                throw new NotImplementedException();
            }

            public Task<int> InserWithIdAsync(Link t)
            {
                throw new NotImplementedException();
            }

            public async Task DeleteAsync(int id)
            {
                try
                {
                    var query = "sp_Links_Delete";
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
