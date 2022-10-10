using System.Data.SqlClient;
using BookStode.DL.Interfaces;
using BookStore.Models.Models.Users;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BookStode.DL.Repositories.MsSql
{
    public class UserInfoSqlRepository : IUserInfoRepository
    {
        private readonly ILogger<UserInfoSqlRepository> _logger;
        private readonly IConfiguration _configuration;

        public UserInfoSqlRepository(IConfiguration configuration, ILogger<UserInfoSqlRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<UserInfo?> GetUserInfoAsync(string email, string password)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<UserInfo>("SELECT * FROM UserInfo WHERE Email = @Email AND Password = @Password", new { Email = email, Password = password });
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetUserInfoAsync)}: {e.Message}", e.Message);
            }
            return null;
        }
    }
}
