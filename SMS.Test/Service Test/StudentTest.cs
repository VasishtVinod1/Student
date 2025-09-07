using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MockQueryable.Moq;
using MockQueryable.Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SMS.Domain.Repositories;
using SMS.Infrastructure;
using SMS.Services.DTO;
using SMS.Services.Implementation;
using SMS.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SMS.Test.Service_Test
{
    [TestClass]
    public class StudentTest
    {
        private Mock<AppDbContext> _mockContext;
        private IStudentService _studentService;



        [TestInitialize]
        public void Setup()
        {
       
            var students = new List<Student>
            {
                new Student { StudentId = 1, FirstName = "John", LastName = "Doe", Email = "john@gmail.com", Address = "123 St", Gender = "Male", PhoneNumber = "111" },
                new Student { StudentId = 2, FirstName = "Jane", LastName = "Smith", Email = "jane@gmail.com", Address = "456 Ave", Gender = "Female", PhoneNumber = "222" }
            };

           
            var mockDbSet = students.BuildMockDbSet();

            mockDbSet.Setup(m => m.FindAsync(It.IsAny<object[]>()))
            .ReturnsAsync((object[] ids) =>
            {
                var id = (int)ids[0];
                return students.FirstOrDefault(s => s.StudentId == id);
            });

            _mockContext = new Mock<AppDbContext>(new DbContextOptions<AppDbContext>());
            _mockContext.Setup(c => c.Students).Returns(mockDbSet.Object);
            _mockContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

          
            _studentService = new StudentServices(_mockContext.Object);
        }



        
        [TestMethod]
        public async Task GetAll_ShouldReturnAllStudents()
        {
            var result = await _studentService.GetAll();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("Jane", result.Last().FirstName);
            Assert.AreEqual("Smith", result.Last().LastName);

        }

        [TestMethod]
        public async Task GetById_ShouldReturnStudent_WhenIdExists()
        {
            var result = await _studentService.GetById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("John", result.FirstName);

           
        }

        [TestMethod]
        public async Task GetById_ShouldReturnNull_WhenIdDoesNotExist()
        {
            var result = await _studentService.GetById(10);

            Assert.IsNull(result);
           
        }

        [TestMethod]
        public async Task Add_ShouldCallSaveChanges()
        {
            var newStudent = new StudentCreateDto
            {
                FirstName = "Alice",
                LastName = "Johnson",
                Email = "alice@gmail.com"
                
            };

            await _studentService.Add(newStudent);

            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }

        [TestMethod]
        public async Task Add_ShouldThrowValidationException_WhenEmailIsInvalid()
        {
            var newStudent = new StudentCreateDto
            {
                FirstName = "Bob",
                LastName = "Brown",
                Email = "invalid-email"
            };
            await Assert.ThrowsExceptionAsync<ValidationException>(async () =>
            {
                await _studentService.Add(newStudent);
            });
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Never);
        }

        [TestMethod]
        public async Task Add_ShouldThrowValidationException_WhenFirstNameIsMissing()
        {
            var newStudent = new StudentCreateDto
            {
                LastName = "Brown",
                Email = "bob@gmail.com"
            };
            await Assert.ThrowsExceptionAsync<ValidationException>(async () =>
            {
                await _studentService.Add(newStudent);
            });
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Never);
        }

        [TestMethod]
        public async Task Update_WithValidData_ShouldCallSaveChanges()
        {
            var updatedStudent = new StudentUpdatedto
            {
                FirstName = "UpdatedName",
                Email = "updated@gmail.com"
            };

            await _studentService.Update(1, updatedStudent);

            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }

        [TestMethod]
        public async Task Update_WithInvalidId_ShouldReturnNull()
        {
            var updatedstudent = new StudentUpdatedto
            {
                FirstName = "Vasisht",
                Email = "vasisht@"
            };
            var result = await _studentService.Update(10, updatedstudent);

            Assert.IsNull(result);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Never);

        }

        [TestMethod]
        public async Task DeleteById_WithExisting_Student()
        {
            var result = await _studentService.DeleteById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1,result.StudentId);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }

        [TestMethod]
        public async Task DeleteById_WithNonExisting_Student()
        {
            var result = await _studentService.DeleteById(10);
            Assert.IsNull(result);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Never);
        }
    }
}
