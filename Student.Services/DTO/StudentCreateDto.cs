using System.ComponentModel.DataAnnotations;

public class StudentCreateDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; } = string.Empty;
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }

    public List<EnrollmentCreateDto> Enrollments { get; set; } = new();
}

public class EnrollmentCreateDto
{
    public int CourseId { get; set; }

    public bool IsActive { get; set; } = true;
}
