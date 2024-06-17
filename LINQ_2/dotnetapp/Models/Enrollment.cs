using System;
using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The student's name must be less than 100 characters.")]
        public string StudentName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; }

        [Required]
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
