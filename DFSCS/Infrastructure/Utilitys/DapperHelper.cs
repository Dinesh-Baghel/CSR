using System.Data.SqlClient;
using System.Data;
using Dapper;
using MyNewEncDec;
using Microsoft.Extensions.Configuration;



namespace Infrastructure.Utilitys
{
    public class DapperHelper
    {
        private readonly string _connectionString;
        private readonly New_Enc_Dec _Enc_Dec;

        // Constructor to initialize the connection string
        public DapperHelper(IConfiguration configuration, New_Enc_Dec Enc_Dec)
        {
            _Enc_Dec = Enc_Dec;
            var ConStr = configuration!.GetConnectionString("DefaultConnection")!;
            var builder = new SqlConnectionStringBuilder(ConStr);
            builder.Password = _Enc_Dec.My_Decode(builder.Password);
            _connectionString = builder.ToString();
        }
        // Open a database connection
        private IDbConnection GetConnection()
        {
            return new SqlConnection(_connectionString); // You can replace this with other connection types if needed.
        }
        // Query a single entity from the database without parameters

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure)
        {
            using (var connection = GetConnection())
            {
                await OpenConnectionAsync(connection);
                try
                {
                    return await connection.QueryFirstOrDefaultAsync<T>(sql, param, commandType: commandType);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        //public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null)
        //{
        //    using (var connection = GetConnection())
        //    {
        //        await OpenConnectionAsync(connection);
        //        try
        //        {
        //            return await connection.QueryFirstOrDefaultAsync<T>(sql, param, commandType: CommandType.StoredProcedure);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw;
        //        }
        //    }
        //}
        // Query a single entity from the database without parameters
        public async Task<T> QuerySingleAsync<T>(string sql)
        {
            using (var connection = GetConnection())
            {
                await OpenConnectionAsync(connection);
                return await connection.QuerySingleOrDefaultAsync<T>(sql);
            }
        }
        // Query a single entity from the database
        public async Task<T> QuerySingleAsync<T>(string sql, object param = null)
        {
            using (var connection = GetConnection())
            {
                await OpenConnectionAsync(connection);
                return await connection.QuerySingleOrDefaultAsync<T>(sql, param);
            }
        }
        // Query a list of entities without parameters
        public async Task<IEnumerable<T>> QueryListAsync<T>(string sql)
        {
            using (var connection = GetConnection())
            {
                await OpenConnectionAsync(connection);
                return await connection.QueryAsync<T>(sql);
            }
        }
        // Query a list of entities from the database
        public async Task<IEnumerable<T>> QueryListAsync<T>(string sql, object param = null)
        {
            using (var connection = GetConnection())
            {
                await OpenConnectionAsync(connection);
                return await connection.QueryAsync<T>(sql, param);
            }
        }
        // Execute a command (e.g., Insert, Update, Delete)
        public async Task<int> ExecuteAsync(string sql, object param = null)
        {
            using (var connection = GetConnection())
            {
                await OpenConnectionAsync(connection);
                return await connection.ExecuteAsync(sql, param);
            }
        }
        // Execute the stored procedure (non-query operation) and return the number of rows affected
        public async Task<int> ExecuteStoredProcedureAsync(string storedProc, object parameters = null)
        {
            var rowsAffected = 0;
            using (var connection = GetConnection())
            {
                await OpenConnectionAsync(connection);
                rowsAffected = await connection.ExecuteAsync(storedProc, parameters, commandType: CommandType.StoredProcedure);
                return rowsAffected;
            }
        }
        // Execute a stored procedure with an Output Parameter
        public async Task<string> ExecuteStoredProcedureAsync(string storedProc, object inputParam, string outputParamName)
        {
            using (var connection = GetConnection())
            {
                var parameters = new DynamicParameters(inputParam);

                // Add an output parameter. This can be any type (int, string, etc.) based on your stored procedure.
                parameters.Add(outputParamName, dbType: DbType.String, direction: ParameterDirection.Output, size: 100);

                await OpenConnectionAsync(connection);
                await connection.ExecuteAsync(storedProc, parameters, commandType: CommandType.StoredProcedure);

                // Retrieve the output parameter's value after execution
                return parameters.Get<string>(outputParamName);
            }
        }
        // Execute a stored procedure Single
        public async Task<T> ExecuteStoredProcedureSingleAsync<T>(string storedProc, object param = null)
        {
            using (var connection = GetConnection())
            {
                await OpenConnectionAsync(connection);
                return await connection.QueryFirstOrDefaultAsync<T>(storedProc, param, commandType: CommandType.StoredProcedure);
            }
        }
        //Execute a stored procedure Single and an output parameter
        public async Task<(T, string)> ExecuteStoredProcedureSingleAsync<T>(string storedProc, object inputParams, string outputParamName)
        {
            using (var connection = GetConnection())
            {
                var parameters = new DynamicParameters(inputParams);

                // Add the output parameter
                parameters.Add(outputParamName, dbType: DbType.String, direction: ParameterDirection.Output, size: 100);

                await OpenConnectionAsync(connection);

                // Execute the stored procedure and retrieve the result set
                var resultSet = await connection.QueryFirstOrDefaultAsync<T>(storedProc, parameters, commandType: CommandType.StoredProcedure);

                // Get the output parameter value
                var outputValue = parameters.Get<string>(outputParamName);

                return (resultSet!, outputValue);
            }
        }
        // Execute a stored procedure
        public async Task<IEnumerable<T>> ExecuteStoredProcedureListAsync<T>(string storedProc, object param = null)
        {
            using (var connection = GetConnection())
            {
                await OpenConnectionAsync(connection);
                try
                {
                    return await connection.QueryAsync<T>(storedProc, param, commandType: CommandType.StoredProcedure);
                }
                catch
                {
                    return null;
                }
            }
        }
        // Execute stored procedure with a single result set and an output parameter
        public async Task<(IEnumerable<T>, string)> ExecuteStoredProcedureListAsync<T>(string storedProc, object inputParams, string outputParamName)
        {
            using (var connection = GetConnection())
            {
                var parameters = new DynamicParameters(inputParams);

                // Add the output parameter
                parameters.Add(outputParamName, dbType: DbType.String, direction: ParameterDirection.Output, size: 100);

                await OpenConnectionAsync(connection);

                // Execute the stored procedure and retrieve the result set
                var resultSet = await connection.QueryAsync<T>(storedProc, parameters, commandType: CommandType.StoredProcedure);

                // Get the output parameter value
                var outputValue = parameters.Get<string>(outputParamName);

                return (resultSet, outputValue);
            }
        }
        // Execute a stored procedure Multiple
        public async Task<(IEnumerable<T1>, IEnumerable<T2>)> ExecuteStoredProcedureMultipleListAsync<T1, T2>(string storedProc, object param = null)
        {
            using (var connection = GetConnection())
            {
                await OpenConnectionAsync(connection);
                using (var multi = await connection.QueryMultipleAsync(storedProc, param, commandType: CommandType.StoredProcedure))
                {
                    // Read the multiple result sets (e.g., two result sets)
                    var result1 = await multi.ReadAsync<T1>();
                    var result2 = await multi.ReadAsync<T2>();

                    return (result1, result2);
                }
            }
        }
        public async Task<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>)> ExecuteStoredProcedureMultiple3ListAsync<T1, T2, T3>(string storedProc, object param = null)
        {
            using (var connection = GetConnection())
            {
                await OpenConnectionAsync(connection);
                using (var multi = await connection.QueryMultipleAsync(storedProc, param, commandType: CommandType.StoredProcedure))
                {
                    // Read the multiple result sets (e.g., two result sets)
                    var result1 = await TryReadAsync<T1>(multi);
                    var result2 = await TryReadAsync<T2>(multi);
                    var result3 = await TryReadAsync<T3>(multi);

                    return (result1, result2, result3);
                }
            }
        }
        // Execute stored procedure with multiple result sets and output parameters
        public async Task<(IEnumerable<T1>, IEnumerable<T2>, string)> ExecuteStoredProcedureMultipleListAsync<T1, T2>(string storedProc, object inputParams, string outputParamName)
        {
            using (var connection = GetConnection())
            {
                var parameters = new DynamicParameters(inputParams);

                // Add the output parameter, it could be any type (int, string, etc.)
                parameters.Add(outputParamName, dbType: DbType.String, direction: ParameterDirection.Output, size: 100);

                await OpenConnectionAsync(connection);

                using (var multi = await connection.QueryMultipleAsync(storedProc, parameters, commandType: CommandType.StoredProcedure))
                {
                    // Read the multiple result sets (e.g., two result sets)
                    var result1 = await multi.ReadAsync<T1>();
                    var result2 = await multi.ReadAsync<T2>();

                    // Get the output parameter after execution
                    var outputValue = parameters.Get<string>(outputParamName);

                    return (result1, result2, outputValue);
                }
            }
        }
        // Execute a stored procedure N Multiple
        public async Task<IEnumerable<dynamic>[]> ExecuteStoredProcedureWithNListAsync(string storedProc, object param = null)
        {
            using (var connection = GetConnection())
            {
                await OpenConnectionAsync(connection);
                // Execute the stored procedure and read multiple result sets
                using (var multi = await connection.QueryMultipleAsync(storedProc, param, commandType: CommandType.StoredProcedure))
                {
                    var resultSets = new List<IEnumerable<dynamic>>();

                    // Read all result sets into the resultSets array
                    while (!multi.IsConsumed)
                    {
                        resultSets.Add(await multi.ReadAsync<dynamic>());
                    }
                    return resultSets.ToArray();
                }
            }
        }
        // Execute stored procedure with N result sets and an output parameter
        public async Task<(IEnumerable<dynamic>[] resultSets, string outputValue)> ExecuteStoredProcedureWithNListAsync(string storedProc, object inputParams, string outputParamName)
        {
            using (var connection = GetConnection())
            {
                var parameters = new DynamicParameters(inputParams);

                // Add the output parameter
                parameters.Add(outputParamName, dbType: DbType.String, direction: ParameterDirection.Output, size: 100);

                await OpenConnectionAsync(connection);

                // Execute the stored procedure and read multiple result sets
                using (var multi = await connection.QueryMultipleAsync(storedProc, parameters, commandType: CommandType.StoredProcedure))
                {
                    var resultSets = new List<IEnumerable<dynamic>>();

                    // Read all result sets into the resultSets array
                    while (!multi.IsConsumed)
                    {
                        resultSets.Add(await multi.ReadAsync<dynamic>());
                    }

                    // Get the output parameter value
                    var outputValue = parameters.Get<string>(outputParamName);

                    return (resultSets.ToArray(), outputValue);
                }
            }
        }
        // Helper method to open connection asynchronously
        private async Task OpenConnectionAsync(IDbConnection connection)
        {
            if (connection is SqlConnection sqlConnection)
            {
                await sqlConnection.OpenAsync(); // For SQL Server connection
            }
            else
            {
                connection.Open(); // For other types of connections, use the synchronous open method
            }
        }
        // Helper method to safely try reading a result set
        private async Task<IEnumerable<T>> TryReadAsync<T>(SqlMapper.GridReader multi)
        {
            try
            {
                // Try to read the result set; if no rows are found, return an empty collection
                var result = await multi.ReadAsync<T>();
                return result ?? Enumerable.Empty<T>();
            }
            catch (InvalidOperationException)
            {
                // If no columns were returned, return an empty collection
                return Enumerable.Empty<T>();
            }
        }
    }
}
