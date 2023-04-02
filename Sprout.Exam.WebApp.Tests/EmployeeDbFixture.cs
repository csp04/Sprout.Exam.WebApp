using Microsoft.EntityFrameworkCore;
using Sprout.Exam.Business.Models;
using Sprout.Exam.DataAccess.Data;
using System;
using System.Linq;

namespace Sprout.Exam.WebApp.Tests
{
    public class EmployeeDbFixture
    {

        private const string ConnectionString = @"Server=.\SQLExpress;Database=SproutExamDb;User Id=spRoutaDmn;Password=340$Uuxwp7Mcxo7Khy;";
        private static readonly object _lock = new();
        private static bool _databaseInitialized = false;

        public SproutExamDbContext CreateContext()
            => new SproutExamDbContext(
                new DbContextOptionsBuilder<SproutExamDbContext>()
                    .UseSqlServer(ConnectionString)
                    .Options);


        public EmployeeDbFixture()
        {
            lock(_lock)
            {
                if(!_databaseInitialized)
                {
                    using (var context = CreateContext())
                    {
                        context.Database.ExecuteSqlRaw("DELETE FROM Employee");

                        context.AddRange(StaticEmployees.ResultList.Select(x => new Employee {
                                        Birthdate = DateTime.Parse(x.Birthdate),
                                        FullName = x.FullName,
                                        Tin = x.Tin,
                                        EmployeeTypeId = x.TypeId
                            }));
                        context.SaveChanges();
                    }

                    _databaseInitialized = true;
                }
            }

        }
    }
}
