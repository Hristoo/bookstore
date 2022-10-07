using BookStode.DL.Interfaces;
using BookStore.BL.Interfaces;
using BookStore.BL.Services;
using BookStore.Models.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore2.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]

    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


        [HttpPost(nameof(AddEmployee))]
        public async void AddEmployee(Employee employee)
        {
            await _employeeService.AddEmployee(employee);
        }

        [HttpPost(nameof(CheckEmployee))]
        public async Task<bool> CheckEmployee(int id)
        {
            return await _employeeService.CheckEmployee(id);
        }

        [HttpPost(nameof(DeleteEmployee))]
        public async Task DeleteEmployee(int id)
        {
            await _employeeService.DeleteEmployee(id);
        }

        [HttpPost(nameof(GetEmployeeDetails))]
        public async Task<IActionResult> GetEmployeeDetails()
        {
            return Ok(await _employeeService.GetEmployeeDetails());
        }


        [HttpPost(nameof(UpdateEmployee))]

        public async Task UpdateEmployee(Employee employee)
        {
            await _employeeService.UpdateEmployee(employee);
        }
    }
}
