using System.Collections.Generic;

namespace dotnetapp.Models
{
public class Course
{
    public int Id { get; set; } // Primary Key
    public string Name { get; set; }
    public string Instructor { get; set; }
    public int Credits { get; set; }
    public ICollection<Student> Students { get; set; }
}
}

