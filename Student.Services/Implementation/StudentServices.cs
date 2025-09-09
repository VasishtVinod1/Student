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
using System.ComponentModel.DataAnnotations;
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

        public async Task<IEnumerable<StudentResponseDto>> GetAll()
        {
            var students = await _context.Students
                .Include(s => s.Enrollment)
                    .ThenInclude(e => e.Course)
                .ToListAsync();

            var studentDtos = students.Select(s => new StudentResponseDto
            {
                StudentId = s.StudentId,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                DateOfBirth = s.DateOfBirth,
                Gender = s.Gender,
                Enrollments = s.Enrollment.Select(e => new EnrollmentResponseDto
                {
                    EnrollmentId = e.EnrollmentId,
                    CourseId = e.CourseId,
                    Course = new CourseResponseDto
                    {
                        CourseId = e.Course.CourseId,
                        CourseName = e.Course.CourseName
                    }
                }).ToList()
            }).ToList();

            return studentDtos;
        }


        public async Task<StudentResponseDto?> GetById(int id)
        {
            var student = await _context.Students
                .Include(s => s.Enrollment)
                    .ThenInclude(e => e.Course)
                .FirstOrDefaultAsync(s => s.StudentId == id);

            if (student == null)
                return null;

            var studentDto = new StudentResponseDto
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                DateOfBirth = student.DateOfBirth,
                Gender = student.Gender,
                Enrollments = student.Enrollment.Select(e => new EnrollmentResponseDto
                {
                    EnrollmentId = e.EnrollmentId,
                    CourseId = e.CourseId,
                    Course = new CourseResponseDto
                    {
                        CourseId = e.Course.CourseId,
                        CourseName = e.Course.CourseName
                    }
                }).ToList()
            };

            return studentDto;
        }



        public async Task<StudentCreateDto> Add(StudentCreateDto dto)
        {
            var validationContext = new ValidationContext(dto);
            Validator.ValidateObject(dto, validationContext, validateAllProperties: true);
            var student = new Student
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Address = dto.Address,
                CreatedAt = DateTime.Now,

                Enrollment = dto.Enrollments.Select(e => new Enrollment
                {
                    CourseId = e.CourseId,
                    IsActive = e.IsActive ,
                    EnrollmentDate = DateTime.Now
                }).ToList()
            };

           

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            
            return dto;
        }

       
        public async Task<StudentUpdatedto> Update(int id, StudentUpdatedto dto)
        {
            var existing = await _context.Students
                .Include(s => s.Enrollment)
                .FirstOrDefaultAsync(s => s.StudentId == id);

            if (existing == null)
                return null;

            existing.FirstName = dto.FirstName;
            existing.LastName = dto.LastName;
            existing.DateOfBirth = dto.DateOfBirth ?? existing.DateOfBirth;
            existing.Gender = dto.Gender ?? existing.Gender;
            existing.Email = dto.Email ?? existing.Email;
            existing.PhoneNumber = dto.PhoneNumber ?? existing.PhoneNumber;
            existing.Address = dto.Address ?? existing.Address;
            existing.UpdatedAt = DateTime.Now;

            
            if (dto.Enrollments != null)
            {
                foreach (var e in dto.Enrollments)
                {
                   
                    var existingEnrollment = existing.Enrollment
                        .FirstOrDefault(en => en.EnrollmentId == e.EnrollmentId);

                    if (existingEnrollment != null)
                    {
                       
                        existingEnrollment.CourseId = e.CourseId;
                        existingEnrollment.IsActive = e.IsActive ?? existingEnrollment.IsActive;
                        existingEnrollment.EnrollmentDate = DateTime.Now;
                    }
                    else
                    {
                      
                        existing.Enrollment.Add(new Enrollment
                        {
                            CourseId = e.CourseId,
                            IsActive = e.IsActive ?? true,
                            EnrollmentDate = DateTime.Now
                        });
                    }
                }
            }



            _context.Students.Update(existing);
            await _context.SaveChangesAsync();

            return dto;

        }



        
        public async Task<StudentUpdatedto?> Patch(int id, StudentUpdatedto dto)
        {
            var existing = await _context.Students
                .Include(s => s.Enrollment)
                .FirstOrDefaultAsync(s => s.StudentId == id);

            if (existing == null) return null;

           
            existing.FirstName = dto.FirstName ?? existing.FirstName;
            existing.LastName = dto.LastName ?? existing.LastName;
            existing.DateOfBirth = dto.DateOfBirth ?? existing.DateOfBirth;
            existing.Gender = dto.Gender ?? existing.Gender;
            existing.Email = dto.Email ?? existing.Email;
            existing.PhoneNumber = dto.PhoneNumber ?? existing.PhoneNumber;
            existing.Address = dto.Address ?? existing.Address;
            existing.UpdatedAt = DateTime.Now;

         
            if (dto.Enrollments != null)
            {
                foreach (var e in dto.Enrollments)
                {
                  
                    var existingEnrollment = existing.Enrollment
                        .FirstOrDefault(en => en.EnrollmentId == e.EnrollmentId);

                    if (existingEnrollment != null)
                    {
                       
                        existingEnrollment.CourseId = e.CourseId;
                        existingEnrollment.IsActive = e.IsActive ?? existingEnrollment.IsActive;
                        existingEnrollment.EnrollmentDate = DateTime.Now;
                    }
                    else
                    {
                        
                        existing.Enrollment.Add(new Enrollment
                        {
                            CourseId = e.CourseId,
                            IsActive = e.IsActive ?? true,
                            EnrollmentDate = DateTime.Now
                        });
                    }
                }
            }


            await _context.SaveChangesAsync();
            return dto;
        }


        
        public async Task<Student> DeleteById(int id)
        {
            var student = await _context.Students.FirstOrDefaultAsync
                (s => s.StudentId==id);
            if (student == null)
                return null;

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return student;
        }
    }
}








