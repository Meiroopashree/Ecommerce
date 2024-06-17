using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string StudentName { get; set; }

        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; }

        public int CourseId { get; set; } // Foreign key property
        public Course Course { get; set; } // Navigation property
    }
}
