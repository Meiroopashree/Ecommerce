using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The title must be less than 100 characters.")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "The description must be less than 500 characters.")]
        public string Description { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The duration must be a positive integer.")]
        public int Duration { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
