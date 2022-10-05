using System.Data.SqlClient;
using BookStode.DL.Interfaces;
using BookStore.Models.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BookStode.DL.Repositories.MsSql
{
    public class AuthorSqlRepository : IAuthorRepository
    {
        private readonly ILogger<AuthorSqlRepository> _logger;
        private readonly IConfiguration _configuration;
        private readonly IBookRepository _bookRepository;

        public AuthorSqlRepository(ILogger<AuthorSqlRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            var results = new List<Author>();

            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryAsync<Author>("SELECT * FROM Authors WITH(NOLOCK)");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAllAuthors)}: {e.Message}", e);
            }
            return Enumerable.Empty<Author>();
        }

        public async Task<Author?> GetById(int id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.QueryFirstOrDefaultAsync<Author>("SELECT * FROM Authors WITH(NOLOCK) WHERE Id = @Id", new { Id = id });
                    return result;

                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAllAuthors)}: {e.Message}", e.Message);
            }

            return new Author();
        }

        public async Task<Author?> AddAuthor(Author autor)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.ExecuteAsync("INSERT INTO Authors (Name, Age, DateOfBirth, NickName) VALUES(@Name, @Age, @DateOfBirth, @NickName)", autor);
                    return autor;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAllAuthors)}: {e.Message}", e.Message);
            }
            return null;
        }

        public async Task<Author?> DeleteAutor(int id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))

                {
                    await conn.OpenAsync();

                    var result = await conn.QueryFirstOrDefaultAsync<Author>("DELETE FROM Authors WITH(NOLOCK) WHERE Id = @Id", new { Id = id });
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(DeleteAutor)}: {e.Message}", e.Message);
            }

            return null;
        }


        public async Task<Author?> GetAuthorByName(string name)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<Author>("SELECT * FROM Authors WITH(NOLOCK) WHERE Name = @Name", new { Name = name });
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAuthorByName)}: {e.Message}", e.Message);
            }

            return null;
        }

        public async Task<Author> UpdateAuthor(Author autor)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.ExecuteAsync("UPDATE Authors SET Name = @Name, Age = @Age, DateOfBirth = @DateOfBirth, NickName = @NickName WHERE Id = @Id", autor);
                    return autor;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(UpdateAuthor)}: {e.Message}", e.Message);
            }
            return null;
        }
    }
}
