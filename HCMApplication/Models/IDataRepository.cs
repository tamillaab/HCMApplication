using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCMApplication.Models
{
    public interface IDataRepository
    {
        Employee GetEmployee(int id);
        IEnumerable<Employee> GetAllEmployees();
        IEnumerable<Employee> GetFilteredEmployees(string email = null, string number = null);
        void CreateEmployee(Employee newEmployee);
        void UpdateEmployee(Employee changedEmployee, Employee originalEmployee = null);
        void DeleteEmployee(int id);
        // Course
        Course GetCourse(int id);
        IEnumerable<Course> GetAllCourses();
        IEnumerable<Course> GetFilteredCourses(string name = null, int? period = null);
        void CreateCourse(Course newCourse);
        void UpdateCourse(Course changedCourse, Course originalCourse = null);
        void DeleteCourse(int id);

        // CourseCalendar
        CourseCalendar GetCourseCalendar(int id);
        IEnumerable<CourseCalendar> GetAllCourseCalendars();
        IEnumerable<Qualification> DetailsCourseCalendar(string courseName, DateTime dateOfStart);
        IEnumerable<CourseCalendar> GetFilteredCourseCalendars(string courseKey = null, int? participants = null);
        void CreateCourseCalendar(CourseCalendar newCourseCalendar);
        void UpdateCourseCalendar(CourseCalendar changedCourseCalendar, CourseCalendar originalCourseCalendar = null);
        void DeleteCourseCalendar(int id);

        // Qualifications
        Qualification GetQualification(int id);
        IEnumerable<Qualification> GetAllQualifications();
        IEnumerable<Qualification> GetFilteredQualifications(string FIO = null, int? grade = null);
        void CreateQualification(Qualification newQualification);
        void UpdateQualification(Qualification changedQualification, Qualification originalQualification = null);
        void DeleteQualification(int id);
        // Jobs
        Job GetJob(int id);
        IEnumerable<Job> GetAllJobs();
        IEnumerable<Job> GetFilteredJobs(string department = null, int? experience= null);
        void CreateJob(Job newJob);
        void UpdateJobs(Job changedJob, Job originalJob = null);
        void DeleteJob(int id);
        // EmployeeTest
        EmployeeTest GetEmployeeTest(int id);
        IEnumerable<EmployeeTest> GetAllEmployeeTests();
        void CreateEmployeeTest(EmployeeTest newEmployeeTest);
        // CourseTest
        CourseTest GetCourseTest(int id);
        IEnumerable<CourseTest> GetAllCourseTests();
        void CreateCourseTest(CourseTest newCourseTest, int id);
    }
}
