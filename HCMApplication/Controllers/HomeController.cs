using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using HCMApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Dynamic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Google.DataTable.Net.Wrapper.Extension;
using Google.DataTable.Net.Wrapper;

namespace HCMApplication.Controllers
{
    [Authorize(Roles = "admin, user")]
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        //private EFDatabaseContext context;
        private IDataRepository repository;
        public HomeController(IDataRepository repo, IHttpContextAccessor httpContextAccessor)
        {
            repository = repo;
        }

        // Employee
        public IActionResult Employee(string email = null, string number = null)
        {
            var employees = repository.GetFilteredEmployees(email, number);
            ViewBag.email = email;
            ViewBag.number = number;
            return View(employees);
        }

        public IActionResult Create()
        {
            ViewBag.CreateMode = true;
            return View("Editor", new Employee());
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            repository.CreateEmployee(employee);
            return RedirectToAction(nameof(Employee));
        }
        public IActionResult Edit(int id)
        {
            ViewBag.CreateMode = false;
            return View("Editor", repository.GetEmployee(id));
        }
        [HttpPost]
        public IActionResult Edit(Employee employee, Employee original)
        {
            repository.UpdateEmployee(employee, original);
            return RedirectToAction(nameof(Employee));
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            repository.DeleteEmployee(id);
            return RedirectToAction(nameof(Employee));
        }

        //Course
        public IActionResult Course(string name = null, int? period = null)
        {
            var courses = repository.GetFilteredCourses(name, period).OrderByDescending(s => s.AddDate);
            ViewBag.name = name;
            ViewBag.period = period;
            return View(courses);
        }

        public IActionResult CreateCourse()
        {
            ViewBag.CreateMode = true;
            return View("EditorCourse", new Course());
        }
        [HttpPost]
        public IActionResult CreateCourse(Course course)
        {
            repository.CreateCourse(course);
            return RedirectToAction(nameof(Course));
        }
        public IActionResult EditCourse(int id)
        {
            ViewBag.CreateMode = false;
            return View("EditorCourse", repository.GetCourse(id));
        }
        [HttpPost]
        public IActionResult EditCourse(Course course, Course original)
        {
            repository.UpdateCourse(course, original);
            return RedirectToAction(nameof(Course));
        }
        [HttpPost]
        public IActionResult DeleteCourse(int id)
        {
            repository.DeleteCourse(id);
            return RedirectToAction(nameof(Course));
        }
        
        // CourseCalendar
        public IActionResult CourseCalendar(string courseKey = null, int? participants = null)
        {
            var courseCalendars = repository.GetFilteredCourseCalendars(courseKey, participants);
            ViewBag.courseKey = courseKey;
            ViewBag.participants = participants;
            return View(courseCalendars);
        }

        public IActionResult CreateCourseCalendar()
        {
            ViewBag.CreateMode = true;
            ViewBag.Courses = from emp in repository.GetAllCourses()
                              select new SelectListItem { Text = emp.Name, Value = emp.Name };
            return View("EditorCourseCalendar", new CourseCalendar());
        }
        [HttpPost]
        public IActionResult CreateCourseCalendar(CourseCalendar courseCalendar)
        {
            repository.CreateCourseCalendar(courseCalendar);
            return RedirectToAction(nameof(CourseCalendar));
        }
        public IActionResult EditCourseCalendar(int id)
        {
            ViewBag.CreateMode = false;
            ViewBag.Courses = from emp in repository.GetAllCourses()
                              select new SelectListItem { Text = emp.Name, Value = emp.Name };
            return View("EditorCourseCalendar", repository.GetCourseCalendar(id));
        }
        [HttpPost]
        public IActionResult EditCourseCalendar(CourseCalendar courseCalendar, CourseCalendar original)
        {
            repository.UpdateCourseCalendar(courseCalendar, original);
            return RedirectToAction(nameof(CourseCalendar));
        }
        public IActionResult DetailsCourseCalendar(int id)
        {
            ViewBag.CreateMode = false;
            DateTime test2 = repository.GetCourseCalendar(id).DateOfEnd;
            String test = repository.GetCourseCalendar(id).Name;
            var test3 = repository.DetailsCourseCalendar(test, test2);
            return View(test3);
        }
        [HttpPost]
        public IActionResult DeleteCourseCalendar(int id)
        {
            repository.DeleteCourseCalendar(id);
            return RedirectToAction(nameof(CourseCalendar));
        }
        // Qualification
        public IActionResult Qualification(string FIO = null, int? grade = null)
        {
            var qualifications = repository.GetFilteredQualifications(FIO, grade);
            if (User.IsInRole("user")) {
                qualifications = repository.GetAllQualifications();
            }            
            ViewBag.FIO = FIO;
            ViewBag.grade = grade;
            return View(qualifications);
        }

