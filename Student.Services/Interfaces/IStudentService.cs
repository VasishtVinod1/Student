using SMS.Domain.Repositories;
using SMS.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Services.Interfaces
{
    public interface IStudentService
    {
    Task<IEnumerable<StudentDto>> GetAll();

    Task<StudentDto?> GetById(int id);

    Task<StudentDTO_POST> Add(StudentDTO_POST studentdtopost);

    Task<StudentDTO_POST> Update(StudentDTO_POST studentdtopost);

    Task<Student>DeleteById(int id);

    Task<StudentDTO_POST> Patch(int id, StudentDTO_POST studentdtopost);
    }
}
