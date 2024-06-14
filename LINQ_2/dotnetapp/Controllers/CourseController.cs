using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;

namespace dotnetapp.Controllers
{
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;

        public CourseController(AppDbContext context)
        {
            _context = context;
        }

        // Display students associated with a course.
        public IActionResult DisplayStudentsForCourse(int courseId)
        {
            var course = _context.Courses.FirstOrDefault(c => c.Id == courseId);

            if (course == null)
            {
                return NotFound(); // Handle the case where the course with the given ID doesn't exist
            }

            var students = _context.Students
                .Where(s => s.CourseId == courseId)
                .ToList();

            return View(students);
        }

        public IActionResult AddCourse()
        {
            return View(); // Return the view to add a new course
        }

        [HttpPost]
        public IActionResult AddCourse(Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Courses.Add(course);
                _context.SaveChanges();

                return RedirectToAction("DisplayAllCourses");
            }

            // If ModelState is not valid, return the view with validation errors
            return View(course);
        }


        // Display all courses in the system.
        public IActionResult DisplayAllCourses()
        {
            var courses = _context.Courses.ToList();
            return View(courses);
        }

        [HttpGet]
        [HttpPost]
        // Method to search for courses by name
        public IActionResult SearchCoursesByName(string query)
        {
            // If query is null or empty, return all courses
            if (string.IsNullOrEmpty(query))
            {
                var allCourses = _context.Courses.ToList();
                return View("DisplayAllCourses", allCourses);
            }

            // Otherwise, filter courses by name
            var filteredCourses = _context.Courses
                .ToList() // Materialize the query
                .Where(c => c.Name.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return View("DisplayAllCourses", filteredCourses);
        }

        // Get courses with no enrolled students.
        public IActionResult GetAvailableCourses()
        {
            var availableCourses = _context.Courses
                .Where(c => !c.Students.Any()) // Courses that have no students enrolled
                .ToList();

            return View(availableCourses);
        }

        // Get courses with enrolled students.
        public IActionResult GetCoursesWithStudents()
        {
            var coursesWithStudents = _context.Courses
                .Where(c => c.Students.Any()) // Courses that have students enrolled
                .ToList();

            return View(coursesWithStudents);
        }
    }
}
