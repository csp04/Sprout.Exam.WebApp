﻿namespace Sprout.Exam.Business.Factories.SalaryCalculator
{
    public interface ISalaryCalculator
    {
        public decimal Compute(decimal absentDays, decimal workedDays, decimal tax = 0.12M);
    }
}
