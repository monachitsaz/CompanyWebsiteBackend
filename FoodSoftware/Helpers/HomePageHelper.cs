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
        public interface IHomePageHelper : IHelper<HomePage>
        {
        }

        public class HomePageHelper : IHomePageHelper
        {
            IGenericRepository<HomePage> repository;
            public HomePageHelper(IGenericRepository<HomePage> repo)
            {
                this.repository = repo;
            }

            public async Task<List<HomePage>> GetAllAsync()
            {
                var query = "sp_HomePage_GetAll";
                var result = await repository.GetAllAsync(query);
      
                return result;
            }

            public async Task<List<HomePage>> GetAllForDrpDn()
            {
                var query = "sp_HomePage_GetAll";
                var result = await repository.GetAllAsync(query);
                return result.ToList();
            }

            public async Task<PaginationResultModel<HomePage>> GetAllWithPaginationAsync(int page, int limit)
            {
                var query = "sp_HomePage_GetAll";
                var result = await repository.GetAllWithPagination(query, page, limit);
                return result;
            }

            public async Task<HomePage> GetByIdAsync(object id)
            {
                var query = "[sp_HomePage_GetById]";
                var model = await repository.GetByIdAsync(id, query, "Id");
            
            return model;
            }

            public async Task InsertAsync(HomePage model)
            {
                try
                {
                    var query = "[sp_HomePage_Insert]";
                    var dictionary = new Dictionary<string, object>();
                    dictionary.Add("HeaderText", model.HeaderText);
                    dictionary.Add("HeaderTourLink", model.HeaderTourLink);
                    dictionary.Add("HeaderTourText", model.HeaderTourText);
                    dictionary.Add("IntroductionText", model.IntroductionText);
                    dictionary.Add("Title1", model.Title1);
                    dictionary.Add("Title2", model.Title2);
                    dictionary.Add("Title3", model.Title3);
                    dictionary.Add("Text1", model.Text1);
                    dictionary.Add("Text2", model.Text2);
                    dictionary.Add("Text3", model.Text3);
                    dictionary.Add("Link1", model.Link1);
                    dictionary.Add("Link2", model.Link2);
                    dictionary.Add("Link3", model.Link3);
                    dictionary.Add("Image1", model.Image1);
                    dictionary.Add("Image2", model.Image2);
                    dictionary.Add("Image3", model.Image3);
                    dictionary.Add("LinkText1", model.LinkText1);
                    dictionary.Add("LinkText2", model.LinkText2);
                    dictionary.Add("LinkText3", model.LinkText3);
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

            public async Task UpdateAsync(HomePage model)
            {
                try
                {
                    var query = "[sp_HomePage_Update]";
                    var dictionary = new Dictionary<string, object>();
                    dictionary.Add("HeaderText", model.HeaderText);
                    dictionary.Add("HeaderTourLink", model.HeaderTourLink);
                    dictionary.Add("HeaderTourText", model.HeaderTourText);
                    dictionary.Add("IntroductionText", model.IntroductionText);
                    dictionary.Add("Title1", model.Title1);
                    dictionary.Add("Title2", model.Title2);
                    dictionary.Add("Title3", model.Title3);
                    dictionary.Add("Text1", model.Text1);
                    dictionary.Add("Text2", model.Text2);
                    dictionary.Add("Text3", model.Text3);
                    dictionary.Add("Link1", model.Link1);
                    dictionary.Add("Link2", model.Link2);
                    dictionary.Add("Link3", model.Link3);
                    dictionary.Add("Image1", model.Image1);
                    dictionary.Add("Image2", model.Image2);
                    dictionary.Add("Image3", model.Image3);
                    dictionary.Add("LinkText1", model.LinkText1);
                    dictionary.Add("LinkText2", model.LinkText2);
                    dictionary.Add("LinkText3", model.LinkText3);
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

            public Task<int> UpdateWithIdAsync(HomePage t)
            {
                throw new NotImplementedException();
            }

            public Task<int> InserWithIdAsync(HomePage t)
            {
                throw new NotImplementedException();
            }

            public async Task DeleteAsync(int id)
            {
                try
                {
                    var query = "sp_HomePage_Delete";
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
