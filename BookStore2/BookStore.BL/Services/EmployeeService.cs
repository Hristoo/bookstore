using BookStode.DL.Interfaces;
using BookStore.Models.Models.Users;
using BookStore.BL.Interfaces;

namespace BookStore.BL.Services
{
    public class EmployeeService : IEmployeeService, IUserInfoService
    {
        private readonly IEmployeesRepository _employeesRepository;
        private readonly IUserInfoRepository _userInfoRepository;

        public EmployeeService(IEmployeesRepository employeesRepository, IUserInfoRepository userInfoRepository)
        {
            _employeesRepository = employeesRepository;
            _userInfoRepository = userInfoRepository;
        }

        public async Task AddEmployee(Employee employee)
        {
            await _employeesRepository.AddEmployee(employee);
        }

        public async Task<bool> CheckEmployee(int id)
        {
            return await _employeesRepository.CheckEmployee(id);
        }

        public async Task DeleteEmployee(int id)
        {
            await _employeesRepository.DeleteEmployee(id);
        }

        public async Task<IEnumerable<Employee>> GetEmployeeDetails()
        {
            return await _employeesRepository.GetEmployeeDetails();
        }

        public async Task<Employee?> GetEmployeeDetails(int id)
        {
            return await _employeesRepository.GetEmployeeDetails(id);
        }

        public async Task<UserInfo?> GetUserInfoAsync(string email, string password)
        {
            return await _userInfoRepository.GetUserInfoAsync(email, password);
        }

        public async Task UpdateEmployee(Employee employee)
        {
            await _employeesRepository.UpdateEmployee(employee);
        }
    }
}
