using Sprout.Exam.Common.Enums;
using System;

namespace Sprout.Exam.Business.Factories.SalaryCalculator
{
    public class SalaryCalculatorFactory : ISalaryCalculatorFactory
    {
        public ISalaryCalculator GetSalaryCalculator(EmployeeTypeEnum type)
        {
            return type switch
            {
                EmployeeTypeEnum.Regular => new RegularSalaryCalculator(20000),
                EmployeeTypeEnum.Contractual => new ContractualSalaryCalculator(500),
                _ => throw new Exception("Invalid employee type.")
            };
        }
    }
}
