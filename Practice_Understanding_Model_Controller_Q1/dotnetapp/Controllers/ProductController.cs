using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;

namespace dotnetapp.Controllers
{
    public class FeedbackController : Controller
    {
        private static List<Feedback> _feedbackList = new List<Feedback>
        {
            new Feedback { Id = 1, StudentName = "John Doe", Course = "Mathematics", Feedbacks = "Great course!", Rating = 5, DateSubmitted = DateTime.Now.AddDays(-5) },
            new Feedback { Id = 2, StudentName = "Alice Johnson", Course = "Physics", Feedbacks = "Enjoyed the lectures.", Rating = 4, DateSubmitted = DateTime.Now.AddDays(-3) },
            new Feedback { Id = 3, StudentName = "Bob Smith", Course = "Biology", Feedbacks = "Needs more interactive sessions.", Rating = 3, DateSubmitted = DateTime.Now.AddDays(-2) }
        };

        public IActionResult Index()
        {
            return View(_feedbackList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                // Assign a simple incremental ID
                feedback.Id = _feedbackList.Count + 1;
                
                // Add the feedback to the static list
                _feedbackList.Add(feedback);

                // Redirect to the feedback list or another action
                return RedirectToAction("Index");
            }

            // If the model state is not valid, return to the create view with validation errors
            return View(feedback);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var feedback = _feedbackList.FirstOrDefault(f => f.Id == id);

            if (feedback == null)
            {
                return NotFound();
            }

            return View(feedback);
        }

        [HttpPost]
        public IActionResult Edit(Feedback feedback)
        {
            var existingFeedback = _feedbackList.FirstOrDefault(f => f.Id == feedback.Id);
            if (existingFeedback == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Update the existing feedback
                existingFeedback.StudentName = feedback.StudentName;
                existingFeedback.Course = feedback.Course;
                existingFeedback.Feedbacks = feedback.Feedbacks;
                existingFeedback.Rating = feedback.Rating;

                return RedirectToAction("Index");
            }

            return View(feedback);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var feedback = _feedbackList.FirstOrDefault(f => f.Id == id);

            if (feedback == null)
            {
                return NotFound();
            }

            return View(feedback);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public IActionResult DeleteConfirmed(int id)
        {
            var feedback = _feedbackList.FirstOrDefault(f => f.Id == id);

            if (feedback != null)
            {
                // Remove the feedback from the static list
                _feedbackList.Remove(feedback);
            }

            return RedirectToAction("Index");
        }
    }
}
