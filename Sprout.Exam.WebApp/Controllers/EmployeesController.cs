using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sprout.Exam.Business.Computations.SalaryCalculator;
using Sprout.Exam.Common.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.DataAccess.Repositories;
using System;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository repository;
        private readonly ISalaryCalculatorFactory salaryCalculatorFactory;

        public EmployeesController(IEmployeeRepository repository, ISalaryCalculatorFactory salaryCalculatorFactory)
        {
            this.repository = repository;
            this.salaryCalculatorFactory = salaryCalculatorFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var employees = await repository.FindAllAsync();
            return Ok(employees);
        }

  
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await repository.FindAsync(id);

            if(employee == null) return NotFound("Employee not found.");

            return Ok(employee);
        }

       
        [HttpPut]
        public async Task<IActionResult> Put(EditEmployeeDto input)
        {
            if(await repository.UpdateAsync(input))
            {
                return Ok();
            }

            return NotFound("Employee not found.");
        }

   
        [HttpPost]
        public async Task<IActionResult> Post(CreateEmployeeDto input)
        {
          
            var id = await repository.CreateAsync(input);

            return Created($"/api/employees/{id}", id);
        }


    
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(await repository.DeleteAsync(id))
            {
                return Ok(id);
            }

            return NotFound("Employee not found.");
        }


        [HttpPost("{id}/calculate")]
        public async Task<IActionResult> Calculate(int id, EmployeeWorkDaysDto input)
        {
            var employee = await repository.FindAsync(id);

            if(employee == null) return NotFound("Employee not found.");

            EmployeeTypeEnum type = (EmployeeTypeEnum)employee.TypeId;

            try
            {
                // will throw an error if the value of EmployeeTypeEnum doesn't exists.
                var calculator = salaryCalculatorFactory.GetSalaryCalculator(type);

                var income = Math.Round(calculator.Compute(input.AbsentDays, input.WorkedDays), 2);

                return Ok(income);
            }
            catch (Exception)
            {
                return NotFound("Employee Type not found.");
            }

        }

    }
}
