using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.WebApp.Exceptions;
using Sprout.Exam.WebApp.Services;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService service;

        public EmployeesController(IEmployeeService service) => this.service = service;

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var employees = await service.GetAll();
            return Ok(employees);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var employee = await service.FindById(id);

                return Ok(employee);
            }
            catch (EmployeeNotFoundException)
            {
                return NotFound("Employee not found.");
            }
           
        }

        /// <summary>
        /// Refactor this method to go through proper layers and update changes to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EditEmployeeDto input)
        {
            try
            {
                var _id = await service.UpdateEmployee(input);

                return Ok(_id);
            }
            catch (EmployeeNotFoundException)
            {
                return NotFound("Employee not found.");
            }
        }

        /// <summary>
        /// Refactor this method to go through proper layers and insert employees to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreateEmployeeDto input)
        {
          
            var id = await service.CreateEmployee(input);

            return Created($"/api/employees/{id}", id);
        }


        /// <summary>
        /// Refactor this method to go through proper layers and perform soft deletion of an employee to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await service.DeleteById(id);

                return Ok(id);
            }
            catch (EmployeeNotFoundException)
            {
                return NotFound("Employee not found.");
            }
        }



        /// <summary>
        /// Refactor this method to go through proper layers and use Factory pattern
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/calculate")]
        public async Task<IActionResult> Calculate(int id, EmployeeWorkDaysDto input)
        {
            try
            {
                var income = await service.CalculateSalary(id, input.AbsentDays, input.WorkedDays);

                return Ok(income);
            }
            catch (EmployeeNotFoundException)
            {
                return NotFound("Employee not found.");
            }
            catch (EmployeeTypeNotFoundException)
            {
                return NotFound("Employee Type not found.");
            }

        }

    }
}
