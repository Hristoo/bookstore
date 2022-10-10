using System.Data.SqlClient;
using System.Xml.Linq;
using BookStode.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Models.Users;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BookStode.DL.Repositories.MsSql
{
    public class EmployeeSqlRepository : IEmployeesRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmployeeSqlRepository> _logger;

        public EmployeeSqlRepository(IConfiguration configuration, ILogger<EmployeeSqlRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }


        public async Task AddEmployee(Employee employee)
        {

            await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await conn.OpenAsync();

                string query = "INSERT INTO [Employee] (EmployeeID, NationalIDNumber, EmployeeName, LoginID, JobTitle, BirthDate, MaritalStatus, Gender, HireDate, VacationHours, SickLeaveHours, rowguid, ModifiedDate) VALUES(@EmployeeID, @NationalIDNumber, @EmployeeName, @LoginID, @JobTitle, @BirthDate, @MaritalStatus, @Gender, @HireDate, @VacationHours, @SickLeaveHours, @rowguid, @ModifiedDate)";

                await conn.ExecuteAsync(query, employee);

            }
        }

        public async Task<bool> CheckEmployee(int id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.QueryFirstOrDefaultAsync<Employee>("SELECT * FROM Employee WITH(NOLOCK) WHERE Id = @Id", new { Id = id });
                    return true;

                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(CheckEmployee)}: {e.Message}", e.Message);
                return false;
            }
        }

        public async Task DeleteEmployee(int id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var result = await conn.QueryFirstOrDefaultAsync<Author>("DELETE FROM Employee WHERE Id = @Id", new { Id = id });
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(DeleteEmployee)}: {e.Message}", e.Message);
            }
        }

        public async Task<IEnumerable<Employee>> GetEmployeeDetails()
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryAsync<Employee>("SELECT * FROM Employee WITH(NOLOCK)");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetEmployeeDetails)}: {e.Message}", e);
            }

            return Enumerable.Empty<Employee>();
        }

        public async Task<Employee?> GetEmployeeDetails(int id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.QueryFirstOrDefaultAsync<Employee>("SELECT * FROM Employee WITH(NOLOCK) WHERE Id = @Id", new { Id = id });
                    return result;

                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(CheckEmployee)}: {e.Message}", e.Message);
            }
            return new Employee();
        }

        public async Task UpdateEmployee(Employee employee)
        {
            string query =
                      @"UPDATE [dbo].[Employee]
                       SET [EmployeeID] = @EmployeeID
                          ,[NationalIDNumber] = @NationalIDNumber
                          ,[EmployeeName] = @EmployeeName
                          ,[LoginID] = @LoginID
                          ,[JobTitle] = @JobTitle
                          ,[BirthDate] = @BirthDate
                          ,[MaritalStatus] = @MaritalStatus
                          ,[Gender] = @Gender
                          ,[HireDate] = @HireDate
                          ,[VacationHours] = @VacationHours
                          ,[SickLeaveHours] = @SickLeaveHours
                          ,[rowguid] = @rowguid
                          ,[ModifiedDate] = @ModifiedDate
                     WHERE EmployeeID = @EmployeeID";

            await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await conn.OpenAsync();
                var result = await conn.ExecuteAsync(query, employee);
            }
        }
    }
}