        public IActionResult CreateQualification()
        {
            ViewBag.CreateMode = true;
            ViewBag.Employees = from emp in repository.GetAllEmployees()
                                select new SelectListItem { Text = emp.Name, Value = emp.Name };
            ViewBag.Courses = from emp in repository.GetAllCourses()
                                select new SelectListItem { Text = emp.Name, Value = emp.Name };
            return View("EditorQualification", new Qualification());
        }
        [HttpPost]
        public IActionResult CreateQualification(Qualification qualification)
        {
            ViewBag.Employees = from emp in repository.GetAllEmployees()
                                 select new SelectListItem { Text = emp.Name, Value = emp.Name };
            repository.CreateQualification(qualification);
            return RedirectToAction(nameof(Qualification));
        }
        public IActionResult EditQualification(int id)
        {
            ViewBag.CreateMode = false;
            ViewBag.Employees = from emp in repository.GetAllEmployees()
                                select new SelectListItem { Text = emp.Name, Value = emp.Name };
            ViewBag.Courses = from emp in repository.GetAllCourses()
                              select new SelectListItem { Text = emp.Name, Value = emp.Name };
            return View("EditorQualification", repository.GetQualification(id));
        }
        [HttpPost]
        public IActionResult EditQualification(Qualification qualification, Qualification original)
        {
            ViewBag.Employees = from emp in repository.GetAllEmployees()
                                select new SelectListItem { Text = emp.Name, Value = emp.Name };
            repository.UpdateQualification(qualification, original);
            return RedirectToAction(nameof(Qualification));
        }
        [HttpPost]
        public IActionResult DeleteQualification(int id)
        {
            repository.DeleteQualification(id);
            return RedirectToAction(nameof(Qualification));
        }

        // Jobs
        public IActionResult Job(string department = null, int? experience = null)
        {
            var jobs = repository.GetFilteredJobs(department, experience);
            ViewBag.department = department;
            ViewBag.experience = experience;
            return View(jobs);
        }

        public IActionResult CreateJob()
        {
            ViewBag.CreateMode = true;
            ViewBag.Employees = from emp in repository.GetAllEmployees()
                                select new SelectListItem { Text = emp.Name, Value = emp.Name };
            return View("EditorJob", new Job());
        }
        [HttpPost]
        public IActionResult CreateJob(Job job)
        {
            ViewBag.Employees = from emp in repository.GetAllEmployees()
                                select new SelectListItem { Text = emp.Name, Value = emp.Name };
            repository.CreateJob(job);
            return RedirectToAction(nameof(Job));
        }
        public IActionResult EditJob(int id)
        {
            ViewBag.CreateMode = false;
            ViewBag.Employees = from emp in repository.GetAllEmployees()
                                select new SelectListItem { Text = emp.Name, Value = emp.Name };
            return View("EditorJob", repository.GetJob(id));
        }
        [HttpPost]
        public IActionResult EditJob(Job job, Job original)
        {
            ViewBag.Employees = from emp in repository.GetAllEmployees()
                                select new SelectListItem { Text = emp.Name, Value = emp.Name };
            repository.UpdateJobs(job, original);
            return RedirectToAction(nameof(Job));
        }
        [HttpPost]
        public IActionResult DeleteJob(int id)
        {
            repository.DeleteJob(id);
            return RedirectToAction(nameof(Job));
        }

