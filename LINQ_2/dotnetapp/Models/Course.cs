using System.Collections.Generic;

namespace dotnetapp.Models
{
public class Course
{
    public int CourseId { get; set; } // Primary Key
    public string CourseName { get; set; }
    public string Description { get; set; }
    public int Credits { get; set; }
    public ICollection<Student> Students { get; set; }
}
}
