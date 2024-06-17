using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Duration must be a positive integer.")]
        public int Duration { get; set; }
        
        public int EnrollmentId { get; set; } // Foreign key property
        public ICollection<Enrollment> Enrollments { get; set; } // Navigation property
    }
}
