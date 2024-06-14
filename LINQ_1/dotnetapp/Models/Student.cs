using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetapp.Models
{
    public class Student
{
    [Key]
    public int Id { get; set; }

    [Required]
    [RegularExpression(@"^ST-\d{5}$", ErrorMessage = "Student number must follow the format 'ST-XXXXX'.")]
    public string StudentNumber { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [DataType(DataType.Date)]
    public DateTime EnrollmentDate { get; set; }

    // Foreign key
    public int CourseId { get; set; }

    // Navigation property
    [ForeignKey("CourseId")]
    public Course? Course { get; set; }
}

}
