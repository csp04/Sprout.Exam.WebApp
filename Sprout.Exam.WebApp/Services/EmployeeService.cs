using Sprout.Exam.Business.Computations.SalaryCalculator;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.WebApp.Exceptions;
using Sprout.Exam.WebApp.Models;
using Sprout.Exam.WebApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository repository;
        private readonly ISalaryCalculatorFactory salaryCalcFactory;

        public EmployeeService(IEmployeeRepository repository, ISalaryCalculatorFactory salaryCalcFactory)
        {
            this.repository = repository;
            this.salaryCalcFactory = salaryCalcFactory;
        }

        public async Task<decimal> CalculateSalary(int id, decimal absentDays, decimal workedDays)
        {
            Employee employee = await repository.Find(id);

            EmployeeTypeEnum type = (EmployeeTypeEnum)employee.EmployeeTypeId;
          
            try
            {
                // will throw an error if the value of EmployeeTypeEnum doesn't exists.
                var calculator = salaryCalcFactory.GetSalaryCalculator(type);

                var income = Math.Round(calculator.Compute(absentDays, workedDays), 2);

                return income;
            }
            catch (Exception)
            {
                throw new EmployeeTypeNotFoundException();
            }
    
        }

        public Task<int> CreateEmployee(CreateEmployeeDto employee) => repository.Create(new Employee
        {
            Birthdate = (DateTime)employee.Birthdate,
            FullName = employee.FullName,
            Tin = employee.Tin,
            EmployeeTypeId = (int)employee.TypeId
        });
        public Task<int> DeleteById(int id) =>repository.Delete(id);
        public async Task<EmployeeDto> FindById(int id)
        {
            var employee = await repository.Find(id);

            if(employee.IsDeleted)
            {
                throw new EmployeeNotFoundException();
            }

            return new EmployeeDto
            {
                Id = employee.Id,
                Birthdate = employee.Birthdate.ToString("yyyy-MM-dd"),
                FullName = employee.FullName,
                Tin = employee.Tin,
                TypeId = employee.EmployeeTypeId
            };
        }
        public async Task<IEnumerable<EmployeeDto>> GetAll() => (await repository.FindAll(x => !x.IsDeleted)).Select(x => new EmployeeDto
        {
            Id = x.Id,
            Birthdate = x.Birthdate.ToString("yyyy-MM-dd"),
            FullName = x.FullName,
            Tin = x.Tin,
            TypeId = x.EmployeeTypeId
        });
        public async Task<int> UpdateEmployee(EditEmployeeDto employee)
        {
            var _employee = await repository.Find((int)employee.Id);

            _employee.FullName = employee.FullName;
            _employee.Tin = employee.Tin;
            _employee.Birthdate = (DateTime)employee.Birthdate;
            _employee.EmployeeTypeId = (int)employee.TypeId;

            await repository.Update(_employee);

            return _employee.Id;
        }

    }
}
