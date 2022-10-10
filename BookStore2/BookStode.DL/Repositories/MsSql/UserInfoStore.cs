using System.Data.SqlClient;
using BookStore.Models.Models.Users;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BookStode.DL.Repositories.MsSql
{
    public class UserInfoStore : IUserPasswordStore<UserInfo>, IUserRoleStore<UserInfo>
    {

        private readonly ILogger<UserInfoStore> _logger;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<UserInfo> _passwordHasher;

        public UserInfoStore(IConfiguration configuration, ILogger<UserInfoStore> logger, IPasswordHasher<UserInfo> passwordHasher)
        {
            _configuration = configuration;
            _logger = logger;
            _passwordHasher = passwordHasher;
        }

        public async Task<IdentityResult> CreateAsync(UserInfo user, CancellationToken cancellationToken)
        {

            await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await conn.OpenAsync();
                var query = ("INSERT INTO UserInfo ( DisplayName, UserName, Email, Password, CreatedDate)  VALUES(@DisplayName, @UserName, @Email, @Password, @CreatedDate)");
                user.Password = _passwordHasher.HashPassword(user, user.Password);

                var result = await conn.ExecuteAsync(query, user);

                return result > 0 ? IdentityResult.Success : IdentityResult.Failed();
            }
        }

        public async Task<IList<string>> GetRolesAsync(UserInfo user, CancellationToken cancellationToken)
        {
            await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await conn.OpenAsync(cancellationToken);

                    var result =
                        await conn.QueryAsync<string>("SELECT r.RoleName FROM Roles r WHERE r.Id IN (SELECT ur.Id FROM UserRoles ur WHERE ur.UserId = @UserId )", new { UserId = user.UserId });

                    return result.ToList();
                }
                catch (Exception e)
                {
                    _logger.LogError($"Error in {nameof(UserRoleStore.FindByNameAsync)}:{e.Message}");
                    return null;
                }
            }
        }

        public Task<IdentityResult> DeleteAsync(UserInfo user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public Task<UserInfo> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<UserInfo> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<UserInfo>("SELECT * FROM UserInfo WITH(NOLOCK) WHERE UserName = @Name", new { Name = normalizedUserName });
                }
        }

        public Task<string> GetNormalizedUserNameAsync(UserInfo user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        public async Task<string?> GetUserIdAsync(UserInfo user, CancellationToken cancellationToken)
        {
            await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await conn.OpenAsync();

                var result =  await conn.QueryFirstOrDefaultAsync<UserInfo>("SELECT * FROM UserInfo WITH(NOLOCK) WHERE UserName = @Name", new { Name = user.UserName });

                return result != null ? user.UserId.ToString() : null;
            }
        }

        public Task<string?> GetUserNameAsync(UserInfo user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }
        public async Task<string> GetPasswordHashAsync(UserInfo user, CancellationToken cancellationToken)
        {
            await using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            await conn.OpenAsync(cancellationToken);

            return await conn.QueryFirstOrDefaultAsync<string>("SELECT Password FROM UserInfo WITH(NOLOCK) WHERE UserId = @userId" ,new {user.UserId});
        }

        public async Task<bool> HasPasswordAsync(UserInfo user, CancellationToken cancellationToken)
        {
            return !string.IsNullOrEmpty(await GetPasswordHashAsync(user, cancellationToken));
        }

        public Task SetNormalizedUserNameAsync(UserInfo user, string normalizedName, CancellationToken cancellationToken)
        {
            user.UserName = normalizedName;

            return Task.FromResult(user.UserName);
        }

        public async Task SetPasswordHashAsync(UserInfo user, string passwordHash, CancellationToken cancellationToken)
        {
            await using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            await conn.OpenAsync(cancellationToken);

            await conn.ExecuteAsync("UPDATE UserInfo SET Password = @password WHERE UserId = @userId", new { user.UserId, passwordHash });

        }

        public Task SetUserNameAsync(UserInfo user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(UserInfo user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task AddToRoleAsync(UserInfo user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(UserInfo user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(UserInfo user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<UserInfo>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
