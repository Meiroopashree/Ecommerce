using System;
using System.Collections.Generic;

namespace dotnetapp.Models
{
    public class Student
    {
        public int Id { get; set; } // Primary Key
        public string StudentNumber { get; set; }
        public string Name { get; set; }
        public DateTime EnrollmentDate { get; set; }

        // Foreign key for the Course
        public int? CourseId { get; set; }
        public Course? Course { get; set; } // Navigation property

    }
}
