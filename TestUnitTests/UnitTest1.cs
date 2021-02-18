using Juan_du_Toit_Has_Test;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace TestUnitTests
{
    public class DbFixture
    {
        public DbFixture()
        {
            var serviceCollection = new ServiceCollection();
            string connectionString = Environment.GetEnvironmentVariable("MyConnectionString");
            serviceCollection.AddDbContext<PracticalTestContext>(
                options => options.UseLazyLoadingProxies().UseSqlServer(connectionString));

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }
    }
    public class UnitTest1 : IClassFixture<DbFixture>
    {
        private static Random random = new Random();
        public int AccountCount { get; set; }
        private ServiceProvider _serviceProvider;
        public UnitTest1(DbFixture fixture)
        {
            AccountCount = 10;
            _serviceProvider = fixture.ServiceProvider;
            
        }
        [Fact]
        public void CheckDBReturnCorrectValues()
        {
            var _context = _serviceProvider.GetService<PracticalTestContext>();
            InitData(_context);
                var employees = _context.Employees.ToList();
                var persons = _context.Persons.ToList();
                Assert.Equal(AccountCount, employees.Count());
                Assert.Equal(AccountCount, persons.Count());
                ClearAfterTest(_context);
        }


        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private void ClearAfterTest(PracticalTestContext _context)
        {
            
                _context.Persons.RemoveRange(_context.Persons.Where(x => x.PersonID > 0));
                _context.Employees.RemoveRange(_context.Employees.Where(x => x.EmployeeId > 0));
                _context.TitleLookups.RemoveRange(_context.TitleLookups.Where(x => x.TitleId > 0));
                _context.SaveChanges();
        }
        private void InitData(PracticalTestContext _context)
        {
            ClearAfterTest(_context);
            var employeeList = new List<Employee>();
            for (int i = 0; i < AccountCount; i++)
            {
                employeeList.Add(new Employee
                {
                    EmployeeNo = RandomString(26),
                    Person = new Person
                    {
                        Surname = RandomString(26),
                        Name = RandomString(26),
                    }
                });
            }
            _context.Employees.AddRange(employeeList);
            _context.SaveChanges();
        }
    }
}
