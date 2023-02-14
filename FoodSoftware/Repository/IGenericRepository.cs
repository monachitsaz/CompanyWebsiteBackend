using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
   public interface IGenericRepository<T> where T:class
    {
        public Task InsertAsync(string qry, Dictionary<string, object> dictionary);

        public Task<int> InsertWithIdAsync(string qry, Dictionary<string, object> dictionary, string idName);

        public Task UpdateAsync(string qry, Dictionary<string, object> dictionary);

        public Task<int> UpdateWithIdAsync(T t);

        public Task DeleteAsync(string qry, int id, string IdParameter);

        public Task<T> GetByIdAsync(object id, string qry, string idParameters);

        public Task<List<T>> GetAllAsync(string qry);

        public Task<PaginationResultModel<T>> GetAllWithPagination(string qry, int page, int limit);

        public Task<List<T>> GetAllAsyncForDrpDn(string qry);

        public Task<List<string>> GetSomeFields(string qry, Dictionary<string, object> dictionary);

        public Task<T> GetOneField(object id, string qry, string idParameters);

        public Task<List<T>> GetByFilterAsync(string qry, Dictionary<string, object> dictionary);


    }
}
