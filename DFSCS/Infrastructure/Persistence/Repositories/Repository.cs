using Dapper;
using  Application.Common.Interfaces;
using static Dapper.SqlMapper;

namespace Infrastructure.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly IDapper _context;
        protected readonly string _tableName;
        public Repository(IDapper context)
        {
            _context = context;
            _tableName = typeof(T).Name + "s"; // assumes table = plural of entity
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var sql = $"SELECT * FROM [{_tableName}]";
            return  await _context.QueryListAsync<T>(sql);
             //await connection.QueryAsync<T>(sql);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            var sql = $"SELECT * FROM [{_tableName}] WHERE Id = @Id";
          
            return await _context.QueryFirstOrDefaultAsync<T>(sql, new { Id = id });
        }

        public async Task<int> AddAsync(T entity)
        {
            // NOTE: Dapper.Contrib can simplify this. Here we show manual SQL for learning.
            var insertQuery = GenerateInsertQuery();
           
            return await _context.ExecuteAsync(insertQuery, entity);
        }

        public async Task<int> UpdateAsync(T entity)
        {
            var updateQuery = GenerateUpdateQuery();
           
            return await _context.ExecuteAsync(updateQuery, entity);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = $"DELETE FROM [{_tableName}] WHERE Id = @Id";
            
            return await _context.ExecuteAsync(sql, new { Id = id });
        }

        // --- Helper Methods ---
        private static string GenerateInsertQuery()
        {
            var properties = typeof(T).GetProperties()
                .Where(p => !string.Equals(p.Name, "Id", StringComparison.OrdinalIgnoreCase))
                .ToArray();
            var tableName = $"{typeof(T).Name}s"; 
            var columnNames = string.Join(", ", properties.Select(p => $"[{p.Name}]"));
            var paramNames = string.Join(", ", properties.Select(p => $"@{p.Name}"));

            return $"INSERT INTO [{tableName}] ({columnNames}) VALUES ({paramNames}); SELECT CAST(SCOPE_IDENTITY() as int);";
        }

        private static string GenerateUpdateQuery()
        {
            var props = typeof(T).GetProperties()
               .Where(p => !string.Equals(p.Name, "Id", StringComparison.OrdinalIgnoreCase))
               .ToArray();
            var setClause = string.Join(", ", props.Select(p => $"{p.Name}=@{p.Name}"));
            return $"UPDATE [{typeof(T).Name}s] SET {setClause} WHERE Id=@Id;";
        }
    }
}
