using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNewEncDec;

namespace Infrastructure.Utilitys
{
    public class SQLHelper
    {
        private readonly string _connectionString;
        private readonly New_Enc_Dec _Enc_Dec;
        public SQLHelper(IConfiguration configuration, New_Enc_Dec Enc_Dec)
        {
            _Enc_Dec = Enc_Dec;
            var ConStr = configuration!.GetConnectionString("DefaultConnection")!;
            var builder = new SqlConnectionStringBuilder(ConStr);
            builder.Password = _Enc_Dec.My_Decode(builder.Password);
            _connectionString = builder.ToString();
        }
        // Method for executing a non-query stored procedure (INSERT, UPDATE, DELETE)
        public async Task<int> ExecuteNonQueryAsync(string storedProcedure, SqlParameter[] parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(storedProcedure, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                await connection.OpenAsync();
                return await command.ExecuteNonQueryAsync();
            }
        }

        // Method for executing a query and returning a DataTable
        public async Task<DataTable> ExecuteQueryAsync(string storedProcedure, SqlParameter[] parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(storedProcedure, connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                var dataTable = new DataTable();
                await connection.OpenAsync();
                adapter.Fill(dataTable);

                return dataTable;
            }
        }
        // Method for executing a query and returning a DataTable
        public async Task<DataTable> ExecuteQueryWithOutputParamAsync(string storedProcedure, SqlParameter[] parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(storedProcedure, connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                var dataTable = new DataTable();
                await connection.OpenAsync();
                adapter.Fill(dataTable);
                // Retrieve output parameter values after execution
                foreach (SqlParameter parameter in command.Parameters)
                {
                    if (parameter.Direction == ParameterDirection.Output)
                    {
                        parameter.Value = command.Parameters[parameter.ParameterName].Value;
                    }
                }
                return dataTable;
            }
        }
        // Method for executing a query and returning a DataTable
        public async Task<DataSet> ExecuteDataSetAsync(string storedProcedure, SqlParameter[] parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(storedProcedure, connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                var dataset = new DataSet();
                await connection.OpenAsync();
                adapter.Fill(dataset);
                // Retrieve output parameter values after execution
                foreach (SqlParameter parameter in command.Parameters)
                {
                    if (parameter.Direction == ParameterDirection.Output)
                    {
                        parameter.Value = command.Parameters[parameter.ParameterName].Value;
                    }
                }
                return dataset;
            }
        }

        // Method for executing a query and returning a scalar value (e.g., for SELECT COUNT, SUM)
        public async Task<object> ExecuteScalarAsync(string storedProcedure, SqlParameter[] parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(storedProcedure, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                await connection.OpenAsync();
                return await command.ExecuteScalarAsync();
            }
        }
    }
}
