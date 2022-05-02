using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace HCMApplication.Models
{
    public class EFDatabaseContext: DbContext{
        public EFDatabaseContext (DbContextOptions<EFDatabaseContext> opts) 
                : base (opts) { }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Course> Courses  { get; set; }
    public DbSet<CourseCalendar> CourseCalendars { get; set; }
    public DbSet<Qualification> Qualifications { get; set; }
    public DbSet<User> Users{ get; set; }
    public DbSet<Role> Roles{ get; set; }


    }
}
