using Sprout.Exam.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Repositories
{
    public interface IEmployeeRepository
    {
        public Task<Employee> Find(int id);

        public Task<List<Employee>> FindAll(Expression<Func<Employee, bool>> predicate);

        public Task<int> Update(Employee employee);

        public Task<int> Create(Employee employee);

        public Task<int> Delete(int id);
    }
}
