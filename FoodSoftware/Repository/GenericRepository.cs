using Common;
using Dapper;
using FoodSoftware.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        ISqlUtility _sqlUtility;
        public GenericRepository(ISqlUtility sqlUtility)
        {
            this._sqlUtility = sqlUtility;
        }

        public async Task<List<T>> GetAllAsync(string qry)
        {
            using (IDbConnection dapper = _sqlUtility.GetNewConnection())
            {
                var result = await dapper.QueryAsync<T>(qry, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<List<string>> GetSomeFields(string qry, Dictionary<string, object> dictionary)
        {
            using (IDbConnection dapper = _sqlUtility.GetNewConnection())
            {
                var result = await dapper.QueryAsync<string>(qry, new DynamicParameters(dictionary), commandType: CommandType.StoredProcedure);
                return result.ToList(); 
            }
        }


        public async Task<List<T>> GetAllAsyncForDrpDn(string qry)
        {
            using (IDbConnection dapper = _sqlUtility.GetNewConnection())
            {
                var result = await dapper.QueryAsync<T>(qry, commandType: CommandType.StoredProcedure);
                return result.ToList();
              
            }
        }

        public async Task<PaginationResultModel<T>> GetAllWithPagination(string qry, int page, int limit)
        {
            var result = new PaginationResultModel<T>();
            using (IDbConnection dapper = _sqlUtility.GetNewConnection())
            {
                var skip = (page - 1) * limit;
                var parameters = new DynamicParameters();
                parameters.Add("Skip", skip);
                parameters.Add("Take", limit);
                parameters.Add("Total", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var queryResult = await dapper.QueryAsync<T>(qry, parameters, commandType: CommandType.StoredProcedure);
                var total = parameters.Get<int>("Total");

                result.Data = queryResult.ToList();
                result.Page = page;
                result.Limit = limit;
                result.Total = total;
                result.Pages = Convert.ToInt32(Math.Ceiling((decimal)total / limit));
                return result;
            }
        }

        public async Task<T> GetByIdAsync(object id, string qry, string idParameter)
        {
            using (IDbConnection dapper = _sqlUtility.GetNewConnection())
            {
                var parameter = new DynamicParameters();
                parameter.Add(idParameter, id);
                var result = await dapper.QuerySingleOrDefaultAsync<T>(qry, parameter, commandType: CommandType.StoredProcedure);
                return result;
            }
        }


        public async Task InsertAsync(string qry, Dictionary<string, object> dictionary)
        {
            using (IDbConnection dapper = _sqlUtility.GetNewConnection())
            {
                await dapper.ExecuteAsync(qry, new DynamicParameters(dictionary), commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> InsertWithIdAsync(string qry, Dictionary<string, object> dictionary, string idName)
        {
            using (IDbConnection dapper = _sqlUtility.GetNewConnection())
            {
                var parameters = new DynamicParameters(dictionary);
                parameters.Add(idName, dbType: DbType.Int32, direction: ParameterDirection.Output);

                await dapper.ExecuteAsync(qry, parameters, commandType: CommandType.StoredProcedure);
                int id = parameters.Get<int>(idName);
                return id;
            }
        }

        public async Task UpdateAsync(string qry, Dictionary<string, object> dictionary)
        {
            using (IDbConnection dapper = _sqlUtility.GetNewConnection())
            {
                await dapper.ExecuteAsync(qry, new DynamicParameters(dictionary), commandType: CommandType.StoredProcedure);
            }
        }

        public Task<int> UpdateWithIdAsync(T t)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(string qry, int id, string IdParameter)
        {
            using (IDbConnection dapper = _sqlUtility.GetNewConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add(IdParameter, id);

                await dapper.ExecuteAsync(qry, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<T> GetOneField(object id, string qry, string idParameter)
        {
            using (IDbConnection dapper = _sqlUtility.GetNewConnection())
            {
                var parameter = new DynamicParameters();
                parameter.Add(idParameter, id);
                var result = await dapper.QueryFirstOrDefaultAsync<T>(qry, parameter, commandType: CommandType.StoredProcedure);
                return result;
            }
        }


        public async Task<object> GetSomFieldsByFilter(string qry, Dictionary<string, object> dictionary)
        {
            using (IDbConnection dapper = _sqlUtility.GetNewConnection())
            {
                var result = await dapper.QueryAsync<object>(qry, new DynamicParameters(dictionary), commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<List<T>> GetByFilterAsync(string qry, Dictionary<string, object> dictionary)
        {
            using (IDbConnection dapper = _sqlUtility.GetNewConnection())
            {
                var result = await dapper.QueryAsync<T>(qry, new DynamicParameters(dictionary), commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

    }
}
