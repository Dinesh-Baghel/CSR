using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IDapper 
    {
        Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure);
        Task<T> QuerySingleAsync<T>(string sql);
        Task<T> QuerySingleAsync<T>(string sql, object param = null);
        Task<IEnumerable<T>> QueryListAsync<T>(string sql);
        Task<IEnumerable<T>> QueryListAsync<T>(string sql, object param = null);

        Task<int> ExecuteAsync(string sql, object param = null);
        Task<int> ExecuteStoredProcedureAsync(string storedProc, object parameters = null);
        Task<string> ExecuteStoredProcedureAsync(string storedProc, object inputParam, string outputParamName);

        Task<T> ExecuteStoredProcedureSingleAsync<T>(string storedProc, object param = null);
        Task<(T, string)> ExecuteStoredProcedureSingleAsync<T>(string storedProc, object inputParams, string outputParamName);

        Task<IEnumerable<T>> ExecuteStoredProcedureListAsync<T>(string storedProc, object param = null);
        Task<(IEnumerable<T>, string)> ExecuteStoredProcedureListAsync<T>(string storedProc, object inputParams, string outputParamName);

        Task<(IEnumerable<T1>, IEnumerable<T2>)> ExecuteStoredProcedureMultipleListAsync<T1, T2>(string storedProc, object param = null);
        Task<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>)> ExecuteStoredProcedureMultiple3ListAsync<T1, T2, T3>(string storedProc, object param = null);
        Task<(IEnumerable<T1>, IEnumerable<T2>, string)> ExecuteStoredProcedureMultipleListAsync<T1, T2>(string storedProc, object inputParams, string outputParamName);

        Task<IEnumerable<dynamic>[]> ExecuteStoredProcedureWithNListAsync(string storedProc, object param = null);
        Task<(IEnumerable<dynamic>[] resultSets, string outputValue)> ExecuteStoredProcedureWithNListAsync(string storedProc, object inputParams, string outputParamName);

        Task<IEnumerable<T>> QueryWithMappingAsync<T>(string storedProc, object? param = null);
        Task<Dictionary<string, string>> ExecuteStoredProcedureAsync(
    string storedProc,
    object inputParam,
    Dictionary<string, DbType> outputParams);
    }
}
