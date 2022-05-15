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

namespace HCMApplication.Controllers
{
    [Authorize(Roles = "admin, user")]
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        //private EFDatabaseContext context;
        private IDataRepository repository;
        public HomeController(IDataRepository repo)
        {
            repository = repo;
        }
        // Employee
        public IActionResult Employee(string married = null, int? children = null)
        {
            var employees = repository.GetFilteredEmployees(married, children);
            ViewBag.married = married;
            ViewBag.children = children;
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
            DateTime test2 = repository.GetCourseCalendar(id).DateOfStart;
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
            ViewBag.FIO = FIO;
            ViewBag.grade = grade;
            return View(qualifications);
        }

        public IActionResult CreateQualification()
        {
            ViewBag.CreateMode = true;
            ViewBag.Employees = from emp in repository.GetAllEmployees()
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
            ViewBag.Departments = from emp in repository.GetAllJobs()
                                select new SelectListItem { Text = emp.Department, Value = emp.Department };
            ViewBag.Employees = from emp in repository.GetAllEmployees()
                                select new SelectListItem { Text = emp.Name, Value = emp.Name };
            return View("EditorJob", new Job());
        }
        [HttpPost]
        public IActionResult CreateJob(Job job)
        {
            ViewBag.Departments = from emp in repository.GetAllJobs()
                                  select new SelectListItem { Text = emp.Department, Value = emp.Department };
            ViewBag.Employees = from emp in repository.GetAllEmployees()
                                select new SelectListItem { Text = emp.Name, Value = emp.Name };
            repository.CreateJob(job);
            return RedirectToAction(nameof(Job));
        }
        public IActionResult EditJob(int id)
        {
            ViewBag.CreateMode = false;
            ViewBag.Departments = from emp in repository.GetAllJobs()
                                  select new SelectListItem { Text = emp.Department, Value = emp.Department };
            ViewBag.Employees = from emp in repository.GetAllEmployees()
                                select new SelectListItem { Text = emp.Name, Value = emp.Name };
            return View("EditorJob", repository.GetJob(id));
        }
        [HttpPost]
        public IActionResult EditJob(Job job, Job original)
        {
            ViewBag.Departments = from emp in repository.GetAllJobs()
                                  select new SelectListItem { Text = emp.Department, Value = emp.Department };
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
