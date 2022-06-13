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
        // Стастистика Оценивания курсов
        public IActionResult PieCharts()
        {
            return View();
        }
        //public IActionResult ChartQuestion1()
        //{
        //    return View();
        //}

        public JsonResult GetStatistics()
        {
            List<BlogPieChart> list = new List<BlogPieChart>();
            list.Add(new BlogPieChart { AnswerNumb = "10 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont1 == 10).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "9 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont1 == 9).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "8 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont1 == 8).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "7 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont1 == 7).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "6 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont1 == 6).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "5 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont1 == 5).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "4 балла", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont1 == 4).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "3 балла", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont1 == 3).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "2 балла", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont1 == 5).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "1 балл", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont1 == 1).Count() });

            return Json(new { JSONList = list });
        }

        public JsonResult GetStatistics_Sec()
        {
            List<BlogPieChart> list = new List<BlogPieChart>();
            list.Add(new BlogPieChart { AnswerNumb = "10 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont2 == 10).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "9 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont2 == 9).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "8 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont2 == 8).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "7 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont2 == 7).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "6 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont2 == 6).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "5 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont2 == 5).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "4 балла", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont2 == 4).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "3 балла", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont2 == 3).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "2 балла", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont2 == 5).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "1 балл", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont2 == 1).Count() });

            return Json(new { JSONList = list });
        }
        public JsonResult GetStatistics_Th()
        {
            List<BlogPieChart> list = new List<BlogPieChart>();
            list.Add(new BlogPieChart { AnswerNumb = "10 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont3 == 10).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "9 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont3 == 9).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "8 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont3 == 8).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "7 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont3 == 7).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "6 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont3 == 6).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "5 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont3 == 5).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "4 балла", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont3 == 4).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "3 балла", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont3 == 3).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "2 балла", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont3 == 5).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "1 балл", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont3 == 1).Count() });

            return Json(new { JSONList = list });
        }

        public JsonResult GetStatistics_Four()
        {
            List<BlogPieChart> list = new List<BlogPieChart>();
            list.Add(new BlogPieChart { AnswerNumb = "10 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont4 == 10).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "9 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont4 == 9).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "8 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont4 == 8).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "7 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont4 == 7).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "6 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont4 == 6).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "5 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont4 == 5).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "4 балла", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont4 == 4).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "3 балла", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont4 == 3).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "2 балла", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont4 == 5).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "1 балл", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont4 == 1).Count() });

            return Json(new { JSONList = list });
        }
        public JsonResult GetStatistics_Five()
        {
            List<BlogPieChart> list = new List<BlogPieChart>();
            list.Add(new BlogPieChart { AnswerNumb = "10 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont5 == 10).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "9 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont5 == 9).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "8 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont5 == 8).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "7 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont5 == 7).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "6 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont5 == 6).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "5 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont5 == 5).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "4 балла", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont5 == 4).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "3 балла", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont5 == 3).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "2 балла", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont5 == 5).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "1 балл", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont5 == 1).Count() });

            return Json(new { JSONList = list });
        }

        public JsonResult GetStatistics_Six()
        {
            List<BlogPieChart> list = new List<BlogPieChart>();
            list.Add(new BlogPieChart { AnswerNumb = "10 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont6 == 10).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "9 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont6 == 9).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "8 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont6 == 8).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "7 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont6 == 7).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "6 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont6 == 6).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "5 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont6 == 5).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "4 балла", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont6 == 4).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "3 балла", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont6 == 3).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "2 балла", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont6 == 5).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "1 балл", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont6 == 1).Count() });

            return Json(new { JSONList = list });
        }

        public JsonResult GetStatistics_Sev()
        {
            List<BlogPieChart> list = new List<BlogPieChart>();
            list.Add(new BlogPieChart { AnswerNumb = "10 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont7 == 10).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "9 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont7 == 9).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "8 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont7 == 8).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "7 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont7 == 7).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "6 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont7 == 6).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "5 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont7 == 5).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "4 балла", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont7 == 4).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "3 балла", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont7 == 3).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "2 балла", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont7 == 5).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "1 балл", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont7 == 1).Count() });

            return Json(new { JSONList = list });
        }

        public JsonResult GetStatistics_Eight()
        {
            List<BlogPieChart> list = new List<BlogPieChart>();
            list.Add(new BlogPieChart { AnswerNumb = "10 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont8 == 10).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "9 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont8 == 9).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "8 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont8 == 8).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "7 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont8 == 7).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "6 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont8 == 6).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "5 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont8 == 5).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "4 балла", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont8 == 4).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "3 балла", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont8 == 3).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "2 балла", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont8 == 5).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "1 балл", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont8 == 1).Count() });

            return Json(new { JSONList = list });
        }

        public JsonResult GetStatistics_Nine()
        {
            List<BlogPieChart> list = new List<BlogPieChart>();
            list.Add(new BlogPieChart { AnswerNumb = "10 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont9 == 10).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "9 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont9 == 9).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "8 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont9 == 8).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "7 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont9 == 7).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "6 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont9 == 6).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "5 баллов", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont9 == 5).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "4 балла", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont9 == 4).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "3 балла", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont9 == 3).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "2 балла", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont9 == 5).Count() });
            list.Add(new BlogPieChart { AnswerNumb = "1 балл", CountNumb = repository.GetAllCourseTests().Where(x => x.Questiont9 == 1).Count() });

            return Json(new { JSONList = list });
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
