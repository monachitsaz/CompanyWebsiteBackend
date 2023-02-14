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
        public interface IMenuItemHelper : IHelper<MenuItems>
        {
          Task<List<MenuItems>> GetAllByParentIdAsync(int id);
          Task<List<MenuItems>> GetAllForDashboardAsync();
        }

        public class MenuItemHelper : IMenuItemHelper
        {
            IGenericRepository<MenuItems> repository;
            public MenuItemHelper(IGenericRepository<MenuItems> repo)
            {
                this.repository = repo;
            }

            public async Task<List<MenuItems>> GetAllAsync()
            {
                var query = "sp_MenuItems_GetAll";
                var result = await repository.GetAllAsync(query);
                return result;
            }

            public async Task<List<MenuItems>> GetAllForDrpDn()
            {
                var query = "[sp_MenuItems_GetForDrpDn]";
                var result = await repository.GetAllAsync(query);
                return result.ToList();
            }

            public async Task<PaginationResultModel<MenuItems>> GetAllWithPaginationAsync(int page, int limit)
            {
                var query = "sp_MenuItems_GetAllWithPagination";
                var result = await repository.GetAllWithPagination(query, page, limit);
                return result;
            }

            public async Task<MenuItems> GetByIdAsync(object id)
            {
                var query = "sp_MenuItems_GetById";
                var model = await repository.GetByIdAsync(id, query, "Id");
                return model;
            }

            public async Task InsertAsync(MenuItems model)
            {
                try
                {
                    var query = "[sp_MenuItems_Insert]";
                    var dictionary = new Dictionary<string, object>();
                    dictionary.Add("Title", model.Title);
                    dictionary.Add("Link", model.Link);
                    dictionary.Add("ParentId", model.ParentId);

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

            public async Task UpdateAsync(MenuItems model)
            {
                try
                {
                    var query = "sp_MenuItems_Update";
                    var dictionary = new Dictionary<string, object>();
                    dictionary.Add("Title", model.Title);   
                    dictionary.Add("Link", model.Link);
                    dictionary.Add("ParentId", model.ParentId);

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

            public Task<int> UpdateWithIdAsync(MenuItems t)
            {
                throw new NotImplementedException();
            }

            public Task<int> InserWithIdAsync(MenuItems t)
            {
                throw new NotImplementedException();
            }

            public async Task DeleteAsync(int id)
            {
                try
                {
                    var query = "sp_MenuItems_Delete";
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

        public async Task<List<MenuItems>> GetAllByParentIdAsync(int id)
        {       
            try
            {
                var query = "[sp_MenuItems_GetByParentId]";
                var dictionary = new Dictionary<string, object>();
                dictionary.Add("Id", id);
                var result = await repository.GetByFilterAsync(query, dictionary);
                return result.ToList();
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

        public async Task<List<MenuItems>> GetAllForDashboardAsync()
        {
            var query = "[sp_MenuItems_GetAllForDashboard]";
            var result = await repository.GetAllAsync(query);
            return result;
        }
    }
}
