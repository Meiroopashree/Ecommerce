using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;

namespace dotnetapp.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly AppDbContext _context;

        public EnrollmentController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult DisplayCoursesForEnrollment(int enrollmentId)
        {
            Console.WriteLine(enrollmentId);
            var enrollment = _context.Enrollments.FirstOrDefault(lc => lc.Id == enrollmentId);

            if (enrollment == null)
            {
                return NotFound(); 
            }

            var courses = _context.Courses
                .Where(b => b.EnrollmentId == enrollmentId)
                .ToList();

            return View(courses);
        }

        public IActionResult AddCourse()
        {
            return View(); // Return the view with validation errors if the model is not valid
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
            return View(course); // Return the view with validation errors if the model is not valid
        }


        // Implement a method to display all courses in the library.
        public IActionResult DisplayAllCourses()
        {
            var courses = _context.Courses.ToList();
            return View(courses);
        }

        [HttpGet]
        [HttpPost]
                // Method to search for courses by title
        public IActionResult SearchCoursesByTitle(string query)
        {
            // If query is null or empty, return all courses
            if (string.IsNullOrEmpty(query))
            {
                var allCourses = _context.Courses.ToList();
                return View("DisplayAllCourses", allCourses);
            }

            // Otherwise, filter Courses by title
            var filteredCourses = _context.Courses
                .ToList() // Materialize the query
                .Where(b => b.Title.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return View("DisplayAllCourses", filteredCourses);
        }

        // Implement a method to get available Courses.
        public IActionResult GetAvailableCourses()
        {
            var availableCourses = _context.Courses
                .Where(b => b.EnrollmentId == null) // Courses that are not borrowed
                .ToList();

            return View(availableCourses);
        }
    }
}
