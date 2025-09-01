namespace SMS.Services.DTO
{
    public class StudentDto
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = null!;

        public List<EnrollmentDto> Enrollments { get; set; } = new();
    }

    public class EnrollmentDto
    {
        public int EnrollmentId { get; set; }
        public int CourseId { get; set; }
        public CourseDto Course { get; set; } = null!;
    }

    public class CourseDto
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; } = null!;
        
    }
}