        // EmployeeTest
        public IActionResult EmployeeTest()
        {
            var employeeTests = repository.GetAllEmployeeTests();
            return View(employeeTests);
        }
        public IActionResult CreateEmployeeTest()
        {
            ViewBag.CreateMode = true;
            ViewBag.Employees = from emp in repository.GetAllEmployees()
                                select new SelectListItem { Text = emp.Name, Value = emp.Name };

            return View("EditorEmployeeTest", new EmployeeTest());
        }
        [HttpPost]
        public IActionResult CreateEmployeeTest(EmployeeTest employeeTest)
        {

            ViewBag.Employees = from emp in repository.GetAllEmployees()
                                select new SelectListItem { Text = emp.Name, Value = emp.Name };
            repository.CreateEmployeeTest(employeeTest);
            return RedirectToAction(nameof(EmployeeTest));
        }
        // CourseTest
        public IActionResult CourseTest()
        {
            var courseTests = repository.GetAllCourseTests();
            return View(courseTests);
        }
        //public IActionResult CreateCourseTest(int id)
        //{
        //    ViewBag.CreateMode = true;
        //    ViewBag.Employees = from emp in repository.GetAllEmployees()
        //                        select new SelectListItem { Text = emp.Name, Value = emp.Name };
        //    CourseTest courseTest = new CourseTest();
        //    courseTest.CourseName = repository.GetQualification(id).CourseName;
        //    return View("EditorEmployeeTest", courseTest);
        //}

        public IActionResult CreateCourseTest()
        {
            ViewBag.CreateMode = true;
            CourseTest courseTest = new CourseTest();
            return View("EditorCourseTest", new CourseTest());
        }
        [HttpPost]
        public IActionResult CreateCourseTest(CourseTest courseTest, int id)
        {
            repository.CreateCourseTest(courseTest, id);
            return RedirectToAction(nameof(Qualification));
        }

        // Testing Pie Chart
        //public IActionResult GetStatistics()
        //{
        //    int number9 = repository.GetAllCourseTests().Where(x => x.Questiont1 == 9).Count();
        //    int number8 = repository.GetAllCourseTests().Where(x => x.Questiont1 == 8).Count();
        //    int number7 = repository.GetAllCourseTests().Where(x => x.Questiont1 == 7).Count();
        //    int number6 = repository.GetAllCourseTests().Where(x => x.Questiont1 == 6).Count();
        //    int number5 = repository.GetAllCourseTests().Where(x => x.Questiont1 == 5).Count();
        //    Ratio obj = new Ratio(number9, number8, number7, number6, number5);
        //    var ans = Json(obj);
        //    return View(ans);
        //}

        public JsonResult GetStatistics()
        {
            List<BlogPieChart> list = new List<BlogPieChart>();
            list.Add(new BlogPieChart { AnswerNumb = "9 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont1 == 9).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "8 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont1 == 8).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "7 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont1 == 7).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "6 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont1 == 6).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "5 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont1 == 5).Count() });

            return Json(new { JSONList = list });
        }

        public ActionResult OnGetChartData()
        {
            var pizza = new[]
            {
                new {Name = "9 баллов", Count = repository.GetAllCourseTests().Where(x => x.Questiont1 == 9).Count()},
                new {Name = "8 баллов", Count = repository.GetAllCourseTests().Where(x => x.Questiont1 == 8).Count()},
                new {Name = "7 баллов", Count = repository.GetAllCourseTests().Where(x => x.Questiont1 == 7).Count()},
                new {Name = "6 баллов", Count = repository.GetAllCourseTests().Where(x => x.Questiont1 == 6).Count()},
                new {Name = "5 баллов", Count = repository.GetAllCourseTests().Where(x => x.Questiont1 == 5).Count()}
            };

            var json = pizza.ToGoogleDataTable()
                    .NewColumn(new Column(ColumnType.String, "Topping"), x => x.Name)
                    .NewColumn(new Column(ColumnType.Number, "Slices"), x => x.Count)
                    .Build()
                    .GetJson();

            return Content(json);
        }


        //Error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("/NotFound")]
        public IActionResult PageNotFound()
        {
            return View();
        }


    }
}
