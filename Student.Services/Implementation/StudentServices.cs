using AutoMapper;
using AutoMapper.QueryableExtensions;
using DotLiquid.Tags;
using Microsoft.EntityFrameworkCore;
using SMS.Domain.Repositories;
using SMS.Infrastructure;
using SMS.Services.DTO;
using SMS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Services.Implementation
{
    public class StudentServices : IStudentService
    {
        private readonly AppDbContext _context;
        

        public StudentServices(AppDbContext context)
        {
            _context = context;
          
        }

        public async Task<IEnumerable<StudentDto>> GetAll()
        {
            var students = await _context.Students
                .Include(s => s.Enrollment)
                    .ThenInclude(e => e.Course)
                .ToListAsync();

            var studentDtos = students.Select(s => new StudentDto
            {
                StudentId = s.StudentId,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                DateOfBirth = s.DateOfBirth,
                Gender = s.Gender,
                Enrollments = s.Enrollment.Select(e => new EnrollmentDto
                {
                    EnrollmentId = e.EnrollmentId,
                    CourseId = e.CourseId,
                    Course = new CourseDto
                    {
                        CourseId = e.Course.CourseId,
                        CourseName = e.Course.CourseName
                        
                    }
                }).ToList()
            }).ToList();

            return studentDtos;
        }

        public async Task<StudentDto?> GetById(int id)
        {
            var student = await _context.Students
                .Include(s => s.Enrollment)
                    .ThenInclude(e => e.Course)
                .FirstOrDefaultAsync(s => s.StudentId == id);

            if (student == null)
                return null;

            var studentDto = new StudentDto
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                DateOfBirth = student.DateOfBirth,
                Gender = student.Gender,
                Enrollments = student.Enrollment.Select(e => new EnrollmentDto
                {
                    EnrollmentId = e.EnrollmentId,
                    CourseId = e.CourseId,
                    Course = new CourseDto
                    {
                        CourseId = e.Course.CourseId,
                        CourseName = e.Course.CourseName
                    }
                }).ToList()
            };

            return studentDto;
        }


        public async Task<StudentDTO_POST> Add(StudentDTO_POST studentdtopost)
        {


            var student = new Student
            {
                StudentId = studentdtopost.StudentId,
                FirstName = studentdtopost.FirstName,
                LastName = studentdtopost.LastName,
                DateOfBirth = studentdtopost.DateOfBirth,
                Gender = studentdtopost.Gender,
                Email = studentdtopost.Email,
                PhoneNumber = studentdtopost.PhoneNumber,
                Address = studentdtopost.Address,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,

                Enrollment = studentdtopost.Enrollments.Select(e => new Enrollment
                {
                    EnrollmentId = e.EnrollmentId,
                    StudentId = e.StudentId,
                    CourseId = e.CourseId,
                    EnrollmentDate = e.EnrollmentDate,
                    IsActive = e.IsActive
                }).ToList()
            };

            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();

            return studentdtopost;
        }

        public async Task<StudentDTO_POST?> Update(StudentDTO_POST studentdtopost)
        {
            var existing = await _context.Students.FindAsync(studentdtopost.StudentId);
            if (existing == null) return null;

            existing.FirstName = studentdtopost.FirstName;
            existing.LastName = studentdtopost.LastName;
            existing.Email = studentdtopost.Email;
            existing.PhoneNumber = studentdtopost.PhoneNumber;
            existing.Address = studentdtopost.Address;
            existing.DateOfBirth = studentdtopost.DateOfBirth;
            existing.Gender = studentdtopost.Gender;
            existing.UpdatedAt = DateTime.UtcNow;

            existing.Enrollment.Clear();
            
            existing.Enrollment = studentdtopost.Enrollments.Select(e => new Enrollment
            {
                CourseId = e.CourseId,
                EnrollmentDate = e.EnrollmentDate,
                IsActive= e.IsActive,
            }).ToList();

            await _context.SaveChangesAsync();
            return studentdtopost;
        }


        public async Task<Student?> DeleteById(int id)
        {
            var existing = await _context.Students.FindAsync(id);
            if (existing == null)
            {
                return null;
            }
            _context.Students.Remove(existing);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<StudentDTO_POST?> Patch(int id, StudentDTO_POST studentdtopost)
        {
            var existing = await _context.Students.FindAsync(id);
            if (existing == null) return null;

            existing.FirstName = studentdtopost.FirstName ?? existing.FirstName;
            existing.LastName = studentdtopost.LastName ?? existing.LastName;
            existing.Email = studentdtopost.Email ?? existing.Email;
            existing.PhoneNumber = studentdtopost.PhoneNumber ?? existing.PhoneNumber;
            existing.Address = studentdtopost.Address ?? existing.Address;
            existing.DateOfBirth = studentdtopost.DateOfBirth != default ? studentdtopost.DateOfBirth : existing.DateOfBirth;
            existing.Gender = studentdtopost.Gender ?? existing.Gender;
            existing.UpdatedAt = DateTime.UtcNow;

            if(studentdtopost.Enrollments != null && studentdtopost.Enrollments.Count > 0)
            {
                existing.Enrollment.Clear();
                existing.Enrollment = studentdtopost.Enrollments.Select(e => new Enrollment
                {
                    CourseId = e.CourseId,
                    EnrollmentDate = e.EnrollmentDate,
                    IsActive = e.IsActive,
                }).ToList();
            }

            await _context.SaveChangesAsync();

            studentdtopost.StudentId = existing.StudentId;
            return studentdtopost;
        }

    }
}








