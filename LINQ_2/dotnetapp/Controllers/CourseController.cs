using System;
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

        // Implement a method to display enrollments associated with a course.
        public IActionResult DisplayEnrollmentsForCourse(int courseId)
        {
            var course = _context.Courses.FirstOrDefault(c => c.Id == courseId);

            if (course == null)
            {
                return NotFound(); // Handle the case where the course with the given ID doesn't exist
            }

            var enrollments = _context.Enrollments
                .Where(e => e.CourseId == courseId)
                .ToList();

            return View(enrollments);
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
            return View(course);
        }

        // Implement a method to display all courses.
        public IActionResult DisplayAllCourses()
        {
            var courses = _context.Courses.ToList();
            return View(courses);
        }

        // Method to search for courses by title
        [HttpGet]
        [HttpPost]
        public IActionResult SearchCoursesByTitle(string query)
        {
            // If query is null or empty, return all courses
            if (string.IsNullOrEmpty(query))
            {
                var allCourses = _context.Courses.ToList();
                return View("DisplayAllCourses", allCourses);
            }

            // Otherwise, filter courses by title
            var filteredCourses = _context.Courses
                .Where(c => c.Title.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return View("DisplayAllCourses", filteredCourses);
        }

        // Implement a method to get available courses.
        public IActionResult GetAvailableCourses()
        {
            var availableCourses = _context.Courses
                .Where(c => !_context.Enrollments.Any(e => e.CourseId == c.Id)) // Courses that have no enrollments
                .ToList();

            return View(availableCourses);
        }

        // Implement a method to get enrolled courses.
        public IActionResult GetEnrolledCourses()
        {
            var enrolledCourses = _context.Courses
                .Where(c => _context.Enrollments.Any(e => e.CourseId == c.Id)) // Courses that have enrollments
                .ToList();

            return View(enrolledCourses);
        }
    }
}
