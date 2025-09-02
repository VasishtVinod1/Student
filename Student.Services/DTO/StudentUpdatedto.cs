using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Services.DTO
{
    public class StudentUpdatedto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }

        public List<EnrollmentUpdatedto> Enrollments { get; set; } = new();
    }

    public class EnrollmentUpdatedto
    {
        public int EnrollmentId { get; set; }
        public int CourseId { get; set; }
        
        public bool? IsActive { get; set; } = true;
    }
}
