using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;

namespace Juan_du_Toit_Has_Test
{
    public class PracticalTestContext :DbContext
    {
        public PracticalTestContext(DbContextOptions<PracticalTestContext> options) : base(options)
        {}
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<TitleLookup> TitleLookups { get; set; }
    }

    public class TitleLookup
    {
        public TitleLookup()
        {
            //pre initialize to avoid nullreference exceptions
            Persons = new List<Person>();
        }
        [Key]
        public int TitleId { get; set; }

        public string Name { get; set; }

        public virtual  List<Person> Persons { get; set; }

    }

    public class Person
    {
        public Person()
        {
            TitleLookup = new TitleLookup();
        }
        [Key]
        public int PersonID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        [ForeignKey("TitleLookup")]
        public int? TitleId { get; set; }
        public virtual TitleLookup TitleLookup { get; set; }

        public virtual Employee Employee { get; set; }
    }

    public class Employee
    {
        public Employee()
        {
            Person = new Person();
        }
        [Key]
        public int EmployeeId { get; set; }
        public string EmployeeNo { get; set; }

        [ForeignKey("Person")]
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
    public class ContextFactory : IDesignTimeDbContextFactory<PracticalTestContext>
    {
        public PracticalTestContext CreateDbContext(string[] args)
        {
            string connectionString = Environment.GetEnvironmentVariable("MyConnectionString");
            var optionsBuilder = new DbContextOptionsBuilder<PracticalTestContext>();
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(connectionString);

            return new PracticalTestContext(optionsBuilder.Options);
        }
    }
}
