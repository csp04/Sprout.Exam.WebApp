
using Microsoft.AspNetCore.Mvc;
using Sprout.Exam.Business.Factories.SalaryCalculator;
using Sprout.Exam.Common.DataTransferObjects;
using Sprout.Exam.DataAccess.Repositories;
using Sprout.Exam.WebApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;



namespace Sprout.Exam.WebApp.Tests
{
    public class EmployeeControllerTest : IClassFixture<EmployeeDbFixture>
    {
        private readonly EmployeesController controller;

        public EmployeeControllerTest(EmployeeDbFixture fixture)
        {
            var repository = new EmployeeRepository(fixture.CreateContext());

            var salaryCalculatorFactory = new SalaryCalculatorFactory();

            controller = new EmployeesController(repository, salaryCalculatorFactory);
        }

        [Fact]
        public async Task ShouldHaveResults()
        {
            var result = await controller.Get();

            Assert.IsType<OkObjectResult>(result);

            var employees = (result as OkObjectResult).Value as IEnumerable<EmployeeDto>;
            Assert.True(employees.Count() > 0);
           
        }

        [Fact]
        public async Task ShouldReturnNotFound_OnFind_When_EmployeeId_DoesntExists()
        {

            var result = await controller.GetById(999999);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task ShouldCreateEmployee()
        {
            var result = await controller.Post(new CreateEmployeeDto {
                FullName = "Test Employee",
                Birthdate = new DateTime(1980, 1, 2),
                Tin = "Sample TIN",
                TypeId = 2
                });

            Assert.IsType<CreatedResult>(result);

            var id = (result as CreatedResult).Value as int?;
            Assert.True(id.HasValue);
        }

        [Fact]
        public async Task ShouldUpdateEmployee()
        {
            var result = await controller.Get();

            Assert.IsType<OkObjectResult>(result);

            var employee = ((result as OkObjectResult).Value as IEnumerable<EmployeeDto>).First();

            Assert.NotNull(employee);

            employee.FullName = "Updated";

            await controller.Put(new EditEmployeeDto
            {
                Id = employee.Id,
                FullName = employee.FullName,
                Birthdate = DateTime.Parse(employee.Birthdate),
                Tin = employee.Tin,
                TypeId = employee.TypeId
            });

            var result2 = await controller.GetById(employee.Id);

            Assert.IsType<OkObjectResult>(result2);

            var updatedEmployee = (result2 as OkObjectResult).Value as EmployeeDto;

            Assert.Equal("Updated", updatedEmployee.FullName);
        }

        [Fact]
        public async Task ShouldReturnNotFound_OnUpdate_When_EmployeeId_DoesntExists()
        {
            
            var result = await controller.Put(new EditEmployeeDto
            {
                Id = 9999,
                FullName = "Test",
                Birthdate = DateTime.Now,
                Tin = "Test",
                TypeId = 3
            });

            Assert.IsType<NotFoundObjectResult>(result);
        }


        [Fact]
        public async Task ShouldDeleteEmployee()
        {
            var result = await controller.Get();

            Assert.IsType<OkObjectResult>(result);

            var employee = ((result as OkObjectResult).Value as IEnumerable<EmployeeDto>).Last();

            Assert.NotNull(employee);

            await controller.Delete(employee.Id);

            var result2 = await controller.GetById(employee.Id);

            Assert.IsType<NotFoundObjectResult>(result2);
        }

        [Fact]
        public async Task ShouldReturnNotFound_OnDelete_When_EmployeeId_DoesntExists()
        {

            var result = await controller.Delete(999999);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Theory]
        [InlineData(1, 16690.91)]
        [InlineData(2, 15781.82)]
        [InlineData(3, 14872.73)]
        public async Task ShouldCalculate_RegularEmployeeSalary(decimal absentDays, decimal expected)
        {
            var result = await controller.Get();

            Assert.IsType<OkObjectResult>(result);

            var employee = ((result as OkObjectResult).Value as IEnumerable<EmployeeDto>).FirstOrDefault(x => x.TypeId == 1);

            Assert.NotNull(employee);

            var result2 = await controller.Calculate(employee.Id, new EmployeeWorkDaysDto
            {
                AbsentDays = absentDays
            });

            Assert.IsType<OkObjectResult>(result2);

            var income = (result2 as OkObjectResult).Value as decimal?;

            Assert.Equal(expected, income.Value);
        }

        [Theory]
        [InlineData(15, 7500.00)]
        [InlineData(15.5, 7750.00)]
        public async Task ShouldCalculate_ContractualEmployeeSalary(decimal workedDays, decimal expected)
        {
            var result = await controller.Get();

            Assert.IsType<OkObjectResult>(result);

            var employee = ((result as OkObjectResult).Value as IEnumerable<EmployeeDto>).FirstOrDefault(x => x.TypeId == 2);

            Assert.NotNull(employee);

            var result2 = await controller.Calculate(employee.Id, new EmployeeWorkDaysDto
            {
                WorkedDays = workedDays
            });

            Assert.IsType<OkObjectResult>(result2);

            var income = (result2 as OkObjectResult).Value as decimal?;

            Assert.Equal(expected, income.Value);
        }

        [Fact]
        public async Task ShouldReturnNotFound_OnCalculate_When_EmployeeId_DoesntExists()
        {

            var result = await controller.Calculate(999999, new EmployeeWorkDaysDto());

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task ShouldReturnEmployeeTypeNotFound_OnCalculate_When_EmployeeTypeId_DoesntExists()
        {
            var result = await controller.Get();

            Assert.IsType<OkObjectResult>(result);

            var employee = ((result as OkObjectResult).Value as IEnumerable<EmployeeDto>).Last();

            Assert.NotNull(employee);

            await controller.Put( new EditEmployeeDto
            {
                Id = employee.Id,
                FullName = employee.FullName,
                Birthdate = DateTime.Parse(employee.Birthdate),
                Tin = employee.Tin,
                TypeId = 99
            });

            var result2 = await controller.Calculate(employee.Id, new EmployeeWorkDaysDto());

            Assert.IsType<NotFoundObjectResult>(result2);

            var reason = (result2 as NotFoundObjectResult).Value as string;

            Assert.Equal("Employee Type not found.", reason);
        }
    }
}
