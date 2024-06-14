using System.Collections.Generic;

namespace dotnetapp.Models
{
public class Student
{
    public int StudentId { get; set; } // Primary Key
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    // Foreign key for the Course
    public int? CourseId { get; set; }
    public Course? Course { get; set; } // Navigation property
}
}
