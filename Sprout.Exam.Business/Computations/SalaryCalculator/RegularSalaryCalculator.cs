using System;

namespace Sprout.Exam.Business.Computations.SalaryCalculator
{
    internal class RegularSalaryCalculator : ISalaryCalculator
    {
        private readonly decimal salary;

        public RegularSalaryCalculator(decimal salary) => this.salary = salary;

        public decimal Compute(decimal absentDays, decimal workedDays, decimal tax = 0.12M)
        {
            var rate = salary / 22;
            var deduction = Math.Max(absentDays, 0) * rate;
            var taxed = salary * tax;

            return salary - deduction - taxed;
        }
    }
}
