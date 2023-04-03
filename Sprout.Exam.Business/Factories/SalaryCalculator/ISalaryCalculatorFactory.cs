using Sprout.Exam.Common.Enums;

namespace Sprout.Exam.Business.Factories.SalaryCalculator
{
    public interface ISalaryCalculatorFactory
    {
        /// <summary>
        /// Gets the calculator for the specified Employee Type.
        /// </summary>
        /// <param name="type">Type of Employee. (Regular, Contractual)</param>
        /// <returns></returns>
        public ISalaryCalculator GetSalaryCalculator(EmployeeTypeEnum type);
    }
}