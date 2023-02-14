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
        public interface IContactsHelper : IHelper<Contacts>
        {
        }

        public class ContactsHelper : IContactsHelper
        {
            IGenericRepository<Contacts> repository;
            public ContactsHelper(IGenericRepository<Contacts> repo)
            {
                this.repository = repo;
            }

            public async Task<List<Contacts>> GetAllAsync()
            {
                var query = "sp_Contacts_GetAll";
                var result = await repository.GetAllAsync(query);
                return result;
            }

            public async Task<List<Contacts>> GetAllForDrpDn()
            {
                var query = "sp_Contacts_GetAll";
                var result = await repository.GetAllAsync(query);
                return result.ToList();
            }

            public async Task<PaginationResultModel<Contacts>> GetAllWithPaginationAsync(int page, int limit)
            {
                var query = "sp_Contacts_GetAllWithPagination";
                var result = await repository.GetAllWithPagination(query, page, limit);
                return result;
            }

            public async Task<Contacts> GetByIdAsync(object id)
            {
                var query = "sp_Contacts_GetById";
                var model = await repository.GetByIdAsync(id, query, "Id");
                return model;
            }

            public async Task InsertAsync(Contacts model)
            {
                try
                {
                    var query = "[sp_Contacts_Insert]";
                    var dictionary = new Dictionary<string, object>();
                    dictionary.Add("ContactTitle", model.ContactTitle);
                    dictionary.Add("ContactValue", model.ContactValue);
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

            public async Task UpdateAsync(Contacts model)
            {
                try
                {
                    var query = "sp_Contacts_Update";
                    var dictionary = new Dictionary<string, object>();
                    dictionary.Add("LinkTitle", model.ContactTitle);
                    dictionary.Add("LinkAddress", model.ContactValue);

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

            public Task<int> UpdateWithIdAsync(Contacts t)
            {
                throw new NotImplementedException();
            }

            public Task<int> InserWithIdAsync(Contacts t)
            {
                throw new NotImplementedException();
            }

            public async Task DeleteAsync(int id)
            {
                try
                {
                    var query = "sp_Contacts_Delete";
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
