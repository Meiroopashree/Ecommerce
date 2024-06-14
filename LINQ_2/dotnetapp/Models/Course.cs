using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models
{
public class Course
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [MaxLength(50)]
    public string Instructor { get; set; }

    [Range(1, 10)]
    public int Credits { get; set; }

    public int? Id { get; set; }

    // Navigation property
    [ForeignKey("CourseId")]
    public Student? Student { get; set; }
}
}
