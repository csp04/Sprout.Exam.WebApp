using System;

namespace Sprout.Exam.Business.Computations.SalaryCalculator
{
    internal class ContractualSalaryCalculator : ISalaryCalculator
    {
        private readonly decimal rate;

        public ContractualSalaryCalculator(decimal rate) => this.rate = rate;

        public decimal Compute(decimal absentDays, decimal workedDays, decimal tax = 0.12M) => rate * Math.Max(workedDays, 0);
    }
}
