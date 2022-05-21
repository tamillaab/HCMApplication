using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace HCMApplication.Models
{
    public class EFDataRepository: IDataRepository
    {
        private EFDatabaseContext context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EFDataRepository(EFDatabaseContext ctx, IHttpContextAccessor httpContextAccessor) { 
            context = ctx;
            _httpContextAccessor = httpContextAccessor;
        }

       
                //public EFDataRepository(IHttpContextAccessor httpContextAccessor)
        //{
        //    _httpContextAccessor = httpContextAccessor;
        //}
        //public void GetUser()
        //{
        //    var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        //}
        public Employee GetEmployee(int id)
        {
            return context.Employees.Find(id);
        }
        public IEnumerable<Employee> GetAllEmployees()
        {
            Console.WriteLine("GetAllEmployees");
            return context.Employees;
        }
        public IEnumerable<Employee> GetFilteredEmployees(string email = null, string number = null)
        {
            IQueryable<Employee> data = context.Employees;
            if (email != null)
            {
                data = data.Where(p => p.Email.Contains(email));
            }
            if (number!= null)
            {
                data = data.Where(p => p.TabelId.Contains(number) || p.Passport.Contains(number));
            }
            return data;
        }
        public void CreateEmployee(Employee newEmployee)
        {
            newEmployee.EmployeeId = 0;
            newEmployee.TabelId = Guid.NewGuid().ToString("N");
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
            originalEmployee.Passport = changedEmployee.Passport;

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
        public IEnumerable<Course> GetFilteredCourses(string name = null, int? period = null)
        {
            IQueryable<Course> data = context.Courses;
            if (name != null)
            {
                data = data.Where(p => p.Name.Contains(name) || p.Author.Contains(name));
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
            originalCourse.Validity = changedCourse.Validity;
            originalCourse.Period = changedCourse.Period;
            originalCourse.AddDate = changedCourse.AddDate;
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
        public IEnumerable<CourseCalendar> GetFilteredCourseCalendars(string courseKey = null, int? participants = null)
        {
            IQueryable<CourseCalendar> data = context.CourseCalendars;
            if (courseKey != null)
            {
                data = data.Where(p => p.CourseKey.Contains(courseKey));
            }
            if (participants != null)
            {
                data = data.Where(p => p.Participants >= participants);
            }
            return data;
        }

        public IEnumerable<Qualification> DetailsCourseCalendar(string courseName, DateTime dateOfStart)
        {
            IQueryable<Qualification> data = context.Qualifications.Where(p => p.CourseName == courseName);
            data = data.Where(p => p.DateOfStart == dateOfStart);
            return data;
        }
        public void CreateCourseCalendar(CourseCalendar newCourseCalendar)
        {
            newCourseCalendar.CourseCalendarId = 0;
            newCourseCalendar.CourseKey = Guid.NewGuid().ToString("N");
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
            originalCourseCalendar.Mentor = changedCourseCalendar.Mentor;
            originalCourseCalendar.Participants = changedCourseCalendar.Participants;
            originalCourseCalendar.DateOfStart = changedCourseCalendar.DateOfStart;
            originalCourseCalendar.DateOfEnd = changedCourseCalendar.DateOfEnd;
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
            var email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            String author = context.Employees.Where(p => p.Email == email).Select(p => p.Name).FirstOrDefault();
            return context.Qualifications.Where(p=>p.FIO == author);
        }
        public IEnumerable<Qualification> GetFilteredQualifications(string FIO = null, int? grade = null)
        {
            IQueryable<Qualification> data = context.Qualifications;
            IQueryable<Employee> dataEmp = context.Employees;

            if (FIO != null)
            {
                
                data = data.Where(p =>p.FIO == FIO);
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

        //Job
        public Job GetJob(int id)
        {
            return context.Jobs.Find(id);
        }
        public IEnumerable<Job> GetAllJobs()
        {
            Console.WriteLine("GetAllJobs");
            return context.Jobs;
        }
        public IEnumerable<Job> GetFilteredJobs(string department = null, int? experience = null)
        {
            IQueryable<Job> data = context.Jobs;

            if (department != null)
            {
                data = data.Where(p => p.Department.Contains(department));
            }
            if (experience != null)
            {
                data = data.Where(p => p.Experience >= experience);
            }
            return data;
        }
        public void CreateJob(Job newJob)
        {
            newJob.JobId = 0;
            context.Jobs.Add(newJob);
            context.SaveChanges();
            Console.WriteLine($"New Key: {newJob.JobId}");
        }
        public void UpdateJobs(Job changedJob, Job originalJob = null)
        {
            if (originalJob == null)
            {
                originalJob = context.Jobs.Find(changedJob.JobId);
            }
            else
            {
                context.Jobs.Attach(originalJob);
            }
            originalJob.FIO = changedJob.FIO;
            originalJob.Position = changedJob.Position;
            originalJob.Department = changedJob.Department;
            originalJob.Experience = changedJob.Experience;
            originalJob.HireDate = changedJob.HireDate;

            context.SaveChanges();
        }
        public void DeleteJob(int id)
        {
            context.Jobs.Remove(new Job { JobId = id });
            context.SaveChanges();
        }
        // EmployeeTest
        public EmployeeTest GetEmployeeTest(int id)
        {
            return context.EmployeeTests.Find(id);
        }
        public IEnumerable<EmployeeTest> GetAllEmployeeTests()
        {
            return context.EmployeeTests;
        }

        public void CreateEmployeeTest(EmployeeTest newEmployeeTest)
        {
            newEmployeeTest.EmpTestId = 0;
            var email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            String author = context.Employees.Where(p => p.Email == email).Select(p=>p.Name).FirstOrDefault();
            String authorId = context.Employees.Where(p => p.Email == email).Select(p => p.TabelId).FirstOrDefault();
            newEmployeeTest.Author = author;
            newEmployeeTest.AuthorId = authorId;
            newEmployeeTest.Total = (newEmployeeTest.Questiont1 + newEmployeeTest.Questiont2 + newEmployeeTest.Questiont3 + newEmployeeTest.Questiont4 + newEmployeeTest.Questiont5 + newEmployeeTest.Questiont6 + newEmployeeTest.Questiont7 + newEmployeeTest.Questiont8 + newEmployeeTest.Questiont9 + newEmployeeTest.Questiont10)/10;
            context.EmployeeTests.Add(newEmployeeTest);
            context.SaveChanges();
            Console.WriteLine($"New Key: {newEmployeeTest.EmpTestId}");
        }

        // CourseTest
        public CourseTest GetCourseTest(int id)
        {
            return context.CourseTests.Find(id);
        }
        public IEnumerable<CourseTest> GetAllCourseTests()
        {
            return context.CourseTests;
        }

        public void CreateCourseTest(CourseTest newCourseTest, int id)
        {
            newCourseTest.CourseTestId = 0;
            var email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            String author = context.Employees.Where(p => p.Email == email).Select(p => p.Name).FirstOrDefault();
            String authorId = context.Employees.Where(p => p.Email == email).Select(p => p.TabelId).FirstOrDefault();
            newCourseTest.Author = author;
            newCourseTest.AuthorId = authorId;
            newCourseTest.CourseName = context.Qualifications.Find(id).CourseName;
            context.CourseTests.Add(newCourseTest);
            context.SaveChanges();
        }

    }
}
