using Sprout.Exam.Business.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Services
{
    public interface IEmployeeService
    {
        public Task<EmployeeDto> FindById(int id);

        public Task<IEnumerable<EmployeeDto>> GetAll();

        public Task<int> UpdateEmployee(EditEmployeeDto employee);

        public Task<int> CreateEmployee(CreateEmployeeDto employee);

        public Task<int> DeleteById(int id);

        public Task<decimal> CalculateSalary(int id, decimal absentDays, decimal workedDays);
    }
}
