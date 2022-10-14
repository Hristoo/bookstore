using System.Data;
using CacheAPI.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace CacheAPI.Repositories
{
    public class Repository<TKey, TValue>
    {
        private readonly IConfiguration _configuration;

        public Repository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<TValue>> GetDBInfo(string storedProcedure, DateTime date)
        {
            await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await conn.OpenAsync();

                var result = await conn.QueryAsync<TValue>($"EXEC @storedProcedure @FromDate",
                    new { @storedProcedure = storedProcedure, @FromDate = date });

                return result;
            }
        }
    }
}
