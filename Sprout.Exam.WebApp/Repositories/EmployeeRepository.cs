using Microsoft.EntityFrameworkCore;
using Sprout.Exam.WebApp.Data;
using Sprout.Exam.WebApp.Exceptions;
using Sprout.Exam.WebApp.Models;
using Sprout.Exam.WebApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly SproutExamDbContext db;

        public EmployeeRepository(SproutExamDbContext db) => this.db = db;

        public async Task<int> Create(Employee employee)
        {
            await db.Employees.AddAsync(employee);
            await db.SaveChangesAsync();

            return employee.Id;
        }
        public async Task<int> Delete(int id)
        {
            var employee = await db.Employees.FindAsync(id);

            if (employee == null)
            {
                throw new EmployeeNotFoundException();
            }

            employee.IsDeleted = true;

            db.Employees.Update(employee);
            await db.SaveChangesAsync();

            return id;
        }

        public async Task<Employee> Find(int id)
        {
            var employee = await db.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if(employee == null)
            {
                throw new EmployeeNotFoundException();
            }

            return employee;
        }
        public async Task<List<Employee>> FindAll(Expression<Func<Employee, bool>> predicate) => await db.Employees.Where(predicate).ToListAsync();

        public async Task<int> Update(Employee employee)
        {
            var result = await db.Employees.FindAsync(employee.Id);

            if (result == null)
            {
                throw new EmployeeNotFoundException();
            }

            db.Employees.Update(employee);
            await db.SaveChangesAsync();

            return employee.Id;
        }
    }
}
