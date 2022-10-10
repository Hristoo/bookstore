using System.Collections.Generic;
using System.Data.SqlClient;
using System.Xml.Linq;
using BookStode.DL.Interfaces;
using BookStore.Models.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookStode.DL.Repositories.MsSql
{
    public class BookSqlRepository : IBookRepository
    {
        private readonly ILogger<AuthorSqlRepository> _logger;
        private readonly IConfiguration _configuration;

        public BookSqlRepository( ILogger<AuthorSqlRepository> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<Book?> AddBook(Book book)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.ExecuteAsync("INSERT INTO Books (AuthorId, Title, LastUpdated, Quantity, Price) VALUES(@AuthorId, @Title, @LastUpdated, @Quantity, @Price)", book);
                    return book;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(AddBook)}: {e.Message}", e.Message);
            }
            return null;
        }

        public async Task<Book?> DeleteBook(int id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.QueryFirstOrDefaultAsync<Author>("DELETE FROM Books WHERE Id = @Id", new { Id = id });
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(DeleteBook)}: {e.Message}", e.Message);
            }

            return null;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.QueryAsync<Book>("SELECT * FROM Books WITH(NOLOCK) ");

                    return result;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAllBooks)}: {e.Message}", e.Message);
            }

            return null;
        }

        public async Task<Book?> GetById(int id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.QueryFirstOrDefaultAsync<Book>("SELECT * FROM Books WITH(NOLOCK) WHERE Id = @Id", new { Id = id });
                    return result;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetById)}: {e.Message}", e.Message);
            }

            return null;
        }

        public async Task<Book?> GetByTitle(string title)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.QueryFirstOrDefaultAsync<Book>("SELECT * FROM Books WITH(NOLOCK) WHERE Title = @Title", new { Title = title });
                    return result;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetByTitle)}: {e.Message}", e.Message);
            }

            return null;
        }

        public async Task<Book> UpdateBook(Book book)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.ExecuteAsync("UPDATE Books SET AuthorId = @AuthorId, Title = @Title,  LastUpdated = @LastUpdated, Quantity = @Quantity, Price = @Price WHERE Id = @Id", book);
                    return book;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(UpdateBook)}: {e.Message}", e.Message);
            }
            return null;
        }

        public async Task<Book> GetBookByAuthorId(int id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.QueryFirstOrDefaultAsync<Book>("SELECT * FROM Books WITH(NOLOCK) WHERE AuthorId = @Id", new { Id = id });
                    return result;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetBookByAuthorId)}: {e.Message}", e.Message);
            }

            return null;
        }
    }
}
