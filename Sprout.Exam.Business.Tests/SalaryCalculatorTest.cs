using Sprout.Exam.Business.Factories.SalaryCalculator;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Sprout.Exam.Business.Tests
{
    public class SalaryCalculatorTest
    {
        [Theory]
        [InlineData(1, 16690.91)]
        [InlineData(2, 15781.82)]
        [InlineData(3, 14872.73)]
        public void ShouldCalculate_RegularEmployeeSalary(decimal absentDays, decimal expected)
        {
            var factory = new SalaryCalculatorFactory();
            var calculator = factory.GetSalaryCalculator(Common.Enums.EmployeeTypeEnum.Regular);

            Assert.Equal(expected, calculator.Compute(absentDays, 0));
        }

        [Theory]
        [InlineData(15, 7500.00)]
        [InlineData(15.5, 7750.00)]
        public void ShouldCalculate_ContractualEmployeeSalary(decimal workedDays, decimal expected)
        {
            var factory = new SalaryCalculatorFactory();
            var calculator = factory.GetSalaryCalculator(Common.Enums.EmployeeTypeEnum.Contractual);

            Assert.Equal(expected, calculator.Compute(0, workedDays));
        }
    }
}
