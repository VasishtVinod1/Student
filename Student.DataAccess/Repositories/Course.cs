using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Domain.Repositories
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required]
        public string CourseName { get; set; }

        [Required]
        public string Description { get; set; }

        public int CourseDuration { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<Enrollment> Enrollment { get; set; } = new List<Enrollment>();
    }
}
