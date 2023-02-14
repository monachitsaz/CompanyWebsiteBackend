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
        public interface IContactUsHelper : IHelper<ContactUs>
        {
        }

        public class ContactUsHelper : IContactUsHelper
        {
            IGenericRepository<ContactUs> repository;
            public ContactUsHelper(IGenericRepository<ContactUs> repo)
            {
                this.repository = repo;
            }

            public async Task<List<ContactUs>> GetAllAsync()
            {
                var query = "sp_ContactUs_GetAll";
                var result = await repository.GetAllAsync(query);
                return result;
            }

            public async Task<List<ContactUs>> GetAllForDrpDn()
            {
                var query = "sp_ContactUs_GetAll";
                var result = await repository.GetAllAsync(query);
                return result.ToList();
            }

            public async Task<PaginationResultModel<ContactUs>> GetAllWithPaginationAsync(int page, int limit)
            {
                var query = "sp_ContactUs_GetAllWithPagination";
                var result = await repository.GetAllWithPagination(query, page, limit);
                return result;
            }

            public async Task<ContactUs> GetByIdAsync(object id)
            {
                var query = "sp_ContactUs_GetById";
                var model = await repository.GetByIdAsync(id, query, "Id");
                return model;
            }

            public async Task InsertAsync(ContactUs model)
            {
                try
                {
                    var query = "[sp_ContactUs_Insert]";
                    var dictionary = new Dictionary<string, object>();
                    dictionary.Add("Name", model.Name);
                    dictionary.Add("Email", model.Email);
                    dictionary.Add("Title", model.Title);
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

            public async Task UpdateAsync(ContactUs model)
            {
                try
                {
                    var query = "sp_ContactUs_Update";
                    var dictionary = new Dictionary<string, object>();
                    dictionary.Add("Name", model.Name);
                    dictionary.Add("Email", model.Email);
                    dictionary.Add("Title", model.Title);
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

            public Task<int> UpdateWithIdAsync(ContactUs t)
            {
                throw new NotImplementedException();
            }

            public Task<int> InserWithIdAsync(ContactUs t)
            {
                throw new NotImplementedException();
            }

            public async Task DeleteAsync(int id)
            {
                try
                {
                    var query = "sp_ContactUs_Delete";
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
