using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HCMApplication.Models
{
    public class EFDataRepository: IDataRepository
    {
        private EFDatabaseContext context;
        public EFDataRepository(EFDatabaseContext ctx) { context = ctx; }
        public Employee GetEmployee(int id)
        {
            return context.Employees.Find(id);
        }
        public IEnumerable<Employee> GetAllEmployees()
        {
            Console.WriteLine("GetAllEmployees");
            return context.Employees;
        }
        public IEnumerable<Employee> GetFilteredEmployees(string married = null, int? children = null)
        {
            IQueryable<Employee> data = context.Employees;
            if (married != null)
            {
                data = data.Where(p => p.Married == married);
            }
            if (children != null)
            {
                data = data.Where(p => p.Children >= children);
            }
            return data;
        }
        public void CreateEmployee(Employee newEmployee)
        {
            newEmployee.EmployeeId = 0;
            context.Employees.Add(newEmployee);
            context.SaveChanges();
            Console.WriteLine($"New Key: {newEmployee.EmployeeId}");
        }
        public void UpdateEmployee(Employee changedEmployee, Employee originalEmployee = null)
        {
            if (originalEmployee == null)
            {
                originalEmployee = context.Employees.Find(changedEmployee.EmployeeId);
            }
            else
            {
                context.Employees.Attach(originalEmployee);
            }
            originalEmployee.Name = changedEmployee.Name;
            originalEmployee.PhoneNumber = changedEmployee.PhoneNumber;
            originalEmployee.Email = changedEmployee.Email;
            originalEmployee.Address = changedEmployee.Address;
            originalEmployee.Married = changedEmployee.Married;
            originalEmployee.Children = changedEmployee.Children;
            originalEmployee.Benefits = changedEmployee.Benefits;
            context.SaveChanges();
        }
        public void DeleteEmployee(int id)
        {
            context.Employees.Remove(new Employee {EmployeeId = id});
            context.SaveChanges();
        }
        //Course
        public Course GetCourse(int id)
        {
            return context.Courses.Find(id);
        }
        public IEnumerable<Course> GetAllCourses()
        {
            Console.WriteLine("GetAllCourses");
            return context.Courses;
        }
        public IEnumerable<Course> GetFilteredCourses(string format = null, int? period = null)
        {
            IQueryable<Course> data = context.Courses;
            if (format != null)
            {
                data = data.Where(p => p.Format == format);
            }
            if (period != null)
            {
                data = data.Where(p => p.Period >= period);
            }
            return data;
        }
        public void CreateCourse(Course newCourse)
        {
            newCourse.CourseId= 0;
            context.Courses.Add(newCourse);
            context.SaveChanges();
            Console.WriteLine($"New Key: {newCourse.CourseId}");
        }
        public void UpdateCourse(Course changedCourse, Course originalCourse = null)
        {
            if (originalCourse == null)
            {
                originalCourse = context.Courses.Find(changedCourse.CourseId);
            }
            else
            {
                context.Courses.Attach(originalCourse);
            }
            originalCourse.Name = changedCourse.Name;
            originalCourse.Description = changedCourse.Description;
            originalCourse.Author = changedCourse.Author;
            originalCourse.Format = changedCourse.Format;
            originalCourse.Trainer = changedCourse.Trainer;
            originalCourse.Validity = changedCourse.Validity;
            originalCourse.Period = changedCourse.Period;
            context.SaveChanges();
        }
        public void DeleteCourse(int id)
        {
            context.Courses.Remove(new Course { CourseId= id });
            context.SaveChanges();
        }

        //CourseCalendar
        public CourseCalendar GetCourseCalendar(int id)
        {
            return context.CourseCalendars.Find(id);
        }
        public IEnumerable<CourseCalendar> GetAllCourseCalendars()
        {
            Console.WriteLine("GetAllCourses");
            return context.CourseCalendars;
        }
        public IEnumerable<CourseCalendar> GetFilteredCourseCalendars(string name = null, int? participants = null)
        {
            IQueryable<CourseCalendar> data = context.CourseCalendars;
            if (name != null)
            {
                data = data.Where(p => p.Name == name);
            }
            if (participants != null)
            {
                data = data.Where(p => p.Participants >= participants);
            }
            return data;
        }
        public void CreateCourseCalendar(CourseCalendar newCourseCalendar)
        {
            newCourseCalendar.CourseCalendarId = 0;
            context.CourseCalendars.Add(newCourseCalendar);
            context.SaveChanges();
            Console.WriteLine($"New Key: {newCourseCalendar.CourseCalendarId}");
        }
        public void UpdateCourseCalendar(CourseCalendar changedCourseCalendar, CourseCalendar originalCourseCalendar = null)
        {
            if (originalCourseCalendar == null)
            {
                originalCourseCalendar = context.CourseCalendars.Find(changedCourseCalendar.CourseCalendarId);
            }
            else
            {
                context.CourseCalendars.Attach(originalCourseCalendar);
            }
            originalCourseCalendar.Name = changedCourseCalendar.Name;
            originalCourseCalendar.Period = changedCourseCalendar.Period;
            originalCourseCalendar.Mentor = changedCourseCalendar.Mentor;
            originalCourseCalendar.Participants = changedCourseCalendar.Participants;
            originalCourseCalendar.DateOfStart = changedCourseCalendar.DateOfStart;
            context.SaveChanges();
        }
        public void DeleteCourseCalendar(int id)
        {
            context.CourseCalendars.Remove(new CourseCalendar { CourseCalendarId = id });
            context.SaveChanges();
        }

        // Qualification

        public Qualification GetQualification(int id)
        {
            return context.Qualifications.Find(id);
        }
        public IEnumerable<Qualification> GetAllQualifications()
        {
            Console.WriteLine("GetAllEmployees");
            return context.Qualifications;
        }
        public IEnumerable<Qualification> GetFilteredQualifications(string courseName = null, int? grade = null)
        {
            IQueryable<Qualification> data = context.Qualifications;
            if (courseName != null)
            {
                data = data.Where(p => p.FIO == courseName);
            }
            if (grade != null)
            {
                data = data.Where(p => p.Grade>= grade);
            }
            return data;
        }
        public void CreateQualification(Qualification newQualification)
        {
            newQualification.QualificationId= 0;
            context.Qualifications.Add(newQualification);
            context.SaveChanges();
            Console.WriteLine($"New Key: {newQualification.QualificationId}");
        }
        public void UpdateQualification(Qualification changedQualification, Qualification originalQualification = null)
        {
            if (originalQualification == null)
            {
                originalQualification = context.Qualifications.Find(changedQualification.QualificationId);
            }
            else
            {
                context.Qualifications.Attach(originalQualification);
            }
            originalQualification.FIO = changedQualification.FIO;
            originalQualification.CourseName = changedQualification.CourseName;
            originalQualification.DateOfStart = changedQualification.DateOfStart;
            originalQualification.Grade = changedQualification.Grade;
            context.SaveChanges();
        }
        public void DeleteQualification(int id)
        {
            context.Qualifications.Remove(new Qualification{ QualificationId= id });
            context.SaveChanges();
        }
    }
}
