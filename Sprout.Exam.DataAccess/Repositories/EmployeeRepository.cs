using Microsoft.EntityFrameworkCore;
using Sprout.Exam.Business.Models;
using Sprout.Exam.Common.DataTransferObjects;
using Sprout.Exam.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly SproutExamDbContext db;

        public EmployeeRepository(SproutExamDbContext db) => this.db = db;

        public async Task<int> CreateAsync(CreateEmployeeDto employee)
        {
            var input = new Employee
            {
                Birthdate = (DateTime)employee.Birthdate,
                FullName = employee.FullName,
                Tin = employee.Tin,
                EmployeeTypeId = (int)employee.TypeId
            };

            await db.Employees.AddAsync(input);
            await db.SaveChangesAsync();

            return input.Id;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var employee = await db.Employees.FindAsync(id);

            if (employee == null)
            {
                return false;
            }

            employee.IsDeleted = true;

            db.Employees.Update(employee);
            return (await db.SaveChangesAsync() > 0);
        }

        public async Task<EmployeeDto> FindAsync(int id)
        {
            var employee = await db.Employees.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id);

            if(employee == null)
            {
                return null;
            }

            return new EmployeeDto
            {
                Id = employee.Id,
                Birthdate = employee.Birthdate.ToString("yyyy-MM-dd"),
                FullName = employee.FullName,
                Tin = employee.Tin,
                TypeId = employee.EmployeeTypeId
            };
        }
        public async Task<IEnumerable<EmployeeDto>> FindAllAsync(Expression<Func<Employee, bool>> predicate = null)
        {
            var query = db.Employees.AsQueryable();

            if(predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.Where(x => !x.IsDeleted).Select(x => new EmployeeDto
            {
                Id = x.Id,
                Birthdate = x.Birthdate.ToString("yyyy-MM-dd"),
                FullName = x.FullName,
                Tin = x.Tin,
                TypeId = x.EmployeeTypeId
            }).ToListAsync();
        }
           

        public async Task<bool> UpdateAsync(EditEmployeeDto employee)
        {
            var result = await db.Employees.FindAsync(employee.Id);

            if (result == null)
            {
                return false;
            }

            result.FullName = employee.FullName;
            result.Tin = employee.Tin;
            result.Birthdate = (DateTime)employee.Birthdate;
            result.EmployeeTypeId = (int)employee.TypeId;

            db.Employees.Update(result);
            return (await db.SaveChangesAsync() > 0);
        }
    }
}
