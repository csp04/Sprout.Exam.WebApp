using Sprout.Exam.Business.Models;
using Sprout.Exam.Common.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Repositories
{
    public interface IEmployeeRepository
    {
        public Task<EmployeeDto> FindAsync(int id);

        public Task<IEnumerable<EmployeeDto>> FindAllAsync(Expression<Func<Employee, bool>> predicate = null);

        public Task<bool> UpdateAsync(EditEmployeeDto employee);

        public Task<int> CreateAsync(CreateEmployeeDto employee);

        public Task<bool> DeleteAsync(int id);
    }
}
