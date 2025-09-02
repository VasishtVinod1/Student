namespace SMS.Services.DTO
{
    public class StudentResponseDto
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
  

        public List<EnrollmentResponseDto> Enrollments { get; set; } = new();
    }

    public class EnrollmentResponseDto
    {
        public int EnrollmentId { get; set; }
        public int CourseId { get; set; }
        public CourseResponseDto Course { get; set; } = new();
    }

    public class CourseResponseDto
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
    }

}
