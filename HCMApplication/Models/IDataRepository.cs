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
        IEnumerable<Employee> GetFilteredEmployees(string married = null, int? children = null);
        void CreateEmployee(Employee newEmployee);
        void UpdateEmployee(Employee changedEmployee, Employee originalEmployee = null);
        void DeleteEmployee(int id);
        // Course
        Course GetCourse(int id);
        IEnumerable<Course> GetAllCourses();
        IEnumerable<Course> GetFilteredCourses(string format = null, int? period = null);
        void CreateCourse(Course newCourse);
        void UpdateCourse(Course changedCourse, Course originalCourse = null);
        void DeleteCourse(int id);

        // CourseCalendar
        CourseCalendar GetCourseCalendar(int id);
        IEnumerable<CourseCalendar> GetAllCourseCalendars();
        IEnumerable<CourseCalendar> GetFilteredCourseCalendars(string name = null, int? participants = null);
        void CreateCourseCalendar(CourseCalendar newCourseCalendar);
        void UpdateCourseCalendar(CourseCalendar changedCourseCalendar, CourseCalendar originalCourseCalendar = null);
        void DeleteCourseCalendar(int id);

        // Qualifications
        Qualification GetQualification(int id);
        IEnumerable<Qualification> GetAllQualifications();
        IEnumerable<Qualification> GetFilteredQualifications(string courseName = null, int? grade = null);
        void CreateQualification(Qualification newQualification);
        void UpdateQualification(Qualification changedQualification, Qualification originalQualification = null);
        void DeleteQualification(int id);
    }
}
