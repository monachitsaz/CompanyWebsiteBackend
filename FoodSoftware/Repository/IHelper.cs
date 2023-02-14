using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
        public interface IHelper<T>
        {
            Task InsertAsync(T t);

            Task<int> InserWithIdAsync(T t);

            Task UpdateAsync(T t);

            Task<int> UpdateWithIdAsync(T t);

            Task DeleteAsync(int id);

            Task<T> GetByIdAsync(object id);

            Task<List<T>> GetAllAsync();

            Task<PaginationResultModel<T>> GetAllWithPaginationAsync(int page, int limit);

            Task<List<T>> GetAllForDrpDn();
        

    }
}
